namespace TestScripts.Client
{
    [TestFixture, Category("CC2113_CreditScoreTrend")]
    [Parallelizable(ParallelScope.Children)]
    public class CC2113_CreditScoreTrend : BaseTestFixture
    {
        private const string className = "CC2113_CreditScoreTrend";
        public CC2113_CreditScoreTrend() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        [Test]
        [Author("Shahab Khan")]
        public void CreditScoreTrend()
        {

            /**************************************************************
             * 
             * Test:- Check Your credit score in Score trend tab in Credit
             * Insights Page
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            CreditInsightsPageSteps creditInsightsPageSteps = new CreditInsightsPageSteps();
            string idNumberWithThreeMonthHistory = dBCreditCoach.FetchIdnumberAvailableForMonths(3);
            string idNumberWithLessThanThreeMonthHistory = dBCreditCoach.FetchIdnumberAvailableForMonths(1);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with idNumber with three month history " + idNumberWithThreeMonthHistory);
            loginPageSteps.OpenLoginPageAndSignin(idNumberWithThreeMonthHistory, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Score Trend with three month credit history");
            creditInsightsPageSteps.VerifyCreditScoreTrendForThreeMonths(idNumberWithThreeMonthHistory);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with idNumber with less than three month credit history" + idNumberWithLessThanThreeMonthHistory);
            loginPageSteps.SignInBack(idNumberWithLessThanThreeMonthHistory);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Score Trend with less than three month credit history");
            creditInsightsPageSteps.VerifyCreditScoreTrendForLessThanThreeMonths(idNumberWithLessThanThreeMonthHistory);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}