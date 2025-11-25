namespace SanlamAutomation.Test.Pages
{
    public class CreditAccountsPage : WebDriverSession
    {
        public By creditaccounticon = By.XPath("/html/body/app-root/app-layout/app-header/header/section/div/div/div[2]/nav[1]/ul/li[3]/a");
        public IWebElement CreditAccountIcon => Driver.FindElement(creditaccounticon);

        public By callmebackbtn = By.XPath("//*[@id=\"CallMeBackHeaderAccount\"]");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("//*[@id=\"CallMeBackYesAccount\"]");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn => Driver.FindElement(callmebackpopupcutbtn);

        public By callmebackpopupsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg => Driver.FindElement(callmebackpopupsuccessmsg);

        public By accountsummaryheading = By.XPath("//h2[text()='Accounts Summary']");
        public IWebElement AccountSummaryHeading => Driver.FindElement(accountsummaryheading);

        public By accountsummarytab = By.XPath("//*[@id=\"AccountsSummary\"]");
        public IWebElement AccountSummaryTab => Driver.FindElement(accountsummarytab);

        public By accountsummarytab_cards = By.XPath("//div[@data-category=\"accounts-summary\"]");
        public IList<IWebElement> AccountSummaryTab_Cards => Driver.FindElements(accountsummarytab_cards);

        public By judgementandlegaltab = By.XPath("//*[@id=\"JudgementsAndLegalAction\"]");
        public IWebElement JudgementAndLegalTab => Driver.FindElement(judgementandlegaltab);

        public By judgementandlegaltab_cards = By.XPath("//div[@data-category=\"judgements-legal\"]");
        public IList<IWebElement> JudgementAndLegalTab_Cards => Driver.FindElements(judgementandlegaltab_cards);

        public By debtcouncellingtab = By.XPath("//*[@id=\"DebtCounselling\"]");
        public IWebElement DebtCounsellingTab => Driver.FindElement(debtcouncellingtab);

        public By debtcouncellingtab_cards = By.XPath("//div[@data-category=\"debt-counselling\"]/div");
        public IList<IWebElement> DebtCounsellingTab_Cards => Driver.FindElements(debtcouncellingtab_cards);

        public By cardsubtitle(string tab, int i) => By.XPath($"//div[@data-category='{tab}'][{i}]/div[2]//h4");
        public IWebElement CardSubtitle(string tab, int i) => Driver.FindElement(cardsubtitle(tab, i));
        public By cardaccnumber(string tab, int i) => By.XPath($"//div[@data-category='{tab}'][{i}]/div//*[contains(text(),'Ac No. ***')]");
        public IWebElement CardAccNumber(string tab, int i) => Driver.FindElement(cardaccnumber(tab, i));
        public By cardamount(string tab, int i) => By.XPath($"//div[@data-category='{tab}'][{i}]/div[2]//h2");
        public IWebElement CardAmount(string tab, int i) => Driver.FindElement(cardamount(tab, i));

        public By cardinstallmentamount(int i) => By.XPath($"//div[@data-category=\"accounts-summary\"][{i}]/div[2]/div/div/div[1]/label[2]");
        public IWebElement CardInstallmentSubtitle(int i) => Driver.FindElement(cardinstallmentamount(i));

        public By cardopeningbalancelimit(int i) => By.XPath($"//div[@data-category=\"accounts-summary\"][{i}]/div[2]/div/div/div[2]/label[2]");
        public IWebElement CardOpeningBalanceLimit(int i) => Driver.FindElement(cardopeningbalancelimit(i));

        public By cardaccountopeneddate(int i) => By.XPath($"//div[@data-category=\"accounts-summary\"][{i}]/div[2]/div/div/div[3]/label[2]");
        public IWebElement CardAccountOpenDate(int i) => Driver.FindElement(cardaccountopeneddate(i));

        public By cardoverdueamount(int i) => By.XPath($"//div[@data-category=\"accounts-summary\"][{i}]/div[2]/div/div/div[4]/label[2]");
        public IWebElement CardOverdueAmount(int i) => Driver.FindElement(cardoverdueamount(i));

        public By cardaccountstatus(int i) => By.XPath($"//div[@data-category=\"accounts-summary\"][{i}]/div[2]/div/div/div[5]/label[2]");
        public IWebElement CardAccountStatus(int i) => Driver.FindElement(cardaccountstatus(i));

        // Judgement and Legal
        public By cardcourtname(int i) => By.XPath($"//div[@data-category=\"judgements-legal\"][{i}]/div[2]/div/div/div/label[2]");
        public IWebElement CardCourtName(int i) => Driver.FindElement(cardcourtname(i));

        public By cardcasetype(int i) => By.XPath($"//div[@data-category=\"judgements-legal\"][{i}]/div[2]/div/div/div[2]/label[2]");
        public IWebElement CardCaseType(int i) => Driver.FindElement(cardcasetype(i));

        public By cardcasereason(int i) => By.XPath($"//div[@data-category=\"judgements-legal\"][{i}]/div[2]/div/div/div[3]/label[2]");
        public IWebElement CardCaseReason(int i) => Driver.FindElement(cardcasereason(i));

        public By cardcasenumber(int i) => By.XPath($"//div[@data-category=\"judgements-legal\"][{i}]/div[2]/div/div/div[4]/label[2]");
        public IWebElement CardCaseNumber(int i) => Driver.FindElement(cardcasenumber(i));

        //Debt Counselling
        public By carddebtreviewstatus(int i) => By.XPath($"//div[@data-category='debt-counselling'][{i}]/div[2]/div/div/div[2]");
        public IWebElement CardDebtReviewStatus(int i) => Driver.FindElement(carddebtreviewstatus(i));

        // General
        public By nocardsmsg(string tabName) => By.XPath($"//*[contains(text(),'Your record does not contain any {tabName}')]");
        public IWebElement NoCardsMsg(string tabName) => Driver.FindElement(nocardsmsg(tabName));
    }
}
