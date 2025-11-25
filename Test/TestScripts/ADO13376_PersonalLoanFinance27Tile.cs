namespace TestScripts.Client
{
    [TestFixture, Category("ADO13376_PersonalLoanFinance27Tile")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO13376_PersonalLoanFinance27Tile : BaseTestFixture
    {
        private const string className = "ADO13376_PersonalLoanFinance27Tile";
        public ADO13376_PersonalLoanFinance27Tile() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"PersonalLoanFinance27Tile{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void PersonalLoanFinance27Tile(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For Qualifiers showing on the Personal Loans 
             * Finance 27 Short-term Loans tiles under
             * the get money section on solution page
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

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify finance27 tile Qualifier as per input file");
            solutionPageSteps.CheckAndUpdatefinance27Qualifier(IdNumber, user.QualifierType);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + IdNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify Personal Finance 27 tile Qualifier");
            solutionPageSteps.VerifyPersonalFinance27TileQualifier(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check for tile LMS Log");
            solutionPageSteps.VerifyPersonalFinance27TileButtonAndLogs(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Communication Log on AgentUi");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, user.emailid, user.agentidpassword);
            agentUiPageSteps.VerifyPersonalFinance27TileQualifiersOnAgentUi(IdNumber);
            agentUiPageSteps.VerifyCommLogsOnAgentUi(agentUiPageSteps.expectedFinance27Qualifier, "Finance 27 Short-term Loans");
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}