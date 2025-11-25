namespace SanlemAutomation
{
    public class API
    {
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();

        /// <summary>
        /// Performs automated registration through API for different sources
        /// </summary>
        /// <param name="idnumber">South African ID number for registration</param>
        /// <param name="fname">First name of the user</param>
        /// <param name="surname">Surname of the user</param>
        /// <param name="pnumber">Phone number for registration</param>
        /// <param name="source">Registration source (e.g. SPL-IVR or other channels)</param>
        /// <returns>void</returns>
        /// <remarks>
        /// - Handles both SPL-IVR and standard registration flows
        /// - Validates registration success via response content
        /// - Logs registration details and status
        /// </remarks>
        [Author("Shahab Khan")]
        public void AutoReg(string idnumber, string fname, string surname, string pnumber, string source)
        {
            var path = genericUtils.GetDataPath("TestResources");
            var json = genericUtils.GetJson(Path.Combine(path, "Collections.json"));
            var api = json[Properties.environment]["api"][source.ToLower()].ToString();
            var headerKey = json[Properties.environment]["header"][source.ToLower()].ToString();

            var request = new RestRequest(api, Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddHeader("x-functions-key", genericUtils.Decrypt(headerKey, 3));

            object requestBody;
            if (source.ToLower() == "spl-ivr")
            {
                requestBody = new
                {
                    PhoneNumber = pnumber,
                    IdNumber = idnumber,
                    AcceptTerms = true,
                    ConsentToCreditReport = true,
                    Source = source.ToUpper()
                };
            }
            else
            {
                requestBody = new
                {
                    FirstName = fname,
                    Surname = surname,
                    email = $"Test12{genericUtils.GetRandomString(4)}@test.com",
                    PhoneNumber = pnumber,
                    IdNumber = idnumber,
                    AcceptTerms = true,
                    Source = source.ToUpper()
                };
            }
            request.AddJsonBody(requestBody);

            var response = new RestClient().Execute(request);
            var content = response.Content;
            Report.ChildLog.Log(Status.Info, content);

            validate.AssertEquals(true, content.Contains("Registration Successful"), "Registration is not successful", true);
            Report.ChildLog.Log(Status.Info, $"User is successfully Auto Registered with content - {content}");
        }

        /// <summary>
        /// Calls a secured API using a certificate, sends ID number, logs status, and validates response contains “ProductsQualification” before returning content.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> APIM(string IdNumber, string source, string Certificate)
        {
            var path = genericUtils.GetDataPath("TestResources");
            var json = genericUtils.GetJson(Path.Combine(path, "Collections.json"));
            var api = json[Properties.environment]["api"][source.ToLower()].ToString();
            var headerKey = json[Properties.environment]["header"][source.ToLower()].ToString();
            var certificateURL = json[Properties.environment]["api"][Certificate.ToLower()].ToString();
            var certificateKey = json[Properties.environment]["header"][Certificate.ToLower()].ToString();

            var certificate_Path = genericUtils.GetDataPath("TestResources\\API_Certificate");
            var certificate = new X509Certificate2(Path.Combine(certificate_Path, "api-client-ppe.new 5.pfx"), genericUtils.Decrypt(certificateKey, 3));

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(certificate);

            var httpClient = new HttpClient(handler);
            var options = new RestClientOptions(certificateURL)
            {
                ConfigureMessageHandler = _ => handler
            };

            RestRequest request = new RestRequest(api, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Ocp-Apim-Subscription-Key", genericUtils.Decrypt(headerKey, 3));

            request.AddJsonBody(new
            {
                idNumber = IdNumber
            });

            var client = new RestClient(options);
            RestResponse response = client.Execute(request);

            string content = response.Content;
            int statusCode = (int)response.StatusCode;
            string statusDescription = response.StatusDescription;

            Dictionary<string, object> apiResponse = new Dictionary<string, object>
            {
                {"content", content },
                {"statusCode", statusCode },
                {"statusDescription", statusDescription }
            };

            return apiResponse;
        }

        /// <summary>
        /// Sends unauthorized API request using certificate authentication, reads JSON config, and returns response content, status code, and description.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="source"></param>
        /// <param name="Certificate"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> API_Unauthorized(string IdNumber, string source, string Certificate)
        {
            var path = genericUtils.GetDataPath("TestResources");
            var json = genericUtils.GetJson(Path.Combine(path, "Collections.json"));
            var api = json[Properties.environment]["api"][source.ToLower()].ToString();
            var certificateURL = json[Properties.environment]["api"][Certificate.ToLower()].ToString();
            var certificateKey = json[Properties.environment]["header"][Certificate.ToLower()].ToString();

            var certificate_Path = genericUtils.GetDataPath("TestResources\\API_Certificate");
            var certificate = new X509Certificate2(Path.Combine(certificate_Path, "api-client-ppe.new 5.pfx"), genericUtils.Decrypt(certificateKey, 3));

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(certificate);

            var httpClient = new HttpClient(handler);
            var options = new RestClientOptions(certificateURL)
            {
                ConfigureMessageHandler = _ => handler
            };

            RestRequest request = new RestRequest(api, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Ocp-Apim-Subscription-Key", "3afb5992276a4b13b5cf99518a651b4v");

            request.AddJsonBody(new
            {
                idNumber = IdNumber
            });

            var client = new RestClient(options);
            RestResponse response = client.Execute(request);

            string content = response.Content;
            int statusCode = (int)response.StatusCode;
            string statusDescription = response.StatusDescription;

            Dictionary<string, object> apiResponse = new Dictionary<string, object>
            {
                {"content", content },
                {"statusCode", statusCode },
                {"statusDescription", statusDescription }
            };

            return apiResponse;
        }

        /// <summary>
        /// Sends API request without certificate, using headers from config; returns response content, status code, and status description.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="source"></param>
        /// <param name="Certificate"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> API_Forbidden(string IdNumber, string source, string Certificate)
        {
            var path = genericUtils.GetDataPath("TestResources");
            var json = genericUtils.GetJson(Path.Combine(path, "Collections.json"));
            var api = json[Properties.environment]["api"][source.ToLower()].ToString();
            var headerKey = json[Properties.environment]["header"][source.ToLower()].ToString();
            var certificate_Path = genericUtils.GetDataPath("TestResources\\API_Certificate");

            RestRequest request = new RestRequest(api, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Ocp-Apim-Subscription-Key", genericUtils.Decrypt(headerKey, 3));

            request.AddJsonBody(new
            {
                idNumber = IdNumber
            });

            var client = new RestClient();
            RestResponse response = client.Execute(request);

            string content = response.Content;
            int statusCode = (int)response.StatusCode;
            string statusDescription = response.StatusDescription;

            Dictionary<string, object> apiResponse = new Dictionary<string, object>
            {
                {"content", content },
                {"statusCode", statusCode },
                {"statusDescription", statusDescription }
            };

            return apiResponse;
        }

        /// <summary>
        /// Sends a POST request with user data and certificate to an API, then returns the response content, status code, and description.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="endPoint"></param>
        /// <param name="source"></param>
        /// <param name="Certificate"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> APIM_AutoReg(string IdNumber, string endPoint, string source, string Certificate, InputData user)
        {
            var path = genericUtils.GetDataPath("TestResources");
            var json = genericUtils.GetJson(Path.Combine(path, "Collections.json"));
            var api = json[Properties.environment]["api"][endPoint.ToLower()].ToString();
            var headerKey = json[Properties.environment]["header"][source.ToLower()].ToString();
            var certificateURL = json[Properties.environment]["api"][Certificate.ToLower()].ToString();
            var certificateKey = json[Properties.environment]["header"][Certificate.ToLower()].ToString();

            var certificate_Path = genericUtils.GetDataPath("TestResources\\API_Certificate");
            var certificate = new X509Certificate2(Path.Combine(certificate_Path, "api-client-ppe.new 5.pfx"), genericUtils.Decrypt(certificateKey, 3));

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(certificate);

            var httpClient = new HttpClient(handler);
            var options = new RestClientOptions(certificateURL)
            {
                ConfigureMessageHandler = _ => handler
            };

            RestRequest request = new RestRequest(api, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Ocp-Apim-Subscription-Key", genericUtils.Decrypt(headerKey, 3));

            request.AddJsonBody(new
            {
                FirstName = user.firstname,
                Surname = user.surname,
                email = user.emailid,
                PhoneNumber = user.number,
                IdNumber = IdNumber,
                AcceptTerms = true,
                Source = source
            });

            var client = new RestClient(options);
            RestResponse response = client.Execute(request);

            string content = response.Content;
            int statusCode = (int)response.StatusCode;
            string statusDescription = response.StatusDescription;

            Dictionary<string, object> apiResponse = new Dictionary<string, object>
            {
                {"content", content },
                {"statusCode", statusCode },
                {"statusDescription", statusDescription }
            };

            return apiResponse;
        }

        /// <summary>
        /// Sends unauthorized API request with certificate, constructs headers and body from input, executes POST, and returns status and content.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="endPoint"></param>
        /// <param name="source"></param>
        /// <param name="Certificate"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> APIM_AutoReg_Unauthorized(string IdNumber, string endPoint, string source, string Certificate, InputData user)
        {
            var path = genericUtils.GetDataPath("TestResources");
            var json = genericUtils.GetJson(Path.Combine(path, "Collections.json"));
            var api = json[Properties.environment]["api"][endPoint.ToLower()].ToString();
            var headerKey = json[Properties.environment]["header"][source.ToLower()].ToString();
            var certificateURL = json[Properties.environment]["api"][Certificate.ToLower()].ToString();
            var certificateKey = json[Properties.environment]["header"][Certificate.ToLower()].ToString();

            var certificate_Path = genericUtils.GetDataPath("TestResources\\API_Certificate");
            var certificate = new X509Certificate2(Path.Combine(certificate_Path, "api-client-ppe.new 5.pfx"), genericUtils.Decrypt(certificateKey, 3));

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(certificate);

            var httpClient = new HttpClient(handler);
            var options = new RestClientOptions(certificateURL)
            {
                ConfigureMessageHandler = _ => handler
            };

            RestRequest request = new RestRequest(api, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Ocp-Apim-Subscription-Key", "3afb5992276a4b13b5cf99518a651b4v");

            request.AddJsonBody(new
            {
                FirstName = user.firstname,
                Surname = user.surname,
                email = user.emailid,
                PhoneNumber = user.number,
                IdNumber = IdNumber,
                AcceptTerms = true,
                Source = source
            });

            var client = new RestClient(options);
            RestResponse response = client.Execute(request);

            string content = response.Content;
            int statusCode = (int)response.StatusCode;
            string statusDescription = response.StatusDescription;

            Dictionary<string, object> apiResponse = new Dictionary<string, object>
            {
                {"content", content },
                {"statusCode", statusCode },
                {"statusDescription", statusDescription }
            };

            return apiResponse;
        }

        /// <summary>
        /// Validates API response by checking status code and asserting expected status description for 200, 409, 422, and 401 cases.
        /// </summary>
        /// <param name="APIM_Response"></param>
        [Author("Piyush Sharma")]
        public void ValidateAPIMAutoRegStatus(Dictionary<string,object> APIM_Response)
        {
            if (APIM_Response["statusCode"].ToString() == "200")
            {
                validate.AssertEquals("OK", APIM_Response["statusDescription"].ToString(), "Status code is mismatch", true);
            }
            else if (APIM_Response["statusCode"].ToString() == "409")
            {
                validate.AssertEquals("Conflict", APIM_Response["statusDescription"].ToString(), "Status Description is mismatch", true);
            }
            else if (APIM_Response["statusCode"].ToString() == "422")
            {
                validate.AssertEquals("Unprocessable Entity", APIM_Response["statusDescription"].ToString(), "Status Description is mismatch", true);
            }
            else if (APIM_Response["statusCode"].ToString() == "401")
            {
                validate.AssertEquals("Access Denied", APIM_Response["statusDescription"].ToString(), "Status Description is mismatch", true);
            }
        }

        /// <summary>
        /// Validates API response by matching status code with expected description using assertions to ensure correct error or success messages.
        /// </summary>
        /// <param name="apiResponse"></param>
        [Author("Piyush Sharma")]
        public void ValidateAPIResponseStatus(Dictionary<string, object> apiResponse)
        {
            if (apiResponse["statusCode"].ToString() == "200")
            {
                validate.AssertEquals("OK", apiResponse["statusDescription"].ToString(), "Status Description is mismatch", true);
            }
            else if (apiResponse["statusCode"].ToString() == "400")
            {
                validate.AssertEquals("Bad Request", apiResponse["statusDescription"].ToString(), "Status Description is mismatch", true);
            }
            else if (apiResponse["statusCode"].ToString() == "404")
            {
                validate.AssertEquals("Not Found", apiResponse["statusDescription"].ToString(), "Status Description is mismatch", true);
            }
            else if (apiResponse["statusCode"].ToString() == "401")
            {
                validate.AssertEquals("Access Denied", apiResponse["statusDescription"].ToString(), "Status Description is mismatch", true);
            }
            else if (apiResponse["statusCode"].ToString() == "403")
            {
                validate.AssertEquals("Certificate not provided", apiResponse["statusDescription"].ToString(), "Status Description is mismatch", true);
            }
        }
    }
}
