namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("CC16127_BranchLMSInsert_NonCoachingRegistration")]
    [Parallelizable(ParallelScope.Children)]
    class CC16127_BranchLMSInsert_NonCoachingRegistration : BaseTestFixture
    {
        private const string className = "CC16127_BranchLMSInsert_NonCoachingRegistration";
        public CC16127_BranchLMSInsert_NonCoachingRegistration() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyBranchLMSInsert_NonCoachingRegistration{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void VerifyBranchLMSInsert_NonCoachingRegistration(InputData user)
        {
            /***************************************************************
             * 
             * Test:- Verify Branch LMS Insert for Non Coaching Registration
             * 
             ***************************************************************/

            string IdNumber = user.idNumber;
            string firstName = user.firstname;
            string surName = user.surname;
            string number = user.number;
            string email = user.emailid;

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            ProfilePageSteps profilePageSteps = new ProfilePageSteps();
            ForgotPasswordPageSteps forgotPasswordPageSteps = new ForgotPasswordPageSteps();
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

            Report.ChildLog = Report.ExtentTestGroup("Check and Validate the condition for Non Coaching Registration and fetch Gross Income");
            int grossIncome = loginPageSteps.CheckAndUpdateTablesForLMSInsert_NonCoachingRegistration(IdNumber, user.currentBalance);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Temp Password - " + tempPass + " and validate user navigation to Credit Insight page after entering Gross Income");
            loginPageSteps.LoginWithTempPass(IdNumber, tempPass);
            loginPageSteps.VerifyCreditInsightPageAfterLogin(grossIncome.ToString());
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check LMS Insert for Non-Coaching Registration");
            loginPageSteps.ValidateBranchLMS_NonCoachingRegistration(IdNumber);
            dBCreditCoach.DeleteExternalCommLog(IdNumber, 5);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}