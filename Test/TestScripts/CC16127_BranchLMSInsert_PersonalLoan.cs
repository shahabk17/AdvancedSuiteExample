namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("CC16127_BranchLMSInsert_PersonalLoan")]
    [Parallelizable(ParallelScope.Children)]
    class CC16127_BranchLMSInsert_PersonalLoan : BaseTestFixture
    {
        private const string className = "CC16127_BranchLMSInsert_PersonalLoan";

        public CC16127_BranchLMSInsert_PersonalLoan() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyBranchLMSInsert_PersonalLoan{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void VerifyBranchLMSInsert_PersonalLoan(InputData user)
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
            string Content_DaLesLogForBranch_Approve = "{\"LeadResponses\":[{\"MessageId\":\"c03145d1-72e5-473f-aa6b-3e2255c2a082\",\"Decision\":\"Approve\",\"BrandName\":\"Sanlam\",\"DecisionReasons\":[\"LES: Applicant does not meet minimum application requirements\",\"LES: Active Application Status Decline Result\"]}],\"MessageId\":\"430bd4e7-d8ab-45bb-80f9-105deb4f1831\"}";
            string Content_DaLesLogForBranch_Decline = "{\"LeadResponses\":[{\"MessageId\":\"c03145d1-72e5-473f-aa6b-3e2255c2a082\",\"Decision\":\"Decline\",\"BrandName\":\"Sanlam\",\"DecisionReasons\":[\"LES: Applicant does not meet minimum application requirements\",\"LES: Active Application Status Decline Result\"]}],\"MessageId\":\"430bd4e7-d8ab-45bb-80f9-105deb4f1831\"}";

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            ProfilePageSteps profilePageSteps = new ProfilePageSteps();
            ForgotPasswordPageSteps forgotPasswordPageSteps = new ForgotPasswordPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            DBQueries dBQueries = new DBQueries();
            AppInsights appInsights = new AppInsights();
            BaseStep baseStep = new BaseStep();
            AzureContainers azureContainers = new AzureContainers();

            dBCreditCoach.UpdateAndDeleteTable(DBQueries.DeleteUser(IdNumber));

            Report.Log = Report.ExtentTest(className + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details"); Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Update Spoofing with Decision to be set as 'Approve'");
            azureContainers.Update_IsSpoofJson("spoofed-container", "IsSpoofed", "DaLesLogForBranch41", true);
            azureContainers.UpdateSpoofData(200, Content_DaLesLogForBranch_Approve, "DaLesLogForBranch41");
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

            Report.ChildLog = Report.ExtentTestGroup("Check and Validate if the Decision is stored as Approve in the ");
            loginPageSteps.CheckAndUpdateTablesForLMSInsert_PersonalLoan(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Temp Password - " + tempPass + " and validate user navigation to Credit Insight page");
            loginPageSteps.LoginWithTempPass(IdNumber, tempPass);
            loginPageSteps.VerifyCreditInsightPageAfterLogin(user.salary.ToString());
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check LMS Insert for Personal Loan");
            loginPageSteps.ValidateBranchLMS_PersonalLoan(IdNumber);
            dBCreditCoach.DeleteExternalCommLog(IdNumber, 5);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Revert back Spoofing Info");
            azureContainers.Update_IsSpoofJson("spoofed-container", "IsSpoofed", "DaLesLogForBranch41", true);
            azureContainers.UpdateSpoofData(200, Content_DaLesLogForBranch_Decline, "DaLesLogForBranch41");
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}