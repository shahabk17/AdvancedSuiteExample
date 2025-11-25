namespace TestScripts.Client
{
    [TestFixture, Category("CC2186_WealthScoreCalculation")]
    [Parallelizable(ParallelScope.Children)]
    public class CC2186_WealthScoreCalculation : BaseTestFixture
    {
        private const string className = "CC2186_WealthScoreCalculation";
        public CC2186_WealthScoreCalculation() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"WealthScoreCalculation{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void WealthScoreCalculation(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check the Total assets, Total liabilities, 
             * BALANCE and Wealth score entry should come correct
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            WealthPageSteps wealthPageSteps = new WealthPageSteps();
            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            loginPageSteps.OpenLoginPageAndSignin(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Update wealth score");
            wealthPageSteps.VerifyWealthScoreCalculation(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
