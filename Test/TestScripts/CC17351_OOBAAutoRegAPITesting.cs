namespace IDM.Digitech.Automation.SCS.Test.TestScripts
{
    [TestFixture, Category("CC17351_OOBAAutoRegAPITesting")]
    [Parallelizable(ParallelScope.Children)]
    class CC17351_OOBAAutoRegAPITesting : BaseTestFixture
    {
        private const string className = "CC17351_OOBAAutoRegAPITesting";
        public CC17351_OOBAAutoRegAPITesting() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils().ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"OOBAAutoRegAPITesting{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Piyush Sharma")]
        public void OOBAAutoRegAPITesting(InputData user)
        {
            /**************************************************************
             * 
             * Test:- OOBA AutoReg API Testing Edge Cases
             * 
             * ************************************************************/

            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            DBQueries dBQueries = new DBQueries();
            API api = new API();

            string idNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            dBCreditCoach.UpdateAndDeleteTable(DBQueries.DeleteUser(idNumber));

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + idNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the OOBA AutoReg API Response - 200 OK");
            var APIM_Response = api.APIM_AutoReg(idNumber, "APIM_AutoReg", "ooba_new", "APIM_Certificate", user);
            api.ValidateAPIMAutoRegStatus(APIM_Response);
            dBCreditCoach.UpdateAndDeleteTable(dBQueries.UpdateUserActiveStatus(idNumber, "True"));
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the OOBA AutoReg API Response - 409 Conflict (ID  Number  already  exists)");
            var apiResponse_409 = api.APIM_AutoReg(idNumber, "APIM_AutoReg", "ooba_new", "APIM_Certificate", user);
            api.ValidateAPIMAutoRegStatus(apiResponse_409);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the OOBA AutoReg API Response - 422 Unprocessable Entity (Invalid Id Number )");
            var apiResponse_422 = api.APIM_AutoReg(user.invalid_idNumber, "APIM_AutoReg", "ooba_new", "APIM_Certificate", user);
            api.ValidateAPIMAutoRegStatus(apiResponse_422);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Validate the OOBA AutoReg API Response - 401 Unauthorized (Invalid API key)");
            var apiResponse_401 = api.APIM_AutoReg_Unauthorized(idNumber, "APIM_AutoReg", "ooba_new", "APIM_Certificate", user);
            api.ValidateAPIMAutoRegStatus(apiResponse_401);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}