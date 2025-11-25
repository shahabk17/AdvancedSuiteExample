namespace TestScripts.Client
{
    [TestFixture, Category("CC_CreditInsightsPageButtons")]
    [Parallelizable(ParallelScope.Children)]
    public class CC_CreditInsightsPageButtons : BaseTestFixture
    {
        private const string className = "CC_CreditInsightsPageButtons";
        public CC_CreditInsightsPageButtons() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyAllCreditInsightsPageButtons{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyAllCreditInsightsPageButtons(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check FAQ Page, 
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

            Report.ChildLog = Report.ExtentTestGroup("Validating Download Credit Report Button");
            creditInsightsPageSteps.VerifyDownloadCreditReportButton(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Solutions for you Button");
            creditInsightsPageSteps.VerifySolutionsForYouButton();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Your Budget Button");
            creditInsightsPageSteps.VerifyYourBudgetButton();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Credit Consolidation Button");
            creditInsightsPageSteps.VerifyCreditConsolidationButton();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating learn about money button and view full credit profile button");
            creditInsightsPageSteps.VerifyYourCreditBreakdownButtons();
            Report.PrintAndClearStep(Report.ChildLog);

        }
    }
}
