namespace TestScripts.Client
{
    [TestFixture, Category("CC2009_CC2033_BudgetScoreCalculation")]
    [Parallelizable(ParallelScope.Children)]
    public class CC2009_CC2033_BudgetScoreCalculation : BaseTestFixture
    {
        private const string className = "CC2009_CC2033_BudgetScoreCalculation";
        public CC2009_CC2033_BudgetScoreCalculation() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"BudgetScoreCalculation{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void BudgetScoreCalculation(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check the Total assets, Total liabilities, 
             * BALANCE and Budget score entry should come correct
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            BudgetPageSteps budgetPageSteps = new BudgetPageSteps();
            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            loginPageSteps.OpenLoginPageAndSignin(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify budget score");
            budgetPageSteps.BudgetScoreCalculation(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
