namespace TestScripts.Client
{
    [TestFixture, Category("CC1845_PreventSameIDNumberFromAttemptingRegistrationtwice")]
    [Parallelizable(ParallelScope.Children)]
    public class CC1845_PreventSameIDNumberFromAttemptingRegistrationtwice : BaseTestFixture
    {
        private const string className = "CC1845_PreventSameIDNumberFromAttemptingRegistrationtwice";
        public CC1845_PreventSameIDNumberFromAttemptingRegistrationtwice() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"PreventSameIDNumberFromAttemptingRegistrationtwice{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        [Author("Shahab Khan")]
        public void PreventSameIDNumberFromAttemptingRegistrationtwice(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For NewUserJourney
             * Registration: Prevent the same ID Number from 
             * attempting registration twice
             * 
             * ************************************************************/

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            string IdNumber = dBCreditCoach.FetchActiveIdnumber(user.idNumber, user.rowData);

            Report.Log = Report.ExtentTest(className + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Prevent the same ID Number from attempting registration twice");
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            registrationPageSteps.EnterIDNumber(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
