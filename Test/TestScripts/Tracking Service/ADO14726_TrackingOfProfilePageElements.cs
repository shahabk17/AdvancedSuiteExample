namespace TestScripts.Client
{
    [TestFixture, Category("ADO14726_TrackingOfProfilePageElements")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO14726_TrackingOfProfilePageElements : BaseTestFixture
    {
        private const string className = "ADO14726_TrackingOfProfilePageElements";
        public ADO14726_TrackingOfProfilePageElements() : base(className, Properties.folderName, isHeadless:Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyProfilePageTracking{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyProfilePageTracking(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For different fields on Profile page in the SCS
             * 
             * ************************************************************/

            LoginPageSteps loginPageSteps = new LoginPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            ProfilePageSteps profilePageSteps = new ProfilePageSteps();
           
            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup($"Login with: {IdNumber}");
            loginPageSteps.OpenLoginPageAndSignin(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);
                    
            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Profile Page Fields");
            profilePageSteps.VerifyFieldsForTracking(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}