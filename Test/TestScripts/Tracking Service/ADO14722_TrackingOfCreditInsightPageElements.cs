namespace TestScripts.Client
{
    [TestFixture, Category("ADO14722_TrackingOfCreditInsightPageElements")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO14722_TrackingOfCreditInsightPageElements : BaseTestFixture
    {
        private const string className = "ADO14722_TrackingOfCreditInsightPageElements";
        public ADO14722_TrackingOfCreditInsightPageElements() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyCreditInsightPageTracking{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyCreditInsightPageTracking(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For different fields on credit insight page in 
             * the SCS
             * 
             * ************************************************************/

            LoginPageSteps loginPageSteps = new LoginPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            CreditInsightsPageSteps creditInsightsPageSteps = new CreditInsightsPageSteps();

            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup($"Login with: {IdNumber}");
            loginPageSteps.OpenLoginPageAndSignin(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Credit Insights Fields");
            creditInsightsPageSteps.VerifyFieldsForTracking(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}