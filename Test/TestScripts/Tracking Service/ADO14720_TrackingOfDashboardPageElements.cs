namespace TestScripts.Client
{
    [TestFixture, Category("ADO14720_TrackingOfDashboardPageElements")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO14720_TrackingOfDashboardPageElements : BaseTestFixture
    {
        private const string className = "ADO14720_TrackingOfDashboardPageElements";
        public ADO14720_TrackingOfDashboardPageElements() : base(className, Properties.folderName, isHeadless:Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyDashboardPageTracking{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyDashboardPageTracking(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For different fields on different pages in the SCS
             * 
             * ************************************************************/

            HomePageSteps homePageSteps = new HomePageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
           
            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup($"Login with: {IdNumber}");
            loginPageSteps.OpenLoginPageAndSignin(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Dashboard Fields");
            homePageSteps.VerifyDashboardFieldsTracking(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}