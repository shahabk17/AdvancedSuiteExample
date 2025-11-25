namespace IDM.Digitech.Automation.SCS.Test.TestScripts.Agent
{
    [TestFixture, Category(nameof(CC10908_Agent_UpdateCreditHistory))]
    [Parallelizable(ParallelScope.Children)]
    class CC10908_Agent_UpdateCreditHistory : BaseTestFixture
    {
        private const string className = "CC10908_Agent_UpdateCreditHistory";
        public CC10908_Agent_UpdateCreditHistory() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"Agent_UpdateCreditHistory{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void Agent_UpdateCreditHistory(InputData user)
        {
            /**************************************************************
             * 
             * Test:- UpDate Credit history button and activating ISActive 
             * flag on credit history           
             * 
             * ************************************************************/

            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            BaseStep baseStep = new BaseStep();

            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(TestContext.CurrentContext.Test.Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Login to Agent");
            dBCreditCoach.UpdateAndDeleteTable(DBQueries.DeleteCreditHistoryOfCurrentMonth(idNumber));
            baseStep.NavigateToApp("agent", Properties.environment);
            agentUiPageSteps.LoginToAgentUI(idNumber, user.agent_Email, user.agent_Password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Update Credit history button");
            agentUiPageSteps.VerifyUpdateCreditHistory();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Activating ISActive flag on credit history");
            agentUiPageSteps.VerifyCustomerDashboard(idNumber);
            int dateTimeDay = DateTime.Now.Day;
            string dateTimeMonth = DateTime.Now.Month.ToString();
            if (dateTimeDay < 19)
                dateTimeMonth = (DateTime.Now.Month - 1).ToString();
            dBCreditCoach.UpdateAndDeleteTable(DBQueries.UpdateCreditHistoryIsActive(idNumber, 1, dateTimeMonth));
            agentUiPageSteps.VerifyCustomerDashboardUiAndResponse();
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}