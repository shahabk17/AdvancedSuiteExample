namespace TestScripts.Client
{
    [TestFixture, Category("CC1916_NewUserJourney")]
    [Parallelizable(ParallelScope.Children)]
    public class CC1916_NewUserJourney : BaseTestFixture
    {
        private const string className = "CC1916_NewUserJourney";
        public CC1916_NewUserJourney() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"NewUserJourney{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void NewUserJourney(InputData user)
        {
            /**************************************************************
             * 
             * Test:- Check For NewUserJourney
             * register user with valid and invalid details 
             * Login with user and update information on profile page.
             * Check solutions page navigation of various tiles under get money.
             * Click on view offer and speak to coach - SPL not qualified user
             * Click on view offer - Spl qualified user             
             * 
             * ************************************************************/

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            ProfilePageSteps profilePageSteps = new ProfilePageSteps();
            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            string IdNumber = user.idNumber;
            string fname = user.firstname;
            string surname = user.surname;
            string number = user.number;
            string salary = user.salary;
            string email = user.emailid;

            string updatedSalary = user.updatedsalary;

            dBCreditCoach.UpdateAndDeleteTable(DBQueries.DeleteUser(IdNumber));

            Report.Log = Report.ExtentTest(className + " for ID - " + IdNumber);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Register with ID");
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            registrationPageSteps.EnterIDNumber(IdNumber);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Customer Details");
            registrationPageSteps.EnterCustDetails(fname, surname, number, Properties.password, email);
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

            ////update information on profile page.
            //Report.ChildLog = Report.ExtentTestGroup("Update Information On Profile Page");
            //profilePageSteps.UpdateInfoOnProfilePage(updatedSalary);
            //Report.PrintAndClearStep(Report.ChildLog);

            ////Check solutions page navigation of various tiles under get money.
            //Report.ChildLog = Report.ExtentTestGroup("Navigation of various tiles under getmoney on Solutions page ");
            //solutionPageSteps.BrokenLinkUnderGetMoneyTab();
            //Report.PrintAndClearStep(Report.ChildLog);

            //Dev team is working on its optimization
            /*           Report.ChildLog = Report.ExtentTestGroup("Click on view offer and speak to coach");
                       SolutionPageSteps.ViewOfferandSpeaktoCoach(IdNumber);
                       Report.PrintAndClearStep(Report.ChildLog);

                       //Click on view offer - Spl qualified user
                       Report.ChildLog = Report.ExtentTestGroup("Click on view offer - Spl qualified user");
                       SolutionPageSteps.ViewOffer_SplQualifiedUser(IdNumber);
                       Report.PrintAndClearStep(Report.ChildLog);*/
        }
    }
}
