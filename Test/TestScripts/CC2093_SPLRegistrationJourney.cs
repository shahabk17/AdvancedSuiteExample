namespace TestScripts.Client
{
    [TestFixture, Category("CC2093_SPLRegistrationJourney")]
    [Parallelizable(ParallelScope.Children)]
    public class CC2093_SPLRegistrationJourney : BaseTestFixture
    {
        private const string className = "CC2093_SPLRegistrationJourney";
        public CC2093_SPLRegistrationJourney() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"SPLRegistrationJourney{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public static void SPLRegistrationJourney(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For SPL journey             
             * 
             * ************************************************************/
            string IdNumber = user.idNumber;
            string firstName = user.firstname;
            string surName = user.surname;
            string number = user.number;
            string salary = user.salary;
            string splqualifieduser = user.splqualifieduser;
            string email = user.emailid;

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            HomePageSteps homePageSteps = new HomePageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            Report.Log = Report.ExtentTest(className + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details"); Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToApp(Properties.environment, "spl");
            registrationPageSteps.EnterIDNumber(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetails(firstName, surName, number, Properties.password, email);
            DateTime currentTimeUtc = DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Basic Verification");
            registrationPageSteps.GetBasicVerification(IdNumber, currentTimeUtc).GetAwaiter().GetResult();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Security Questions");
            registrationPageSteps.HandleSecurityQuestions(IdNumber, user.IsSecondSetRequired);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login Page and Success Popup");
            bool spluser = bool.Parse(splqualifieduser);
            dBCreditCoach.UpdateSplQualifiedUser(IdNumber, "Approve");
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, user.webSource, salary);
            //homePageSteps.SuccessPopup(spluser);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
