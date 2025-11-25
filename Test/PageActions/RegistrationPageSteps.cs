namespace SanlamAutomation
{
    /// <summary>
    /// Handles all registration page related test actions and validations
    /// </summary>
    public class RegistrationPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly DBCreditCoach dbCreditCoach = new();
        private readonly AzureContainers azureContainers = new();
        private readonly AzureTables azureTables = new();
        private readonly RegistrationPage registrationPage = new();
        private readonly loginPage loginPage = new();
        private readonly CreditAccountsPage creditAccountsPage = new();
        private readonly AgentUiPage agentUiPage = new();
        private readonly HomePage homePage = new();
        private readonly SolutionPage solutionPage = new();

        /// <summary>
        /// Navigates to the application based on environment and web source
        /// </summary>
        [Author("Shahab Khan")]
        public void NavigateToApp(string environment, string websource)
        {
            Report.ChildLog.Log(Status.Info, "Env is :- " + environment);
            string path = genericUtils.GetDataPath("TestResources");
            JObject json = genericUtils.GetJson(path + "\\Url.json");
            string url = json[environment][websource].ToString();
            baseStep.wait.GenericWait(2000);
            NavigateToUrl(url);
            validate.TakeStepFullScreenShot("Landing Page is Visible", Status.Info);
        }

        /// <summary>
        /// Navigates to the specified URL using the web driver and waits until the page has fully loaded.
        /// </summary>
        /// <param name="url"></param>
        [Author("Piyush Sharma")]
        public void NavigateToURL(string url)
        {
            Driver.Navigate().GoToUrl(url);
            baseStep.wait.WaitTillPageLoad();
        }

        /// <summary>
        /// This method navigates to a branch-specific URL based on the environment and web source, then captures a screenshot to confirm the landing page visibility.
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="websource"></param>
        /// <param name="user"></param>
        [Author("Piyush Sharma")]
        public void NavigateToBranchURL(string environment, string websource, InputData user)
        {
            string urlParameters = "websource=springs&utm_source=branches&utm_medium=gp&utm_campaign=RB1A576600&utm_content=SKA4993476";

            Report.ChildLog.Log(Status.Info, "Env is :- " + environment);
            string path = genericUtils.GetDataPath("TestResources");
            JObject json = genericUtils.GetJson(path + "\\Url.json");
            string url = json[environment][websource].ToString();
            baseStep.wait.GenericWait(2000);
            string branchURL = url + urlParameters;
            NavigateToUrl(branchURL);
            validate.TakeStepFullScreenShot("Landing Page is Visible", Status.Info);
        }

        /// <summary>
        /// Navigates to the application for auto registration process
        /// </summary>
        [Author("Shahab Khan")]
        public void NavigateToAppForAutoReg(string IdNumber, string websource, bool registerWithResponseUrl)
        {
            string url = dbCreditCoach.GetResponseUrl(IdNumber);
            if (registerWithResponseUrl)
            {
                ProcessResponseUrlRegistration(url);
            }
            else
            {
                ProcessNormalRegistration(url, websource);
            }
        }

        /// <summary>
        /// Enters and validates ID number during registration
        /// </summary>
        [Author("Shahab Khan")]
        public void EnterIDNumber(string IdNumber)
        {
            baseStep.wait.WaitForElementClickableLongWait(registrationPage.idnumber, 60);
            baseStep.wait.GenericWait(5000);
            baseStep.SendKeys(registrationPage.IDNumber, IdNumber);
            Report.ChildLog.Log(Status.Info, "ID Number Entered By User is :- " + IdNumber);
            validate.TakeStepFullScreenShot("IDNumber", Status.Pass);

            ValidateIDNumberSubmission(IdNumber);
        }

        /// <summary>
        /// This method enters a given ID number into the registration form and submits it, capturing a screenshot afterward.
        /// </summary>
        /// <param name="IdNumber"></param>
        [Author("Piyush Sharma")]
        public void UJC_EnterIDNumber(string IdNumber)
        {
            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.ujc_registrationform_idnumber, 60);
            baseStep.wait.GenericWait(5000);
            baseStep.SendKeys(agentUiPage.UJC_RegistrationForm_IdNumber, IdNumber);
            baseStep.Click(agentUiPage.UJC_RegistrationForm_SubmitButton);
            Report.ChildLog.Log(Status.Info, "ID Number Entered By User is :- " + IdNumber);
            validate.TakeStepFullScreenShot("IDNumber", Status.Pass);
            baseStep.wait.WaitTillPageLoad();
        }

        /// <summary>
        /// Enters customer details during registration
        /// </summary>
        [Author("Shahab Khan")]
        public void EnterCustDetails(string firstName, string surname, string pnumber, string password, string email)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.firstname, 60);
            Assert.That(registrationPage.isPageDisplayed());

            EnterBasicDetails(firstName, surname, pnumber, email);
            EnterLoginCredentials(password);
            SubmitRegistration();
        }

        /// <summary>
        /// Enters customer details during registration
        /// </summary>
        [Author("Piyush sharma")]
        public void EnterCustDetails_BranchRegistration(string firstName, string surname, string pnumber, string email)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.firstname, 60);
            Assert.That(registrationPage.isPageDisplayed());

            EnterBasicDetails(firstName, surname, pnumber, email);
            SubmitRegistration();
        }

        /// <summary>
        /// Enters customer details for auto registration process
        /// </summary>
        [Author("Shahab Khan")]
        public void EnterCustDetailsForAutoReg(string idnumber, string password, string websource, string source, bool registerWithResponseUrl, string email)
        {
            if (registerWithResponseUrl && source.ToLower() == "spl-ivr")
            {
                HandleSplIvrRegistration(idnumber, password, email);
            }
            else if (registerWithResponseUrl)
            {
                HandleResponseUrlRegistration(password);
            }
            else
            {
                HandleWebSourceRegistration(idnumber, password, websource, source, email);
            }
        }

        /// <summary>
        /// Handles OTP verification during registration
        /// </summary>
        [Author("Shahab Khan")]
        public void EnterOTP(string phoneNumber)
        {
            InitiateOTP();
            ValidateAndSubmitOTP(phoneNumber);
        }

        /// <summary>
        /// Handles security questions during registration
        /// </summary>
        [Author("Shahab Khan")]
        public void HandleSecurityQuestions(string IdNumber, bool isSecondSetRequired)
        {
            try
            {
                SwitchToSecurityFrame();
                ProcessSecurityQuestions(IdNumber, isSecondSetRequired);
            }
            catch
            {
                Report.ChildLog.Log(Status.Info, "No security questions, user entered correct details");
            }
        }

        /// <summary>
        /// Handles security questions for SPL journey
        /// </summary>
        [Author("Shahab Khan")]
        public void HandleSecurityQuestionsforSPLJourney(string idNumber, string websource)
        {
            try
            {
                ProcessSPLSecurityQuestions(idNumber, websource);
            }
            catch
            {
                HandleSPLFallback(websource);
            }
        }

        /// <summary>
        /// Gets basic verification from Azure storage
        /// </summary>
        [Author("Shahab Khan")]
        public async Task GetBasicVerification(string idNumber, DateTime currentTimeUtc)
        {
            AzureTables storageBrowser = new AzureTables();
            await ValidateStorageEntries(storageBrowser, idNumber, currentTimeUtc);
        }

        /// <summary>
        /// Performs post-registration validation
        /// </summary>
        [Author("Shahab Khan")]
        public void GetPostValidationAfterReg(string idnumber)
        {
            baseStep.wait.GenericWait(2000);
            validate.TakeStepFullScreenShot("Registration Successful", Status.Info);
            dbCreditCoach.GetPostValidationAfterReg(idnumber);
            ValidateDaLesLoginExternalCommLog(idnumber, 15, 1);
        }

        /// <summary>
        /// Handles login after validation
        /// </summary>
        [Author("Shahab Khan")]
        public string LoginUserAfterValidation(string IDNumber, string Password, string websource, string salary)
        {
            try
            {
                ProcessInitialLogin(IDNumber, Password);
            }
            catch (Exception e)
            {
                HandleLoginRetry(IDNumber, Password, websource);
            }

            return ValidateLoginSuccess(salary);
        }

        /// <summary>
        /// This method logs in a user after registration and validates the landing page based on campaign presence.
        /// </summary>
        /// <param name="IDNumber"></param>
        /// <param name="Password"></param>
        /// <param name="websource"></param>
        /// <param name="registrationDataset"></param>
        /// <param name="campaignPageURL"></param>
        [Author("Piyush sharma")]
        public void UJC_LoginUserAfterRegistration(string IDNumber, string Password, string websource, Dictionary<string, object> registrationDataset, bool campaignPageURL)
        {
            try
            {
                ProcessInitialLogin(IDNumber, Password);
            }
            catch (Exception e)
            {
                HandleLoginRetry(IDNumber, Password, websource);
            }

            if (campaignPageURL)
            {
                if (Driver.Url.Contains("/portal/account"))
                {
                    baseStep.wait.WaitForElementVisibilityLongWait(creditAccountsPage.accountsummaryheading, 60);
                    validate.AssertEquals(true, Driver.Url.Contains(registrationDataset["Landing_Page_URL"].ToString()), "Landing Page URL is Mismatch", true);
                }
                else if (Driver.Url.Contains("/portal/offers"))
                {
                    baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.solutionsheading, 60);
                    validate.AssertEquals(true, Driver.Url.Contains(registrationDataset["Product_Page_URL"].ToString()), "Product Page URL is Mismatch", true);
                }
            }
            else
            {
                baseStep.wait.WaitForElementVisibilityLongWait(homePage.homepage, 60);
                validate.AssertEquals(true, Driver.Url.Contains("/portal/home"), "Landing Page URL is Mismatch", true);
            }
        }

        /// <summary>
        /// Formats system date into UTC
        /// </summary>
        [Author("Shahab Khan")]
        public DateTime FormatTheSystemDateIntoUTC(double v)
        {
            string s = DateTime.Now.ToUniversalTime().AddHours(v).ToString("yyyy-MM-dd HH:mm:ss.fffffff");
            return DateTime.ParseExact(s, "yyyy-MM-dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Handles failed security questions scenario
        /// </summary>
        [Author("Shahab Khan")]
        public void UserFailedSevenSecurityQuestions()
        {
            try
            {
                ProcessFailedSecurityQuestions();
            }
            catch
            {
                HandleSecondSecurityQuestionSet();
            }
        }

        /// <summary>
        /// This method validates the branch login screen by checking the registration success message (if present), verifying URL redirection, and confirming the login page title.
        /// </summary>
        [Author("Piyush Sharma")]
        public void ValidateBranchLoginScreen()
        {
            baseStep.wait.WaitTillPageLoad();
            try
            {
                baseStep.wait.WaitForElementVisibilityLongWait(loginPage.registrationsuccessmsg, 30);
                validate.AssertEquals("User Registered Successfully", baseStep.getText.Text(loginPage.RegistrationSuccessMsg), "Registration Success message is not displayed", false);
            }
            catch
            {
                Report.ChildLog.Log(Status.Info, "Registration Success message will not be displayed");
            }
            validate.AssertEquals(true, Driver.Url.Contains("login"), "User didn't redicted to Login page", true);
            baseStep.wait.GenericWait(5000);
            validate.AssertEquals("Login to your credit profile using the password sent via SMS", baseStep.getText.Text(loginPage.LoginPageTitle), "Login page title is mismatch", true);
        }

        /// <summary>
        /// Navigates to a given URL and handles the registration process using the provided password.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="password"></param>
        [Author("Piyush Sharma")]
        public void NavigateToAutoRegURL(string url, string password)
        {
            ProcessResponseUrlRegistration(url);
            HandleResponseUrlRegistration(password);
        }

        #region Private Helper Methods

        private void ValidateDaLesLoginExternalCommLog(string idNumber, int logTypeId, int platformId)
        {
            var daLesLoginLog = dbCreditCoach.FetchExternalCommLogInfo(idNumber, 15, 0);
            string idNumber_SqlExt = daLesLoginLog["IdNumber"].ToString();
            string date_SqlExt = daLesLoginLog["RequestTime"].ToString();
            DateTime createdDate = DateTime.Parse(date_SqlExt);

            validate.AssertEquals(idNumber_SqlExt, idNumber, "IdNumber is Mismatch", true);
            validate.AssertEquals(DateTime.Now.ToString("dd-MM-yyyy"), createdDate.ToString("dd-MM-yyyy"), "Created date is not matching", true);

            var ExternalCommLogList = azureTables.GetExternalCommLogInfo(idNumber, logTypeId, platformId);
            string idNumber_AzureExt = ExternalCommLogList.IdNumber;
            DateTime requestTime = ExternalCommLogList.RequestTime;

            validate.AssertEquals(idNumber_AzureExt, idNumber, "IdNumber is Mismatch", true);
            validate.AssertEquals(DateTime.Now.ToString("dd-MM-yyyy"), requestTime.ToString("dd-MM-yyyy"), "Created date is not matching", true);
        }

        private void ProcessResponseUrlRegistration(string url)
        {
            baseStep.wait.GenericWait(2000);
            Driver.Navigate().GoToUrl(url);
            validate.TakeStepFullScreenShot("Landing Page is Visible", Status.Info);
        }

        private void ProcessNormalRegistration(string url, string websource)
        {
            string environment = Properties.environment;
            string path = genericUtils.GetDataPath("TestResources");
            JObject json = genericUtils.GetJson(path + "\\Url.json");

            Driver.Navigate().GoToUrl(url);
            validate.TakeStepFullScreenShot("Response URL is used first time", Status.Info);
            baseStep.wait.GenericWait(2000);

            Driver.Navigate().GoToUrl(url);
            validate.TakeStepFullScreenShot("Response URL is used second time", Status.Info);

            string url2 = json[environment][websource].ToString();
            baseStep.wait.GenericWait(2000);
            Driver.Navigate().GoToUrl(url2);
            validate.TakeStepFullScreenShot("Landing Page is Visible", Status.Info);
        }

        private void ValidateIDNumberSubmission(string IdNumber)
        {
            baseStep.Click(registrationPage.NextBtn);
            baseStep.wait.WaitTillPageLoad();

            if (registrationPage.AlreadyExistMessage.Displayed)
            {
                validate.TakeStepFullScreenShot("Error Msg", Status.Pass);
                string AlreadyExistMessage = baseStep.getText.Text(registrationPage.AlreadyExistMessage);
                Assert.That(registrationPage.AlreadyExistMessage.Displayed);
                Report.ChildLog.Log(Status.Info, "Error message is visible for the Id is :- " + AlreadyExistMessage);
            }
        }

        private void EnterBasicDetails(string firstName, string surname, string pnumber, string email)
        {
            baseStep.SendKeys(registrationPage.FirstNumber, firstName);
            Report.ChildLog.Log(Status.Info, "FirstNumber Entered By User is :- " + firstName);

            baseStep.SendKeys(registrationPage.SurName, surname);
            Report.ChildLog.Log(Status.Info, "SurName Entered By User is :- " + surname);

            baseStep.SendKeys(registrationPage.CellPhoneNumber, pnumber);
            Report.ChildLog.Log(Status.Info, "CellPhoneNumber Entered By User is :- " + pnumber);

            baseStep.SendKeys(registrationPage.EmailAddress, email);
            Report.ChildLog.Log(Status.Info, "EmailAddress Entered By User is :- " + email);
        }

        private void EnterLoginCredentials(string password)
        {
            baseStep.SendKeys(registrationPage.Password, password);
            Report.ChildLog.Log(Status.Info, "Password Entered By User is :- " + password);

            baseStep.SendKeys(registrationPage.ConfirmPassword, password);
            Report.ChildLog.Log(Status.Info, "ConfirmPassword Entered By User is :- " + password);
        }

        private void SubmitRegistration()
        {
            baseStep.ScrollToElement(registrationPage.RegisterBtn);
            baseStep.wait.GenericWait(5000);
            baseStep.ScrollToElement(registrationPage.RegisterBtn);
            baseStep.Click(registrationPage.Checkbox);

            validate.TakeStepFullScreenShot("Customer Details", Status.Info);
            baseStep.Click(registrationPage.RegisterBtn);
        }

        private void HandleSplIvrRegistration(string idnumber, string password, string email)
        {
            EnterIDNumber(idnumber);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.artAndcslider_sourceurl_spl_ivr, 10);

            baseStep.SendKeys(registrationPage.AutoRegEmail_SourceUrl_SPL_IVR, email);
            Report.ChildLog.Log(Status.Info, "Email Entered By User is :- " + email);

            EnterAutoRegCredentials(password, registrationPage.AutoRegPassword_SourceUrl_SPL_IVR,
                registrationPage.AutoRegConfirmPassword_SourceUrl_SPL_IVR);

            CompleteAutoRegRegistration(registrationPage.AutoRegRegisterBtn_SourceUrl_SPL_IVR,
                registrationPage.AutoRegTandCSlider_SourceUrl_SPL_IVR);
        }

        private void HandleResponseUrlRegistration(string password)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.arpassword, 10);

            EnterAutoRegCredentials(password, registrationPage.AutoRegPassword, registrationPage.AutoRegConfirmPassword);
            validate.TakeStepFullScreenShot("Customer Details", Status.Info);
            baseStep.Click(registrationPage.AutoRegRegisterBtn);
        }

        private void HandleWebSourceRegistration(string idnumber, string password, string websource, string source, string email)
        {
            EnterIDNumber(idnumber);
            baseStep.wait.WaitTillPageLoad();

            switch (websource.ToLower())
            {
                case "spl":
                    HandleSplWebSource(password, source, email);
                    break;
                case "hl":
                    HandleHlWebSource(password, source, email);
                    break;
                default:
                    HandleDefaultWebSource(password, source, email);
                    break;
            }
        }

        private void InitiateOTP()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.sendotpbtn, 60);
            validate.TakeStepFullScreenShot("OTP Popup is Visible", Status.Info);
            baseStep.Click(registrationPage.SendOtpBtn);
        }

        private void ValidateAndSubmitOTP(string phoneNumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(registrationPage.submitotp, 60);

            int[] pins;
            do
            {
                baseStep.wait.GenericWait(3000);
                var sortedEntities = new OTPStorageAccount().GetOtpDataFromPhoneNumber(phoneNumber);
                pins = sortedEntities.Select(entity => entity.Pin).ToArray();
            } while (pins.Length < 2);

            EnterOTPDigits(pins[1].ToString());
        }

        private void EnterOTPDigits(string otp)
        {
            for (int i = 0; i < otp.Length; i++)
            {
                string input = otp[i].ToString();
                baseStep.SendKeys(registrationPage.EnterOTP(i + 1), input);
            }

            validate.TakeStepFullScreenShot("OTP Entered Successfully", Status.Info);
            baseStep.Click(registrationPage.SubmitOTP);
            baseStep.wait.WaitTillPageLoad();
        }

        private void ProcessSecurityQuestions(string IdNumber, bool isSecondSetRequired)
        {
            if (isSecondSetRequired)
            {
                UserFailedAtSecurityQuestions(IdNumber);
                SelectSecurityQuestion(IdNumber);
            }
            else
            {
                SelectSecurityQuestion(IdNumber);
            }
        }

        private void ProcessInitialLogin(string IDNumber, string Password)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(loginPage.loginidnumber, 60);

            Assert.That(loginPage.isLoginPageDisplayedAfterRegis);
            validate.TakeStepFullScreenShot("Login Page is displayed after registration", Status.Info);

            baseStep.SendKeys(loginPage.LoginIdNumber, IDNumber);
            baseStep.SendKeys(loginPage.LoginIDPassword, Password);
            validate.TakeStepFullScreenShot("Credentials Entered", Status.Info);
            baseStep.Click(loginPage.LoginBtn);
            baseStep.wait.WaitTillPageLoad();
        }

        private void HandleLoginRetry(string IDNumber, string Password, string websource)
        {
            Driver?.Quit();
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            NavigateToApp(Properties.environment, websource);
            baseStep.Click(loginPage.LoginIconOnLandingPage);
            ProcessInitialLogin(IDNumber, Password);
        }

        private string ValidateLoginSuccess(string salary)
        {
            var homePageSteps = new HomePageSteps();
            homePageSteps.IsUserWelcomeTextHomePage();
            baseStep.wait.WaitTillPageLoad();
            homePageSteps.WaitTillSalaryPopUpIsDisplayed(salary);
            baseStep.wait.WaitTillPageLoad();
            homePageSteps.IsdashboardPageDispalyed();
            homePageSteps.HandlePreQualifyLoanPopup();
            return "pass";
        }

        private void ProcessFailedSecurityQuestions()
        {
            baseStep.wait.GenericWait(3000);
            baseStep.wait.WaitForElementExistsLongWait(registrationPage.securityquestioniframe, 20);
            Driver.SwitchTo().Frame(registrationPage.SecurityQuestionIframe);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.securityquestion, 10);
            validate.TakeStepFullScreenShot("Security Question are visible", Status.Info);
            dbCreditCoach.SelectSevenWrongQuestionClick();
            ValidateFailedSecurityQuestions();
        }

        private void ValidateFailedSecurityQuestions()
        {
            baseStep.wait.GenericWait(2000);
            validate.TakeStepFullScreenShot("7 out of 7 Questions are Selected", Status.Info);
            baseStep.wait.WaitTillPageLoad();
            if (validate.IsElementDisplayed(registrationPage.securityquestionfailedmsg))
            {
                string SecurityQuestionFailedMsg = baseStep.getText.Text(registrationPage.SecurityQuestionFailedMsg);
                Report.ChildLog.Log(Status.Info, "Error Message is visible with text is :- " + SecurityQuestionFailedMsg);
            }
            Driver.SwitchTo().DefaultContent();
        }

        private void HandleSplWebSource(string password, string source, string email)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.artAndcslider_sourceurl_spl_ivr, 10);
            if (source.ToLower() == "spl-ivr")
            {
                baseStep.SendKeys(registrationPage.AutoRegEmail_SourceUrl_SPL_IVR, email);
                Report.ChildLog.Log(Status.Info, "Email Entered By User is :- " + email);
            }
            EnterAutoRegCredentials(password, registrationPage.AutoRegPassword_SourceUrl_SPL_IVR, registrationPage.AutoRegConfirmPassword_SourceUrl_SPL_IVR);
            CompleteAutoRegRegistration(registrationPage.AutoRegRegisterBtn_SourceUrl_SPL_IVR, registrationPage.AutoRegTandCSlider_SourceUrl_SPL_IVR);
        }

        private void HandleHlWebSource(string password, string source, string email)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.artAndcslider_sourceurl, 10);
            if (source.ToLower() == "spl-ivr")
            {
                baseStep.SendKeys(registrationPage.AutoRegEmail_SourceUrl_HL, email);
                Report.ChildLog.Log(Status.Info, "Email Entered By User is :- " + email);
            }
            EnterAutoRegCredentials(password, registrationPage.AutoRegPassword_SourceUrl_HL, registrationPage.AutoRegConfirmPassword_SourceUrl_HL);
            CompleteAutoRegRegistration(registrationPage.AutoRegRegisterBtn_SourceUrl_HL, registrationPage.AutoRegTandCSlider_SourceUrl);
        }

        private void HandleDefaultWebSource(string password, string source, string email)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.artAndcslider_sourceurl, 10);
            if (source.ToLower() == "spl-ivr")
            {
                baseStep.SendKeys(registrationPage.AutoRegEmail_SourceUrl, email);
                Report.ChildLog.Log(Status.Info, "Email Entered By User is :- " + email);
            }
            EnterAutoRegCredentials(password, registrationPage.AutoRegPassword_SourceUrl, registrationPage.AutoRegConfirmPassword_SourceUrl);
            CompleteAutoRegRegistration(registrationPage.AutoRegRegisterBtn_SourceUrl, registrationPage.AutoRegTandCSlider_SourceUrl);
        }

        private void EnterAutoRegCredentials(string password, IWebElement passwordField, IWebElement confirmPasswordField)
        {
            baseStep.SendKeys(passwordField, password);
            Report.ChildLog.Log(Status.Info, "Password Entered By User is :- " + password);
            baseStep.SendKeys(confirmPasswordField, password);
            Report.ChildLog.Log(Status.Info, "ConfirmPassword Entered By User is :- " + password);
        }

        private void CompleteAutoRegRegistration(IWebElement registerButton, IWebElement tandCSlider)
        {
            baseStep.wait.GenericWait(5000);
            baseStep.ScrollToElement(registerButton);
            baseStep.Click(tandCSlider);
            validate.TakeStepFullScreenShot("Customer Details", Status.Info);
            baseStep.Click(registerButton);
        }

        private void SwitchToSecurityFrame()
        {
            baseStep.wait.GenericWait(3000);
            baseStep.wait.WaitForElementExistsLongWait(registrationPage.securityquestioniframe, 10);
            Driver.SwitchTo().Frame(registrationPage.SecurityQuestionIframe);
            baseStep.wait.WaitTillPageLoad();
        }

        private void ProcessSPLSecurityQuestions(string websource, string idNumber)
        {
            baseStep.wait.GenericWait(3000);
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.securityquestion, 10);
            validate.TakeStepFullScreenShot("Security Question are visible", Status.Info);
            dbCreditCoach.GetQuestionClick(new DBQueries().QuestionQuery(idNumber));
        }

        private void HandleSPLFallback(string websource)
        {
            NavigateToApp(Properties.environment, websource);
            baseStep.wait.WaitForElementVisibilityLongWait(loginPage.loginicononlandingpage, 60);
            baseStep.wait.GenericWait(2000);
            baseStep.Click(loginPage.LoginIconOnLandingPage);
            baseStep.wait.WaitForElementVisibilityLongWait(loginPage.loginidnumber, 60);
            baseStep.wait.GenericWait(2000);
            validate.TakeStepFullScreenShot("No Security Questions for this User", Status.Info);
        }

        private async Task ValidateStorageEntries(AzureTables storageBrowser, string idNumber, DateTime currentTimeUtc)
        {
            baseStep.wait.GenericWait(2000);
            baseStep.wait.WaitTillPageLoad();
            var sortedEntities = await storageBrowser.GetExternalCommLogTableEntries(idNumber, currentTimeUtc);
            var getTableEntries = sortedEntities.Where(x => x.LogTypeId == 102).FirstOrDefault();
            Report.ChildLog.Log(Status.Info, "Request present in storage table for ID " + idNumber + " is " + getTableEntries.RequestParam);
            Report.ChildLog.Log(Status.Info, "ResponseData present in storage table for ID " + idNumber + " is " + getTableEntries.ResponseData);

            string scsLogFileUrl = getTableEntries.ResponseData;
            int lastSlashIndex = scsLogFileUrl.LastIndexOf("/");
            int dotJsonIndex = scsLogFileUrl.LastIndexOf(".json");
            string scsLogFile = scsLogFileUrl.Substring(lastSlashIndex + 1, dotJsonIndex - lastSlashIndex - 1);

            var value = storageBrowser.ReadBlobFileData(scsLogFile, "scs-logs", "VccbBasicInfoLog/");
            Report.ChildLog.Log(Status.Info, $"Basic Info are" + value);
        }

        private void HandleSecondSecurityQuestionSet()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.securityquestion, 10);
            validate.TakeStepFullScreenShot("Second Security question set is visible", Status.Info);
        }

        private void UserFailedAtSecurityQuestions(string IdNumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.GenericWait(3000);
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.securityquestion, 10);
            validate.TakeStepFullScreenShot("Security Question are visible", Status.Info);
            dbCreditCoach.GetWrongQuestionClick(IdNumber);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.GenericWait(5000);
            baseStep.wait.WaitTillPageLoad();

            if (validate.IsElementDisplayed(registrationPage.securityquestionfailedmsg))
            {
                string SecurityQuestionFailedMsg = baseStep.getText.Text(registrationPage.SecurityQuestionFailedMsg);
                Report.ChildLog.Log(Status.Info, "Error Message is visible with text is :- " + SecurityQuestionFailedMsg);
            }
        }

        private void SelectSecurityQuestionWithoutSpoofing(string IdNumber)
        {
            string query = new DBQueries().QuestionQuery(IdNumber);
            bool getbasicInfoVerifyConfirmed = dbCreditCoach.GetbasicInfoVerifyConfirmed(IdNumber);

            try
            {
                if (!getbasicInfoVerifyConfirmed || registrationPage.IsSecurityQuestionDisplayed())
                {
                    baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.securityquestion, 60);
                    validate.TakeStepFullScreenShot("Security Question are visible", Status.Info);
                    genericUtils.HandleAlert("decline");
                    dbCreditCoach.GetQuestionClick(query);
                    baseStep.wait.GenericWait(5000);
                    By spinner = By.XPath("//ngx-spinner/div");
                    baseStep.wait.WaitForElementInvisibilityLongWait(spinner, 60);
                    validate.TakeStepFullScreenShot("Security questions success message", Status.Info);
                    baseStep.wait.WaitForElementInvisibilityLongWait(registrationPage.aftersecurityquestionsuccessmsg, 60);
                    Driver.SwitchTo().DefaultContent();
                }
                else
                {
                    baseStep.wait.GenericWait(5000);
                    baseStep.wait.WaitTillPageLoad();
                    baseStep.wait.WaitForElementVisibility(loginPage.loginidnumber);
                    validate.TakeStepFullScreenShot("No Security Questions for this User", Status.Info);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SelectSecurityQuestion(string IdNumber)
        {
            string visitId = dbCreditCoach.GetVisitId(IdNumber);
            var questions = azureContainers.SpoofedQuestions_JsonArray(visitId);
            baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.securityquestion, 60);

            for (int i = 1; i < 6; i++)
            {
                string displayedQuestionText = registrationPage.SecurityQuestionText();
                var matchingQuestion = questions.FirstOrDefault(q => q["Question"].ToString() == displayedQuestionText);

                if (matchingQuestion != null)
                {
                    List<int> answerIds = matchingQuestion["SecurityAnswerIds"].ToObject<List<int>>();
                    bool isAnswerSelect = false;
                    int questionCount = 0;

                    foreach (var answerId in answerIds)
                    {
                        do
                        {
                            baseStep.Click(registrationPage.OptionSelect(answerId.ToString()));
                            isAnswerSelect = registrationPage.isAnswerSelect(answerId.ToString());
                            string numberOfQuestion = baseStep.getText.Text(registrationPage.SecurityQuestionCountText);
                            char desiredCharacter = numberOfQuestion[9];
                            questionCount = int.Parse(desiredCharacter.ToString());
                        } while (i == questionCount && !isAnswerSelect);
                        break;
                    }
                }
            }

            validate.TakeStepFullScreenShot("Second Set Security Questions for this User are selected", Status.Info);

            do
            {
                baseStep.wait.WaitForElementVisibilityLongWait(registrationPage.aftersecurityquestionsubmitbtn, 15);
                baseStep.ScrollToElement(registrationPage.AfterSecurityQuestionSubmitBtn);
                baseStep.Click(registrationPage.AfterSecurityQuestionSubmitBtn);
                baseStep.wait.WaitTillPageLoad();
            } while (validate.IsElementDisplayed(registrationPage.aftersecurityquestionsubmitbtn));

            baseStep.wait.GenericWait(5000);
            By spinner = By.XPath("//ngx-spinner/div");
            baseStep.wait.WaitForElementInvisibilityLongWait(spinner, 60);
            validate.TakeStepFullScreenShot("Security questions success message", Status.Info);
            baseStep.wait.WaitForElementInvisibilityLongWait(registrationPage.aftersecurityquestionsuccessmsg, 60);
            Driver.SwitchTo().DefaultContent();
        }

        private void NavigateToUrl(string url, int retryAttempts = 3)
        {
            int currentAttempt = 0;
            Exception lastException = null;

            while (currentAttempt < retryAttempts)
            {
                try
                {
                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(180));

                    ((IJavaScriptExecutor)Driver).ExecuteScript("window.setTimeout(arguments[arguments.length - 1], 180000);");
                    Driver.Navigate().GoToUrl(url);
                    wait.Until(driver =>
                    {
                        string readyState = ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString();
                        return readyState.Equals("complete", StringComparison.OrdinalIgnoreCase);
                    });

                    return;
                }
                catch (WebDriverException ex) when (ex.Message.Contains("timeout") || ex.Message.Contains("ERR_CONNECTION_TIMED_OUT"))
                {
                    lastException = ex;
                    currentAttempt++;

                    if (currentAttempt < retryAttempts)
                    {
                        Thread.Sleep(5000);
                        Driver.Navigate().Refresh();
                    }
                }
            }

            throw new Exception($"Failed to load URL after {retryAttempts} attempts. Last error: {lastException?.Message}", lastException);
        }
        #endregion
    }
}
