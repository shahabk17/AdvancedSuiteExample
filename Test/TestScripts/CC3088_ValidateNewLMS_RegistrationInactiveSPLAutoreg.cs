namespace TestScripts.Client
{
    [TestFixture]
    public class CC3088_ValidateNewLMS_RegistrationInactiveSPLAutoreg() : BaseTestFixture(className, Properties.folderName, isHeadless: Properties.isHeadless)
    {
        public static string className = "CC3088_NewLMS_RegistrationInactiveSPLAutoreg";

        /// <summary>
        /// This test case is to validate LMS and run after 
        /// CC3088_NewLMS_RegistrationInactiveSPLAutoreg test
        /// </summary>
        [Test, Category("ValidateLMS_RegistrationFailedSecurityQuestions_RegistrationInactiveSPLIVRAutoreg")]
        public static void ValidateLMS_RegistrationFailedSecurityQuestions_RegistrationInactiveSPLIVRAutoreg()
        {
            /**************************************************************
             * 
             * Test:- Check For New LMS - RegistrationInactiveSPLAutoreg                  
             * for Ooba/SPL/SPL-IVR take 5 mins hours to visible
             * ************************************************************/

            var userAtIndex = GetInputDataCustom("SPL-IVR");

            string IdNumber = userAtIndex.idNumber;
            string source = userAtIndex.source;
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.ChildLog.Log(Status.Info, "RegistrationFailedSecurityQuestions - Taking 5 mins to generate");
            Report.PrintAndClearStep(Report.ChildLog);

            // RegistrationFailedSecurityQuestions - Taking 5 mins to generate
            Report.ChildLog = Report.ExtentTestGroup("Check For Registration Inactive SPL IVR Autoreg validateLMS_RegistrationFailedSecurityQuestions");
            dBCreditCoach.ValidateLMS_RegistrationFailedSecurityQuestions(IdNumber, source);
            Report.PrintAndClearStep(Report.ChildLog);


        }

        [Test, Category("ValidateRegistrationFailedOTP_NewLMS_RegistrationInactiveSPLAutoreg")]
        public static void ValidateRegistrationFailedOTP_NewLMS_RegistrationInactiveSPLAutoreg()
        {

            /**************************************************************
             * 
             * Test:- Check For New LMS - RegistrationInactiveSPLAutoreg                  
             * for Ooba/SPL/SPL-IVR take 1 hours to visible
             * ************************************************************/
           var userAtIndex = GetInputDataCustom("SPL");

            string IdNumber = userAtIndex.idNumber;
            string fname = userAtIndex.firstname;
            string surname = userAtIndex.surname;
            string number = userAtIndex.number;
            string source = userAtIndex.source;
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            DateTime currentTimeUtc = DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.ChildLog.Log(Status.Info, "Input1 " + IdNumber);
            Report.ChildLog.Log(Status.Info, "Input2 " + fname);
            Report.ChildLog.Log(Status.Info, "Input3 " + surname);
            Report.ChildLog.Log(Status.Info, "Input4 " + number);
            Report.ChildLog.Log(Status.Info, "Input5 " + source);
            Report.PrintAndClearStep(Report.ChildLog);

            // validateLMS_RegistrationFailedOTP - Taking one hour to generate
            Report.ChildLog = Report.ExtentTestGroup("Check For Registration Inactive SPL Autoreg validateLMS_RegistrationFailedOTP");
            dBCreditCoach.ValidateLMS_RegistrationFailedOTP(IdNumber, source);
            Report.PrintAndClearStep(Report.ChildLog);

        }

        [Test, Category("ValidateRegistrationInactiveSPL_NewLMS_RegistrationInactiveSPLAutoreg")]
        public static void ValidateRegistrationInactiveSPL_NewLMS_RegistrationInactiveSPLAutoreg()
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
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.ChildLog.Log(Status.Info, "Input1 " + IdNumber);
            Report.ChildLog.Log(Status.Info, "Input2 " + fname);
            Report.ChildLog.Log(Status.Info, "Input3 " + surname);
            Report.ChildLog.Log(Status.Info, "Input4 " + number);
            Report.ChildLog.Log(Status.Info, "Input5 " + source);
            Report.PrintAndClearStep(Report.ChildLog);

            // validateLMS_RegistrationInactiveSPL - Taking 24 hour to generate
            Report.ChildLog = Report.ExtentTestGroup("Check For Registration Inactive SPL Autoreg validateLMS_RegistrationInactiveSPL");
            dBCreditCoach.ValidateLMS_RegistrationInactiveSPL(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

        }

        [Test, Category("ValidateRegistrationInactiveSPL_NewLMS_RegistrationInactiveSPLIVRAutoreg")]
        public static void ValidateRegistrationInactiveSP_NewLMS_RegistrationInactiveSPLIVRAutoreg()
        {
            /**************************************************************
             * 
             * Test:- Check For New LMS - RegistrationInactiveSPLAutoreg                  
             * for Ooba/SPL/SPL-IVR take 24 hours to visible
             * ************************************************************/

            var userAtIndex = GetInputDataCustom("SPL-IVR");

            string IdNumber = userAtIndex.idNumber;
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            // validateLMS_RegistrationInactiveSPL - Taking 24 hour to generate
            Report.ChildLog = Report.ExtentTestGroup("Check For Registration Inactive SPL IVR Autoreg ");
            dBCreditCoach.ValidateLMS_RegistrationInactiveSPL(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);


        }

        [Test, Category("ValidateLMS_RegistrationInactiveForOOBA_NewLMS_RegistrationInactiveOOBAAutoreg")]
        public static void ValidateLMS_RegistrationInactiveForOOBA_NewLMS_RegistrationInactiveOOBAAutoreg()
        {

            /**************************************************************
             * 
             * Test:- Check For New LMS - RegistrationInactiveSPLAutoreg                  
             * for Ooba/SPL/SPL-IVR take 24 hours to visible
             * ************************************************************/
            var userAtIndex = GetInputDataCustom("OOBA");

            string IdNumber = userAtIndex.idNumber;
            string source = userAtIndex.source;
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);


            // validateLMS_RegistrationInactiveForOOBA - Taking 24 hour to generate
            Report.ChildLog = Report.ExtentTestGroup("Check For No Registration Inactive OOBA Autoreg");
            dBCreditCoach.ValidateLMS_RegistrationInactiveForOOBA(IdNumber, source);
            Report.PrintAndClearStep(Report.ChildLog);


        }
        private static InputData GetInputDataCustom(string source)
        {
            GenericUtils genericUtils = new GenericUtils();
            var data = genericUtils.ReadInputData<InputData>(Properties.environment, className);
            return data.Where(x => x.source == source).FirstOrDefault();
        }
    }
}
