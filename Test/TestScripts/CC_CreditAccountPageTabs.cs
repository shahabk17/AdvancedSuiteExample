namespace TestScripts.Client
{
    [TestFixture, Category("CC_CreditAccountPageTabs")]
    [Parallelizable(ParallelScope.Children)]
    public class CC_CreditAccountPageTabs : BaseTestFixture
    {
        private const string className = "CC_CreditAccountPageTabs";
        public CC_CreditAccountPageTabs() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"Account_Judgements_LegalActionTabFields{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void Account_Judgements_LegalActionTabFields(InputData user)
        {

            /**************************************************************
             * 
             * Test:- 1.Check data under All, Account Summary,Judgements 
             * and legal Action and Debt Counselling data
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            CreditAccountsPageSteps creditAccountsPageSteps = new CreditAccountsPageSteps();
            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            loginPageSteps.OpenLoginPageAndSignin(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and verify Account Summary,Judgements, Legal Action and Debt Counselling data");
            creditAccountsPageSteps.VerifyCreditAccountTabCards(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
