namespace TestScripts.Client
{
    [TestFixture, Category("CC_AllDashboardButtons")]
    [Parallelizable(ParallelScope.Children)]
    public class CC_AllDashboardButtons : BaseTestFixture
    {
        private const string className = "CC_AllDashboardButtons";
        public CC_AllDashboardButtons() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyAllDashboardButtons{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyAllDashboardButtons(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check FAQ Page, 
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            HomePageSteps homePageSteps = new HomePageSteps();
            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);
            Console.WriteLine($"[DEBUG] idNumber is: {idNumber}");

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            loginPageSteps.OpenLoginPageAndSignin(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating View Solutions Button");
            homePageSteps.VerifyViewSolutionsButton();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating View Credit Insights Button");
            homePageSteps.VerifyViewCreditInsightsButton();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validating Recommended solutions for you Tiles");
            homePageSteps.VerifyRecommendeSolutionsForYouTiles();
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
