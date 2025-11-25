using SanlamAutomation.Test.Pages;

namespace SanlamAutomation.Test.Steps
{
    [Author("Shahab Khan")]
    public class BankAccountsPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();

        /// <summary>
        /// Verifies call me back functionality on Bank Accounts page
        /// </summary>
        /// <param name="IdNumber">User ID number for campaign validation</param>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackBankAccountsPage(string IdNumber)
        {
            var bankAccountsPage = new BankAccountsPage();
            var dBCreditCoach = new DBCreditCoach();

            NavigateToBankAccounts(bankAccountsPage);
            InitiateCallMeBack(bankAccountsPage);
            HandleCallMeBackConfirmation(bankAccountsPage, IdNumber, dBCreditCoach);
        }

        /// <summary>
        /// Verifies call me back functionality for Budget Tool Link Account page
        /// </summary>
        /// <param name="IdNumber">User ID number for campaign validation</param>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackBudgetTool_LinkAccPage(string IdNumber)
        {
            var bankAccountsPage = new BankAccountsPage();
            var budgetPage = new BudgetPage();
            var dBCreditCoach = new DBCreditCoach();

            NavigateToBankAccounts(bankAccountsPage);
            LinkBankAccount(bankAccountsPage);
            HandleBudgetToolCallMeBack(bankAccountsPage, budgetPage, IdNumber, dBCreditCoach);
        }

        /// <summary>
        /// Method is used to check all fields on a page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyFieldsForTracking(string idnumber)
        {
            BankAccountsPage bankAccountsPage = new();
            baseStep.wait.WaitForElementClickableLongWait(bankAccountsPage.bankaccounticon, 10);
            baseStep.Click(bankAccountsPage.BankAccountIcon);
            MultipleClickOnElement(bankAccountsPage, idnumber, "//button", 0);
            MultipleClickOnElement(bankAccountsPage, idnumber, "//input", 0);
            MultipleClickOnElement(bankAccountsPage, idnumber, "//a", 17);
        }

        #region Private Helper Methods
        private void MultipleClickOnElement(BankAccountsPage bankAccountsPage, string idnumber, string elementType, int fieldIndex)
        {
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>Checking for {elementType} on Bank Account Page<<<<<<<<<<<");
            DBQueries dBQueries = new();
            AppInsights appInsights = new();
            IList<IWebElement> totalFields = Driver.FindElements(By.XPath(elementType));
            int j = 0;
            List<Task> logTasks = new List<Task>();
            for (int i = fieldIndex; i < totalFields.Count; i++)
            {

                IWebElement element = totalFields[i];
                genericUtils.ScrollTillFullPage();
                if (validate.IsElementClickable(element, 5))
                {
                    try
                    {
                        string id = element.GetDomAttribute("id");
                        baseStep.ScrollToElement(element);
                        var (attributeKey, attributeValue) = appInsights.GetElementIdentifier(element);
                        var currentDateTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                        baseStep.Click(element);
                        baseStep.wait.WaitTillPageLoad();
                        Report.ChildLog.Log(Status.Info, $"Click on Element with attribute [{attributeKey}={attributeValue}]");
                        DBCreditCoach dBCreditCoach = new DBCreditCoach();
                        string userId = dBCreditCoach.GetUserId(idnumber);
                        string query = dBQueries.FetchCustomEvents(id, userId, currentDateTime);
                        logTasks.Add(Task.Run(() => appInsights.GetLogsFromAppInsights(query, attributeKey, attributeValue, currentDateTime)));
                        if (validate.IsElementDisplayed(bankAccountsPage.callmebackpopupcutbtn))
                        {
                            baseStep.Click(bankAccountsPage.CallMeBackPopupCutBtn);
                        }
                        var windows = Driver.WindowHandles.Count;
                        if (windows > 1)
                        {
                            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                            Driver.Close();
                            Driver.SwitchTo().Window(Driver.WindowHandles.First());
                        }
                        j++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(element.GetDomProperty);
                        Console.WriteLine(ex);
                    }
                }
                if (validate.IsElementClickable(bankAccountsPage.bankaccounticon))
                {
                    baseStep.ScrollToElement(bankAccountsPage.BankAccountIcon);
                    baseStep.wait.WaitForElementClickableLongWait(bankAccountsPage.bankaccounticon, 10);
                    baseStep.Click(bankAccountsPage.BankAccountIcon);
                    baseStep.wait.WaitTillPageLoad();
                }
                totalFields = Driver.FindElements(By.XPath(elementType));
            }

            Task.WhenAll(logTasks).GetAwaiter().GetResult();
            appInsights.PrintCollectedLogs();
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>>>>Checked total fields: {j} of tag {elementType} and failure is not occur for user {idnumber}<<<<<<<<<<<<");
        }

        private void NavigateToBankAccounts(BankAccountsPage bankAccountsPage)
        {
            baseStep.Click(bankAccountsPage.BankAccountIcon);
            baseStep.wait.WaitTillPageLoad();
        }

        private void InitiateCallMeBack(BankAccountsPage bankAccountsPage)
        {
            do { genericUtils.ScrollTillHalfPage(); }
            while (!bankAccountsPage.IsCallMeBackBtnDisplayed());

            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(bankAccountsPage.CallMeBackBtn);
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(bankAccountsPage.CallMeBackBtn);
        }

