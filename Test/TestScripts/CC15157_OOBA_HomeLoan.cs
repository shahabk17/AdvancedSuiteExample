namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("CC15157_OOBA_HomeLoan")]
    [Parallelizable(ParallelScope.Children)]
    class CC15157_OOBA_HomeLoan : BaseTestFixture
    {
        private const string className = "CC15157_OOBA_HomeLoan";
        public CC15157_OOBA_HomeLoan() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"OOBA_HomeLoan{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void OOBA_HomeLoan(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check and Verify OOBA Home Loan
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            BaseStep baseStep = new BaseStep();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();

            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            loginPageSteps.OpenLoginPageAndSignin(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify OOBA Home Loan");
            solutionPageSteps.VerifyOOBAHomeLoan(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify OOBA Home Loan Communication Log on Agent UI");
            baseStep.NavigateToApp("agent", Properties.environment);
            agentUiPageSteps.LoginToAgentUI(idNumber, user.agent_Email, user.agent_Password);
            agentUiPageSteps.VerifyCommlogForOOBAHomeLoan("Digital", "User");
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}