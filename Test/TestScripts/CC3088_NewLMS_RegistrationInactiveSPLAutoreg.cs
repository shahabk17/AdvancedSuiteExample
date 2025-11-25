namespace TestScripts.Client
{
    [TestFixture, Category("CC3088_NewLMS_RegistrationInactiveSPLAutoreg")]
    [Parallelizable(ParallelScope.Children)]
    public class CC3088_NewLMS_RegistrationInactiveSPLAutoreg() : BaseTestFixture(className, Properties.folderName, isHeadless: Properties.isHeadless)
    {
        GenericUtils genericUtils = new GenericUtils();
        public static string className = "CC3088_NewLMS_RegistrationInactiveSPLAutoreg";

        [Test, Order(1)]
        public void NewLMS_RegistrationInactiveSPLAutoreg()
        {

            /**************************************************************
             * 
             * Test:- Check For New LMS - RegistrationInactiveSPLAutoreg                  
             * for Ooba/SPL/SPL-IVR take 24 hours to visible
             * ************************************************************/
            var userAtIndex = GetInputDataCustom("SPL");

            string IdNumber = userAtIndex.idNumber;
            string fname = userAtIndex.firstname;
            string surname = userAtIndex.surname;
            string number = userAtIndex.number;
            string source = userAtIndex.source;
            string email = userAtIndex.emailid;

            API API = new API();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.ChildLog.Log(Status.Info, "Input1 " + IdNumber);
            Report.ChildLog.Log(Status.Info, "Input2 " + fname);
            Report.ChildLog.Log(Status.Info, "Input3 " + surname);
            Report.ChildLog.Log(Status.Info, "Input4 " + number);
            Report.ChildLog.Log(Status.Info, "Input5 " + source);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Auto Registration");
            API.AutoReg(IdNumber, fname, surname, number, source);
            var CurrentDateTime = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fffffff");
            DateTime RegistrationFailedOTPTime = genericUtils.FormatTheSystemDateIntoUtc(1);
            DateTime RegistrationInactiveSPL = genericUtils.FormatTheSystemDateIntoUtc(24);
            Report.ChildLog.Log(Status.Info, $"Current Date Time in UTC is  {CurrentDateTime}");
            Report.ChildLog.Log(Status.Info, $"RegistrationFailedOTP LMS after 1 hour is {RegistrationFailedOTPTime}");
            Report.ChildLog.Log(Status.Info, $"RegistrationInactiveSPL LMS after 24 hour is {RegistrationInactiveSPL}");
            Report.PrintAndClearStep(Report.ChildLog);
        }

        [Test, Order(2)]
        public void RegistrationInactiveSPLIVRAutoreg()
        {

            /**************************************************************
             * 
             * Test:- Check For New LMS - RegistrationInactiveSPLAutoreg                  
             * for Ooba/SPL/SPL-IVR take 24 hours to visible
             * ************************************************************/
            var userAtIndex = GetInputDataCustom("SPL-IVR");

            string IdNumber = userAtIndex.idNumber;
            string fname = userAtIndex.firstname;
            string surname = userAtIndex.surname;
            string number = userAtIndex.number;
            string source = userAtIndex.source;
            string email = userAtIndex.emailid;
            bool registerWithResponseUrl = bool.Parse(userAtIndex.registerWithResponseUrl);

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            API API = new API();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details"); Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("User Auto Registration with ID - " + IdNumber);
            API.AutoReg(IdNumber, fname, surname, number, source);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToAppForAutoReg(IdNumber, "normal", registerWithResponseUrl);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetailsForAutoReg(IdNumber, Properties.password, "normal", source, registerWithResponseUrl, email);
            DateTime currentTimeUtc = DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Basic Verification");
            registrationPageSteps.GetBasicVerification(IdNumber, currentTimeUtc).GetAwaiter().GetResult();
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("User failed at security question.");
            registrationPageSteps.UserFailedSevenSecurityQuestions();
            var CurrentDateTime = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fffffff");
            DateTime RegistrationFailedSecurityQuestionsTime = registrationPageSteps.FormatTheSystemDateIntoUTC(0.1);
            DateTime RegistrationInactiveSPL = registrationPageSteps.FormatTheSystemDateIntoUTC(24);
            Report.ChildLog.Log(Status.Info, $"Current Date Time in UTC is  {CurrentDateTime}");
            Report.ChildLog.Log(Status.Info, $"RegistrationFailed Securityquestion LMS after 5 mins is {RegistrationFailedSecurityQuestionsTime}");
            Report.ChildLog.Log(Status.Info, $"RegistrationInactiveSPL LMS after 24 hour is {RegistrationInactiveSPL}");
            Report.PrintAndClearStep(Report.ChildLog);
        }

        [Test, Order(3)]
        public void RegistrationInactiveOOBAAutoreg()
        {

            /**************************************************************
             * 
             * Test:- Check For New LMS - RegistrationInactiveSPLAutoreg                  
             * for Ooba/SPL/SPL-IVR take 24 hours to visible
             * ************************************************************/
            var userAtIndex = GetInputDataCustom("OOBA");

            string IdNumber = userAtIndex.idNumber;
            string fname = userAtIndex.firstname;
            string surname = userAtIndex.surname;
            string number = userAtIndex.number;
            string source = userAtIndex.source;
            string email = userAtIndex.emailid;

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            API API = new API();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Auto Registration");
            API.AutoReg(IdNumber, fname, surname, number, source);
            var CurrentDateTime = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fffffff");
            DateTime RegistrationInactiveOOBAAutoreg = registrationPageSteps.FormatTheSystemDateIntoUTC(24);
            Report.ChildLog.Log(Status.Info, $"Current Date Time in UTC is  {CurrentDateTime}");
            Report.ChildLog.Log(Status.Info, $"RegistrationInactiveOOBAAutoreg LMS after 24 hour is {RegistrationInactiveOOBAAutoreg}");
            Report.PrintAndClearStep(Report.ChildLog);
        }
        private InputData GetInputDataCustom(string source)
        {
            var data = genericUtils.ReadInputData<InputData>(Properties.environment, className);
            return data.Where(x => x.source == source).FirstOrDefault();
        }
    }
}
