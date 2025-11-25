namespace TestScripts.Client
{
    [TestFixture, Category("ADO13375_CapfinPersonalLoanTile")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO13375_CapfinPersonalLoanTile : BaseTestFixture
    {
        private const string className = "ADO13375_CapfinPersonalLoanTile";
        public ADO13375_CapfinPersonalLoanTile() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"CapfinPersonalLoanTile{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void CapfinPersonalLoanTile(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For Qualifiers showing on the Capfin tiles under
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

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Capfin tile Qualifier as per input file");
            solutionPageSteps.CheckAndUpdateCapfinQualifier(IdNumber, user.QualifierType);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + IdNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password); 
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify Capfin tile Qualifier");
            solutionPageSteps.VerifyCapfinTileQualifier(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check for tile LMS Log");
            solutionPageSteps.VerifyCapfinTileButtonAndLogs();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Communication Log on AgentUi");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, user.emailid, user.agentidpassword);
            agentUiPageSteps.VerifyCapfinTileQualifiersOnAgentUi(IdNumber);
            agentUiPageSteps.VerifyCommLogsOnAgentUi("Capfin Personal Loans", "Transfer - Personal Loan");
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}