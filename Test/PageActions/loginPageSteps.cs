namespace SanlamAutomation
{
    [Author("Shahab Khan")]
    public class LoginPageSteps : BaseStep
    {
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly BaseStep baseStep = new();
        private readonly DBCreditCoach dBCreditCoach = new();

        /// <summary>
        /// Navigates to the application URL and validates landing page
        /// </summary>
        /// <param name="url">Target application URL</param>
        [Author("Shahab Khan")]
        public void LoginT0App(string url)
        {
            Driver.Navigate().GoToUrl(url);
            validate.TakeStepFullScreenShot("Landing Page is Visible", Status.Info);
        }

        /// <summary>
        /// Enters ID number in registration form and initiates registration
        /// </summary>
        /// <param name="IdNumber">South African ID number</param>
        [Author("Shahab Khan")]
        public void EnterIDNumber(string IdNumber)
        {
            var loginPage = new loginPage();
            wait.WaitForElementClickableLongWait(loginPage.idnumber, 60);
            wait.GenericWait(5000);
            SendKeys(loginPage.IDNumber, IdNumber);
            Report.ChildLog.Log(Status.Info, $"ID Number Entered By User is :- {IdNumber}");
            validate.TakeStepFullScreenShot("IDNumber", Status.Pass);
            Click(loginPage.RegisterBtn);
        }

        /// <summary>
        /// Performs user login after validation using ID and password
        /// </summary>
        /// <param name="IDNumber">South African ID number</param>
        /// <param name="Password">User password</param>
        [Author("Shahab Khan")]
        public void LoginUserAfterValidation(string IDNumber, string Password)
        {
            var loginPage = new loginPage();
            wait.WaitForElementVisibilityLongWait(loginPage.loginidnumber, 60);
            wait.GenericWait(5000);

            Assert.That(loginPage.isLoginPageDisplayedAfterRegis);
            validate.TakeStepFullScreenShot("Login Page is displayed after registration", Status.Info);
            SendKeys(loginPage.LoginIdNumber, IDNumber);
            SendKeys(loginPage.LoginIDPassword, Password);
            validate.TakeStepFullScreenShot("Credentials Entered", Status.Info);
            Click(loginPage.LoginBtn);
        }

        /// <summary>
        /// Performs login with ID number and handles deactivated user scenarios
        /// </summary>
        /// <param name="IDNumber">South African ID number</param>
        /// <param name="Password">User password</param>
        [Author("Shahab Khan")]
        public void LoginWithID(string IDNumber, string Password)
        {
            var loginPage = new loginPage();
            var homePageSteps = new HomePageSteps();

            wait.WaitForElementVisibilityLongWait(loginPage.loginicononlandingpage, 60);
            wait.GenericWait(2000);

            MultipleClick(loginPage.loginicononlandingpage, 3);
            wait.WaitForElementVisibilityLongWait(loginPage.loginidnumber, 60);
            wait.GenericWait(5000);

            Assert.That(loginPage.isLoginPageDisplayedAfterRegis);
            validate.TakeStepFullScreenShot("Login Page is displayed after registration", Status.Info);

            SendKeys(loginPage.LoginIdNumber, IDNumber);
            SendKeys(loginPage.LoginIDPassword, Password);
            validate.TakeStepFullScreenShot("Credentials Entered", Status.Info);

            Click(loginPage.LoginBtn);
            wait.WaitTillPageLoad();
            wait.GenericWait(5000);

            HandleLoginOutcome(loginPage, homePageSteps);
        }

        /// <summary>
        /// This method automates logging in with a temporary password by entering credentials, capturing screenshots, clicking login, and waiting for the page to load.
        /// </summary>
        /// <param name="IDNumber"></param>
        /// <param name="Password"></param>
        [Author("Piyush Sharma")]
        public void LoginWithTempPass(string IDNumber, string Password)
        {
            var loginPage = new loginPage();
            var homePageSteps = new HomePageSteps();
            var creditInsightsPage = new CreditInsightsPage();

            wait.WaitForElementVisibilityLongWait(loginPage.loginidnumber, 60);
            wait.GenericWait(5000);

            Assert.That(loginPage.isLoginPageDisplayedAfterRegis);
            validate.TakeStepFullScreenShot("Login Page after branch registration", Status.Info);

            SendKeys(loginPage.LoginIdNumber, IDNumber);
            SendKeys(loginPage.LoginIDPassword, Password);
            validate.TakeStepFullScreenShot("Credentials Entered", Status.Info);

            Click(loginPage.LoginBtn);
            wait.WaitTillPageLoad();
            wait.GenericWait(10000);
        }

        /// <summary>
        /// This method automates logging in with a new password, verifies successful login, checks if the dashboard is displayed, and validates password update in the database.
        /// </summary>
        /// <param name="IDNumber"></param>
        /// <param name="Password"></param>
        [Author("Piyush Sharma")]
        public void LoginWithNewPass(string IDNumber, string Password)
        {
            var loginPage = new loginPage();
            var homePageSteps = new HomePageSteps();
            var creditInsightsPage = new CreditInsightsPage();

            wait.WaitForElementVisibilityLongWait(loginPage.loginidnumber, 60);
            wait.GenericWait(5000);

            Assert.That(loginPage.isLoginPageDisplayedAfterRegis);
            validate.TakeStepFullScreenShot("Login Page after branch registration", Status.Info);

            SendKeys(loginPage.LoginIdNumber, IDNumber);
            SendKeys(loginPage.LoginIDPassword, Password);
            validate.TakeStepFullScreenShot("Credentials Entered", Status.Info);

            Click(loginPage.LoginBtn);
            wait.WaitTillPageLoad();
            wait.GenericWait(5000);

            homePageSteps.IsdashboardPageDispalyed();

            var userDetails = dBCreditCoach.FetchUserDetailsFromUserTable(IDNumber);
            validate.AssertEquals("True", userDetails["IsBranchPwdUpdated"].ToString(), "IsBranch User Password updated is False", true);
        }

        /// <summary>
        /// This method verifies the Credit Insight page after login by waiting for the salary popup, ensuring the page loads, and checking credit score visibility.
        /// </summary>
        /// <param name="salary"></param>
        [Author("Piyuhs Sharma")]
        public void VerifyCreditInsightPageAfterLogin(string salary)
        {
            var homePageSteps = new HomePageSteps();
            var creditInsightsPage = new CreditInsightsPage();

            homePageSteps.WaitTillSalaryPopUpIsDisplayed(salary);
            wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(creditInsightsPage.creditinsights_creditscore, 10);
        }

        /// <summary>
        /// This method verifies the login screen by checking the page title's visibility, validating its text, and confirming the login page is displayed after registration.
        /// </summary>
        [Author("Piyuhs Sharma")]
        public void VerifyLoginScreen()
        {
            var loginPage = new loginPage();

            wait.WaitForElementVisibilityLongWait(loginPage.loginpagetitle, 60);
            validate.AssertEquals("Login to your Credit profile", baseStep.getText.Text(loginPage.LoginPageTitle), "Login page text is mismatch", true);
            wait.GenericWait(2000);
            Assert.That(loginPage.isLoginPageDisplayedAfterRegis);
        }

        /// <summary>
        /// Opens login page and performs complete signin process with validation
        /// </summary>
        /// <param name="idNumber">South African ID number</param>
        /// <param name="Password">User password</param>
        [Author("Shahab Khan")]
        public void OpenLoginPageAndSignin(string idNumber, string Password)
        {
            Report.ChildLog.Log(Status.Info, $"Method >>>>>>>>>>{MethodBase.GetCurrentMethod().Name}<<<<<<<<<<");

            var loginPage = new loginPage();
            var homePageSteps = new HomePageSteps();
            var homePage = new HomePage();

            NavigateToApp("login", Properties.environment);
            wait.WaitTillPageLoad();

            HandleUpdatePopup(homePage);
            PerformLogin(loginPage, idNumber, Password);
            ValidateLoginOutcome(loginPage, homePageSteps, homePage);
        }

        /// <summary>
        /// Signs back into the application after logout
        /// </summary>
        /// <param name="idNumber">South African ID number</param>
        [Author("Shahab Khan")]
        public void SignInBack(string idNumber)
        {
            var profilePageSteps = new ProfilePageSteps();
            var loginPage = new loginPage();

            profilePageSteps.LogOut();
            wait.WaitForElementVisibilityLongWait(loginPage.loginidnumber, 60);
            wait.GenericWait(3000);

            SendKeys(loginPage.LoginIdNumber, idNumber);
            SendKeys(loginPage.LoginIDPassword, Properties.password);
            validate.TakeStepFullScreenShot("Credentials Entered", Status.Info);

            Click(loginPage.LoginBtn);
            wait.WaitTillPageLoad();
            wait.GenericWait(5000);
            wait.WaitTillPageLoad();
        }

        /// <summary>
        /// Fetches external communication log for ID, parses request parameters, and validates the campaign source matches expected string using assertions.
        /// </summary>
        /// <param name="IdNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateBranchLMS_BranchUserNonLogin(string IdNumber)
        {
            var extCommLogInfo = FetchExternalCommLogInfo(IdNumber, 5, 1, 360);
            string requestParam = extCommLogInfo["RequestParam"].ToString();
            JObject json = JObject.Parse(requestParam);
            validate.AssertEquals("Registration None Zero Take-home salary", json["campaign_source"].ToString(), "Campaign Source Mismatch", true);
        }

        /// <summary>
        /// Checks if total balance is below threshold, updates it if needed, calculates gross income from instalments, and returns the result.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="currentBalance"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public int CheckAndUpdateTablesForLMSInsert_DebtCounsellingRegistration(string IdNumber, string currentBalance)
        {
            var creditHealthInfo = dBCreditCoach.CreditHealthInfoTable(IdNumber);
            double totalCurrentBalance = double.Parse(creditHealthInfo["TotalCurrentBalance"].ToString());

            if (totalCurrentBalance < 15000)
            {
                dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, currentBalance);
            }

            double totalMonthlyInstalments = double.Parse(creditHealthInfo["TotalMonthlyInstalments"].ToString());
            int grossIncome = (int)Math.Round(totalMonthlyInstalments * 100 / 36);
            return grossIncome;
        }

        /// <summary>
        /// Fetches external log info, parses request JSON, and asserts that campaign source equals "Registration: Debt Counselling" for given ID number.
        /// </summary>
        /// <param name="IdNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateBranchLMS_DebtCounsellingRegistration(string IdNumber)
        {
            var extCommLogInfo = FetchExternalCommLogInfo(IdNumber, 5, 1);
            string requestParam = extCommLogInfo["RequestParam"].ToString();
            JObject json = JObject.Parse(requestParam);
            validate.AssertEquals("Registration: Debt Counselling", json["campaign_source"].ToString(), "Campaign Source Mismatch", true);
        }

        /// <summary>
        /// Checks credit balance, updates if below threshold, calculates and returns gross income based on total monthly instalments from database.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="currentBalance"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public int CheckAndUpdateTablesForLMSInsert_NonCoachingRegistration(string IdNumber, string currentBalance)
        {
            var creditHealthInfo = dBCreditCoach.CreditHealthInfoTable(IdNumber);
            double totalCurrentBalance = double.Parse(creditHealthInfo["TotalCurrentBalance"].ToString());

            if (totalCurrentBalance < 60000)
            {
                dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, currentBalance);
            }

            double totalMonthlyInstalments = double.Parse(creditHealthInfo["TotalMonthlyInstalments"].ToString());
            int grossIncome = (int)Math.Round(totalMonthlyInstalments * 100 / 34);
            return grossIncome;
        }

        /// <summary>
        /// Fetches external log data, parses request parameters, and validates that campaign source is "Registration None Coaching" for given ID number.
        /// </summary>
        /// <param name="IdNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateBranchLMS_NonCoachingRegistration(string IdNumber)
        {
            var extCommLogInfo = FetchExternalCommLogInfo(IdNumber, 5, 1);
            string requestParam = extCommLogInfo["RequestParam"].ToString();
            JObject json = JObject.Parse(requestParam);
            validate.AssertEquals("Registration None Coaching", json["campaign_source"].ToString(), "Campaign Source Mismatch", true);
        }

        /// <summary>
        /// Fetches SPL qualification decision from database and asserts that the decision is "Approve" for the provided ID number.
        /// </summary>
        /// <param name="IdNumber"></param>
        [Author("Piyush Sharma")]
        public void CheckAndUpdateTablesForLMSInsert_PersonalLoan(string IdNumber)
        {
            var SPLQualificationInfo = dBCreditCoach.FetchSPLQualificationDecision(IdNumber);
            validate.AssertEquals("Approve", SPLQualificationInfo["Decision"].ToString(), "Decision is mismatch", true);
        }

        /// <summary>
        /// Fetches external communication log, parses request JSON, and validates campaign source equals "Registration: Personal Loan" for the given ID number.
        /// </summary>
        /// <param name="IdNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateBranchLMS_PersonalLoan(string IdNumber)
        {
            var extCommLogInfo = FetchExternalCommLogInfo(IdNumber, 5, 1, 300);
            string requestParam = extCommLogInfo["RequestParam"].ToString();
            JObject json = JObject.Parse(requestParam);
            validate.AssertEquals("Registration: Personal Loan", json["campaign_source"].ToString(), "Campaign Source Mismatch", true);
        }

        public void NavigateToApp(string webSource, string environment)
        {
            string json = AzureContainers.LoadEmbeddedResource("Url.json");
            var url = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json)[environment][webSource];
            wait.GenericWait(2000);
            base.Driver.Navigate().GoToUrl(url);
            TakeStepFullScreenShot("Landing Page is Visible");
        }

        #region Private Helper Methods

        private Dictionary<string, object> FetchExternalCommLogInfo(string IdNumber, int externalCommLogTypeId, int index, int timeoutInSeconds = 180)
        {
            DateTime timeout = DateTime.UtcNow.AddSeconds(timeoutInSeconds);

            while (DateTime.UtcNow < timeout)
            {
                var externalCommLog = dBCreditCoach.FetchExternalCommLogInfo(IdNumber, externalCommLogTypeId, index);
                if (externalCommLog.Count >= 1)
                    return externalCommLog;

                baseStep.wait.GenericWait(5000);
            }
            return null;
        }

        private void HandleLoginOutcome(loginPage loginPage, HomePageSteps homePageSteps)
        {
            try
            {
                if (loginPage.InvalidErrorMsg.Displayed)
                {
                    Report.ChildLog.Log(Status.Info, "User is Deactivated");
                    validate.TakeStepFullScreenShot("User is Deactivated", Status.Info);
                }
            }
            catch
            {
                homePageSteps.IsdashboardPageDispalyed();
            }
        }

        private void HandleUpdatePopup(HomePage homePage)
        {
            if (validate.IsElementClickable(homePage.newupdatepopup_laterbtn, 5))
            {
                Click(homePage.NewUpdatePopUp_LaterBtn);
            }
            validate.TakeStepFullScreenShot("Login Page is Visible", Status.Info);
        }

        private void PerformLogin(loginPage loginPage, string idNumber, string Password)
        {
            wait.WaitForElementVisibilityLongWait(loginPage.loginidnumber, 60);
            wait.GenericWait(3000);

            SendKeys(loginPage.LoginIdNumber, idNumber);
            SendKeys(loginPage.LoginIDPassword, Password);
            validate.TakeStepFullScreenShot("Credentials Entered", Status.Info);

            Click(loginPage.LoginBtn);
            wait.WaitTillPageLoad();
            wait.GenericWait(5000);
            wait.WaitTillPageLoad();
        }

        private void ValidateLoginOutcome(loginPage loginPage, HomePageSteps homePageSteps, HomePage homePage)
        {
            if (validate.IsElementDisplayed(loginPage.loginbtn))
            {
                wait.WaitForElementVisibilityLongWait(loginPage.lockedaccountmsg, 60);
                Assert.That(validate.IsElementDisplayed(loginPage.lockedaccountmsg));
                validate.TakeStepFullScreenShot("Error is displayed and user is deactivated succesfully", Status.Pass);
            }
            else
            {
                homePageSteps.IsdashboardPageDispalyed();
            }

            if (validate.IsElementClickable(homePage.userwelcometexthomepage))
            {
                homePageSteps.IsUserWelcomeTextHomePage();
                wait.WaitTillPageLoad();
                homePageSteps.WaitTillSalaryPopUpIsDisplayed("4000");
                wait.WaitTillPageLoad();
                homePageSteps.HandlePreQualifyLoanPopup();
            }
        }

        #endregion
    }
}