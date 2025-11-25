namespace TestScripts.Client
{
    [TestFixture, Category("ADO14723_TrackingOfCreditAccountsPageElements")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO14723_TrackingOfCreditAccountsPageElements : BaseTestFixture
    {
        private const string className = "ADO14723_TrackingOfCreditAccountsPageElements";
        public ADO14723_TrackingOfCreditAccountsPageElements() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyCreditAccountsPageTracking{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyCreditAccountsPageTracking(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For different fields on credit accounts page
             * in the SCS
             * 
             * ************************************************************/

            LoginPageSteps loginPageSteps = new LoginPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            CreditAccountsPageSteps creditAccountsPageSteps = new CreditAccountsPageSteps();

            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup($"Login with: {IdNumber}");
            loginPageSteps.OpenLoginPageAndSignin(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Credit Accounts Fields");
            creditAccountsPageSteps.VerifyFieldsForTracking(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}