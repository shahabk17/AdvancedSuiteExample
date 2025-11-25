namespace TestScripts.Client
{
    [TestFixture, Category("CC_HowYouMeasureUpScale")]
    [Parallelizable(ParallelScope.Children)]
    public class CC_HowYouMeasureUpScale : BaseTestFixture
    {
        private const string className = "CC_HowYouMeasureUpScale";
        public CC_HowYouMeasureUpScale() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"HowYouMeasureUpScale{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void HowYouMeasureUpScale(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check How you measure up scale field on Credit Insight 
             * Page
             *  
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            CreditInsightsPageSteps creditInsightsPageSteps = new CreditInsightsPageSteps();
            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            loginPageSteps.OpenLoginPageAndSignin(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Scale: How You Measure Up");
            creditInsightsPageSteps.VerifyHowYouMeasureUpScale(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
