namespace SanlamAutomation
{
    public class RegistrationPage : BaseStep
    {

        public By idnumber = By.XPath("//*[contains(@id,\"Next_btn\")]/parent::div/parent::div/div/div/input");
        public IWebElement IDNumber => Driver.FindElement(idnumber);

        public By nextbtn = By.XPath("//*[contains(@id,\"Next_btn\")]");
        public IWebElement NextBtn => Driver.FindElement(nextbtn);

        public By firstname = By.XPath("//*[@formcontrolname=\"FirstName\"]");
        public IWebElement FirstNumber => Driver.FindElement(firstname);

        public By surname = By.XPath("//*[@formcontrolname=\"Surname\"]");
        public IWebElement SurName => Driver.FindElement(surname);

        public By cellphonenumber = By.XPath("//*[@formcontrolname=\"PhoneNumber\"]");
        public IWebElement CellPhoneNumber => Driver.FindElement(cellphonenumber);

        public By emailaddress = By.XPath("//*[@formcontrolname=\"PhoneNumber\"]/parent::div/parent::div/following-sibling::div/div[@class='form-field']/input[@formcontrolname='Email']");
        public IWebElement EmailAddress => Driver.FindElement(emailaddress);

        public By password = By.XPath("//*[@formcontrolname=\"PhoneNumber\"]/parent::div/parent::div/following-sibling::div[5]/div/input");
        public IWebElement Password => Driver.FindElement(password);


        public By confirmpassword = By.XPath("//*[@formcontrolname=\"PhoneNumber\"]/parent::div/parent::div/following-sibling::div[6]/div/input");
        public IWebElement ConfirmPassword => Driver.FindElement(confirmpassword);

        public By checkbox = By.XPath("//input[@id='AcceptTerms_RegistrationPage' or @id='AcceptTerms_SPL_RegistrationPage' or @id='AcceptTerms_HL_RegistrationPage']");
        public IWebElement Checkbox => Driver.FindElement(checkbox);

        public By registerbtn = By.XPath("//button[@id='Register_btn' or @id='SPLRegiter_btn' or @id='HLRegister_btn']");
        public IWebElement RegisterBtn => Driver.FindElement(registerbtn);

        public By securityquestioniframe = By.XPath("//*[@id=\"target\"]");
        public IWebElement SecurityQuestionIframe => Driver.FindElement(securityquestioniframe);

        public By securityquestion = By.XPath("//p[contains(text(),'Question')]/following-sibling::h4");
        public IWebElement SecurityQuestion => Driver.FindElement(securityquestion);

        public By securityquestioncounttext = By.XPath("//p[contains(text(),'Question')]");
        public IWebElement SecurityQuestionCountText => Driver.FindElement(securityquestioncounttext);

        public By aftersecurityquestionsubmitbtn = By.XPath("//*[contains(@id,'btn_step') and contains(@id,'_securityQuestionsSubmit_scs')]");
        public IWebElement AfterSecurityQuestionSubmitBtn => Driver.FindElement(aftersecurityquestionsubmitbtn);

        public By aftersecurityquestionsuccessmsg = By.XPath("//*[contains(text(),'We are currently verifying your security questions. Please wait a moment.')]");
        public IWebElement AfterSecurityQuestionSuccessMsg => Driver.FindElement(aftersecurityquestionsuccessmsg);

        // Auto Reg with Response URL

        public By aremail = By.XPath("//*[@id=\"Email_RegistrationPage_AutoReg\"]");
        public IWebElement AutoRegEmail => Driver.FindElement(aremail);

        public By arpassword = By.XPath("//*[@id='Password']");
        public IWebElement AutoRegPassword => Driver.FindElement(arpassword);


        public By arconfirmpassword = By.XPath("//*[@id='ConfirmPassword']");
        public IWebElement AutoRegConfirmPassword => Driver.FindElement(arconfirmpassword);

