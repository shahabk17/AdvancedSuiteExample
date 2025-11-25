namespace SanlamAutomation.Test.Pages
{
    public class FAQPage : WebDriverSession
    {
        public By faqicon = By.XPath("(//*[text()=' FAQs'])[1]");
        public IWebElement FAQIcon => Driver.FindElement(faqicon);

        public By callmebackbtn = By.XPath("//*[@id=\"Call Me Back Header Faq\"]");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("//*[@id=\"CallMeBackYesFaq\"]");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn => Driver.FindElement(callmebackpopupcutbtn);

        public By callmebackpopupsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg => Driver.FindElement(callmebackpopupsuccessmsg);

        public By faqquestions = By.XPath("//*[@class='card-header']");
        public IList<IWebElement> FAQQuestions => Driver.FindElements(faqquestions);

        public By faqanswer = By.XPath("//*[@class='collapse show ng-star-inserted']");
        public IWebElement FAQAnswer => Driver.FindElement(faqanswer);
    }
}
