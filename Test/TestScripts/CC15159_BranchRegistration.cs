namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("CC15159_BranchRegistration")]
    [Parallelizable(ParallelScope.Children)]
    class CC15159_BranchRegistration : BaseTestFixture
    {
        private const string className = "CC15159_BranchRegistration";
        public CC15159_BranchRegistration() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"BranchRegistrationJourney{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void BranchRegistrationJourney(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Branch Registration
             * 
             * ************************************************************/

            string IdNumber = user.idNumber;
            string firstName = user.firstname;
            string surName = user.surname;
            string number = user.number;
            string email = user.emailid;
            string salary = user.salary;
            string urlParameters = "websource=springs&utm_source=branches&utm_medium=gp&utm_campaign=RB1A576600&utm_content=SKA4993476";

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            HomePageSteps homePageSteps = new HomePageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            ProfilePageSteps profilePageSteps = new ProfilePageSteps();
            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            ForgotPasswordPageSteps forgotPasswordPageSteps = new ForgotPasswordPageSteps();
            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            DBQueries dBQueries = new DBQueries();
            AppInsights appInsights = new AppInsights();
            BaseStep baseStep = new BaseStep();

            dBCreditCoach.UpdateAndDeleteTable(DBQueries.DeleteUser(IdNumber));

            Report.Log = Report.ExtentTest(className + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details"); Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToBranchURL(Properties.environment, "branch", user);
            registrationPageSteps.EnterIDNumber(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetails_BranchRegistration(firstName, surName, number, email);
            DateTime currentTimeUtc = DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Basic Verification");
            registrationPageSteps.GetBasicVerification(IdNumber, currentTimeUtc).GetAwaiter().GetResult();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Security Questions");
            registrationPageSteps.HandleSecurityQuestions(IdNumber, user.IsSecondSetRequired);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate Branch Login screen after registration and Fetching Temporary password from AppInsight");
            registrationPageSteps.ValidateBranchLoginScreen();
            string query = dBQueries.FetchTempPassword(IdNumber);
            string tempPass = appInsights.FetchTemporaryPassword(IdNumber, query);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Temp Password - " + tempPass + " and validate user navigation to Credit Insight page");
            loginPageSteps.LoginWithTempPass(IdNumber, tempPass);
            loginPageSteps.VerifyCreditInsightPageAfterLogin(salary);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the branch user details in the User and ExternalCommlog table");
            homePageSteps.ValidateDBPostBranchRegistration(IdNumber, urlParameters);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Logout and Verify the Login screen");
            profilePageSteps.LogOut();
            loginPageSteps.VerifyLoginScreen();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Temp Password and create a New Password");
            loginPageSteps.LoginWithTempPass(IdNumber, tempPass);
            forgotPasswordPageSteps.CreateNewPassword(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with New Password and Validate DaLesLog");
            registrationPageSteps.ValidateBranchLoginScreen();
            loginPageSteps.LoginWithNewPass(IdNumber, Properties.password);
            Assert.That(homePageSteps.ValidateDaLesLog(IdNumber, 15, 0), Is.True);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Navigate to Solution page and check Sanlam Personal's Qualification Decision. Make Qualification Decision 'Approve' if its not Approve");
            solutionPageSteps.CheckSPLQualificationDecision(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify the applied Sanlam Personal Loan and check the IDP API call");
            solutionPageSteps.SPLApplyProcess(number, IdNumber, urlParameters);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify Call Me Back and check Eazyflow API call");
            Assert.That(solutionPageSteps.SPLCallMeBackProcess(IdNumber, 9), Is.True);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify Communication Log on Agent UI");
            baseStep.NavigateToApp("agent", Properties.environment);
            agentUiPageSteps.LoginToAgentUI(IdNumber, user.agent_Email, user.agent_Password);
            agentUiPageSteps.VerifyCommlogForSPL("Digital", "User");
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}