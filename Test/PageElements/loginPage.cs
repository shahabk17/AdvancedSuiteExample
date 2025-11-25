namespace SanlamAutomation
{
    public class loginPage : WebDriverSession
    {
        public By idnumber = By.XPath("//*[@id=\"IdNumber\"]");
        public IWebElement IDNumber => Driver.FindElement(idnumber);

        public By registerbtn = By.XPath("//*[@id=\"id-number-form\"]/div[2]/div/button");
        public IWebElement RegisterBtn => Driver.FindElement(registerbtn);

        public By loginicononlandingpage = By.XPath("//*[contains(@id,\"Login\") and contains(@id,\"Header\")]");
        public IWebElement LoginIconOnLandingPage => Driver.FindElement(loginicononlandingpage);

        public By loginidnumber = By.XPath("//*[@id=\"logonIdentifier\"]");
        public IWebElement LoginIdNumber => Driver.FindElement(loginidnumber);

        public By loginidpassword = By.XPath("//*[@id=\"password\"]");
        public IWebElement LoginIDPassword => Driver.FindElement(loginidpassword);

        public By loginbtn = By.XPath("//*[@id=\"next\"]");
        public IWebElement LoginBtn => Driver.FindElement(loginbtn);

        public By invaliderrormsg = By.XPath("//*[@id=\"localAccountForm\"]/div[2]/p");
        public IWebElement InvalidErrorMsg => Driver.FindElement(invaliderrormsg);

        public By forgotpassword = By.XPath("//*[@id=\"forgotPassword\"]");
        public IWebElement ForgotPassword => Driver.FindElement(forgotpassword);
        public bool isLoginPageDisplayedAfterRegis => LoginIdNumber.Displayed;

        public By lockedaccountmsg = By.XPath("//*[contains(text(),'Your account has been locked')]");
        public IWebElement LockedAccountMsg => Driver.FindElement(lockedaccountmsg);

        public By registrationsuccessmsg = By.XPath("//p[@class='cc-success-message']");
        public IWebElement RegistrationSuccessMsg => Driver.FindElement(registrationsuccessmsg);

        public By loginpagetitle = By.XPath("//section[@class='login'] //h2");
        public IWebElement LoginPageTitle => Driver.FindElement(loginpagetitle);
    }
}