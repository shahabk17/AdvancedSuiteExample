namespace SanlamAutomation.Test.Pages
{
    public  class SettingsPage : WebDriverSession
    {
        public By settingicon = By.XPath("//*[text()=' Settings']");
        public IWebElement SettingIcon => Driver.FindElement(settingicon);

        public By callmebackbtn = By.XPath("//*[@id=\"CallMeBackSettingPage\"]");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("//*[@id=\"CallMeBackYesSetting\"]");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn => Driver.FindElement(callmebackpopupcutbtn);

        public By callmebackpopupsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg => Driver.FindElement(callmebackpopupsuccessmsg);
    }
}
