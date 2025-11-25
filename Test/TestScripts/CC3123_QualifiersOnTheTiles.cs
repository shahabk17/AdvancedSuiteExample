namespace TestScripts.Client
{
    [TestFixture, Category("CC3123_QualifiersOnTheTiles")]
    [Parallelizable(ParallelScope.Children)]
    public class CC3123_QualifiersOnTheTiles : BaseTestFixture
    {
        private const string className = "CC3123_QualifiersOnTheTiles";
        public CC3123_QualifiersOnTheTiles() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"QualifiersOnTheTiles{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void QualifiersOnTheTiles(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For Qualifiers showing on the various tiles under
             * the get money section on solution page
             * 
             * ************************************************************/

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            HomePageSteps homePageSteps = new HomePageSteps();
            AgentUiPageSteps agentUiPageSteps = new AgentUiPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + IdNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify SPL tile Qualifier");
            try
            {
                solutionPageSteps.VerifySPLTile(IdNumber, user.les_decision, bool.Parse(user.isQualifiedSPL));
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"SPLTile Qualifier is not verified due to error {ex}");
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify  Capfin tile Qualifier");
            try
            {
                solutionPageSteps.VerifyCapfinTileQualifier(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"CapfinTile Qualifier is not verified due to error {ex}");
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Credit Card And MobiCred tile Qualifier");
            try
            {
                solutionPageSteps.VerifyCreditCardAndMobiCredTileQualifier(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"CreditCard And MobiCredTile Qualifier is not verified due to error {ex}");
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify  Credit Consolidation tile Qualifier");
            try
            {
                solutionPageSteps.VerifyCreditConsolidationTileQualifier(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"CreditConsolidationTile Qualifier is not verified due to error {ex}");
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Storecard Trueworth and Identity");
            try
            {
                solutionPageSteps.VerifyStoreCardTileQualifier(IdNumber);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"Storecard Trueworth and Identity Qualifiers are not verified due to error {ex}");
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify tile Qualifier on Home Page");
            homePageSteps.VerifyTileQualifiersOnHomePage(IdNumber, bool.Parse(user.isQualifiedSPL));
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify tile Qualifier on AgentUi Homepage and Solution Page");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, user.emailid, user.agentidpassword);
            agentUiPageSteps.VerifyTileQualifiersOnAgentUi(IdNumber, bool.Parse(user.isQualifiedSPL));
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}