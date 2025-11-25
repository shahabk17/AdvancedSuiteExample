namespace TestScripts.Client
{
    [TestFixture, Category("ADO15155_SaveMoneyTile")]
    [Parallelizable(ParallelScope.Children)]
    class ADO15155_SaveMoneyTile : BaseTestFixture
    {
        private const string className = "ADO15155_SaveMoneyTile";
        public ADO15155_SaveMoneyTile() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"SaveMoneyTile{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Piyush Sharma")]
        public void SaveMoneyTile(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check Save Money tile and its communicationlog in Agent UI
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();

            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + IdNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate Save Money Tiles");
            solutionPageSteps.ValidateSaveMoneyTiles();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate Save Money Communication Log in AgentUI");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, user.agent_Email, user.agent_Password);
            agentUiPageSteps.ValidateCommunicationLog();
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}