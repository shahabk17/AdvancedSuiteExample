namespace TestScripts.Client
{
    [TestFixture, Category("ADO14725_TrackingOfBudgetPageElements")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO14725_TrackingOfBudgetPageElements : BaseTestFixture
    {
        private const string className = "ADO14725_TrackingOfBudgetPageElements";
        public ADO14725_TrackingOfBudgetPageElements() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyBudgetPageTracking{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyBudgetPageTracking(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For different fields on Budget page in the SCS
             * 
             * ************************************************************/

            LoginPageSteps loginPageSteps = new LoginPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            BudgetPageSteps budgetPageSteps = new BudgetPageSteps();

            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup($"Login with: {IdNumber}");
            loginPageSteps.OpenLoginPageAndSignin(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Budget Page Fields");
            budgetPageSteps.VerifyFieldsForTracking(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}