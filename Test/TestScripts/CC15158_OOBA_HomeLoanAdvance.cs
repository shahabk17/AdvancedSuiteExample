namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("CC15158_OOBA_HomeLoanAdvance")]
    [Parallelizable(ParallelScope.Children)]
    class CC15158_OOBA_HomeLoanAdvance : BaseTestFixture
    {
        private const string className = "CC15158_OOBA_HomeLoanAdvance";
        public CC15158_OOBA_HomeLoanAdvance() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"OOBA_HomeLoanAdvance{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void OOBA_HomeLoanAdvance(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check and Verify OOBA Home Loan Advance
             * 
             **************************************************************/

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

            Report.ChildLog = Report.ExtentTestGroup("Verify OOBA Home Loan Advance");
            solutionPageSteps.VerifyOOBAHomeLoanAdvance(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify OOBA Home Loan Advance Communication Log on Agent UI");
            baseStep.NavigateToApp("agent", Properties.environment);
            agentUiPageSteps.LoginToAgentUI(idNumber, user.agent_Email, user.agent_Password);
            agentUiPageSteps.VerifyCommlogForOOBAHomeLoanAdvance("Digital", "User");
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}