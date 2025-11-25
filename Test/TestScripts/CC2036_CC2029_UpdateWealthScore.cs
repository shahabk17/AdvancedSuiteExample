namespace TestScripts.Client
{
    [TestFixture, Category("CC2036_CC2029_UpdateWealthScore")]
    [Parallelizable(ParallelScope.Children)]
    public class CC2036_CC2029_UpdateWealthScore : BaseTestFixture
    {
        private const string className = "CC2036_CC2029_UpdateWealthScore";
        public CC2036_CC2029_UpdateWealthScore() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"UpdateWealthScore{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void UpdateWealthScore(InputData user)
        {

            /**************************************************************
             * 
             * Test:- If user has not updated the wealth score then value of 
             * Wealth score details should not be display
             * 
             * If user click on update button from wealth section then user 
             * should redirect to wealth page
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            HomePageSteps homePageSteps = new HomePageSteps();
            WealthPageSteps wealthPageSteps = new WealthPageSteps();
            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            dBCreditCoach.DeleteUserWealth(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            loginPageSteps.OpenLoginPageAndSignin(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Wealth score without change");
            try
            {
                homePageSteps.VerifyWealthScore();
            }
            catch (WebDriverTimeoutException)
            {
                Report.ChildLog.Log(Status.Info, "This user already have budget score try with other row data from input file");
            }
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Update Wealth Score and verify");
            wealthPageSteps.UpdateWealthScore();            
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
