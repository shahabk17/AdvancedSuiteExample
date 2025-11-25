namespace TestScripts.Client
{
    [TestFixture, Category("ADO13645_StorecardTrueworthAndIdentityTile")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO13645_StorecardTrueworthAndIdentityTile : BaseTestFixture
    {
        private const string className = "ADO13645_StorecardTrueworthAndIdentityTile";
        public ADO13645_StorecardTrueworthAndIdentityTile() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"StorecardTrueworthAndIdentityTile{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void StorecardTrueworthAndIdentityTile(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For Qualifiers showing on the Storecard 
             * Trueworth And Identity tiles under
             * the get money section on solution page
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

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify store card  tile Qualifier as per input file");
            solutionPageSteps.CheckAndUpdateStorecardQualifier(IdNumber, user.QualifierType);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + IdNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Store Card tiles Qualifier");
            string keyVal_creditcoachscore_storecard = solutionPageSteps.VerifyStoreCardTileQualifier(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check for tile LMS Log");
            solutionPageSteps.VerifySoreCardsTileButtonAndLogs();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Communication Log on AgentUi");
            registrationPageSteps.NavigateToApp(Properties.environment, "agent");
            agentUiPageSteps.LoginToAgentUI(IdNumber, user.emailid, user.agentidpassword);
            agentUiPageSteps.VerifyStoreCardsTileQualifiersOnAgentUi(keyVal_creditcoachscore_storecard);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}