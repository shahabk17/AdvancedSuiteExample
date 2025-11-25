namespace TestScripts.Client
{
    [TestFixture, Category("CC3288_CreditScore")]
    [Parallelizable(ParallelScope.Children)]
    public class CC3288_CreditScore : BaseTestFixture
    {
        private const string className = "CC3288_CreditScore";
        public CC3288_CreditScore() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"VerifyCreditScore{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void VerifyCreditScore(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For Qualifiers showing on the various tiles under
             * the get money section on solution page
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            HomePageSteps homePageSteps = new HomePageSteps();
            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            dBCreditCoach.UpdateCreditScore(idNumber, user.creditScore);
            loginPageSteps.OpenLoginPageAndSignin(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Verify credit score");
            homePageSteps.YourCreditScore(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
