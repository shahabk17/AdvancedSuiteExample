namespace SanlemAutomation
{
    public class AppInsights : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly ConcurrentQueue<string> logMessages = new ConcurrentQueue<string>();
        /// <summary>
        /// Method used to fetch the traces
        /// </summary>
        /// <param name="query"></param>
        /// <param name="currentDateTime"></param>
        [Author("Shahab Khan")]
        public void GetLogsFromAppInsights(string query, string attributeKey, string attributeValue, string currentDateTime)
        {
            var (apiUrl, header) = GetAppInsightsConfiguration("platform_appinsight");
            var headers = new Dictionary<string, string> { { "x-api-key", genericUtils.Decrypt(header, 3) } };
            var fullApiUrl = $"{apiUrl}{Uri.EscapeDataString(query)}";
            Task.Run(() =>
            {
                Dictionary<string, string> columnValues = FetchLogsWithRetry(fullApiUrl, headers, 180);
                LogResults(columnValues, attributeKey, attributeValue, currentDateTime);
            }).Wait();
        }

        public (string attributeKey, string attributeValue) GetElementIdentifier(IWebElement element)
        {
            var attributes = new Dictionary<string, string>
    {
        { "id", element.GetDomAttribute("id") },
        { "class", element.GetDomAttribute("class") },
        { "name", element.GetDomAttribute("name") },
        { "href", element.GetDomAttribute("href") }
    };

            foreach (var attr in attributes)
            {
                if (!string.IsNullOrEmpty(attr.Value))
                    return (attr.Key, attr.Value);
            }

            return ("text", element.Text); // Default to element text if no attribute is found
        }

        public void CaptureClickLog(string log)
        {
            logMessages.Enqueue(log); // Enqueue maintains order
        }

        public void PrintCollectedLogs()
        {
            while (!logMessages.IsEmpty)
            {
                if (logMessages.TryDequeue(out string log)) // Dequeue maintains FIFO order
                {
                    var status = log.Contains("Warning") ? Status.Warning :
                             log.Contains("Pass") ? Status.Pass : Status.Info;
                    Report.ChildLog.Log(status, log);
                    Console.WriteLine(log);
                }
            }
        }

        public string FetchTemporaryPassword(string idNumber, string query)
        {
            var (apiUrl, header) = GetAppInsightsConfiguration("password_appinsight");
            var fullApiUrl = $"{apiUrl}{Uri.EscapeDataString(query)}";
            var client = new RestClient(fullApiUrl);
            var request = new RestRequest("", Method.Get);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-api-key", genericUtils.Decrypt(header, 3));

            JToken tables;

            do
            {
                var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
                var content = response.Content;

                var jsonResponse = JObject.Parse(content);
                tables = jsonResponse["tables"]?[0]?["rows"];

                foreach (var row in tables)
                {
                    var customDimensions = JObject.Parse(row[4]?.ToString() ?? "{}");

                    if (customDimensions.ContainsKey("TempPass"))
                    {
                        var tempPass = customDimensions["TempPass"]?.ToString();
                        return tempPass.ToString();
                    }
                }

                baseStep.wait.GenericWait(15000);
            }
            while (true);
        }


        #region Private helper methods

        private (string apiUrl, string header) GetAppInsightsConfiguration(string appInsightType)
        {
            string path = genericUtils.GetDataPath("TestResources");
            JObject json = genericUtils.GetJson(path + "\\AppInsights.json");
            return (
                json[Properties.environment][appInsightType]["apiUrl"].ToString(),
                json[Properties.environment][appInsightType]["header"].ToString()
            );
        }

        private Dictionary<string, string> FetchLogsWithRetry(string apiUrl, Dictionary<string, string> headers, int sec)
        {
            Dictionary<string, string> columnValues = new();
            DateTime maxRetry = DateTime.Now.AddSeconds(sec);
            RestHelper restHelper = new();
            while (DateTime.Now < maxRetry && columnValues.Count == 0)
            {
                //HandleAutoLogoutWhileWaiting(5000);
                baseStep.wait.GenericWait(5000);
                var response = restHelper.GetAsync(apiUrl, headers, null).GetAwaiter().GetResult();
                columnValues = ProcessResponse(response);
            }
            return columnValues;
        }

        private Dictionary<string, string> ProcessResponse(RestResponse response)
        {
            Dictionary<string, string> columnValues = new();
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                JObject jObject = JObject.Parse(response.Content);
                columnValues = GetAllColumnValues(jObject);
            }
            return columnValues;
        }

        private void LogResults(Dictionary<string, string> columnValues, string attributeKey, string attributeValue, string currentDateTime)
        {
            if (columnValues.Count > 0)
            {
                foreach (var entry in columnValues)
                {
                    if (entry.Key.Contains("customDimensions"))
                    {
                        CaptureClickLog($"Pass: {entry.Key}: {entry.Value}");
                    }
                }
            }
            else
            {
                CaptureClickLog($"Warning: No Logs visible in Custom Events for Element with attribute [{attributeKey}={attributeValue}]");

                string errorQuery = new DBQueries().FetchException(currentDateTime);
                CheckFailureLogsInAppInsights(errorQuery, attributeKey, attributeValue, currentDateTime);

                CaptureClickLog($"Pass: Element with attribute [{attributeKey}={attributeValue}] is showing no failure");
            }
        }

        private Dictionary<string, string> GetAllColumnValues(JObject json)
        {
            var result = new Dictionary<string, string>();
            var rows = json["tables"]?[0]?["rows"] as JArray;
            var columns = json["tables"]?[0]?["columns"] as JArray;

            if (rows != null && rows.Count > 0 && columns != null)
            {
                var firstRow = rows[0] as JArray;
                if (firstRow != null)
                {
                    for (int i = 0; i < columns.Count(); i++)
                    {
                        var columnName = columns[i]?["name"]?.ToString();
                        var value = firstRow[i]?.ToString();
                        if (!string.IsNullOrEmpty(columnName))
                        {
                            if (columnName == "customDimensions" && !string.IsNullOrEmpty(value))
                            {
                                try
                                {
                                    JObject customDimensions = JObject.Parse(value);
                                    result[columnName] = customDimensions.ToString(Formatting.Indented);
                                }
                                catch (JsonException)
                                {
                                    result[columnName] = "Invalid customDimensions JSON";
                                }
                            }
                            else
                            {
                                result[columnName] = value;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private void CheckFailureLogsInAppInsights(string query, string attributeKey, string attributeValue, string currentDateTime)
        {
            var (apiUrl, header) = GetAppInsightsConfiguration("tracking_appinsight");
            var headers = new Dictionary<string, string> { { "x-api-key", genericUtils.Decrypt(header, 3) } };
            var fullApiUrl = $"{apiUrl}{Uri.EscapeDataString(query)}";
            Dictionary<string, string> columnValues = FetchLogsWithRetry(fullApiUrl, headers, 10);

            foreach (var entry in columnValues)
            {
                if (entry.Key == "problemId")
                {
                    validate.AssertEquals(false, "System.Runtime.InteropServices.COMException at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw".Contains(entry.Value), $"Error is visible : {entry.Value} for Dom Element with attribute [{attributeKey}={attributeValue}]", true);
                }
            }
        }

        private void HandleAutoLogoutWhileWaiting(int waitTimeMillis)
        {
            SolutionPage solutionPage = new();
            int elapsedTime = 0;
            int checkInterval = 5000; // Check every 5 seconds
            IWebDriver localDriver = Driver;

            Task.Run(() =>
            {
                while (elapsedTime < waitTimeMillis)
                {
                    try
                    {
                        var stayButton = localDriver.FindElement(solutionPage.logoutpopupstay_btn);

                        if (stayButton.Displayed)
                        {
                            stayButton.Click();
                            Report.ChildLog.Log(Status.Info, "Auto-logout popup appeared. Clicked 'Stay' button.");
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("No PopUp Visible");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error while checking for Stay button: {ex.Message}");
                    }

                    baseStep.wait.GenericWait(checkInterval);
                    elapsedTime += checkInterval;
                }
            });

            baseStep.wait.GenericWait(waitTimeMillis);
        }
        #endregion
    }
}
