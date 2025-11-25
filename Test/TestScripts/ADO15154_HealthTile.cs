using NUnit.Framework;

namespace TestScripts.Client
{
    [TestFixture, Category("ADO15154_HealthTile")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO15154_HealthTile : BaseTestFixture
    {
        private const string className = "ADO15154_HealthTile";
        public ADO15154_HealthTile() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"HealthTile{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Piyush Sharma")]
        public void HealthTile(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check Health tile and its communicationlog in Agent UI
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

            Report.ChildLog = Report.ExtentTestGroup("Validate Health Tiles");
            solutionPageSteps.ValidateHealthTiles();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate Health Communication Log in AgentUI");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, user.agent_Email, user.agent_Password);
            agentUiPageSteps.ValidateCommunicationLog();
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}