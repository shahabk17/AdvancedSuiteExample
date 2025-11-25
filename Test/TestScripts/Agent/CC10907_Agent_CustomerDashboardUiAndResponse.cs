namespace IDM.Digitech.Automation.SCS.Test.TestScripts.Agent
{
    [TestFixture, Category(nameof(CC10907_Agent_CustomerDashboardUiAndResponse))]
    [Parallelizable(ParallelScope.Children)]
    class CC10907_Agent_CustomerDashboardUiAndResponse : BaseTestFixture
    {
        private const string className = "CC10907_Agent_CustomerDashboardUiAndResponse";
        public CC10907_Agent_CustomerDashboardUiAndResponse() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"Agent_CustomerDashboardUiAndResponse{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void Agent_CustomerDashboardUiAndResponse(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For Customer Dashboard Ui And Response
             * On Agent UI
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

            Report.ChildLog = Report.ExtentTestGroup("Verify Customer Dashboard Ui And Response");
            agentUiPageSteps.VerifyCustomerDashboardUiAndResponse();
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}