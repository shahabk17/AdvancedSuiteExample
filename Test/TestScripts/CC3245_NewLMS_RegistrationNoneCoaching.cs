namespace TestScripts.Client
{
    [TestFixture, Category("CC3245_NewLMS_RegistrationNoneCoaching")]
    [Parallelizable(ParallelScope.Children)]
    public class CC3245_NewLMS_RegistrationNoneCoaching() : BaseTestFixture(className, Properties.folderName, isHeadless: Properties.isHeadless)
    {
        BaseStep baseStep = new BaseStep();
        WebDriverSession WebDriverSession = new WebDriverSession();
        GenericUtils genericUtils = new GenericUtils();

        public static string className = "CC3245_NewLMS_RegistrationNoneCoaching";

        [Test]
        public void NewLMS_RegistrationNoneCoaching_AutoReg()
        {

            /**************************************************************
             * 
             * Test:- Check For NewLMS_RegistrationBudgetingAdvice                   
             * for AutoReg
             * ************************************************************/
            var userAtIndex = GetInputDataCustom("auto_reg");

            string IdNumber = userAtIndex.idNumber;
            string fname = userAtIndex.firstname;
            string surname = userAtIndex.surname;
            string number = userAtIndex.number;
            string source = userAtIndex.source;
            string email = userAtIndex.emailid;
            bool registerWithResponseUrl = bool.Parse(userAtIndex.registerWithResponseUrl);

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            ProfilePageSteps profilePageSteps = new ProfilePageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            API API = new API();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);


            Report.ChildLog = Report.ExtentTestGroup("Auto Registration");
            API.AutoReg(IdNumber, fname, surname, number, source);
            WebDriverSession.CurrentDateTime = DateTime.Now;
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToAppForAutoReg(IdNumber, "normal", registerWithResponseUrl);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetailsForAutoReg(IdNumber, Properties.password, userAtIndex.webSource, source, registerWithResponseUrl, email);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Security Questions");
            registrationPageSteps.HandleSecurityQuestions(IdNumber, false);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check For Registration NoneCoaching");
            dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, "70000");
            string salary = (dBCreditCoach.SalaryTowardsDebt(IdNumber, 0.3)).ToString();
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, userAtIndex.webSource, salary);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Registration None Coaching");
            dBCreditCoach.GetClientFromExternalLead(IdNumber, source.ToUpper());
            Report.PrintAndClearStep(Report.ChildLog);

        }

        [Test]
        public void NewLMS_RegistrationNoneCoaching_SPL()
        {

            /**************************************************************
             * 
             * Test:- Check For NewLMS_RegistrationBudgetingAdvice                   
             * for SPL
             * ************************************************************/

            var userAtIndex = GetInputDataCustom("spl");

            string IdNumber = userAtIndex.idNumber;
            string fname = userAtIndex.firstname;
            string surname = userAtIndex.surname;
            string number = userAtIndex.number;
            string email = userAtIndex.emailid;

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);


            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToApp(Properties.environment, "spl");
            registrationPageSteps.EnterIDNumber(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetails(fname, surname, number, Properties.password, email);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Security Questions");
            registrationPageSteps.HandleSecurityQuestions(IdNumber, false);
            Report.PrintAndClearStep(Report.ChildLog);

            // Need to confirm if this LMS is working after login 30 mins
            //Report.ChildLog = Report.ExtentTestGroup("Check For Registration None Zero Take-home salary");
            //baseStep.wait.WaitTakingSystemTimeReference(0.51);
            //dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Registration None Zero Take-home salary");
            //dBCreditCoach.GetRegistrationSource(IdNumber, "spl-registration");
            //Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check For Registration NoneCoaching");
            dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, "70000");
            string salary = (dBCreditCoach.SalaryTowardsDebt(IdNumber, 0.3)).ToString();
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, userAtIndex.webSource, salary);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Registration None Coaching");
            Report.PrintAndClearStep(Report.ChildLog);

        }

        [Test]
        public void NewLMS_RegistrationNoneCoaching_HL()
        {

            /**************************************************************
             * 
             Test:- Check For NewLMS_RegistrationNoneZeroTakehomesalary               
             * for HL 
             * ************************************************************/
            var userAtIndex = GetInputDataCustom("hl");

            string IdNumber = userAtIndex.idNumber;
            string fname = userAtIndex.firstname;
            string surname = userAtIndex.surname;
            string number = userAtIndex.number;
            string email = userAtIndex.emailid;

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);


            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToApp(Properties.environment, "hl");
            registrationPageSteps.EnterIDNumber(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetails(fname, surname, number, Properties.password, email);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Security Questions");
            registrationPageSteps.HandleSecurityQuestions(IdNumber, false);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check For Registration NoneCoaching");
            dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, "70000");
            string salary = (dBCreditCoach.SalaryTowardsDebt(IdNumber, 0.3)).ToString();
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, userAtIndex.webSource, salary);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Registration None Coaching");
            dBCreditCoach.GetRegistrationSource(IdNumber, "hl-registration");
            Report.PrintAndClearStep(Report.ChildLog);



        }
        [Test]
        public void NewLMS_RegistrationNoneCoaching_normal()
        {

            /**************************************************************
             * 
             Test:- Check For NewLMS_RegistrationBudgetingAdvice                   
             * for HL 
             * ************************************************************/
            var userAtIndex = GetInputDataCustom("normal");

            string IdNumber = userAtIndex.idNumber;
            string fname = userAtIndex.firstname;
            string surname = userAtIndex.surname;
            string number = userAtIndex.number;
            string email = userAtIndex.emailid;

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);


            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToApp(Properties.environment, "normal");
            registrationPageSteps.EnterIDNumber(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetails(fname, surname, number, Properties.password, email);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Security Questions");
            registrationPageSteps.HandleSecurityQuestions(IdNumber, false);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check For Registration NoneCoaching");
            dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, "70000");
            string salary = (dBCreditCoach.SalaryTowardsDebt(IdNumber, 0.3)).ToString();
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, userAtIndex.webSource, salary);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Registration None Coaching");
            dBCreditCoach.GetRegistrationSource(IdNumber, "register");
            Report.PrintAndClearStep(Report.ChildLog);

        }
        private InputData GetInputDataCustom(string websource)
        {
            var data = genericUtils.ReadInputData<InputData>(Properties.environment, className);
            return data.Where(x => x.webSource == websource).FirstOrDefault();
        }
    }
}
