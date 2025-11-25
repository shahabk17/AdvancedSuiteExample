namespace IDM.Digitech.Automation.SCS.Test.TestScripts.Agent
{
    [TestFixture, Category(nameof(CC10906_Agent_LogAndVerifyCommLogDetails))]
    [Parallelizable(ParallelScope.Children)]
    class CC10906_Agent_LogAndVerifyCommLogDetails : BaseTestFixture
    {
        private const string className = "CC10906_Agent_LogAndVerifyCommLogDetails";
        public CC10906_Agent_LogAndVerifyCommLogDetails() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"Agent_LogAndVerifyCommLogDetails{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void Agent_LogAndVerifyCommLogDetails(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Create logs and verify Communication log
             *  
             * ************************************************************/

            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();
            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            BaseStep baseStep = new BaseStep();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(TestContext.CurrentContext.Test.Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Login in Agent UI");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(idNumber, user.agent_Email, user.agent_Password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify Communication Logs");
            agentUiPageSteps.CreateLogAndVerifyCommLog();
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}