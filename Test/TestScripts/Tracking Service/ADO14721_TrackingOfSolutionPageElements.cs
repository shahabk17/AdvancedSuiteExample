namespace TestScripts.Client
{
    [TestFixture, Category("ADO14721_TrackingOfSolutionPageElements")]
    [Parallelizable(ParallelScope.Children)]
    public class ADO14721_TrackingOfSolutionPageElements : BaseTestFixture
    {
        private const string className = "ADO14721_TrackingOfSolutionPageElements";
        public ADO14721_TrackingOfSolutionPageElements() : base(className, Properties.folderName, isHeadless:Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifySolutionPageTracking{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifySolutionPageTracking(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For different fields on Solution page in the SCS
             * 
             * ************************************************************/

            LoginPageSteps loginPageSteps = new LoginPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            
            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup($"Login with: {IdNumber}");
            loginPageSteps.OpenLoginPageAndSignin(IdNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);            

            Report.ChildLog = Report.ExtentTestGroup("Check and Verify Solutions Page Fields");
            solutionPageSteps.VerifyFieldsForTracking(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}