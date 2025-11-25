namespace SanlamAutomation.Test.Pages
{
    public class WealthPage : WebDriverSession
    {
        public By yourbudgetscorefield = By.XPath("/html/body/app-root/app-layout/app-landing/div[1]/section[1]/div/div/div[2]/div[1]/div[1]/h3");
        public IWebElement YourBudgetScoreField => Driver.FindElement(yourbudgetscorefield);

        public By wealthupdatefield = By.XPath("/html/body/app-root/app-layout/app-landing/div[1]/section[1]/div/div/div[2]/div[2]/div/h3");
        public IWebElement WealthUpdateField => Driver.FindElement(wealthupdatefield);

        public By wealthupdatebtn = By.XPath("//*[@id=\"Wealth_For_You_Home_Page\"]");
        public IWebElement WealthUpdateBtn => Driver.FindElement(wealthupdatebtn);

        public By wealthupdatebtn_firsttimelogin = By.XPath("//*[@id=\"wealth_Score_Capture_Redirect\"]");
        public IWebElement WealthUpdateBtn_FirstTimeLogin => Driver.FindElement(wealthupdatebtn_firsttimelogin);

        public By callmebackbtn = By.XPath("/html/body/app-root/app-layout/app-wealth-score/div/div/button");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("/html/body/ngb-modal-window/div/div/div/footer/div[1]/button");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn => Driver.FindElement(callmebackpopupcutbtn);

        public By callmebackpopupsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg => Driver.FindElement(callmebackpopupsuccessmsg);

        public By vehiclefield = By.XPath("//*[@id=\"Vehicle_WealthScore\"]");
        public IWebElement VehicleField => Driver.FindElement(vehiclefield);

        public By recordupdatedsuccessmsg = By.XPath("//*[contains(text(),'Record updated Successfully')]");
        public IWebElement RecordUpdateSuccessMsg => Driver.FindElement(recordupdatedsuccessmsg);

        public By propertyfield = By.XPath("//*[@id=\"Property_WealthScore\"]");
        public IWebElement PropertyField => Driver.FindElement(propertyfield);

        public By retirementfield = By.XPath("//*[@id=\"RetirementSavings_WealthScore\"]");
        public IWebElement RetirementField => Driver.FindElement(retirementfield);

        public By investandsavingfield = By.XPath("//*[@id=\"OtherSavings_WealthScore\"]");
        public IWebElement InvestAndSavingField => Driver.FindElement(investandsavingfield);

        public By viewwealthscore_btn = By.XPath("//*[@id=\"ViewWealthScore_btn\"]");
        public IWebElement ViewWealthScore_Btn => Driver.FindElement(viewwealthscore_btn);

        public By wealthscore = By.XPath("//*[@id=\"ViewWealthScore_btn\"]");
        public IWebElement WealthScore => Driver.FindElement(wealthscore);

        public By wealthscoretext = By.XPath("//*[text()='Your Wealth Score']/following-sibling::div/div");
        public IWebElement WealthScoreText => Driver.FindElement(wealthscoretext);

        public By totalAssets = By.XPath("//*[contains(text(),'Total assets')]/following-sibling::div");
        public IWebElement TotalAssets => Driver.FindElement(totalAssets);

        public By liabilities = By.XPath("//*[contains(text(),'Total liabilities')]/following-sibling::div");
        public IWebElement Liabilities => Driver.FindElement(liabilities);

        public By balance = By.XPath("//*[contains(text(),'balance')]/following-sibling::div/span");
        public IWebElement Balance => Driver.FindElement(balance);

        // wealth call me back at home page

        public By callmebackbtn_wealthscore = By.XPath("//*[@id=\"WealthCallMeBack_HomePage\"]");
        public IWebElement CallMeBackBtn_WealthScore => Driver.FindElement(callmebackbtn_wealthscore);

        public By callmebackyesbtn_wealthscore = By.XPath("//*[@id=\"WealthCallMeBackYes_HomePage\"]");
        public IWebElement CallMeBackYesBtn_WealthScore => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn_wealthscore = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn_WealthScore => Driver.FindElement(callmebackpopupcutbtn);

    }
}
