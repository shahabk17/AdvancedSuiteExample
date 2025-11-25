namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("CC17658_SanlamOnlineAPI")]
    [Parallelizable(ParallelScope.Children)]
    class CC17658_SanlamOnlineAPI : BaseTestFixture
    {
        private const string className = "CC17658_SanlamOnlineAPI";
        public CC17658_SanlamOnlineAPI() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"ValidateSanlamOnlineAPI{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Piyush Sharma")]
        public void ValidateSanlamOnlineAPI(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check and Validate Sanlam Online API
             * 
             * ************************************************************/

            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            LoginPageSteps loginPageSteps = new LoginPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            API api = new API();

            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Fetch and Validate the Sanlam Online API Response");
            var apiResponse = api.APIM(idNumber, "sanlam_online", "APIM_Certificate");
            solutionPageSteps.ValidateSalnamOnlineAPIResponse(apiResponse["content"].ToString(), idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}