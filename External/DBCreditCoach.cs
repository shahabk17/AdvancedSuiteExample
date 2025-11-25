namespace SanlamAutomation
{
    [Author("Shahab Khan")]
    public class DBCreditCoach
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        private readonly OTPStorageAccount otpStorageAccount = new();
        private readonly DBQueries dBQueries = new();
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();

        /// <summary>
        /// Gets the question click based on the provided SQL query.
        /// This method retrieves security questions and their corresponding answers,
        /// selects the answers in the registration page, and submits the answers.
        /// </summary>
        /// <param name="query">The SQL query to retrieve security questions and answers.</param>

        [Author("Shahab Khan")]
        public void GetQuestionClick(string query)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            var dataList = ExecuteSqlQuery(query);

            var questionBook = dataList.ToDictionary(
                row => row["Question"].ToString(),
                row => row["CurrectAnswer0"].ToString()
            );

            int questionIndex = 0;

            while (registrationPage.IsSecurityQuestionDisplayed() && questionIndex < 6)
            {
                SelectAnswerOfSecurity(questionBook, registrationPage, questionIndex);
                questionIndex++;
            }

            validate.TakeStepFullScreenShot("5 out of 5 Questions are Selected", Status.Info);

            while (validate.IsElementDisplayed(registrationPage.aftersecurityquestionsubmitbtn))
            {
                baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.aftersecurityquestionsubmitbtn, 15);
                baseStep.ScrollToElement(registrationPage.AfterSecurityQuestionSubmitBtn);
                baseStep.Click(registrationPage.AfterSecurityQuestionSubmitBtn);
                baseStep.wait.WaitTillPageLoad();
            }
        }

        /// <summary>
        /// Selects the answer for the security question displayed on the registration page.
        /// This method checks if the current question is in the provided question book,
        /// and selects the corresponding answer until it is confirmed as selected or the question count changes.
        /// </summary>
        /// <param name="questionBook">A dictionary containing questions and their corresponding answer IDs.</param>
        /// <param name="registrationPage">The registration page instance to interact with.</param>
        /// <param name="questionIndex">The index of the current question being answered.</param>
        private void SelectAnswerOfSecurity(IDictionary<string, string> questionBook, RegistrationPage registrationPage, int questionIndex)
        {
            string question = registrationPage.SecurityQuestionText();

            if (questionBook.TryGetValue(question, out string answerID))
            {
                Report.ChildLog.Log(Status.Info, $"question is {question} answerID is {answerID}");

                bool isAnswerSelected = false;
                int currentQuestionCount;

                do
                {
                    int answerIndex = int.Parse(answerID) - 1; // Convert answerID to zero-based index
                    baseStep.Click(registrationPage.OptionSelect(answerIndex.ToString()));
                    isAnswerSelected = registrationPage.isAnswerSelect(answerIndex.ToString());

                    string numberOfQuestion = baseStep.getText.Text(registrationPage.SecurityQuestionCountText);
                    currentQuestionCount = int.Parse(numberOfQuestion[9].ToString());
                } while (questionIndex == currentQuestionCount && !isAnswerSelected);
            }
        }

        /// <summary>
        /// Selects the answer for the seventh security question based on the question count.
        /// This method retrieves the answer text from the question book and selects the answer
        /// if the current question index matches the expected question count.
        /// </summary>
        /// <param name="questionBook">A dictionary containing question counts and their corresponding answer texts.</param>
        /// <param name="registrationPage">The registration page instance to interact with.</param>
        /// <param name="questionIndex">The index of the current question being answered.</param>
        private void SelectAnswerOfSevenSecurity(IDictionary<string, string> questionBook, RegistrationPage registrationPage, int questionIndex)
        {
            string questionCount = baseStep.getText.Text(registrationPage.SecurityQuestionCountText).Split(' ')[1];
            string question = registrationPage.SecurityQuestionText();

            if (questionBook.TryGetValue(questionCount, out string answerText))
            {
                Report.ChildLog.Log(Status.Info, $"question is {question} answer is {answerText}");

                if (questionIndex == int.Parse(questionCount))
                {
                    baseStep.Click(registrationPage.OptionSelectWithText(answerText));
                }
            }
        }

        /// <summary>
        /// Verifies basic information in the database for a given ID number.
        /// This method checks if the request and response data for the specified ID number
        /// are present in the database and logs the information.
        /// </summary>
        /// <param name="idNumber">The ID number to verify in the database.</param>

        [Author("Shahab Khan")]
        public void VerifyDBBasicVerification(string idNumber)
        {
            var dataList = ExecuteSqlQuery(dBQueries.BasicverificationfromDBQuery(idNumber));

            if (dataList.Count > 0)
            {
                var reqQuestionBook = new Dictionary<string, string>
        {
            { dataList[0]["IdNumber"].ToString(), dataList[0]["RequestParam"].ToString() }
        };

                var resQuestionBook = new Dictionary<string, string>
        {
            { dataList[0]["IdNumber"].ToString(), dataList[0]["ResponseData"].ToString() }
        };

                Report.ChildLog.Log(Status.Info, $"Request present in DB for ID {idNumber} is {reqQuestionBook[idNumber]}");
                Report.ChildLog.Log(Status.Info, $"ResponseData present in DB for ID {idNumber} is {resQuestionBook[idNumber]}");
            }
        }

        /// <summary>
        /// Performs post-registration validation for a given ID number.
        /// This method checks the active status, security question confirmation,
        /// basic information verification, and phone number confirmation in the database.
        /// </summary>
        /// <param name="idNumber">The ID number to validate in the database.</param>

        [Author("Shahab Khan")]
        public void GetPostValidationAfterReg(string idNumber)
        {
            List<IDictionary<string, object>> dataList;

            do
            {
                baseStep.wait.GenericWait(2000);
                dataList = ExecuteSqlQuery(dBQueries.Dbo_UserTable(idNumber));
            } while (dataList.Count <= 0);

            var isActiveQuestionBook = new Dictionary<string, string>
    {
        { dataList[0]["IdNumber"].ToString(), dataList[0]["IsActive"].ToString() }
    };
            Report.ChildLog.Log(Status.Info, $"IsActive Status present in DB for ID {idNumber} is {isActiveQuestionBook[idNumber]}");
            Assert.That(isActiveQuestionBook[idNumber].Equals("True"));

            var securityQuestionConfirmed = new Dictionary<string, string>
    {
        { dataList[0]["IdNumber"].ToString(), dataList[0]["SecurityQuestionConfirmed"].ToString() }
    };
            Report.ChildLog.Log(Status.Info, $"SecurityQuestionConfirmed Status present in DB for ID {idNumber} is {securityQuestionConfirmed[idNumber]}");

            var basicInfoVerifyConfirmed = new Dictionary<string, string>
    {
        { dataList[0]["IdNumber"].ToString(), dataList[0]["BasicInfoVerifyConfirmed"].ToString() }
    };
            Report.ChildLog.Log(Status.Info, $"BasicInfoVerifyConfirmed Status present in DB for ID {idNumber} is {basicInfoVerifyConfirmed[idNumber]}");

            if (securityQuestionConfirmed[idNumber].Equals("True"))
            {
                Assert.That(basicInfoVerifyConfirmed[idNumber].Equals("False"));
            }
            else
            {
                Assert.That(basicInfoVerifyConfirmed[idNumber].Equals("True"));
            }

            var phoneNumberConfirmed = new Dictionary<string, string>
    {
        { dataList[0]["IdNumber"].ToString(), dataList[0]["PhoneNumberConfirmed"].ToString() }
    };
            Report.ChildLog.Log(Status.Info, $"PhoneNumberConfirmed Status present in DB for ID {idNumber} is {phoneNumberConfirmed[idNumber]}");
            Assert.That(phoneNumberConfirmed[idNumber].Equals("True"));
        }

        /// <summary>
        /// Updates the SPL qualification status of a user based on their ID number and decision.
        /// This method retrieves the user ID from the database and executes an SQL query to update
        /// the user's SPL qualification status. It logs the update action.
        /// </summary>
        /// <param name="idNumber">The ID number of the user to update.</param>
        /// <param name="decision">The decision to update the user's SPL qualification status.</param>
        [Author("Shahab Khan")]
        public void UpdateSplQualifiedUser(string idNumber, string decision)
        {
            Log.Info($"Updating SPL qualification for user {idNumber} with decision: {decision}");
            string DBId = GetUserId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.UpdateSPLUserDecision(DBId, decision));
            Log.Info($"SPL qualification update completed for user {idNumber}");
        }

        /// <summary>
        /// Updates the SPL qualification status of a user before they log in.
        /// This method runs an asynchronous task to retrieve the user ID and update their SPL qualification
        /// status to "Approve". It logs the update action.
        /// </summary>
        /// <param name="idNumber">The ID number of the user to update.</param>
        [Author("Shahab Khan")]
        public void UpdateSplQualifiedUserbeforelogin(string idNumber)
        {
            Task.Run(async () =>
            {
                string DBId = GetUserId(idNumber);
                var dataList = ExecuteSqlQuery(dBQueries.UpdateSPLUserDecision(DBId, "Approve"));

                if (dataList.Count <= 0) return;

                Report.ChildLog.Log(Status.Info, "Updated SPL user to qualified for ID " + idNumber);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Retrieves the credit history ID for a given user ID number.
        /// This method repeatedly executes an SQL query until a valid credit history ID is found.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose credit history ID is to be retrieved.</param>
        /// <returns>The credit history ID associated with the specified user ID number.</returns>
        [Author("Shahab Khan")]
        public string GetCreditHistoryId(string idNumber)
        {
            Log.Info($"Getting credit history ID for user {idNumber}");
            IDictionary<string, object> creditHistory;

            do
            {
                creditHistory = ExecuteSqlQuery(dBQueries.GetCreditHistoryIDfromDB(idNumber))
      .FirstOrDefault();
            } while (creditHistory.Count <= 0);
            var creditHistoryId = creditHistory["Id"].ToString();
            Log.Info($"Retrieved credit history ID: {creditHistoryId}");
            return creditHistoryId;
        }

        /// <summary>
        /// Retrieves the user ID for a given user ID number.
        /// This method executes an SQL query to get the user details and returns the user ID.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose user ID is to be retrieved.</param>
        /// <returns>The user ID associated with the specified user ID number.</returns>
        [Author("Shahab Khan")]
        public string GetUserId(string idNumber)
        {
            Log.Info($"Getting user ID for {idNumber}");
            var userData = ExecuteSqlQuery(dBQueries.Dbo_UserTable(idNumber))
        .FirstOrDefault();
            Log.Info($"Retrieved user ID: {userData["Id"].ToString()}");
            return userData["Id"].ToString();
        }

        /// <summary>
        /// Retrieves the value associated with a specific key from the credit history for a given user ID number.
        /// This method first retrieves the credit history ID and then executes an SQL query to get the value
        /// associated with the specified key name.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose credit history value is to be retrieved.</param>
        /// <param name="Keyname">The key name for which the value is to be retrieved.</param>
        /// <returns>The value associated with the specified key name from the credit history.</returns>
        [Author("Shahab Khan")]
        public string KeynameValuefromCreditHistory(string idNumber, string Keyname)
        {
            string DBId = GetCreditHistoryId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.GetKeynameValuefromCreditHistory(DBId, Keyname));

            string keyVal = dataList[0]["keyVal"].ToString();
            return keyVal;
        }

        /// <summary>
        /// Retrieves the decision value from the user's SPL qualification decision based on their ID number.
        /// This method retrieves the user ID and executes an SQL query to get the decision value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose decision value is to be retrieved.</param>
        /// <returns>The decision value associated with the specified user ID number.</returns>
        [Author("Shahab Khan")]
        public string LesDecisionFromUserSPLQualificationDecision(string idNumber)
        {
            string DBId = GetUserId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.getLesDecisionFromUserSPLQualificationDecision(DBId));

            // Is decisionValue
            string decisionValue = dataList[0]["Decision"].ToString();
            return decisionValue;
        }

        /// <summary>
        /// Retrieves client conversion details from the database for a given user ID number.
        /// This method executes an SQL query to get the client conversion details and logs the values.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose client conversion details are to be retrieved.</param>
        /// <returns>A dictionary containing the client and conversion details.</returns>
        [Author("Shahab Khan")]
        public IDictionary<string, string> DBC_Client_ConversionFromDB(string idNumber)
        {
            var dataList = ExecuteSqlQuery(dBQueries.DBC_Client_Conversion(idNumber));
            string dbClient = dataList[0]["DBClient"].ToString();
            string dbC_Conversion = dataList[0]["DBC_Conversion"].ToString();

            Report.ChildLog.Log(Status.Info, "dbClient value - " + dbClient);
            Report.ChildLog.Log(Status.Info, "dbC_Conversion value - " + dbC_Conversion);

            IDictionary<string, string> dictionary = new Dictionary<string, string>
    {
        { "dbClient", dbClient },
        { "dbC_Conversion", dbC_Conversion }
    };

            return dictionary;
        }

        /// <summary>
        /// Updates the Credit Coach score for a personal loan based on the user's ID number and key-value pair.
        /// This method retrieves the credit history ID and executes an SQL query to update the score.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose score is to be updated.</param>
        /// <param name="keyName">The key name associated with the score update.</param>
        /// <param name="KeyVal">The new value to update the score with.</param>
        [Author("Shahab Khan")]
        public void UpdateCreditCoachScore_PersonalLoan(string idNumber, string keyName, string KeyVal)
        {
            string DBId = GetCreditHistoryId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.UpdateCreditCoachScore_PersonalLoan(DBId, keyName, KeyVal));

            Report.ChildLog.Log(Status.Info, "update " + keyName + " for ID " + idNumber);
        }

        /// <summary>
        /// Updates the Les decision for a user based on their ID number and the new decision value.
        /// This method retrieves the user ID and executes an SQL query to update the decision.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose decision is to be updated.</param>
        /// <param name="decision">The new decision value to update.</param>
        [Author("Shahab Khan")]
        public void UpdateLesDecision(string idNumber, string decision)
        {
            string DBId = GetUserId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.UpdateSPLUserDecision(DBId, decision));

            Report.ChildLog.Log(Status.Info, "update Les Decision for ID " + idNumber);
        }

        /// <summary>
        /// Validates the campaign source for a user based on their ID number and expected campaign source.
        /// This method retrieves the campaign source from the database and compares it with the expected value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose campaign source is to be validated.</param>
        /// <param name="expectedCampaignSource">The expected campaign source value for validation.</param>
        [Author("Shahab Khan")]
        public void GetCampaignSourceValidate(string idNumber, string expectedCampaignSource)
        {
            string RequestParam = null;
            string Response = null;
            DateTime retryTime;
            DateTime currentTime = DateTime.Now;

            do
            {
                try
                {
                    baseStep.wait.WaitTillPageLoad();
                    List<IDictionary<string, object>> dataList;

                    do
                    {
                        dataList = ExecuteSqlQuery(dBQueries.GetCampaignSource(idNumber));
                        baseStep.wait.GenericWait(10000);
                        Response = dataList[0]["ResponseData"].ToString();
                        retryTime = DateTime.Now;
                    } while (dataList.Count <= 0 && retryTime < currentTime.AddMinutes(5));

                    RequestParam = dataList[0]["RequestParam"].ToString();
                    Response = dataList[0]["ResponseData"].ToString();
                }
                catch
                {
                    baseStep.wait.GenericWait(60000);
                }
                retryTime = DateTime.Now;
            } while (Response == null && retryTime < currentTime.AddMinutes(5));

            JObject json = JObject.Parse(RequestParam);
            string actualCampaignSource = (string?)json["campaign_source"] ?? "";

            Report.ChildLog.Log(Status.Info, "Campaign Source for ID " + idNumber + " is " + json["campaign_source"]);
            Report.ChildLog.Log(Status.Info, "Response against this campaign is " + Response);
            validate.AssertEqualWithMessage(expectedCampaignSource, actualCampaignSource, "CampaignSource as expected", true);
            Report.ChildLog.Log(Status.Info, "Expected: " + expectedCampaignSource + " and was: " + actualCampaignSource);
        }

        /// <summary>
        /// Updates the total current balance for a user based on their ID number and the new balance value.
        /// This method retrieves the credit history ID and executes an SQL query to update the balance.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose balance is to be updated.</param>
        /// <param name="TotalCurrentBalance">The new total current balance value to update.</param>
        [Author("Shahab Khan")]
        public void UpdateTotalCurrentBalance(string idNumber, string TotalCurrentBalance)
        {
            string DBId = GetCreditHistoryId(idNumber);
            ExecuteSqlQuery(dBQueries.UpdateTotalCurrentBalance(DBId, TotalCurrentBalance));

            Report.ChildLog.Log(Status.Info, "update Total Current Balance for ID " + idNumber);
        }

        /// <summary>
        /// Calculates the salary required to cover debt based on the user's ID number and a percentage.
        /// This method retrieves the credit health information and calculates the salary based on the total monthly instalments.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose salary towards debt is to be calculated.</param>
        /// <param name="salaryTowardsDebtPer">The percentage of salary towards debt.</param>
        /// <returns>The calculated salary required to cover the debt.</returns>
        [Author("Shahab Khan")]
        public int SalaryTowardsDebt(string idNumber, double salaryTowardsDebtPer)
        {
            string DBId = GetCreditHistoryId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.GetCreditHealthInfo(DBId));
            string TotalMonthlyInstalments = dataList[0]["TotalMonthlyInstalments"].ToString();

            Report.ChildLog.Log(Status.Info, "TotalMonthlyInstalments is  " + TotalMonthlyInstalments);

            int TotalMonthlyInstalment = int.Parse(genericUtils.SplitString(TotalMonthlyInstalments, ".", 0));
            double salary = TotalMonthlyInstalment / salaryTowardsDebtPer;

            int returnSalary = (int)Math.Round(salary);
            return returnSalary;
        }

        /// <summary>
        /// Retrieves the response URL for a given user ID number.
        /// This method retrieves the user ID and executes an SQL query to get the response URL.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose response URL is to be retrieved.</param>
        /// <returns>The response URL associated with the specified user ID number.</returns>
        [Author("Shahab Khan")]
        public string GetResponseUrl(string idNumber)
        {
            var userId = GetUserId(idNumber);
            var responseData = ExecuteSqlQuery(dBQueries.GetResponseUrl(userId))
                .FirstOrDefault();
            string ResponseUrl = responseData["ResponseUrl"].ToString();

            Report.ChildLog.Log(Status.Info, "ResponseUrl is  " + ResponseUrl);
            return ResponseUrl;
        }

        /// <summary>
        /// Retrieves the Credit Coach score for a given user ID number.
        /// This method retrieves the credit history ID and executes an SQL query to get the score.
        /// If the score is null, it defaults to "1000".
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose Credit Coach score is to be retrieved.</param>
        /// <returns>The Credit Coach score associated with the specified user ID number.</returns>
        [Author("Shahab Khan")]
        public string GetCreditCoachScore(string idNumber)
        {
            string DBId = GetCreditHistoryId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.GetCreditCoachScore(DBId));
            string keyVal = dataList[0]["keyVal"].ToString();

            Report.ChildLog.Log(Status.Info, "keyVal for ID " + idNumber + " is " + keyVal);

            if (keyVal == null)
            {
                keyVal = "1000";
            }

            return keyVal;
        }

        /// <summary>
        /// Checks if the basic information verification is confirmed for a given user ID number.
        /// This method repeatedly executes an SQL query until the verification status is retrieved.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose basic information verification status is to be checked.</param>
        /// <returns>True if the basic information is verified, otherwise false.</returns>
        [Author("Shahab Khan")]
        public bool GetbasicInfoVerifyConfirmed(string idNumber)
        {
            string basicInfoVerifyConfirmed;

            do
            {
                try
                {
                    baseStep.wait.GenericWait(2000);
                    var dataList = ExecuteSqlQuery(dBQueries.Dbo_UserTable(idNumber));
                    basicInfoVerifyConfirmed = dataList[0]["BasicInfoVerifyConfirmed"].ToString();
                }
                catch
                {
                    basicInfoVerifyConfirmed = null;
                }
            } while (basicInfoVerifyConfirmed == null);

            bool basicInfoVerify = bool.Parse(basicInfoVerifyConfirmed);
            return basicInfoVerify;
        }

        /// <summary>
        /// Validates the LMS registration status for a user based on their ID number.
        /// This method retrieves the campaign source response time and validates the campaign source.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose LMS registration status is to be validated.</param>
        [Author("Shahab Khan")]
        public void ValidateLMS_RegistrationInactiveSPL(string idNumber)
        {
            DateTime ResponseTime;

            do
            {
                ResponseTime = GetCampaignSourceResponseTime(idNumber);
                Report.ChildLog.Log(Status.Info, "LMS is generated at time " + ResponseTime);
                GetCampaignSourceValidate(idNumber, "Registration: Inactive SPL Autoreg");
            } while (ResponseTime == null);

            GetIsActiveStatus(idNumber);
        }

        /// <summary>
        /// Retrieves the campaign source response time for a given user ID number.
        /// This method executes an SQL query to get the campaign source data and parses the response time
        /// into a DateTime object, which is then returned.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose campaign source response time is to be retrieved.</param>
        /// <returns>The parsed DateTime representing the campaign source response time.</returns>
        [Author("Shahab Khan")]
        public DateTime GetCampaignSourceResponseTime(string idNumber)
        {
            var dataList = ExecuteSqlQuery(dBQueries.GetCampaignSource(idNumber));
            string ResponseTime = dataList[0]["ResponseTime"].ToString();
            DateTime parsedresponseTime = DateTime.ParseExact(ResponseTime, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            string formattedDateTime = parsedresponseTime.ToString("yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture);
            DateTime parsedDateTime = DateTime.ParseExact(formattedDateTime, "yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture);

            Report.ChildLog.Log(Status.Info, "Response Time is " + ResponseTime);
            return parsedDateTime;
        }

        /// <summary>
        /// Validates the LMS registration status when the OTP fails for a given user ID number and source.
        /// This method checks the active status and source of the user, retrieves the campaign source response time,
        /// and validates the campaign source against the expected value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose LMS registration status is to be validated.</param>
        /// <param name="source">The expected source for validation.</param>
        [Author("Shahab Khan")]
        public void ValidateLMS_RegistrationFailedOTP(string idNumber, string source)
        {
            GetIsActiveStatus(idNumber);
            GetSource(idNumber, source);
            string ResponseTime = null;

            do
            {
                try
                {
                    ResponseTime = GetCampaignSourceResponseTime(idNumber).ToString();
                }
                catch { }

                Report.ChildLog.Log(Status.Info, "LMS generated at time " + ResponseTime);
                GetCampaignSourceValidate(idNumber, "Registration: Failed OTP");

            } while (ResponseTime == null);
        }

        /// <summary>
        /// Retrieves the active status of a user based on their ID number.
        /// This method repeatedly executes an SQL query until the active status is retrieved and logs the status.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose active status is to be retrieved.</param>
        [Author("Shahab Khan")]
        public void GetIsActiveStatus(string idNumber)
        {
            string IsActive = null;

            do
            {
                try
                {
                    var dataList = ExecuteSqlQuery(dBQueries.Dbo_UserTable(idNumber));
                    IsActive = dataList[0]["IsActive"].ToString();
                }
                catch
                {
                    baseStep.wait.GenericWait(60000);
                }
            } while (IsActive == null);

            Report.ChildLog.Log(Status.Info, "IsActive Status present in DB for ID " + idNumber + " is " + IsActive);
            Assert.That(IsActive.Equals("False"));
        }

        /// <summary>
        /// Retrieves the source for a given user ID number and validates it against the expected source.
        /// This method executes an SQL query to get the response URL and logs the source value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose source is to be retrieved.</param>
        /// <param name="source">The expected source value for validation.</param>
        [Author("Shahab Khan")]
        public void GetSource(string idNumber, string source)
        {
            string userId = GetUserId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.GetResponseUrl(userId));
            string sourceFromDB = dataList[0]["Source"].ToString();

            Report.ChildLog.Log(Status.Info, "Source present in DB for ID " + idNumber + " is " + sourceFromDB);
            Assert.That(sourceFromDB.Equals(source));
        }

        /// <summary>
        /// Validates the LMS registration status when the security questions fail for a given user ID number and source.
        /// This method checks the active status and source of the user, retrieves the campaign source response time,
        /// and validates the campaign source against the expected value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose LMS registration status is to be validated.</param>
        /// <param name="source">The expected source for validation.</param>
        [Author("Shahab Khan")]
        public void ValidateLMS_RegistrationFailedSecurityQuestions(string idNumber, string source)
        {
            GetIsActiveStatus(idNumber);
            GetSource(idNumber, source);
            DateTime ResponseTime;

            do
            {
                ResponseTime = GetCampaignSourceResponseTime(idNumber);
                Report.ChildLog.Log(Status.Info, "LMS generated at time " + ResponseTime);
                GetCampaignSourceValidate(idNumber, "Registration: Failed Security Questions");

            } while (ResponseTime == null);
        }

        /// <summary>
        /// Validates the LMS registration status when inactive for OOBA for a given user ID number and source.
        /// This method checks the active status and source of the user, and attempts to validate the campaign source.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose LMS registration status is to be validated.</param>
        /// <param name="source">The expected source for validation.</param>
        [Author("Shahab Khan")]
        public void ValidateLMS_RegistrationInactiveForOOBA(string idNumber, string source)
        {
            GetIsActiveStatus(idNumber);
            GetSource(idNumber, source);

            try
            {
                GetCampaignSourceValidate(idNumber, "Registration: Failed OTP");
            }
            catch
            {
                Report.ChildLog.Log(Status.Info, "Campaign Source is not visible for OOBA after 24 hours");
            }
        }

        /// <summary>
        /// Validates the LMS registration status for budgeting advice for a given user ID number and registration source.
        /// This method retrieves the registration source and validates the campaign source against the expected value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose LMS registration status is to be validated.</param>
        /// <param name="regSource">The expected registration source for validation.</param>
        [Author("Shahab Khan")]
        public void ValidateLMS_RegistrationBudgetingAdvice(string idNumber, string regSource)
        {
            GetRegistrationSource(idNumber, regSource + "-registration");
            GetCampaignSourceValidate(idNumber, "Registration: Budgeting Advice");
        }

        /// <summary>
        /// Retrieves the registration source for a given user ID number and validates it against the expected registration source.
        /// This method executes an SQL query to get the registration source and logs the value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose registration source is to be retrieved.</param>
        /// <param name="regSource">The expected registration source for validation.</param>
        [Author("Shahab Khan")]
        public void GetRegistrationSource(string idNumber, string regSource)
        {
            var dataList = ExecuteSqlQuery(dBQueries.GetRegistrationSource(idNumber));
            string RegistrationSource = dataList[0]["RegistrationSource"].ToString();

            Report.ChildLog.Log(Status.Info, "RegistrationSource Status present in DB for ID " + idNumber + " is " + RegistrationSource);

            if (!(regSource.ToLower() == "normal-registration") && !(regSource.ToLower() == "ooba-registration"))
            {
                Assert.That(RegistrationSource.Equals(regSource));
            }
        }

        /// <summary>
        /// Retrieves the client information from an external lead for a given user ID number and validates it against the expected source.
        /// This method executes an SQL query to get the client information and logs the value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose client information is to be retrieved.</param>
        /// <param name="source">The expected client source for validation.</param>
        [Author("Shahab Khan")]
        public void GetClientFromExternalLead(string idNumber, string source)
        {
            string userId = GetUserId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.GetExternalLead(userId));
            string Client = dataList[0]["Client"].ToString();

            Report.ChildLog.Log(Status.Info, "Client present in DB for ID " + idNumber + " is " + Client);
            Assert.That(Client.ToLower() == source.ToLower());
        }

        /// <summary>
        /// Verifies the SPL LES information for a given user ID number.
        /// This method retrieves various fields from the SPL LES information and logs their values.
        /// It determines the qualification status based on specific criteria.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose SPL LES information is to be verified.</param>
        /// <returns>The qualification status as "qualified" or "disQualified".</returns>
        [Author("Shahab Khan")]
        public string VerifySPLLESInformation(string idNumber)
        {
            string status = null;
            string DBId = GetCreditHistoryId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.GetSPLLESInformation(DBId));

            string STTS_LTST_AL_AL_DTRV_AL = dataList[0]["STTS_LTST_AL_AL_DTRV_AL"].ToString();
            Report.ChildLog.Log(Status.Info, "STTS_LTST_AL_AL_DTRV_AL in DB for ID " + idNumber + " is " + STTS_LTST_AL_AL_DTRV_AL);

            string CreditCoachScore_PersonalLoan = dataList[0]["CreditCoachScore_PersonalLoan"].ToString();
            Report.ChildLog.Log(Status.Info, "CreditCoachScore_PersonalLoan in DB for ID " + idNumber + " is " + CreditCoachScore_PersonalLoan);

            string AGEY_BRTH_AL_AL_ALLT_AL = dataList[0]["AGEY_BRTH_AL_AL_ALLT_AL"].ToString();
            Report.ChildLog.Log(Status.Info, "AGEY_BRTH_AL_AL_ALLT_AL in DB for ID " + idNumber + " is " + AGEY_BRTH_AL_AL_ALLT_AL);

            string NUMB_LTST_AL_AL_ALLT_C9 = dataList[0]["NUMB_LTST_AL_AL_ALLT_C9"].ToString();
            Report.ChildLog.Log(Status.Info, "NUMB_LTST_AL_AL_ALLT_C9 in DB for ID " + idNumber + " is " + NUMB_LTST_AL_AL_ALLT_C9);

            string NUMB_LTST_AL_AL_ALLT_6p = dataList[0]["NUMB_LTST_AL_AL_ALLT_6p"].ToString();
            Report.ChildLog.Log(Status.Info, "NUMB_LTST_AL_AL_ALLT_6p in DB for ID " + idNumber + " is " + NUMB_LTST_AL_AL_ALLT_6p);

            string AGEM_OLDT_AL_AL_ALLT_AL = dataList[0]["AGEM_OLDT_AL_AL_ALLT_AL"].ToString();
            Report.ChildLog.Log(Status.Info, "AGEM_OLDT_AL_AL_ALLT_AL in DB for ID " + idNumber + " is " + AGEM_OLDT_AL_AL_ALLT_AL);

            string TTBL_OPNG_AL_AL_ALLT_AL = dataList[0]["TTBL_OPNG_AL_AL_ALLT_AL"].ToString();
            Report.ChildLog.Log(Status.Info, "TTBL_OPNG_AL_AL_ALLT_AL in DB for ID " + idNumber + " is " + TTBL_OPNG_AL_AL_ALLT_AL);

            if (int.Parse(STTS_LTST_AL_AL_DTRV_AL) <= -2 && int.Parse(CreditCoachScore_PersonalLoan) >= 70 && int.Parse(AGEY_BRTH_AL_AL_ALLT_AL) >= 19
                && int.Parse(AGEY_BRTH_AL_AL_ALLT_AL) <= 65 && int.Parse(NUMB_LTST_AL_AL_ALLT_C9) < 1 && int.Parse(NUMB_LTST_AL_AL_ALLT_6p) < 2 && int.Parse(AGEM_OLDT_AL_AL_ALLT_AL) > 5
                && int.Parse(TTBL_OPNG_AL_AL_ALLT_AL) >= 5000)
            {
                status = "qualified";
            }
            else
            {
                status = "disQualified";
            }

            return status;
        }

        /// <summary>
        /// Retrieves the JSON decision from the user's SPL qualification decision based on their ID number.
        /// This method checks if the decision value contains a specific message and returns the qualification status accordingly.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose SPL qualification decision is to be retrieved.</param>
        /// <returns>The qualification status as "qualified" or an empty string.</returns>
        [Author("Shahab Khan")]
        public string JsonDecisionFromUserSPLQualificationDecision(string idNumber)
        {
            string DBId = GetUserId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.getLesDecisionFromUserSPLQualificationDecision(DBId));

            string decisionValue = dataList[0]["JsonDecisionReasons"].ToString();
            if (decisionValue.Contains("LES: Unable to retrieve customer data"))
            {
                string status = "qualified";
                return status;
            }

            return "";
        }

        /// <summary>
        /// Handles the process of clicking wrong answers for security questions for a given user ID number.
        /// This method retrieves the security questions and their corresponding answers, and attempts to select wrong answers.
        /// </summary>
        /// <param name="IdNumber">The ID number of the user whose security questions are to be answered.</param>
        [Author("Shahab Khan")]
        public void GetWrongQuestionClick(string idNumber)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            IDictionary<string, string> questionBook = new Dictionary<string, string>();
            var dataList = ExecuteSqlQuery(dBQueries.QuestionQuery(idNumber));

            foreach (var row in dataList)
            {
                questionBook.Add(new KeyValuePair<string, string>(row["Question"].ToString(), row["CurrectAnswer0"].ToString()));
            }

            int index = 1;

            while (registrationPage.IsSecurityQuestionDisplayed())
            {
                SelectWrongAnswerOfSecurity(questionBook, registrationPage, index);
                index++;

                if (index == 6)
                {
                    break;
                }
            }

            validate.TakeStepFullScreenShot("5 out of 5 Questions are Selected", Status.Info);
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.aftersecurityquestionsubmitbtn, 15);
            baseStep.Click(registrationPage.AfterSecurityQuestionSubmitBtn);
        }

        /// <summary>
        /// Updates the SPL LES information for a given user ID number based on their qualification status.
        /// This method retrieves the credit history ID and updates the SPL LES information to either "80" or "60"
        /// depending on whether the user is qualified or not.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose SPL LES information is to be updated.</param>
        /// <param name="isQualifiedSPL">Indicates whether the user is qualified for SPL.</param>
        [Author("Shahab Khan")]
        public void UpdateSPLLESInformation(string idNumber, string les_decision, bool isQualifiedSPL)
        {
            string DBId = GetCreditHistoryId(idNumber);
            string userId = GetUserId(idNumber);
            if (isQualifiedSPL)
            {
                if (les_decision.ToLower() == "approve" || les_decision.ToLower() == "maybe")
                {
                    ExecuteSqlQuery(dBQueries.UpdateSPLLESInformation(DBId, "80"));
                }
                else
                {
                    ExecuteSqlQuery(dBQueries.UpdateSPLJsonDecisionReasons(userId, "[\"LES: Unable to retrieve customer data\"]"));
                }
            }
            else
            {
                if (les_decision.ToLower() == "maybe" || les_decision.ToLower() == "approve")
                {
                    ExecuteSqlQuery(dBQueries.UpdateSPLLESInformation(DBId, "60"));
                }
                else
                {
                    ExecuteSqlQuery(dBQueries.UpdateSPLJsonDecisionReasons(userId, "[\"LES: SPL Maybe decision due to insufficient customer data\"]"));
                }
            }
        }

        /// <summary>
        /// Selects seven predefined wrong answers for security questions.
        /// This method initializes a dictionary with predefined questions and answers and calls the method to handle the selection.
        /// </summary>
        [Author("Shahab Khan")]
        public void SelectSevenWrongQuestionClick()
        {
            IDictionary<string, string> questionBook = new Dictionary<string, string>
    {
        { "1", "Yes" },
        { "2", "Yes" },
        { "3", "Yes" },
        { "4", "I DO NOT HAVE" },
        { "5", "ABSA BANK LIMITED - ABSA HOME LOANS PRE-REGISTRATION" },
        { "6", "6001 - 7000" },
        { "7", "MARKHAM" }
    };

            GetTempSevenQuestionClick(questionBook);
        }

        /// <summary>
        /// Handles the selection of answers for seven security questions based on the provided question book.
        /// This method iterates through the questions, selects the corresponding answers, and submits the answers.
        /// </summary>
        /// <param name="questionBook">A dictionary containing the questions and their corresponding answers.</param>
        [Author("Shahab Khan")]
        public void GetTempSevenQuestionClick(IDictionary<string, string> questionBook)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            int i = 0;

            while (registrationPage.IsSecurityQuestionDisplayed())
            {
                SelectAnswerOfSevenSecurity(questionBook, registrationPage, i + 1);
                i++;
                if (i == 7)
                {
                    break;
                }
            }

            validate.TakeStepFullScreenShot("7 out of 7 Questions are Selected", Status.Info);

            try
            {
                do
                {
                    baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.aftersecurityquestionsubmitbtn, 15);
                    baseStep.ScrollToElement(registrationPage.AfterSecurityQuestionSubmitBtn);
                    baseStep.Click(registrationPage.AfterSecurityQuestionSubmitBtn);
                    baseStep.wait.WaitTillPageLoad();
                } while (validate.IsElementDisplayed(registrationPage.aftersecurityquestionsubmitbtn));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Fetches the active phone number for a given row of data from the database.
        /// This method executes a SQL query to retrieve the phone number based on the specified row data.
        /// </summary>
        /// <param name="rowData">The row index for which the phone number is to be fetched.</param>
        /// <returns>The active phone number as a string.</returns>
        [Author("Shahab Khan")]
        public string FetchActivePhoneNumber(int rowData)
        {
            var dictionary = FetchTableData(dBQueries.FetchPhoneNumberfromDB(), rowData);
            return dictionary["PhoneNumber"].ToString();
        }

        /// <summary>
        /// Fetches the active ID number for a given row of data from the database.
        /// This method executes a SQL query to retrieve the ID number based on the specified row data.
        /// </summary>
        /// <param name="rowData">The row index for which the ID number is to be fetched.</param>
        /// <returns>The active ID number as a string.</returns>
        [Author("Shahab Khan")]
        public string FetchActiveIdnumber(string idNumber, int rowData)
        {
            if (!string.IsNullOrEmpty(idNumber) && idNumber.Count() == 13)
            {
                return idNumber;
            }
            else
            {
                var dictionary = FetchTableData(dBQueries.FetchIdNumberfromDB(), rowData);
                return dictionary["IdNumber"].ToString();
            }
        }

        /// <summary>
        /// Updates the credit score for a given user ID number to the specified score.
        /// This method repeatedly updates the score in the database until the fetched score matches the specified score.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose credit score is to be updated.</param>
        /// <param name="score">The new credit score to be set.</param>
        [Author("Shahab Khan")]
        public void UpdateCreditScore(string idNumber, string score)
        {
            string fetchedScore;

            do
            {
                UpdateAndDeleteTable(dBQueries.UpdateScorefromDB(score, GetCreditHistoryId(idNumber)));
                baseStep.wait.GenericWait(2000);
                fetchedScore = GetCreditScoreFromDB(idNumber);
            } while (fetchedScore != score);
        }

        /// <summary>
        /// Retrieves the credit score from the database for a given user ID number.
        /// This method fetches the score information from the database and returns the score as a string.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose credit score is to be retrieved.</param>
        /// <returns>The credit score as a string.</returns>
        [Author("Shahab Khan")]
        public string GetCreditScoreFromDB(string idNumber)
        {
            var dic = FetchScoreInformationTable(idNumber);
            return dic["ScorePercent"].ToString();
        }

        /// <summary>
        /// Calculates the wealth score for a given user ID number based on their liabilities.
        /// This method retrieves the user's assets and calculates the wealth score using the formula (assets / liabilities) - 1.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose wealth score is to be calculated.</param>
        /// <param name="liabilities">The total liabilities of the user.</param>
        /// <returns>The calculated wealth score as a double.</returns>
        [Author("Shahab Khan")]
        public double CalculateWealthScore(string idNumber, int liabilities)
        {
            string userId = GetUserId(idNumber);
            var dictionary = FetchTableData(dBQueries.WealthScoreTable(userId));

            double assets = double.Parse(dictionary["Properties"].ToString()) +
                            double.Parse(dictionary["Vehicles"].ToString()) +
                            double.Parse(dictionary["RetirementSavings"].ToString()) +
                            double.Parse(dictionary["OtherSavings"].ToString());

            return (assets / liabilities) - 1;
        }

        /// <summary>
        /// Deletes the wealth entry for a given user ID number from the database.
        /// This method retrieves the user ID and executes a SQL query to delete the user's wealth entry.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose wealth entry is to be deleted.</param>
        [Author("Shahab Khan")]
        public void DeleteUserWealth(string idNumber)
        {
            string userId = GetUserId(idNumber);
            UpdateAndDeleteTable(dBQueries.DeleteUserWealthEntry(userId));
        }

        /// <summary>
        /// Fetches the budget table data for a given user ID number.
        /// This method retrieves the user ID and executes a SQL query to fetch the user's budget data.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose budget data is to be fetched.</param>
        /// <returns>A dictionary containing the budget data.</returns>
        [Author("Shahab Khan")]
        public Dictionary<string, object> FetchBudgetTable(string idNumber)
        {
            string userId = GetUserId(idNumber);
            return FetchTableData(dBQueries.UserBudgetTable(userId));
        }

        /// <summary>
        /// Retrieves frequently asked questions (FAQ) from the database.
        /// This method fetches questions and answers from the FAQ table and returns them as a dictionary.
        /// </summary>
        /// <returns>A dictionary containing FAQ questions and their corresponding answers.</returns>
        [Author("Shahab Khan")]
        public Dictionary<string, string> FAQQuestions()
        {
            Dictionary<string, object> dictionary;
            string question;
            string answer;
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            int i = 0;

            try
            {
                do
                {
                    dictionary = FetchTableData(dBQueries.FaqTable(), i);
                    question = dictionary["Question"].ToString();
                    answer = dictionary["Answer"].ToString();

                    if (!keyValuePairs.ContainsKey(question))
                        keyValuePairs.Add(question, answer);

                    i++;
                } while (dictionary != null);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return keyValuePairs;
        }

        /// <summary>
        /// Retrieves the percentage of salary going toward debt for a given user ID number.
        /// This method fetches the credit health information from the database and returns the corresponding value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose salary percentage is to be fetched.</param>
        /// <returns>The percentage of salary going toward debt as a string.</returns>
        [Author("Shahab Khan")]
        public string SalaryGoingTowardDebtPercent(string idNumber)
        {
            string DBId = GetCreditHistoryId(idNumber);
            var dic = FetchTableData(dBQueries.GetCreditHealthInfo(DBId));
            return dic["SalaryGoingTowardDebtPercent"].ToString();
        }

        /// <summary>
        /// Retrieves the total overdue amount for a given user ID number.
        /// This method fetches the credit health information from the database and returns the corresponding value.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose total overdue amount is to be fetched.</param>
        /// <returns>The total overdue amount as a string.</returns>
        [Author("Shahab Khan")]
        public string TotalOverdueAmount(string idNumber)
        {
            string DBId = GetCreditHistoryId(idNumber);
            var dic = FetchTableData(dBQueries.GetCreditHealthInfo(DBId));
            return dic["TotalOverdueAmount"].ToString();
        }

        /// <summary>
        /// Retrieves the full name of a user based on their ID number.
        /// This method fetches the user's first name and surname from the database and returns the full name.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose name is to be retrieved.</param>
        /// <returns>The full name of the user as a string.</returns>
        [Author("Shahab Khan")]
        public string GetUserName(string idNumber)
        {
            var dic = FetchTableData(dBQueries.Dbo_UserTable(idNumber));
            string firstname = dic["FirstName"].ToString();
            string surname = dic["Surname"].ToString();
            return $"{firstname} {surname}";
        }

        /// <summary>
        /// Retrieves the visit ID for a given user ID number.
        /// This method fetches the user's visit log from the database and returns the visit ID.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose visit ID is to be retrieved.</param>
        /// <returns>The visit ID as a string.</returns>
        [Author("Shahab Khan")]
        public string GetVisitId(string idNumber)
        {
            string userId = GetUserId(idNumber);
            var dic = FetchTableData(dBQueries.SapiVisitLogTable(userId));
            return dic["VisitId"].ToString();
        }

        /// <summary>
        /// Fetches the score information table for a given user ID number.
        /// This method retrieves the credit history ID and fetches the corresponding score information from the database.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose score information is to be fetched.</param>
        /// <returns>A dictionary containing the score information.</returns>
        [Author("Shahab Khan")]
        public Dictionary<string, object> FetchScoreInformationTable(string idNumber)
        {
            string creditId = GetCreditHistoryId(idNumber);
            return FetchTableData(dBQueries.ScoreInformationTable(creditId));
        }

        /// <summary>
        /// Fetches the credit health information table for a given user ID number.
        /// This method retrieves the credit history ID and fetches the corresponding credit health information from the database.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose credit health information is to be fetched.</param>
        /// <returns>A dictionary containing the credit health information.</returns>
        [Author("Shahab Khan")]
        public Dictionary<string, object> CreditHealthInfoTable(string idNumber)
        {
            string DBId = GetCreditHistoryId(idNumber);
            return FetchTableData(dBQueries.GetCreditHealthInfo(DBId));
        }

        /// <summary>
        /// Fetches an ID number that has a credit history for a specified number of months.
        /// This method executes a SQL query to retrieve an ID number based on the specified criteria.
        /// </summary>
        /// <param name="numberOfMonths">The number of months for which the credit history is to be checked.</param>
        /// <returns>The ID number as a string.</returns>
        [Author("Shahab Khan")]
        public string FetchIdnumberAvailableForMonths(int numberOfMonths)
        {
            var dictionary = FetchTableData(dBQueries.FetchIdnumberHavingNumberOfMonthsCreditHistory(numberOfMonths), 1);
            return dictionary["IdNumber"].ToString();
        }

        /// <summary>
        /// Fetches the credit scores for the previous three months for a given user ID number.
        /// This method retrieves the user's ID and logs the credit scores for the last three months.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose credit scores are to be fetched.</param>
        [Author("Shahab Khan")]
        public void FetchIdnumberAvailableForMonths(string idNumber)
        {
            string userId = GetUserId(idNumber);

            for (int i = 0; i < 3; i++)
            {
                var dictionary = FetchTableData(dBQueries.FetchCreditScoreForPreviousThreeMonths(userId), i);
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(int.Parse(dictionary["Month"].ToString()));
                Report.ChildLog.Log(Status.Info, $"Credit Score for {month} is {dictionary["ScorePercent"].ToString()}");
            }
        }

        /// <summary>
        /// Updates and deletes records in the database based on the provided SQL query.
        /// This method executes the SQL query until no records are returned.
        /// </summary>
        /// <param name="dBQuery">The SQL query to be executed for updating and deleting records.</param>
        public void UpdateAndDeleteTable(string dBQuery)
        {
            List<IDictionary<string, object>> dataList;

            do
            {
                dataList = ExecuteSqlQuery(dBQuery);
            } while (dataList.Count != 0);
        }

        /// <summary>
        /// Fetches account information for a given user ID number, card title, and account number.
        /// This method retrieves the credit history ID and fetches the corresponding account information from the database.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose account information is to be fetched.</param>
        /// <param name="cardTitle">The title of the card associated with the account.</param>
        /// <param name="accNumber">The account number to be fetched.</param>
        /// <returns>A dictionary containing the account information.</returns>
        [Author("Shahab Khan")]
        public Dictionary<string, object> FetchAccountInformationTable(string idNumber, string cardTitle, string accNumber)
        {
            string creditId = GetCreditHistoryId(idNumber);
            return FetchTableData(dBQueries.AccountInformationTable(creditId, cardTitle, accNumber));
        }

        /// <summary>
        /// Fetches judgment information for a given user ID number and card title.
        /// This method retrieves the credit history ID and fetches the corresponding judgment and legal action information from the database.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose judgment information is to be fetched.</param>
        /// <param name="cardTitle">The title of the card associated with the judgment.</param>
        /// <returns>A dictionary containing the judgment information.</returns>
        [Author("Shahab Khan")]
        public Dictionary<string, object> FetchJudgmentInformationTable(string idNumber, string cardTitle)
        {
            string creditId = GetCreditHistoryId(idNumber);
            return FetchTableData(dBQueries.JudgementsAndLegalActionTable(creditId, cardTitle));
        }

        /// <summary>
        /// Fetches debt restructure review information for a given user ID number and card title.
        /// This method retrieves the credit history ID and fetches the corresponding debt restructure review information from the database.
        /// </summary>
        /// <param name="idNumber">The ID number of the user whose debt restructure review information is to be fetched.</param>
        /// <param name="cardTitle">The title of the card associated with the debt restructure review.</param>
        /// <returns>A dictionary containing the debt restructure review information.</returns>
        [Author("Shahab Khan")]
        public Dictionary<string, object> FetchDebtRestructureReviewTable(string idNumber, string cardTitle)
        {
            string creditId = GetCreditHistoryId(idNumber);
            return FetchTableData(dBQueries.DebtRestructureReviewTable(creditId, cardTitle));
        }

        /// <summary>
        /// Fetches external commlog table as per commlogtype id
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        [Author("Shahab Khan")]
        public Dictionary<string, object> FetchExternalCommLogTable(string idNumber, int externalCommLogTypeId)
        {
            return FetchTableData(dBQueries.GetExternalCommLog(idNumber, externalCommLogTypeId));
        }

        /// <summary>
        /// Update DbQuoteInfo Table
        /// </summary>
        /// <param name="query"></param>
        [Author("Shahab Khan")]
        public void UpdateDbQuoteInfoTable(string idNumber, string dBClient, string dBC_Conversion)
        {
            string userId = GetUserId(idNumber);
            UpdateAndDeleteTable(dBQueries.UpdateDBC_Client_Conversion(userId, dBClient, dBC_Conversion));
            Report.ChildLog.Log(Status.Info, $"updated DbQuoteInfo Table with dBClient {dBClient} and dBC_Conversion {dBC_Conversion}");
        }

        /// <summary>
        /// Fetches login log data from an external communication log using SQL query, returning results as a dictionary with specified parameters.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="externalCommLogTypeId"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> FetchExternalCommLogInfo(string idNumber, int externalCommLogTypeId, int index)
        {
            return FetchTableData(dBQueries.GetExternalCommLog(idNumber, externalCommLogTypeId), index);
        }

        /// <summary>
        /// Fetches user details from the user table using an SQL query, returning results as a dictionary based on the provided ID number.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> FetchUserDetailsFromUserTable(string idNumber)
        {
            return FetchTableData(dBQueries.Dbo_UserTable(idNumber), 0);
        }

        /// <summary>
        /// Fetches branch details using an SQL query, returning results as a dictionary based on the provided branch ID.
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> FetchBranchDetails(string branchId)
        {
            return FetchTableData(dBQueries.GetBranchInfo(branchId), 0);
        }

        /// <summary>
        /// Fetches SPL qualification decision data for a given ID by retrieving user ID and querying the database, returning results as a dictionary.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> FetchSPLQualificationDecision(string idNumber)
        {
            string userId = GetUserId(idNumber);
            var dictionary = FetchTableData(dBQueries.getLesDecisionFromUserSPLQualificationDecision(userId), 0);
            return dictionary;
        }

        /// <summary>
        /// Retrieves the cellphone number from the OTP table using the provided cellphone number.
        /// </summary>
        /// <param name="CellphoneNumber"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> getCellphoneNumberfromOTPSTable(string CellphoneNumber)
        {
            var OTPS = otpStorageAccount.GetOTPSInfoFromOTPTable(CellphoneNumber);

            Dictionary<string, object> otpDetails = new Dictionary<string, object>
            {
                { "OTPObjectiveId", OTPS.OtpObjectiveId },
                { "Pin", OTPS.Pin }
            };

            return otpDetails;
        }

        /// <summary>
        /// Deletes an external communication log entry from the database based on the given ID number and log type identifier.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="ExternalCommLogTypeId"></param>
        [Author("Piyush Sharma")]
        public void DeleteExternalCommLog(string idNumber, int ExternalCommLogTypeId)
        {
            UpdateAndDeleteTable(dBQueries.DeleteFromExternalCommLog(idNumber, ExternalCommLogTypeId));
        }

        /// <summary>
        /// Fetches a credit history ID using the given ID number, queries database for quote info, and returns the result as a dictionary.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> FetchDCQuoteInfo(string idNumber)
        {
            string DBId = GetCreditHistoryId(idNumber);
            var dictionary = FetchTableData(dBQueries.FetchDCQuoteInfo(DBId), 0);
            return dictionary;
        }

        /// <summary>
        /// Retrieves credit history ID using the ID number, fetches SPLLES information from the database, and returns it as a dictionary.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> FetchSPLLESInformation(string idNumber)
        {
            string DBId = GetCreditHistoryId(idNumber);
            var dictionary = FetchTableData(dBQueries.GetSPLLESInformation(DBId), 0);
            return dictionary;
        }

        /// <summary>
        /// Fetches a user ID using the given ID number, runs a SQL query for external leads, and returns the results list.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public List<IDictionary<string, object>> FetchExternalLead(string idNumber)
        {
            string userId = GetUserId(idNumber);
            var dataList = ExecuteSqlQuery(dBQueries.GetExternalLead(userId));
            return dataList;
        }

        /// <summary>
        /// Parses and validates external lead request and response data against expected values, returning the response URL if all checks pass.
        /// </summary>
        /// <param name="externalLeadInfo"></param>
        /// <param name="idNumber"></param>
        /// <param name="source"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public string ValidateExternalLead(List<IDictionary<string, object>> externalLeadInfo, string idNumber, string source, string client)
        {
            string userId = GetUserId(idNumber);

            string request = externalLeadInfo[0]["RequestParam"].ToString();
            var requestParam = JObject.Parse(request);
            validate.AssertEquals(idNumber, requestParam["IdNumber"].ToString(), "Idnumber is mismatch", true);

            string response = externalLeadInfo[0]["ResponseData"].ToString();
            var responseParam = JObject.Parse(response);
            validate.AssertEquals(userId, responseParam["UserId"].ToString(), "Idnumber is mismatch", true);

            validate.AssertEquals(source, externalLeadInfo[0]["Source"].ToString(), "Source is mismatch", true);

            validate.AssertEquals(client, externalLeadInfo[0]["Client"].ToString(), "Client is mismatch", true);

            return externalLeadInfo[0]["ResponseUrl"].ToString();
        }

        /// <summary>
        /// Validates that the user's ID number matches and that their "IsActive" status is false.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="idNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateUserInfo(IDictionary<string, object> userInfo, string idNumber)
        {
            validate.AssertEquals(idNumber, userInfo["IdNumber"].ToString(), "IdNumber is mismatch", true);
            validate.AssertEquals("False", userInfo["IsActive"].ToString(), "IsActive status is mismatch", true);
        }

        /// <summary>
        /// Fetches lead log data from the database and returns it as a dictionary.
        /// </summary>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> FetchLeadLog()
        {
            var dictionary = FetchTableData_CCFunction(dBQueries.FetchLeadLog(), 0);
            return dictionary;
        }

        /// <summary>
        /// This method fetches campaign log details using a UTM ID by executing a query and returning the result as a dictionary.
        /// </summary>
        /// <param name="campaignUtmId"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> FetchCampaignLog(string campaignUtmId)
        {
            var dictionary = FetchTableData(dBQueries.FetchCampaignDetails(campaignUtmId), 0);
            return dictionary;
        }

        /// <summary>
        /// Fetches credit history for a given ID number by executing a database query and returning the result as a dictionary.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> FetchCreditHistory(string idNumber)
        {
            var dictionary = FetchTableData(dBQueries.GetCreditHistoryQuery(idNumber), 0);
            return dictionary;
        }

        /// <summary>
        /// Parses and validates lead log request and response data, checking ID number, status code, source, and client for correctness.
        /// </summary>
        /// <param name="LeadLogInfo"></param>
        /// <param name="idNumber"></param>
        /// <param name="source"></param>
        /// <param name="client"></param>
        [Author("Piyush Sharma")]
        public void ValidateLeadLog(Dictionary<string, object> LeadLogInfo, string idNumber, string source, string client)
        {
            string request = LeadLogInfo["RequestParam"].ToString();
            var requestParam = JObject.Parse(request);
            validate.AssertEquals(idNumber, requestParam["IdNumber"].ToString(), "Idnumber is mismatch", true);

            string response = LeadLogInfo["ResponseData"].ToString();
            var responseParam = JObject.Parse(response);
            validate.AssertEquals("200", responseParam["status"].ToString(), "Status Code is mismatch", true);

            validate.AssertEquals(source, LeadLogInfo["Source"].ToString(), "Source is mismatch", true);

            validate.AssertEquals(client, LeadLogInfo["Client"].ToString(), "Client is mismatch", true);
        }

        #region Private Helper Method

        /// <summary>
        /// Executes a SQL query and returns the result as a list of dictionaries.
        /// This method establishes a database connection using the connection string and executes the provided SQL query.
        /// </summary>
        /// <param name="sqlQuery">The SQL query to be executed.</param>
        /// <returns>A list of dictionaries containing the result of the SQL query.</returns>
        private List<IDictionary<string, object>> ExecuteSqlQuery(string sqlQuery)
        {
            var dbConnectionString = genericUtils.GetDbConnectionString(Properties.environment, "db_connectionstring");
            var sqlDbConnection = new SqlDatabase(genericUtils.Decrypt(dbConnectionString, 3));
            return sqlDbConnection.ExecuteSqlQuery(sqlQuery).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Executes a SQL query using a decrypted CCFunction DB connection string and returns the result as a list of dictionaries.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        private List<IDictionary<string, object>> ExecuteSqlQuery_CCFunction(string sqlQuery)
        {
            var dbConnectionString = genericUtils.GetDbConnectionString(Properties.environment, "db_connectionstring_CCFunction");
            var sqlDbConnection = new SqlDatabase(genericUtils.Decrypt(dbConnectionString, 3));
            return sqlDbConnection.ExecuteSqlQuery(sqlQuery).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Selects an incorrect answer to a security question based on the current question and predefined answers, ensuring selection before proceeding.
        /// </summary>
        /// <param name="questionBook"></param>
        /// <param name="registrationPage"></param>
        /// <param name="index"></param>
        private void SelectWrongAnswerOfSecurity(IDictionary<string, string> questionBook, RegistrationPage registrationPage, int index)
        {
            string question = registrationPage.SecurityQuestionText();

            if (questionBook.ContainsKey(question))
            {
                string answerID = questionBook[question];
                bool isAnswerSelected = false;
                int questionCount = 0;

                do
                {
                    if (answerID != "1")
                    {
                        baseStep.Click(registrationPage.OptionSelect("1"));
                        isAnswerSelected = registrationPage.isAnswerSelect("1");
                    }
                    else
                    {
                        baseStep.Click(registrationPage.OptionSelect("2"));
                        isAnswerSelected = registrationPage.isAnswerSelect("2");
                    }

                    string numberOfQuestion = baseStep.getText.Text(registrationPage.SecurityQuestionCountText);
                    char desiredCharacter = numberOfQuestion[9];
                    questionCount = int.Parse(desiredCharacter.ToString());

                } while (index == questionCount && !isAnswerSelected);
            }
            else
            {
                baseStep.Click(registrationPage.OptionSelect("2"));
            }
        }

        /// <summary>
        /// Fetches table data from the database based on the provided SQL query and row index.
        /// This method executes the SQL query and returns the data as a dictionary.
        /// </summary>
        /// <param name="dBQuery">The SQL query to be executed.</param>
        /// <param name="rowData">The row index for which the data is to be fetched (default is 0).</param>
        /// <returns>A dictionary containing the fetched data.</returns>
        private Dictionary<string, object> FetchTableData(string dBQuery, int rowData = 0)
        {
            var dataList = ExecuteSqlQuery(dBQuery);
            var resultDictionary = new Dictionary<string, object>();

            if (dataList.Count > 0)
            {
                foreach (var column in dataList[rowData].Keys)
                {
                    resultDictionary[column] = dataList[rowData][column].ToString();
                }
            }

            return resultDictionary;
        }

        /// <summary>
        /// Executes a SQL query, retrieves a specific row from the result, and returns its data as a dictionary of key-value pairs.
        /// </summary>
        /// <param name="dBQuery"></param>
        /// <param name="rowData"></param>
        /// <returns></returns>
        private Dictionary<string, object> FetchTableData_CCFunction(string dBQuery, int rowData = 0)
        {
            var dataList = ExecuteSqlQuery_CCFunction(dBQuery);
            var resultDictionary = new Dictionary<string, object>();

            if (dataList.Count > 0)
            {
                foreach (var column in dataList[rowData].Keys)
                {
                    resultDictionary[column] = dataList[rowData][column].ToString();
                }
            }

            return resultDictionary;
        }

        #endregion
    }
}