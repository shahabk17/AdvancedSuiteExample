namespace TestScripts.Client
{
    [TestFixture, Category("CC_FactorsAffectingYourScore")]
    [Parallelizable(ParallelScope.Children)]
    public class CC_FactorsAffectingYourScore : BaseTestFixture
    {
        private const string className = "CC_FactorsAffectingYourScore";
        public CC_FactorsAffectingYourScore() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"ValidatingFactorsAffectingYourScore{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void ValidatingFactorsAffectingYourScore(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check FactorsAffectingYourScore, 
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

            Report.ChildLog = Report.ExtentTestGroup("Validating Take-home salary toward debt");
            creditInsightsPageSteps.VerifyTakeHomeSalaryTowardDebt();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Overdue Amount");
            creditInsightsPageSteps.VerifyOverdueAmount();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Money Left For Expenses");
            creditInsightsPageSteps.VerifyMoneyLeftForExpenses(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Estimated Monthly Interest Payments");
            creditInsightsPageSteps.VerifyEstimatedMonthlyInterestPayments(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
