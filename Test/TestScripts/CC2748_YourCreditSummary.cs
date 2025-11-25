namespace TestScripts.Client
{
    [TestFixture, Category("CC2748_YourCreditSummary")]
    [Parallelizable(ParallelScope.Children)]
    public class CC2748_YourCreditSummary : BaseTestFixture
    {
        private const string className = "CC2748_YourCreditSummary";
        public CC2748_YourCreditSummary() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"YourCreditSummary{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void YourCreditSummary(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check Your credit summary in Credit
             * Insights Page
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

            Report.ChildLog = Report.ExtentTestGroup("Check and verify Your credit summary");
            creditInsightsPageSteps.VerifyYourCreditSummary(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
