namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("CC17659_SanlamOnlineAPITesting")]
    [Parallelizable(ParallelScope.Children)]
    class CC17659_SanlamOnlineAPITesting : BaseTestFixture
    {
        private const string className = "CC17659_SanlamOnlineAPITesting";
        public CC17659_SanlamOnlineAPITesting() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"SanlamOnlineAPITesting{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Piyush Sharma")]
        public void SanlamOnlineAPITesting(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Sanlam Online API Testing Edge Cases
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            DBQueries dBQueries = new DBQueries();
            API api = new API();

            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the Sanlam Online API Response - 200 OK");
            var apiResponse_200 = api.APIM(idNumber, "sanlam_online", "APIM_Certificate");
            api.ValidateAPIResponseStatus(apiResponse_200);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the Sanlam Online API Response - 400 Bad Request (Blank IdNumber)");
            var apiResponse_400_BlankIdNumber = api.APIM("", "sanlam_online", "APIM_Certificate");
            api.ValidateAPIResponseStatus(apiResponse_400_BlankIdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the Sanlam Online API Response - 400 Bad Request (Invalid IdNumber)");
            var apiResponse_400_InvalidIdNumber = api.APIM(user.invalid_idNumber, "sanlam_online", "APIM_Certificate");
            api.ValidateAPIResponseStatus(apiResponse_400_InvalidIdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the Sanlam Online API Response - 400 Bad Request (Inactive IdNumber)");
            var UserDetails = dBCreditCoach.FetchUserDetailsFromUserTable(user.inactive_idNumber);
            if (UserDetails["IsActive"].ToString() == "True")
            {
                dBCreditCoach.UpdateAndDeleteTable(dBQueries.UpdateUserActiveStatus(user.inactive_idNumber, "False"));
            }
            var apiResponse_400_InactiveIdNumber = api.APIM(user.inactive_idNumber, "sanlam_online", "APIM_Certificate");
            api.ValidateAPIResponseStatus(apiResponse_400_InactiveIdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the Sanlam Online API Response - 401 Unauthorized (Invalid API key)");
            var apiResponse_401_Unauthorized = api.API_Unauthorized(idNumber, "sanlam_online", "APIM_Certificate");
            api.ValidateAPIResponseStatus(apiResponse_401_Unauthorized);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the Sanlam Online API Response - 403 Forbidden (Certificate is not provided.)");
            var apiResponse_403_API_Forbidden = api.API_Forbidden(idNumber, "sanlam_online", "APIM_Certificate");
            api.ValidateAPIResponseStatus(apiResponse_403_API_Forbidden);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}