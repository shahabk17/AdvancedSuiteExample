namespace SanlamAutomation
{
    public class ProfilePage : WebDriverSession
    {    

        public By profileicon = By.XPath("//*[@id=\"navbarDropdown\"]");
        public IWebElement ProfileIcon => Driver.FindElement(profileicon);

        public By profileoption = By.XPath("//*[@id=\"navbarDropdown\"]/following-sibling::ul/li/a[contains(text(),'Profile')]");
        public IWebElement ProfileOption => Driver.FindElement(profileoption);

        public By logout = By.XPath("//*[@id=\"navbarDropdown\"]/following-sibling::ul//a[contains(text(),'Logout')]");
        public IWebElement LogOut => Driver.FindElement(logout);

        public By profilecurrencyfield = By.XPath("//*[@id=\"Take Home Salary\"]");
        public IWebElement ProfileCurrencyField => Driver.FindElement(profilecurrencyfield);

        public By profilecurrencyfieldmsg = By.XPath("//*[@id=\"Take Home Salary\"]/following-sibling::div");
        public IWebElement ProfileCurrencyFieldMsg => Driver.FindElement(profilecurrencyfieldmsg);

        public By profileupdatebtn = By.XPath("//button[contains(text(),' Update Profile ')]");
        public IWebElement ProfileUpdateBtn => Driver.FindElement(profileupdatebtn);

        public By profileupdatemsg = By.XPath("//*[text()=' Profile Updated Successfully. ']");
        public IWebElement ProfileUpdateMsg => Driver.FindElement(profileupdatemsg);

        public By homeicon = By.XPath("//a[@href='/portal/home']");
        public IWebElement HomeIcon => Driver.FindElement(homeicon);

        public By newcellphonenumber = By.XPath("/html/body/app-root/app-layout/app-cell-number-update/section/div/div/div[2]/form/div/div[1]/input");
        public IWebElement NewCellphoneNumber => Driver.FindElement(newcellphonenumber);

        public By sendotpbtn = By.XPath("/html/body/app-root/app-layout/app-cell-number-update/section/div/div/div[2]/form/div/div[2]/button");
        public IWebElement SendOtpBtn => Driver.FindElement(sendotpbtn);

        public By resendotpbtn = By.XPath("/html/body/app-root/app-layout/app-cell-number-update/section/div/div/div[2]/form/div[2]/a");
        public IWebElement ReSendOtpBtn => Driver.FindElement(resendotpbtn);

        public By cellphonenumberupdatemsg = By.XPath("/html/body/app-root/app-layout/app-profile/section/div/div/div/form/div[1]/div");
        public IWebElement CellPhoneNumberUpdateMsg => Driver.FindElement(cellphonenumberupdatemsg);

        public By cellphonenumber = By.XPath("//*[@id=\"PhoneNumber\"]");
        public IWebElement CellPhoneNumber => Driver.FindElement(cellphonenumber);

        public By newotpsentmsg = By.XPath("/html/body/app-root/app-layout/app-cell-number-update/section/div/div/div[2]/form/div[1]/div[2]");
        public IWebElement NewOtpSentMsg => Driver.FindElement(newotpsentmsg);

        public By errormsgafter3otp = By.XPath("/html/body/app-root/app-layout/app-profile/section/div/div/div/form/div[2]/div");
        public IWebElement ErrorMsgAfter3OTP => Driver.FindElement(errormsgafter3otp);

        // Profile Page call me back button


        public By callmebackbtn = By.XPath("//*[@id=\"Call Me Back Profile\"]");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("//*[@id=\"CallMeBackYesProfile\"]");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn => Driver.FindElement(callmebackpopupcutbtn);

        public By callmebackpopupsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg => Driver.FindElement(callmebackpopupsuccessmsg);

    }
}