        public By arregisterbtn = By.XPath("//*[@id=\"SubmitCreatePassword\"]");
        public IWebElement AutoRegRegisterBtn => Driver.FindElement(arregisterbtn);

        // Auto Reg with given source url - normal

        public By aremail_sourceurl = By.XPath("//*[@id=\"Email_RegistrationPage_AutoReg\"]");
        public IWebElement AutoRegEmail_SourceUrl => Driver.FindElement(aremail_sourceurl);

        public By arpassword_sourceurl = By.XPath("//*[@id=\"Password_RegistrationPage_AutoReg\"]");
        public IWebElement AutoRegPassword_SourceUrl => Driver.FindElement(arpassword_sourceurl);


        public By arconfirmpassword_sourceurl = By.XPath("//*[@id=\"ConfirmPassword_RegistrationPage_AutoReg\"]");
        public IWebElement AutoRegConfirmPassword_SourceUrl => Driver.FindElement(arconfirmpassword_sourceurl);

        public By artAndcslider_sourceurl = By.XPath("//*[@id=\"AcceptTerms_AutoReg\"]");
        public IWebElement AutoRegTandCSlider_SourceUrl => Driver.FindElement(artAndcslider_sourceurl);

        public By arregisterbtn_sourceurl = By.XPath("//*[@id=\"Register_btn_AutoReg\"]");
        public IWebElement AutoRegRegisterBtn_SourceUrl => Driver.FindElement(arregisterbtn_sourceurl);

        // Auto Reg with given source url - hl

        public By aremail_sourceurl_hl = By.XPath("//*[@id=\"Email_HLRegistrationPage_AutoReg\"]");
        public IWebElement AutoRegEmail_SourceUrl_HL => Driver.FindElement(aremail_sourceurl_hl);

        public By arpassword_sourceurl_hl = By.XPath("//*[@id=\"Password_HLRegistrationPage_AutoReg\"]");
        public IWebElement AutoRegPassword_SourceUrl_HL => Driver.FindElement(arpassword_sourceurl_hl);


        public By arconfirmpassword_sourceurl_hl = By.XPath("//*[@id=\"ConfirmPassword_HLRegistrationPage_AutoReg\"]");
        public IWebElement AutoRegConfirmPassword_SourceUrl_HL => Driver.FindElement(arconfirmpassword_sourceurl_hl);

        public By arregisterbtn_sourceurl_hl = By.XPath("//*[@id=\"HLRegister_btn_AutoReg\"]");
        public IWebElement AutoRegRegisterBtn_SourceUrl_HL => Driver.FindElement(arregisterbtn_sourceurl_hl);

        // Auto Reg with given source url - spl

        public By aremail_sourceurl_spl = By.XPath("//*[@id=\"Email_SPLRegistrationPage_AutoReg\"]");
        public IWebElement AutoRegEmail_SourceUrl_SPL => Driver.FindElement(aremail_sourceurl_spl);

        public By arpassword_sourceurl_spl = By.XPath("//*[@id=\"Password_SPLRegistrationPage_AutoReg\"]");
        public IWebElement AutoRegPassword_SourceUrl_SPL => Driver.FindElement(arpassword_sourceurl_spl);


        public By arconfirmpassword_sourceurl_spl = By.XPath("//*[@id=\"ConfirmPassword_SPLRegistrationPage_AutoReg\"]");
        public IWebElement AutoRegConfirmPassword_SourceUrl_SPL => Driver.FindElement(arconfirmpassword_sourceurl_spl);

        public By arregisterbtn_sourceurl_spl = By.XPath("//*[@id=\"SPLRegister_btn_AutoReg\"]");
        public IWebElement AutoRegRegisterBtn_SourceUrl_SPL => Driver.FindElement(arregisterbtn_sourceurl_spl);

        public By artAndcslider_sourceurl_spl = By.XPath("//*[@id=\"AcceptTerms_SPL_RegistrationPage\"]");
        public IWebElement AutoRegTandCSlider_SourceUrl_SPL => Driver.FindElement(artAndcslider_sourceurl_spl);

