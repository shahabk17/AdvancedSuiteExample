namespace TestScripts.Client
{
    [TestFixture, Category("CC3185_LMSOnSanlamCreditSolutionPlatform")]
    [Parallelizable(ParallelScope.Children)]
    public class CC3185_LMSOnSanlamCreditSolutionPlatform : BaseTestFixture
    {
        Validate validate = new Validate();
        private const string className = "CC3185_LMSOnSanlamCreditSolutionPlatform";
        public CC3185_LMSOnSanlamCreditSolutionPlatform() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.folderName, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"LMSOnSanlamCreditSolutionPlatform{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void LMSOnSanlamCreditSolutionPlatform(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For number of LMS's inserted based on different
             * criteria on the SCS platform. 
             * 
             * ************************************************************/
            string IdNumber = user.idNumber;
            string firstName = user.firstname;
            string surName = user.surname;
            string number = user.number;
            string idPassword = user.agentidpassword;
            string emailId = user.emailid;
            string salary = user.salary;

            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            WealthPageSteps wealthPageSteps = new WealthPageSteps();
            CreditInsightsPageSteps creditInsightsPageSteps = new CreditInsightsPageSteps();
            ProfilePageSteps profilePageSteps = new ProfilePageSteps();
            HomePageSteps homePageSteps = new HomePageSteps();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);


            Report.ChildLog = Report.ExtentTestGroup("Register with Id " + IdNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            registrationPageSteps.EnterIDNumber(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source - Registration: Failed Security Questions");
            registrationPageSteps.EnterCustDetails(firstName, surName, number, Properties.password, emailId);
            registrationPageSteps.EnterOTP(number);
            registrationPageSteps.UserFailedSevenSecurityQuestions();
            try
            {
                DBCreditCoach dBCreditCoach = new DBCreditCoach();
                dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Registration: Failed Security Questions");
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source - Registration: Failed Security Questions is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Credit consolidation");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, emailId, idPassword);
            agentUiPageSteps.ActivateOrDeactivateUserFromAgentUi(true);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            try
            {
                creditInsightsPageSteps.VerifyCreditConsolidationLMS(IdNumber, salary);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Credit consolidation is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source - Registration: Debt Counselling");
            try
            {
                profilePageSteps.VerifyLMSDebtCouncelling(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source - Registration: Debt Counselling is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Home Page");
            try
            {
                homePageSteps.VerifyCallMeBackHomePage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Home Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Wealth Page");
            try
            {
                wealthPageSteps.VerifyCallMeBackWealthPage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Wealth Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Wealth Balance");
            try
            {
                wealthPageSteps.VerifyCallMeBackWealthHomePage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Wealth Balance is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Credit Insights Page");
            try
            {
                creditInsightsPageSteps.verifyCallMeBackCreditInsightsPage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Credit Insights Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Credit Accounts Page");
            CreditAccountsPageSteps creditAccountsPageSteps = new CreditAccountsPageSteps();
            try
            {
                creditAccountsPageSteps.VerifyCallMeBackCreditAccountsPage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Credit Accounts Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Bank Accounts Page");
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            BankAccountsPageSteps bankAccountsPageSteps = new BankAccountsPageSteps();
            try
            {
                bankAccountsPageSteps.VerifyCallMeBackBankAccountsPage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Bank Accounts Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Solutions Page");
            try
            {
                solutionPageSteps.VerifyCallMeBackSolutionPage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Solution Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source -Credit Consolidation Decline");
            try
            {
                solutionPageSteps.VerifyCallMeBackCreditConsolidationTile(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Credit Consolidation Decline is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Budget Page");
            BudgetPageSteps budgetPageSteps = new BudgetPageSteps();
            try
            {
                budgetPageSteps.VerifyCallMeBackBudgetPage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Budget Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Budget Tool");
            try
            {
                bankAccountsPageSteps.VerifyCallMeBackBudgetTool_LinkAccPage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Budget Tool is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Profile Page");
            try
            {
                profilePageSteps.VerifyCallMeBackProfilePage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Profile Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client FAQ Page");
            FAQPageSteps fAQPageSteps = new FAQPageSteps();
            try
            {
                fAQPageSteps.VerifyCallMeBackFAQPage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client FAQ Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Client Settings Page");
            SettingsPageSteps settingsPageSteps = new SettingsPageSteps();
            try
            {
                settingsPageSteps.VerifyCallMeBackSettingsPage(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Client Settings Page is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- SPL decline");
            try
            {
                solutionPageSteps.VerifyCallMeBackSPLTile(IdNumber, "SPL decline");
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- SPL decline is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Campaign source- Personal Loan");
            try
            {
                solutionPageSteps.VerifyCallMeBackSPLTile(IdNumber, "Personal Loan");
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, "Error: " + ex);
                validate.TakeStepFullScreenShot("Campaign source- Personal Loan is not completed", Status.Fail);
            }
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
