namespace SanlamAutomation
{
    public class ForgotPasswordPage : WebDriverSession
    {
        public By loginbtn = By.XPath("//*[contains(@id,'Login') and contains(@id,'Header')]");
        public IWebElement LoginBtn => Driver.FindElement(loginbtn);

        public By forgotpasswordlink = By.XPath("//*[@id=\"forgotPassword\"]");
        public IWebElement ForgotPasswordLink => Driver.FindElement(forgotpasswordlink);
       
        public By cellphonenumber = By.XPath("//*[@id=\"SearchText\"]");
        public IWebElement CellPhoneNumber => Driver.FindElement(cellphonenumber);

        public By sendotpbtn = By.XPath("//*[@id=\"Send OTPForForgotPassword\"]");
        public IWebElement SendOtpBtn => Driver.FindElement(sendotpbtn);

        public By enterotp = By.XPath("//*[@id=\"otpAutoFill\"]");
        public IWebElement EnterOtp => Driver.FindElement(enterotp);

        public By password = By.XPath("//*[@id=\"Password\"]");
        public IWebElement Password => Driver.FindElement(password);

        public By confirmpassword = By.XPath("//*[@id=\"ConfirmPassword\"]");
        public IWebElement ConfirmPassword => Driver.FindElement(confirmpassword);

        public By submitbtn = By.XPath("//*[@id=\"SubmitForgotPassword\"]");
        public IWebElement SubmitBtn => Driver.FindElement(submitbtn);

        public By resendotpbtn = By.XPath("//*[@id=\"Resend OTPForForgotPassword\"]");
        public IWebElement ResendOtpBtn => Driver.FindElement(resendotpbtn);

        public By otprequiredmsg = By.XPath("/html/body/app-root/app-forgot/section[1]/div/div/div/div[1]/form/div[1]/div/div/div[1]/div/div/div");
        public IWebElement OtpRequiredMsg => Driver.FindElement(otprequiredmsg);

        public By passwordrequiredmsg = By.XPath("/html/body/app-root/app-forgot/section[1]/div/div/div/div[1]/form/div[1]/div/div/div[2]/div/div/div");
        public IWebElement PasswordRequiredMsg => Driver.FindElement(passwordrequiredmsg);

        public By confirmpasswordrequiredmsg = By.XPath("/html/body/app-root/app-forgot/section[1]/div/div/div/div[1]/form/div[1]/div/div/div[3]/div/div/div");
        public IWebElement ConfirmPasswordRequiredMsg => Driver.FindElement(confirmpasswordrequiredmsg);

        public By passwordmustmatchmsg = By.XPath("/html/body/app-root/app-forgot/section[1]/div/div/div/div[1]/form/div[1]/div/div/div[3]/div/div/div");
        public IWebElement PasswordMustMatchMsg => Driver.FindElement(passwordmustmatchmsg);

        public By incorrectotpmsg = By.XPath("/html/body/app-root/app-forgot/section[1]/div/div/div/div[1]/form/div[1]/div/div[1]");
        public IWebElement IncorrectOtpMsg => Driver.FindElement(incorrectotpmsg);

        public By otpresentmsg = By.XPath("/html/body/app-root/app-forgot/section[1]/div/div/div/div[1]/form/div[1]/div/div[1]");
        public IWebElement OtpResentMsg => Driver.FindElement(otpresentmsg);

        public By otpcountexceedmsg = By.XPath("/html/body/app-root/app-forgot/section[1]/div/div/div/div[1]/form/div[1]/div/div[1]");
        public IWebElement OtpCountExceedMsg => Driver.FindElement(otpcountexceedmsg);

        public By paswordupdatedmsg = By.XPath("/html/body/app-root/app-forgot/section[1]/div/div/div/div[1]/div/h2");
        public IWebElement PasswordUpdatedMsg => Driver.FindElement(paswordupdatedmsg);

        public By loginbelowpaswordupdatedmsg = By.XPath("//*[@id=\"LoginForgotPasswordPage\"]");
        public IWebElement LoginBelowPasswordUpdatedMsg => Driver.FindElement(loginbelowpaswordupdatedmsg);

        public bool ForgotPasswordLinkDisplayed()
        {
            bool stat = false;
            try
            {
                if (ForgotPasswordLink.Enabled)
                {
                    stat = true;
                    return stat;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return stat;
            }
        }

        public bool isOtpCountExceedMsgDisplayed()
        {
            bool stat = false;
            try
            {
                if (OtpCountExceedMsg.Displayed)
                {
                    stat = true;
                    return stat;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return stat;
            }
        }

        public bool isSendOtpBtn()
        {
            bool stat = false;
            try
            {
                if (SendOtpBtn.Displayed)
                {
                    stat = true;
                    return stat;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return stat;
            }
        }
    }
}
