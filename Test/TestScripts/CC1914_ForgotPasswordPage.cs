namespace TestScripts.Client
{
    [TestFixture, Category("CC1914_ForgotPasswordPage")]
    [Parallelizable(ParallelScope.Children)]
    public class CC1914_ForgotPasswordPage : BaseTestFixture
    {
        private const string className = "CC1914_ForgotPasswordPage";
        public CC1914_ForgotPasswordPage() : base(className, Properties.folderName, isHeadless: Properties.isHeadless) { }

        public static IEnumerable<TestCaseData> GetInputData()
        {
            foreach (var input in new GenericUtils()
                .ReadInputData<InputData>(Properties.environment, className))
            {
                yield return new TestCaseData(input)
                    .SetName($"ForgotPasswordPage{input.testSequence}");
            }
        }

        [Test, TestCaseSource(nameof(GetInputData))]
        public void ForgotPasswordPage(InputData user)
        {

            /**************************************************************
             * 
             * Test:- Check For NewUserJourney
             * Registration: Prevent the same ID Number from 
             * attempting registration twice
             * 
             * ************************************************************/

            RegistrationPageSteps registrationPageSteps = new RegistrationPageSteps();
            ForgotPasswordPageSteps forgotPasswordPageSteps = new ForgotPasswordPageSteps();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            OTPStorageAccount otpStorageAccount = new OTPStorageAccount();

            Report.Log = Report.ExtentTest(MethodBase.GetCurrentMethod().Name);
            Report.ChildLog = Report.ExtentTestGroup("Test Details");
            Report.ChildLog.Log(Status.Info, "Test Case Name " + MethodBase.GetCurrentMethod().Name);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("Forget Password and Fields Validation");
            string number = dBCreditCoach.FetchActivePhoneNumber(user.rowData);
            string phoneNumber = number.ToString();
            otpStorageAccount.DeleteOtpTableAsync(phoneNumber);
            registrationPageSteps.NavigateToApp(Properties.environment, user.webSource);
            forgotPasswordPageSteps.ValidationandRandomOtpfirstTime(number, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("After Resend Otp, Previous Otp is Disabled");
            forgotPasswordPageSteps.AfterRresendOtpPreviuosOtpisDisabled(number);
            Report.PrintAndClearStep(Report.ChildLog);

            Report.ChildLog = Report.ExtentTestGroup("User is able to reset password untill 3 counts");
            forgotPasswordPageSteps.UserIsAbleToResetPasstill3counts(number, Properties.password);
            Report.PrintAndClearStep(Report.ChildLog);
        }
    }
}