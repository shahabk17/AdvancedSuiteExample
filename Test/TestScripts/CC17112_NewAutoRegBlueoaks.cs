namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("CC17112_NewAutoRegBlueoaks")]
    [Parallelizable(ParallelScope.Children)]
    class CC17112_NewAutoRegBlueoaks : BaseTestFixture
    {
        private const string className = "CC17112_NewAutoRegBlueoaks";
        public CC17112_NewAutoRegBlueoaks() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"NewAutoRegBlueoaks{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Piyush Sharma")]
        public void NewAutoRegBlueoaks(InputData user)
        {
            /**************************************************************
             * 
             * Test:- New AutoReg Process with Blueoaks API
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            API api = new API();

            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            dBCreditCoach.UpdateAndDeleteTable(DBQueries.DeleteUser(IdNumber));

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Execute Blueoaks AutoReg API");
            var APIM_Response = api.APIM_AutoReg(IdNumber, "APIM_AutoReg", "blueoaks", "APIM_Certificate", user);
            api.ValidateAPIMAutoRegStatus(APIM_Response);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate and Fetch details from ExternalLead and User Table");
            var externalLeadInfo = dBCreditCoach.FetchExternalLead(IdNumber);
            string url = dBCreditCoach.ValidateExternalLead(externalLeadInfo, IdNumber, "blueoaks", "blueoaks");
            var userInfo = dBCreditCoach.FetchUserDetailsFromUserTable(IdNumber);
            dBCreditCoach.ValidateUserInfo(userInfo, IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Navigate to URL and Enter Password");
            registrationPageSteps.NavigateToAutoRegURL(url, Properties.password);
            DateTime currentTimeUtc = DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(user.number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Basic Verification");
            registrationPageSteps.GetBasicVerification(IdNumber, currentTimeUtc).GetAwaiter().GetResult();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Security Questions");
            registrationPageSteps.HandleSecurityQuestions(IdNumber, user.IsSecondSetRequired);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login Page");
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, user.webSource, user.salary);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("DB Post Validation");
            registrationPageSteps.GetPostValidationAfterReg(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the details from LeadLog Table in CC Function Database");
            var LeadLogInfo = dBCreditCoach.FetchLeadLog();
            dBCreditCoach.ValidateLeadLog(LeadLogInfo, IdNumber, "blueoaks", "blueoaks");
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}