namespace TestScripts.Client
{
    [TestFixture, Category("CC_InsightsForYou")]
    [Parallelizable(ParallelScope.Children)]
    public class CC_InsightsForYou : BaseTestFixture
    {
        private const string className = "CC_InsightsForYou";
        public CC_InsightsForYou() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyInsightsForYou{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyInsightsForYou(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check FAQ Page, 
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            HomePageSteps homePageSteps = new HomePageSteps();
            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            loginPageSteps.OpenLoginPageAndSignin(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Take home salary towards debt");
            homePageSteps.VerifyTakeHomeSalaryTowardsDebt(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Overdue Amount");
            homePageSteps.VerifyOverdueAmount(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
