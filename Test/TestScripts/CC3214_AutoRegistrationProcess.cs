namespace TestScripts.Client
{
    [TestFixture, Category("CC3214_AutoRegistrationProcess")]
    [Parallelizable(ParallelScope.Children)]
    public class CC3214_AutoRegistrationProcess : BaseTestFixture
    {
        private const string className = "CC3214_AutoRegistrationProcess";
        public CC3214_AutoRegistrationProcess() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"AutoRegistrationProcess{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public static void AutoRegistrationProcess(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For Auto Reg UserJourney                       
             * for Ooba/SPL/SPL-IVR 
             * ************************************************************/

            string IdNumber = user.idNumber;
            string fname = user.firstname;
            string surname = user.surname;
            string number = user.number;
            string salary = user.salary;
            string source = user.source;
            string email = user.emailid;
            bool registerWithResponseUrl = bool.Parse(user.registerWithResponseUrl);

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            API API = new API();

            Report.Log = Report.ExtentTest(className + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Auto Registration");
            API.AutoReg(IdNumber, fname, surname, number, source);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToAppForAutoReg(IdNumber, user.webSource, registerWithResponseUrl);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetailsForAutoReg(IdNumber, Properties.password, user.webSource, source, registerWithResponseUrl, email);
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

            Report.ChildLog = Report.ExtentTestGroup("Login Page");
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, user.webSource, salary);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("DB Post Validation");
            registrationPageSteps.GetPostValidationAfterReg(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}
