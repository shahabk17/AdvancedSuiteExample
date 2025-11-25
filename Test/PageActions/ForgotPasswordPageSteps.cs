namespace SanlamAutomation
{
    [Author("Shahab Khan")]
    public class ForgotPasswordPageSteps
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();

        /// <summary>
        /// Validates password requirements and handles first-time OTP validation
        /// </summary>
        /// <param name="phonenumber">User's phone number</param>
        /// <param name="password">New password to be set</param>
        [Author("Shahab Khan")]
        public void ValidationandRandomOtpfirstTime(string phonenumber, string password)
        {
            var forgotPasswordPage = new ForgotPasswordPage();
            InitiateForgotPassword(forgotPasswordPage, phonenumber);
            ValidateEmptyFieldMessages(forgotPasswordPage);
            ValidatePasswordMismatch(forgotPasswordPage, password);
            ValidateRandomOtp(forgotPasswordPage, password);
        }

        /// <summary>
        /// Validates that previous OTP becomes invalid after requesting a new one
        /// </summary>
        /// <param name="phonenumber">User's phone number</param>
        [Author("Shahab Khan")]
        public void AfterRresendOtpPreviuosOtpisDisabled(string phonenumber)
        {
            var forgotPasswordPage = new ForgotPasswordPage();
            var otpStorageAccount = new OTPStorageAccount();

            var pins = GetOtpPins(otpStorageAccount, phonenumber);
            string previousOtp = pins[0].ToString();

            HandleOtpExpiry(otpStorageAccount, phonenumber);
            ValidatePreviousOtpInvalid(forgotPasswordPage, previousOtp);
        }

        /// <summary>
        /// Verifies password reset functionality within allowed attempts
        /// </summary>
        /// <param name="phonenumber">User's phone number</param>
        /// <param name="password">New password to be set</param>
        [Author("Shahab Khan")]
        public void UserIsAbleToResetPasstill3counts(string phonenumber, string password)
        {
            var forgotPasswordPage = new ForgotPasswordPage();
            var otpStorageAccount = new OTPStorageAccount();

            var pins = GetOtpPins(otpStorageAccount, phonenumber);
            ProcessFirstPasswordReset(forgotPasswordPage, pins[0].ToString());
            ProcessSecondPasswordReset(forgotPasswordPage, phonenumber, password);
            ValidateThirdAttemptBlocked(forgotPasswordPage, phonenumber);
        }

        /// <summary>
        /// This method automates the process of resetting a password by entering a phone number, retrieving an OTP, setting a new password, and confirming success.
        /// </summary>
        /// <param name="number"></param>
        [Author("Piyush Sharma")]
        public void CreateNewPassword(string number)
        {
            var forgotPasswordPage = new ForgotPasswordPage();
            var otpStorageAccount = new OTPStorageAccount();

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.sendotpbtn, 60);
            baseStep.SendKeys(forgotPasswordPage.CellPhoneNumber, number);
            validate.TakeStepFullScreenShot($"Cellphone number is entered - {number}", Status.Info);
            baseStep.Click(forgotPasswordPage.SendOtpBtn);
            baseStep.wait.WaitTillPageLoad();

            var pins = GetOtpPins(otpStorageAccount, number);

            baseStep.ScrollToElement(forgotPasswordPage.EnterOtp);
            baseStep.ClearAndSendKeys(forgotPasswordPage.EnterOtp, pins[1].ToString());
            baseStep.ClearAndSendKeys(forgotPasswordPage.Password, Properties.password);
            baseStep.ClearAndSendKeys(forgotPasswordPage.ConfirmPassword, Properties.password);
            baseStep.Click(forgotPasswordPage.SubmitBtn);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.loginbelowpaswordupdatedmsg, 30);
            validate.TakeStepFullScreenShot("New Password is Created", Status.Info);
            baseStep.Click(forgotPasswordPage.LoginBelowPasswordUpdatedMsg);
        }

        #region Private Helper Methods

        private void InitiateForgotPassword(ForgotPasswordPage forgotPasswordPage, string phonenumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.loginbtn, 60);
            baseStep.Click(forgotPasswordPage.LoginBtn);

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.forgotpasswordlink, 60);
            baseStep.wait.GenericWait(5000);
            validate.TakeStepFullScreenShot("Enter cellphone number", Status.Info);
            baseStep.Click(forgotPasswordPage.ForgotPasswordLink);

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.sendotpbtn, 60);
            baseStep.SendKeys(forgotPasswordPage.CellPhoneNumber, phonenumber);
            validate.TakeStepFullScreenShot($"cellphone number is entered - {phonenumber}", Status.Info);
            baseStep.Click(forgotPasswordPage.SendOtpBtn);
        }

        private void ValidateEmptyFieldMessages(ForgotPasswordPage forgotPasswordPage)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.submitbtn, 60);
            baseStep.Click(forgotPasswordPage.SubmitBtn);

            validate.AssertEquals(true, forgotPasswordPage.OtpRequiredMsg.Displayed, "OtpRequiredMsg is not displayed", false);
            validate.AssertEquals(true, forgotPasswordPage.PasswordRequiredMsg.Displayed, "PasswordRequiredMsg is not displayed", true);
            validate.AssertEquals(true, forgotPasswordPage.ConfirmPasswordRequiredMsg.Displayed, "ConfirmPasswordRequiredMsg is not displayed", true);
        }

        private void ValidatePasswordMismatch(ForgotPasswordPage forgotPasswordPage, string password)
        {
            string randomPass = genericUtils.GetRandomString(7);
            string randomOtp = genericUtils.RandomInteger(100000, 999999).ToString();

            baseStep.ClearAndSendKeys(forgotPasswordPage.EnterOtp, randomOtp);
            baseStep.ClearAndSendKeys(forgotPasswordPage.Password, password);
            baseStep.ClearAndSendKeys(forgotPasswordPage.ConfirmPassword, randomPass);

            validate.AssertEquals(true, forgotPasswordPage.PasswordMustMatchMsg.Displayed, "PasswordMustMatchMsg is not displayed", false);
        }

        private int[] GetOtpPins(OTPStorageAccount otpStorageAccount, string phonenumber)
        {
            int[] pins;
            do
            {
                baseStep.wait.GenericWait(3000);
                var sortedEntities = otpStorageAccount.GetOtpDataFromPhoneNumber(phonenumber);
                pins = sortedEntities.Select(entity => entity.Pin).ToArray();
            } while (pins.Length < 2);
            return pins;
        }

        private void HandleOtpExpiry(OTPStorageAccount otpStorageAccount, string phonenumber)
        {
            baseStep.wait.GenericWait(2000);
            otpStorageAccount.UpdateOtpExpiryTimeAsync(phonenumber, 0);
            otpStorageAccount.GetOtpDataFromPhoneNumber(phonenumber);
            baseStep.wait.GenericWait(60000);
        }

        private void ValidateRandomOtp(ForgotPasswordPage forgotPasswordPage, string password)
        {
            baseStep.ClearAndSendKeys(forgotPasswordPage.ConfirmPassword, password);
            baseStep.Click(forgotPasswordPage.SubmitBtn);
            validate.AssertEquals(true, forgotPasswordPage.IncorrectOtpMsg.Displayed, "IncorrectOtpMsg is not displayed", false);
        }

        private void ValidatePreviousOtpInvalid(ForgotPasswordPage forgotPasswordPage, string previousOtp)
        {
            baseStep.ScrollToElement(forgotPasswordPage.ResendOtpBtn);
            baseStep.wait.GenericWait(2000);
            baseStep.Click(forgotPasswordPage.ResendOtpBtn);
            baseStep.wait.WaitTillPageLoad();
            validate.AssertEquals(true, forgotPasswordPage.OtpResentMsg.Displayed, "OtpResentMsg is not displayed", false);

            baseStep.ScrollToElement(forgotPasswordPage.EnterOtp);
            baseStep.ClearAndSendKeys(forgotPasswordPage.EnterOtp, previousOtp);
            baseStep.wait.GenericWait(2000);
            baseStep.Click(forgotPasswordPage.SubmitBtn);
            baseStep.wait.WaitTillPageLoad();
            validate.AssertEquals(true, forgotPasswordPage.IncorrectOtpMsg.Displayed, "IncorrectOtpMsg is not displayed", false);
        }

        private void ProcessFirstPasswordReset(ForgotPasswordPage forgotPasswordPage, string otp)
        {
            baseStep.ClearAndSendKeys(forgotPasswordPage.EnterOtp, otp);
            baseStep.Click(forgotPasswordPage.SubmitBtn);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.loginbelowpaswordupdatedmsg, 10);
            baseStep.ScrollToElement(forgotPasswordPage.LoginBelowPasswordUpdatedMsg);
            baseStep.wait.GenericWait(2000);
            validate.TakeStepFullScreenShot("Password is updated first time", Status.Info);
            baseStep.Click(forgotPasswordPage.LoginBelowPasswordUpdatedMsg);
        }

        private void ProcessSecondPasswordReset(ForgotPasswordPage forgotPasswordPage, string phonenumber, string password)
        {
            NavigateToForgotPassword(forgotPasswordPage);
            InitiatePasswordReset(forgotPasswordPage, phonenumber);
            HandleResendOtp(forgotPasswordPage);
            CompletePasswordReset(forgotPasswordPage, phonenumber, password);
        }

        private void ValidateThirdAttemptBlocked(ForgotPasswordPage forgotPasswordPage, string phonenumber)
        {
            baseStep.wait.GenericWait(5000);
            baseStep.wait.WaitTillPageLoad();
            NavigateToForgotPassword(forgotPasswordPage);

            baseStep.SendKeys(forgotPasswordPage.CellPhoneNumber, phonenumber);
            baseStep.Click(forgotPasswordPage.SendOtpBtn);
            baseStep.wait.WaitTillPageLoad();
            validate.AssertEquals(true, forgotPasswordPage.isOtpCountExceedMsgDisplayed(), "OtpCountExceedMsg is not displayed", true);
        }

        private void NavigateToForgotPassword(ForgotPasswordPage forgotPasswordPage)
        {
            try
            {
                bool ForgotPasswordLinkDisplayed;
                do
                {
                    baseStep.wait.GenericWait(3000);
                    baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.forgotpasswordlink, 5);
                    baseStep.Click(forgotPasswordPage.ForgotPasswordLink);
                    baseStep.wait.WaitTillPageLoad();
                    baseStep.wait.GenericWait(5000);
                    ForgotPasswordLinkDisplayed = forgotPasswordPage.ForgotPasswordLinkDisplayed();
                } while (ForgotPasswordLinkDisplayed);
            }
            catch
            {
                if (!forgotPasswordPage.isSendOtpBtn())
                {
                    baseStep.wait.GenericWait(5000);
                    baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.forgotpasswordlink, 5);
                    baseStep.Click(forgotPasswordPage.ForgotPasswordLink);
                }
            }
        }

        private void InitiatePasswordReset(ForgotPasswordPage forgotPasswordPage, string phonenumber)
        {
            baseStep.wait.WaitTillPageLoad();
            try
            {
                baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.sendotpbtn, 10);
            }
            catch
            {
                baseStep.Click(forgotPasswordPage.ForgotPasswordLink);
                baseStep.wait.WaitTillPageLoad();
            }

            baseStep.SendKeys(forgotPasswordPage.CellPhoneNumber, phonenumber);
            baseStep.Click(forgotPasswordPage.SendOtpBtn);
        }

        private void HandleResendOtp(ForgotPasswordPage forgotPasswordPage)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.resendotpbtn, 60);
            baseStep.ScrollToElement(forgotPasswordPage.ResendOtpBtn);
            baseStep.Click(forgotPasswordPage.ResendOtpBtn);
            baseStep.wait.WaitTillPageLoad();
            validate.AssertEquals(true, forgotPasswordPage.OtpResentMsg.Displayed, "OtpResentMsg is not displayed", false);

            if (!forgotPasswordPage.isOtpCountExceedMsgDisplayed())
            {
                baseStep.ScrollToElement(forgotPasswordPage.ResendOtpBtn);
                baseStep.Click(forgotPasswordPage.ResendOtpBtn);
                baseStep.wait.WaitTillPageLoad();
                validate.AssertEquals(true, forgotPasswordPage.OtpResentMsg.Displayed, "OtpResentMsg is not displayed", true);
                Report.ChildLog.Log(Status.Info, "OtpResentMsg is displayed again");
            }
        }

        private void CompletePasswordReset(ForgotPasswordPage forgotPasswordPage, string phonenumber, string password)
        {
            var pins = GetOtpPins(new OTPStorageAccount(), phonenumber);
            string resendOtp = pins[1].ToString();

            baseStep.ScrollToElement(forgotPasswordPage.EnterOtp);
            baseStep.ClearAndSendKeys(forgotPasswordPage.EnterOtp, resendOtp);
            baseStep.ClearAndSendKeys(forgotPasswordPage.Password, password);
            baseStep.ClearAndSendKeys(forgotPasswordPage.ConfirmPassword, password);
            baseStep.Click(forgotPasswordPage.SubmitBtn);
            baseStep.wait.WaitTillPageLoad();

            baseStep.wait.WaitForElementExistsLongWait(forgotPasswordPage.loginbelowpaswordupdatedmsg, 60);
            validate.TakeStepFullScreenShot("Password is updated Second Time", Status.Info);
            baseStep.ScrollToElement(forgotPasswordPage.LoginBelowPasswordUpdatedMsg);
            baseStep.Click(forgotPasswordPage.LoginBelowPasswordUpdatedMsg);
        }

        #endregion
    }
}
