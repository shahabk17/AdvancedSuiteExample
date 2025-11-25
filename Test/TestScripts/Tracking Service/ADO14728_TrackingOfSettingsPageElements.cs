namespace TestScripts.Client
{
    [TestFixture, Category("ADO14728_TrackingOfSettingsPageElements")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO14728_TrackingOfSettingsPageElements : BaseTestFixture
    {
        private const string className = "ADO14728_TrackingOfSettingsPageElements";
        public ADO14728_TrackingOfSettingsPageElements() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifySettingsPageTracking{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifySettingsPageTracking(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For different fields on Settings page in the SCS
             * 
             * ************************************************************/

            LoginPageSteps loginPageSteps = new LoginPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            SettingsPageSteps settingsPageSteps = new SettingsPageSteps();

            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup($"Login with: {IdNumber}");
            loginPageSteps.OpenLoginPageAndSignin(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Settings Page Fields");
            settingsPageSteps.VerifyFieldsForTracking(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}