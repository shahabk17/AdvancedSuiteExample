namespace SanlamAutomation.Test.Pages
{
    public class BankAccountsPage : BaseStep
    {
        public By bankaccounticon = By.XPath("/html/body/app-root/app-layout/app-header/header/section/div/div/div[2]/nav[1]/ul/li[4]/a");
        public IWebElement BankAccountIcon => Driver.FindElement(bankaccounticon);

        public By callmebackbtn = By.XPath("//*[@name=\"Call Me Back Header\"]");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("//*[@name='CallMeBackYes']");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn => Driver.FindElement(callmebackpopupcutbtn);

        public By callmebackpopupsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg => Driver.FindElement(callmebackpopupsuccessmsg);

        public By linkaccountbtn = By.XPath("//*[contains(@value,\"Link an Account\")]");
        public IWebElement LinkAccountBtn => Driver.FindElement(linkaccountbtn);

        public By linkaccountaddbtn = By.XPath("//*[contains(@value,\"Link an Account\")]");
        public IWebElement LinkAccountAddBtn => Driver.FindElement(linkaccountaddbtn);

        public By iframe = By.XPath("//*[@id=\"container-fastlink\"]/div/iframe");
        public IWebElement Iframe => Driver.FindElement(iframe);

        public By username_textbox = By.XPath("//*[contains(text(),'User Name')]/following-sibling::div/input");
        public IWebElement UserName_TextBox => Driver.FindElement(username_textbox);

        public By password_textbox = By.XPath("//*[contains(text(),'Password')]/following-sibling::div/input");
        public IWebElement Password_TextBox => Driver.FindElement(password_textbox);

        public By iqbank_link = By.XPath("//*[contains(text(),'IQ Bank')]/ancestor::a");
        public IWebElement IqBank_Link => Driver.FindElement(iqbank_link);

        public By search_text = By.XPath("//*[@id=\"searchInputField\"]");
        public IWebElement Search_Text => Driver.FindElement(search_text);

        public By submitbtn = By.XPath("//*[contains(text(),'Submit')]");
        public IWebElement SubmitBtn => Driver.FindElement(submitbtn);

        public By saveandfinishbtn = By.XPath("//*[@id=\"save-finish-btn\"]");
        public IWebElement SaveAndFinishBtn => Driver.FindElement(saveandfinishbtn);

        public By callmebackbtn_linkaccount = By.XPath("//*[@id=\"CallMeBackLinkedaccounts\"]");
        public IWebElement CallMeBackBtn_LinkAccount => Driver.FindElement(callmebackbtn_linkaccount);

        public By callmebackyesbtn_linkaccount = By.XPath("//*[@id=\"BudgetCallBackYes\"]");
        public IWebElement CallMeBackYesBtn_LinkAccount => Driver.FindElement(callmebackyesbtn_linkaccount);

        public By callmebackpopupcutbtn_linkaccount = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn_LinkAccount => Driver.FindElement(callmebackpopupcutbtn_linkaccount);

        public By callmebackpopupsuccessmsg_linkaccount = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg_LinkAccount => Driver.FindElement(callmebackpopupsuccessmsg_linkaccount);


        public bool IsCallMeBackBtnDisplayed()
        {
            bool stat = false;
           
            try
            {
                wait.WaitForElementVisibility(callmebackbtn);
                if (CallMeBackBtn.Displayed)
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
