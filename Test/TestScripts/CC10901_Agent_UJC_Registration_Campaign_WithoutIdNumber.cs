namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category(nameof(CC10901_Agent_UJC_Registration_Campaign_WithoutIdNumber))]
    [Parallelizable(ParallelScope.Children)]
    class CC10901_Agent_UJC_Registration_Campaign_WithoutIdNumber : BaseTestFixture
    {
        private const string className = "CC10901_Agent_UJC_Registration_Campaign_WithoutIdNumber";

        public CC10901_Agent_UJC_Registration_Campaign_WithoutIdNumber() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"Agent_UJC_RegistrationCampaign_Campaign_WithoutIdNumber{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void Agent_UJC_RegistrationCampaign_Campaign_WithoutIdNumber(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For UJC Registration Campaign on Agent UI 
             * without IdNumber and Landing Page URL set to Product Page
             *  
             * ************************************************************/

            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();
            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(TestContext.CurrentContext.Test.Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Login in Agent UI");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(idNumber, user.agent_Email, user.agent_Password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Creating Registration Campaign that will contain id number");
            var registrationDataset = agentUiPageSteps.CreateRegistrationCampaign_ContainIdNumber(false, false, true);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Manage and Validate Registration Campaign");
            var ujc_updatedInfo = agentUiPageSteps.ValidateAndManageRegistrationCampaign(registrationDataset, false, false, true);
            Report.PrintAndClearStep(Report.ChildLog);

            string fname = user.firstname;
            string surname = user.surname;
            string number = user.number;
            string salary = user.salary;
            string email = user.emailid;

            dBCreditCoach.UpdateAndDeleteTable(DBQueries.DeleteUser(idNumber));

            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToURL(ujc_updatedInfo["UJC_Url"].ToString());
            agentUiPageSteps.ValidateRegistrationURL(registrationDataset, ujc_updatedInfo, false);
            registrationPageSteps.UJC_EnterIDNumber(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetails(fname, surname, number, Properties.password, email);
            DateTime currentTimeUtc = DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Basic Verification");
            registrationPageSteps.GetBasicVerification(idNumber, currentTimeUtc).GetAwaiter().GetResult();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Security Questions");
            registrationPageSteps.HandleSecurityQuestions(idNumber, user.IsSecondSetRequired);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login Page");
            registrationPageSteps.UJC_LoginUserAfterRegistration(idNumber, Properties.password, user.webSource, registrationDataset, true);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Post DB Validation");
            agentUiPageSteps.ValidateCampaignOnDB(registrationDataset, ujc_updatedInfo, true, false);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}