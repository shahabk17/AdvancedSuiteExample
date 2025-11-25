namespace TestScripts.Client
{
    [TestFixture, Category("CC3244_NewLMS_RegistrationBudgetingAdvice")]
    [Parallelizable(ParallelScope.Children)]
    public class CC3244_NewLMS_RegistrationBudgetingAdvice() : BaseTestFixture(className, Properties.folderName, isHeadless: Properties.isHeadless)
    {
        public static string className = "CC3244_NewLMS_RegistrationBudgetingAdvice";


        [Test]
        public void NewLMS_RegistrationBudgetingAdvice_AutoReg()
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
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            API API = new API();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);


            Report.ChildLog = Report.ExtentTestGroup("Auto Registration");
            API.AutoReg(IdNumber, fname, surname, number, "OOBA");
            CurrentDateTime = DateTime.Now;
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToAppForAutoReg(IdNumber, "normal", registerWithResponseUrl);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetailsForAutoReg(IdNumber, Properties.password, "normal", "OOBA", registerWithResponseUrl, email);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("OTP PopUp");
            registrationPageSteps.EnterOTP(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Security Questions");
            registrationPageSteps.HandleSecurityQuestions(IdNumber, false);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Login Page");
            dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, "14000");
            string salary = (dBCreditCoach.SalaryTowardsDebt(IdNumber, 0.7)).ToString();
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, "normal", salary);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check For Registration Budgeting Advice LMS");
            dBCreditCoach.ValidateLMS_RegistrationBudgetingAdvice(IdNumber, "OOBA");
            dBCreditCoach.GetClientFromExternalLead(IdNumber, "OOBA");
            Report.PrintAndClearStep(Report.ChildLog);

        }

        [Test]
        public void NewLMS_RegistrationBudgetingAdvice_SPL()
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

            Report.ChildLog = Report.ExtentTestGroup("Login Page");
            dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, "14000");
            string salary = (dBCreditCoach.SalaryTowardsDebt(IdNumber, 0.7)).ToString();
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, userAtIndex.webSource, salary);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check For Registration Budgeting Advice LMS");
            dBCreditCoach.ValidateLMS_RegistrationBudgetingAdvice(IdNumber, "spl");
            Report.PrintAndClearStep(Report.ChildLog);
        }

        [Test]
        public void NewLMS_RegistrationBudgetingAdvice_HL()
        {

            /**************************************************************
             * 
             Test:- Check For NewLMS_RegistrationBudgetingAdvice                   
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

            Report.ChildLog = Report.ExtentTestGroup("Login Page");
            dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, "14000");
            string salary = (dBCreditCoach.SalaryTowardsDebt(IdNumber, 0.7)).ToString();
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, userAtIndex.webSource, salary);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check For Registration Budgeting Advice LMS");
            dBCreditCoach.ValidateLMS_RegistrationBudgetingAdvice(IdNumber, "hl");
            Report.PrintAndClearStep(Report.ChildLog);


        }
        [Test]
        public void NewLMS_RegistrationBudgetingAdvice_normal()
        {

            /**************************************************************
             * 
             Test:- Check For NewLMS_RegistrationBudgetingAdvice                   
             * for normal
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

            Report.ChildLog = Report.ExtentTestGroup("Login Page");
            dBCreditCoach.UpdateTotalCurrentBalance(IdNumber, "14000");
            string salary = (dBCreditCoach.SalaryTowardsDebt(IdNumber, 0.7)).ToString();
            registrationPageSteps.LoginUserAfterValidation(IdNumber, Properties.password, userAtIndex.webSource, salary);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Check For Registration Budgeting Advice LMS");
            dBCreditCoach.ValidateLMS_RegistrationBudgetingAdvice(IdNumber, "normal");
            Report.PrintAndClearStep(Report.ChildLog);


        }
        private InputData GetInputDataCustom(string source)
        {
            GenericUtils genericUtils = new GenericUtils();
            var data = genericUtils.ReadInputData<InputData>(Properties.environment, className);
            return data.Where(x => x.source == source).FirstOrDefault();
        }
    }
}