        private void HandleCallMeBackConfirmation(BankAccountsPage bankAccountsPage, string IdNumber, DBCreditCoach dBCreditCoach)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(bankAccountsPage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(bankAccountsPage.CallMeBackYesBtn);

            ValidateCallMeBackSuccess(bankAccountsPage, IdNumber, dBCreditCoach);
        }

        private void ValidateCallMeBackSuccess(BankAccountsPage bankAccountsPage, string IdNumber, DBCreditCoach dBCreditCoach)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(bankAccountsPage.callmebackpopupsuccessmsg, 60);

            var ccSuccessMsg = baseStep.getText.Text(bankAccountsPage.CallMeBackPopupSuccessMsg);
            Assert.That(bankAccountsPage.CallMeBackPopupSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, $"Success Message is Visible with text {ccSuccessMsg}");

            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(bankAccountsPage.CallMeBackPopupCutBtn);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Client Bank Accounts Page");
        }

        private void LinkBankAccount(BankAccountsPage bankAccountsPage)
        {
            try
            {
                InitiateLinkAccount(bankAccountsPage);
                ProcessIQBankLinking(bankAccountsPage);
            }
            catch (Exception e)
            {
                Report.ChildLog.Log(Status.Info, $"Error occur while link with account is {e}");
            }
            Driver.SwitchTo().DefaultContent();
            baseStep.wait.WaitTillPageLoad();
        }

        private void InitiateLinkAccount(BankAccountsPage bankAccountsPage)
        {
            try
            {
                baseStep.wait.GenericWait(2000);
                baseStep.Click(bankAccountsPage.LinkAccountBtn);
                baseStep.wait.WaitTillPageLoad();
            }
            catch
            {
                genericUtils.ScrollAtTopOfThePage();
                baseStep.wait.GenericWait(2000);
                baseStep.Click(bankAccountsPage.LinkAccountBtn);
                baseStep.wait.WaitTillPageLoad();
            }
        }

        private void ProcessIQBankLinking(BankAccountsPage bankAccountsPage)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(bankAccountsPage.iframe, 120);
            baseStep.wait.GenericWait(5000);

            Driver.SwitchTo().Frame(bankAccountsPage.Iframe);
            baseStep.wait.WaitForElementVisibilityLongWait(bankAccountsPage.search_text, 20);
            baseStep.SendKeys(bankAccountsPage.Search_Text, "iq");
            baseStep.wait.WaitTillPageLoad();

            baseStep.wait.WaitForElementClickableLongWait(bankAccountsPage.iqbank_link, 60);
            baseStep.Click(bankAccountsPage.IqBank_Link);

            EnterBankCredentials(bankAccountsPage);
        }

        private void EnterBankCredentials(BankAccountsPage bankAccountsPage)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(bankAccountsPage.username_textbox, 60);
            baseStep.SendKeys(bankAccountsPage.UserName_TextBox, "bank_494652");
            baseStep.SendKeys(bankAccountsPage.Password_TextBox, "bank@iqb");
            baseStep.ScrollToElement(bankAccountsPage.SubmitBtn);
            baseStep.Click(bankAccountsPage.SubmitBtn);
            baseStep.wait.WaitForElementVisibilityLongWait(bankAccountsPage.saveandfinishbtn, 60);
            baseStep.ScrollToElement(bankAccountsPage.SaveAndFinishBtn);
            baseStep.Click(bankAccountsPage.SaveAndFinishBtn);
        }

        private void HandleBudgetToolCallMeBack(BankAccountsPage bankAccountsPage, BudgetPage budgetPage, string IdNumber, DBCreditCoach dBCreditCoach)
        {
            do
            {
                baseStep.Click(budgetPage.BudgetIcon);
                baseStep.wait.WaitTillPageLoad();
            } while (!validate.IsElementDisplayed(budgetPage.callmebackbtn_linkaccount));

            InitiateBudgetToolCallMeBack(bankAccountsPage, budgetPage);
            ValidateBudgetToolCallMeBack(bankAccountsPage, IdNumber, dBCreditCoach);
        }

        private void InitiateBudgetToolCallMeBack(BankAccountsPage bankAccountsPage, BudgetPage budgetPage)
        {
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(budgetPage.CallMeBackBtn_LinkAccount);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(bankAccountsPage.callmebackyesbtn_linkaccount, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(bankAccountsPage.CallMeBackYesBtn_LinkAccount);
        }

        private void ValidateBudgetToolCallMeBack(BankAccountsPage bankAccountsPage, string IdNumber, DBCreditCoach dBCreditCoach)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(bankAccountsPage.callmebackpopupsuccessmsg_linkaccount, 60);

            var ccSuccessMsg = baseStep.getText.Text(bankAccountsPage.CallMeBackPopupSuccessMsg_LinkAccount);
            Assert.That(bankAccountsPage.CallMeBackPopupSuccessMsg_LinkAccount.Displayed);
            Report.ChildLog.Log(Status.Info, $"Success Message is Visible with text {ccSuccessMsg}");

            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(bankAccountsPage.CallMeBackPopupCutBtn_LinkAccount);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Budget Tool");
        }

        #endregion
    }
}