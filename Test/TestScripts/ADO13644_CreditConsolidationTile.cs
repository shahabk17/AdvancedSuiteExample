namespace TestScripts.Client
{
    [TestFixture, Category("ADO13644_CreditConsolidationTile")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO13644_CreditConsolidationTile : BaseTestFixture
    {
        private const string className = "ADO13644_CreditConsolidationTile";
        public ADO13644_CreditConsolidationTile() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyCreditConsolidationTile{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyCreditConsolidationTile(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For Qualifiers showing on the Credit Consolidation
             * tiles under the get money section on solution page
             * 
             * ************************************************************/

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + IdNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Credit Consolidation tile Qualifier on Client UI");
            dBCreditCoach.UpdateDbQuoteInfoTable(IdNumber, user.dBClient, user.dBC_Conversion);
            solutionPageSteps.VerifyCreditConsolidationTileQualifier(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check for tile LMS, External Comm Log and SQL");
            bool isQualified = solutionPageSteps.VerifyCreditConsolTileButtonAndLogs(IdNumber, user.dBClient, user.dBC_Conversion);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify tile Qualifier on Home Page On Client UI");
            solutionPageSteps.VerifyCreditConsolidationTileQualifiersOnHomePage(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Communication Log on AgentUi");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, user.emailid, user.agentidpassword);
            agentUiPageSteps.VerifyCreditConsolidationTileQualifierOnAgentUI(IdNumber);
            agentUiPageSteps.VerifyCommLogsOnAgentUi(agentUiPageSteps.expectedCreditConsolQualifier, "Transfer - DebtBusters", isQualified);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}