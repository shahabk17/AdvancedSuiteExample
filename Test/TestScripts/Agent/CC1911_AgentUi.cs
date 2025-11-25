namespace IDM.Digitech.Automation.SCS.Test.TestScripts.Agent
{
    [TestFixture, Category("CC1911_AgentUi")]
    [Parallelizable(ParallelScope.Self)]
    public class CC1911_AgentUi : BaseTestFixture
    {
        private const string className = "CC1911_AgentUi";
        public CC1911_AgentUi() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> ActivateUser_GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"ActivateUserfromAgentUi{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(ActivateUser_GetInputData)), Order(1)]
            public void ActivateUserfromAgentUi(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For User failed at security question.
             * Agent must be able to activate user.
             * 
             * ************************************************************/
            string IdNumber = user.idNumber;
            string fname = user.firstname;
            string surname = user.surname;
            string number = user.number;
            string idPassword = user.agentidpassword;
            string emailId = user.emailid;

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("NavigateToApp");
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            registrationPageSteps.EnterIDNumber(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetails(fname, surname, number, Properties.password, emailId);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("User failed at security question.");
            registrationPageSteps.UserFailedSevenSecurityQuestions();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Navigate to Agent Ui");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, emailId, idPassword);
            agentUiPageSteps.ActivateOrDeactivateUserFromAgentUi(true);
            agentUiPageSteps.PullUserCreditHistory();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check for User activate status");
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

        }

        public static IEnumerable<TestCaseData> DeactivateUser_GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>("preprod", className))
            {
                yield return new TestCaseData(input)
                    .SetName($"DeactivateIdFromAgentUi{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(DeactivateUser_GetInputData)), Order(2)]
        public static void DeactivateIdFromAgentUi(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For User is in Active state.
             * Agent must be able to deactivate user.
             * 
             * ************************************************************/

            string IdNumber = user.idNumber;
            string idPassword = user.agentidpassword;
            string emailId = user.emailid;
            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Navigate to Agent Ui to Deactivate User");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, emailId, idPassword);
            agentUiPageSteps.ActivateOrDeactivateUserFromAgentUi(false);
            agentUiPageSteps.PullUserCreditHistory();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check for User Deactivate status");
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