        // Auto Reg with given source url - spl_ivr

        public By aremail_sourceurl_spl_ivr = By.XPath("//*[@id=\"Email_SPLRegistrationPage_IVR\"]");
        public IWebElement AutoRegEmail_SourceUrl_SPL_IVR => Driver.FindElement(aremail_sourceurl_spl_ivr);

        public By arpassword_sourceurl_spl_ivr = By.XPath("//*[@id=\"Password_SPLRegistrationPage_IVR\"]");
        public IWebElement AutoRegPassword_SourceUrl_SPL_IVR => Driver.FindElement(arpassword_sourceurl_spl_ivr);


        public By arconfirmpassword_sourceurl_spl_ivr = By.XPath("//*[@id=\"ConfirmPassword_SPLRegistrationPage_IVR\"]");
        public IWebElement AutoRegConfirmPassword_SourceUrl_SPL_IVR => Driver.FindElement(arconfirmpassword_sourceurl_spl_ivr);

        public By arregisterbtn_sourceurl_spl_ivr = By.XPath("//*[@id=\"SPLRegister_btn_IVR\"]");
        public IWebElement AutoRegRegisterBtn_SourceUrl_SPL_IVR => Driver.FindElement(arregisterbtn_sourceurl_spl_ivr);

        public By artAndcslider_sourceurl_spl_ivr = By.XPath("//*[@id=\"AcceptTerms_IVR\"]");
        public IWebElement AutoRegTandCSlider_SourceUrl_SPL_IVR => Driver.FindElement(artAndcslider_sourceurl_spl_ivr);

        //OTP popup



        public By sendotpbtn = By.XPath("//*[@id=\"Send OTP_btn\"]");
        public IWebElement SendOtpBtn => Driver.FindElement(sendotpbtn);


        public By submitotp = By.XPath("//*[@id=\"OTPSubmit_btn\"]");
        public IWebElement SubmitOTP => Driver.FindElement(submitotp);


        // already registered error messages

        public By alreadyexistmsg = By.XPath("//*[@id=\"id-number-form\"]/div[1]");
        public IWebElement AlreadyExistMessage => Driver.FindElement(alreadyexistmsg);


        // Security question failed msg

        public By securityquestionfailedmsg = By.XPath("//*[@id=\"id-number-form\"]/div[contains(@class,'alert alert-danger')]");
        public IWebElement SecurityQuestionFailedMsg => Driver.FindElement(securityquestionfailedmsg);

        public IWebElement EnterOTP(int i)
        {
            wait.WaitForElementVisibility(submitotp);
            return Driver.FindElement(By.XPath("//*[@id='otp" + i + "']"));

        }


        public IWebElement OptionSelect(string optionID)
        {
            wait.WaitForElementVisibility(securityquestion);
            return Driver.FindElement(By.XPath($"//*[contains(@id,'_step') and contains(@id,'scs_qes') and contains(@id,'option_{optionID}')]"));
        }

        public IWebElement OptionSelectWithText(string text)
        {
            wait.WaitForElementVisibility(securityquestion);
            return Driver.FindElement(By.XPath($"(//*[contains(text(),'{text}')]/preceding-sibling::input)[1]"));
        }
        public bool isPageDisplayed()
        {
            return FirstNumber.Displayed;
        }

        public bool IsSecurityQuestionDisplayed()
        {
            bool stat = false;
            try
            {
                wait.GenericWait(3000);
                wait.WaitForElementVisibilityLongWait(securityquestion, 30);
                if (SecurityQuestion.Displayed)
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

        public bool isAnswerSelect(string option)
        {
            bool stat = false;
            try
            {
                if (OptionSelect(option).Selected)
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

        public String SecurityQuestionText()
        {
            return SecurityQuestion.Text;
        }
    }
}
