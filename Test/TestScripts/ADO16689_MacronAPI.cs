namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("ADO16689_MacronAPI")]
    [Parallelizable(ParallelScope.Children)]
    class ADO16689_MacronAPI : BaseTestFixture
    {
        private const string className = "ADO16689_MacronAPI";
        public ADO16689_MacronAPI() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"ValidateMacronAPIResponse{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Piyush Sharma")]
        public void ValidateMacronAPIResponse(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check and Validate Macron API Response
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

            Report.ChildLog = Report.ExtentTestGroup("Login with Id " + idNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            loginPageSteps.LoginWithID(idNumber, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Fetch and Validate the Macron API Response");
            var apiResponse = api.APIM(idNumber, "macron", "APIM_Certificate");
            solutionPageSteps.ValidateMacronAPIResponse(apiResponse["content"].ToString(), idNumber);
            solutionPageSteps.ValidateMacronAPILogInExternalCommLog(idNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}