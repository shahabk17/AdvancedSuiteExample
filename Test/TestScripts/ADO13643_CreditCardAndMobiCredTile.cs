namespace TestScripts.Client
{
    [TestFixture, Category("ADO13643_CreditCardAndMobiCredTile")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO13643_CreditCardAndMobiCredTile : BaseTestFixture
    {
        private const string className = "ADO13643_CreditCardAndMobiCredTile";
        public ADO13643_CreditCardAndMobiCredTile() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"CreditCardAndMobiCredTile{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void CreditCardAndMobiCredTile(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For Qualifiers showing on the CreditCard And 
             * MobiCred tiles under the get money section on solution page
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

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify mobicredit and moneysave creditcard tile Qualifier as per input file");
            solutionPageSteps.CheckAndUpdateMobicreditAndMoneysaverQualifier(IdNumber, user.QualifierType);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + IdNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify CreditCard And MobiCred tile Qualifier");
            string keyVal_CreditCoachScore_CreditCard = solutionPageSteps.VerifyCreditCardAndMobiCredTileQualifier(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check for tile LMS Log");
            solutionPageSteps.VerifyCreditCardAndMobiCredTileButtons(IdNumber, keyVal_CreditCoachScore_CreditCard);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Communication Log on AgentUi");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, user.emailid, user.agentidpassword);
            agentUiPageSteps.VerifyCreditCardAndMobiCredTileQualifierOnAgentUI(IdNumber);
            agentUiPageSteps.VerifyCommLogsOnAgentUi(agentUiPageSteps.expectedCCQualifier, "Credit Card");
            agentUiPageSteps.VerifyTileLogsInLogDetailsTable(agentUiPageSteps.expectedCCQualifier, "Online Credit For Online Shopping");
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
