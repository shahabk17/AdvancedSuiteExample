namespace SanlamAutomation.Test.Steps
{
    [Author("Shahab Khan")]
    public class CreditAccountsPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly DBCreditCoach dBCreditCoach = new();
        private readonly CreditAccountsPage creditAccountsPage = new();

        /// <summary>
        /// Verifies call me back functionality on Credit Accounts page
        /// </summary>
        /// <param name="IdNumber">User ID number for campaign validation</param>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackCreditAccountsPage(string IdNumber)
        {
            Report.ChildLog.Log(Status.Info, $"Method >>>>>>>>>>{MethodBase.GetCurrentMethod().Name}<<<<<<<<<<");
            NavigateToCreditAccounts();
            InitiateCallMeBack();
            HandleCallMeBackConfirmation(IdNumber);
        }

        /// <summary>
        /// Verifies all credit account tab cards and their details
        /// </summary>
        /// <param name="idNumber">User ID number for data validation</param>
        [Author("Shahab Khan")]
        public void VerifyCreditAccountTabCards(string idNumber)
        {
            Report.ChildLog.Log(Status.Info, $"Method >>>>>>>>>>{MethodBase.GetCurrentMethod().Name}<<<<<<<<<<");
            NavigateToAccountsTab();
            VerifyAllTabCards(idNumber);
        }

        /// <summary>
        /// Method is used to check all fields on a page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyFieldsForTracking(string idnumber)
        {
            baseStep.wait.WaitForElementClickableLongWait(creditAccountsPage.creditaccounticon, 10);
            baseStep.Click(creditAccountsPage.CreditAccountIcon);
            MultipleClickOnElement(idnumber, "//button", 0);
            MultipleClickOnElement(idnumber, "//a", 17);
        }

        #region Private Helper Methods

        private void MultipleClickOnElement(string idnumber, string elementType, int fieldIndex)
        {
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>Checking for {elementType} on Credit Accounts Page<<<<<<<<<<<");
            DBQueries dBQueries = new();
            AppInsights appInsights = new();
            IList<IWebElement> totalFields = Driver.FindElements(By.XPath(elementType));
            int j = 0;
            List<Task> logTasks = new List<Task>();

            for (int i = fieldIndex; i < totalFields.Count; i++)
            {

                IWebElement element = totalFields[i];
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
                        appInsights.GetLogsFromAppInsights(query, attributeKey, attributeValue, currentDateTime);
                        if (validate.IsElementDisplayed(creditAccountsPage.callmebackpopupcutbtn))
                        {
                            baseStep.Click(creditAccountsPage.CallMeBackPopupCutBtn);
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
                if (validate.IsElementClickable(creditAccountsPage.creditaccounticon))
                {
                    baseStep.ScrollToElement(creditAccountsPage.CreditAccountIcon);
                    baseStep.wait.WaitForElementClickableLongWait(creditAccountsPage.creditaccounticon, 10);
                    baseStep.Click(creditAccountsPage.CreditAccountIcon);
                    baseStep.wait.WaitTillPageLoad();
                }
                totalFields = Driver.FindElements(By.XPath(elementType));
            }
            Task.WhenAll(logTasks).GetAwaiter().GetResult();
            appInsights.PrintCollectedLogs();
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>>>>Checked total fields: {j} of tag {elementType} and failure is not occur for user {idnumber}<<<<<<<<<<<<");
        }

        private void NavigateToCreditAccounts()
        {
            baseStep.Click(creditAccountsPage.CreditAccountIcon);
            baseStep.wait.WaitTillPageLoad();
        }

        private void InitiateCallMeBack()
        {
            do
            {
                genericUtils.ScrollTillHalfPage();
                baseStep.wait.GenericWait(2000);
            }
            while (!creditAccountsPage.CallMeBackBtn.Displayed);

            baseStep.ScrollToElement(creditAccountsPage.CallMeBackBtn);
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(creditAccountsPage.CallMeBackBtn);
        }

        private void HandleCallMeBackConfirmation(string IdNumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(creditAccountsPage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(creditAccountsPage.CallMeBackYesBtn);

            ValidateCallMeBackSuccess(IdNumber);
        }

        private void ValidateCallMeBackSuccess(string IdNumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(creditAccountsPage.callmebackpopupsuccessmsg, 60);

            var ccSuccessMsg = baseStep.getText.Text(creditAccountsPage.CallMeBackPopupSuccessMsg);
            Assert.That(validate.IsElementDisplayed(creditAccountsPage.callmebackpopupsuccessmsg));
            Report.ChildLog.Log(Status.Info, $"Success Message is Visible with text {ccSuccessMsg}");

            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(creditAccountsPage.CallMeBackPopupCutBtn);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Client Credit Accounts Page");
        }

        private void NavigateToAccountsTab()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditAccountsPage.creditaccounticon, 20);
            baseStep.Click(creditAccountsPage.CreditAccountIcon);
            baseStep.wait.WaitTillPageLoad();
        }

        private void VerifyAllTabCards(string idNumber)
        {
            VerifyAccountSummaryCards(idNumber);
            VerifyJudgementAndLegalCards(idNumber);
            VerifyDebtCouncellingCards(idNumber);
        }

        [Author("Shahab Khan")]
        private void VerifyAccountSummaryCards(string idNumber)
        {
            Report.ChildLog.Log(Status.Info, $"Method >>>>>>>>>>{MethodBase.GetCurrentMethod().Name}<<<<<<<<<<");
            baseStep.wait.WaitForElementClickableLongWait(creditAccountsPage.accountsummarytab, 10);
            baseStep.Click(creditAccountsPage.AccountSummaryTab);

            var numberOfCards = creditAccountsPage.AccountSummaryTab_Cards.Count;
            if (numberOfCards > 0)
            {
                ValidateAccountSummaryTabCardsDetails(idNumber, numberOfCards);
            }
            else
            {
                ValidateNoCardsMessage("Accounts Summary");
            }
        }

        [Author("Shahab Khan")]
        private void VerifyJudgementAndLegalCards(string idNumber)
        {
            Report.ChildLog.Log(Status.Info, $"Method >>>>>>>>>>{MethodBase.GetCurrentMethod().Name}<<<<<<<<<<");
            baseStep.Click(creditAccountsPage.JudgementAndLegalTab);

            var numberOfCards = creditAccountsPage.JudgementAndLegalTab_Cards.Count;
            if (numberOfCards > 0)
            {
                ValidateJudgementAndLegalTabCardsDetails(idNumber, numberOfCards);
            }
            else
            {
                ValidateNoCardsMessage("Judgments & Legal");
            }
        }

        [Author("Shahab Khan")]
        private void VerifyDebtCouncellingCards(string idNumber)
        {
            Report.ChildLog.Log(Status.Info, $"Method >>>>>>>>>>{MethodBase.GetCurrentMethod().Name}<<<<<<<<<<");
            baseStep.Click(creditAccountsPage.DebtCounsellingTab);

            var numberOfCards = creditAccountsPage.DebtCounsellingTab_Cards.Count;
            if (numberOfCards > 0)
            {
                ValidateDebtCouncellingTabCardsDetails(idNumber, numberOfCards);
            }
            else
            {
                ValidateNoCardsMessage("Debt Counselling");
            }
        }
        private void ValidateNoCardsMessage(string tabName)
        {
            var noCardMsg = baseStep.getText.Text(creditAccountsPage.NoCardsMsg(tabName));
            var expectedMsg = $"Your record does not contain any {tabName}";
            validate.AssertEqualWithMessage(expectedMsg, noCardMsg, $"No card message is visible: {noCardMsg}", false);
        }

        private void ValidateAccountSummaryTabCardsDetails(string idNumber, int numberOfCards)
        {
            for (int i = 1; i <= numberOfCards; i++)
            {
                string cardSubtitle = baseStep.getText.Text(creditAccountsPage.CardSubtitle("accounts-summary", i));
                string cardAccNumber = genericUtils.SplitString(baseStep.getText.Text(creditAccountsPage.CardAccNumber("accounts-summary", i)), " ", 2);
                var dic = dBCreditCoach.FetchAccountInformationTable(idNumber, cardSubtitle, cardAccNumber);
                validate.AssertEqualWithMessage(cardSubtitle, dic["Name"], $"Card title is as expected: {cardSubtitle}", false);

                string cardAmount = genericUtils.SplitString(baseStep.getText.Text(creditAccountsPage.CardAmount("accounts-summary", i)), " ", 1).Replace(",", "");
                string actualCardAmount = Math.Round(double.Parse(dic["Current_Balance"].ToString())).ToString();
                validate.AssertEqualWithMessage(cardAmount, actualCardAmount, $"{cardSubtitle} Card Amount is as expected: {cardAmount}", true);

                string cardInstallmentAmount = baseStep.getText.Text(creditAccountsPage.CardInstallmentSubtitle(i)).Replace(",", "");
                string actualCardInstallmentSubtitle = Math.Round(double.Parse(dic["Installment_Amount"].ToString())).ToString();
                validate.AssertEqualWithMessage(cardInstallmentAmount, actualCardInstallmentSubtitle, $"{cardSubtitle} Card Installment Amount is as expected: {cardInstallmentAmount}", true);

                string cardOpeningBalanceLimit = baseStep.getText.Text(creditAccountsPage.CardOpeningBalanceLimit(i)).Replace(",", "");
                string actualOpeningBalanceCreditLimit = Math.Round(double.Parse(dic["Opening_Balance_Credit_Limit"].ToString())).ToString();
                validate.AssertEqualWithMessage(cardOpeningBalanceLimit, actualOpeningBalanceCreditLimit, $"{cardSubtitle} Card Opening_Balance_Credit_Limit is as expected: {cardOpeningBalanceLimit}", true);

                string cardAccountOpenDate = baseStep.getText.Text(creditAccountsPage.CardAccountOpenDate(i)).Replace(",", "");
                string actualDateAccountOpened = DateTime.Parse(dic["Date_Account_Opened"].ToString()).ToString("dd.MM.yy");
                validate.AssertEqualWithMessage(cardAccountOpenDate, actualDateAccountOpened, $"{cardSubtitle} Card Date_Account_Opened is as expected: {cardAccountOpenDate}", true);

                string cardOverdueAmount = baseStep.getText.Text(creditAccountsPage.CardOverdueAmount(i)).Replace(",", "");
                string actualAmountOverdue = Math.Round(double.Parse(dic["Amount_Overdue"].ToString())).ToString();
                validate.AssertEqualWithMessage(cardOverdueAmount, actualAmountOverdue, $"{cardSubtitle} Card Amount_Overdue is as expected: {cardOverdueAmount}", true);

                string cardAccountStatus = baseStep.getText.Text(creditAccountsPage.CardAccountStatus(i));
                string actualAccStatus = dic["Acc_Status"].ToString();
                validate.AssertEqualWithMessage(cardAccountStatus, actualAccStatus, $"{cardSubtitle} Card Acc_Status is as expected: {cardAccountStatus}", true);
            }
        }

        private void ValidateJudgementAndLegalTabCardsDetails(string idNumber, int numberOfCards)
        {
            for (int i = 1; i <= numberOfCards; i++)
            {
                string cardSubtitle = baseStep.getText.Text(creditAccountsPage.CardSubtitle("judgements-legal", i));
                var dic = dBCreditCoach.FetchJudgmentInformationTable(idNumber, cardSubtitle);
                validate.AssertEqualWithMessage(cardSubtitle, dic["Plaintiff"], $"Card title is as expected: {cardSubtitle}", false);

                string cardAmount = genericUtils.SplitString(baseStep.getText.Text(creditAccountsPage.CardAmount("judgements-legal", i)), " ", 1).Replace(",", "");
                string actualCardAmount = Math.Round(double.Parse(dic["Amount"].ToString()), 2).ToString();
                validate.AssertEqualWithMessage(cardAmount, actualCardAmount, $"{cardSubtitle} Card Amount is as expected: {cardAmount}", true);

                string cardCourt_Name = baseStep.getText.Text(creditAccountsPage.CardCourtName(i));
                validate.AssertEqualWithMessage(cardCourt_Name, dic["Court_Name"].ToString(), $"{cardSubtitle} Card Court_Name is as expected: {cardCourt_Name}", true);

                string cardCase_Type = baseStep.getText.Text(creditAccountsPage.CardCaseType(i));
                validate.AssertEqualWithMessage(cardCase_Type, dic["Case_Type"].ToString(), $"{cardSubtitle} Card Case_Type is as expected: {cardCase_Type}", true);

                string cardCase_Reason = baseStep.getText.Text(creditAccountsPage.CardCaseReason(i));
                validate.AssertEqualWithMessage(cardCase_Reason, dic["Case_Reason"].ToString(), $"{cardSubtitle} Card Case_Reason is as expected: {cardCase_Reason}", true);

                string cardCase_Number = baseStep.getText.Text(creditAccountsPage.CardCaseNumber(i));
                string actualAmountOverdue = dic["Case_Number"].ToString();
                validate.AssertEqualWithMessage(cardCase_Number, actualAmountOverdue, $"{cardSubtitle} Card Case_Number is as expected: {cardCase_Number}", true);
            }
        }

        private void ValidateDebtCouncellingTabCardsDetails(string idNumber, int numberOfCards)
        {
            for (int i = 1; i <= numberOfCards; i++)
            {
                string cardSubtitle = baseStep.getText.Text(creditAccountsPage.CardSubtitle("debt-counselling", i));
                var dic = dBCreditCoach.FetchDebtRestructureReviewTable(idNumber, cardSubtitle);
                validate.AssertEqualWithMessage(cardSubtitle, $"{dic["Counsellor_First_Name"]} {dic["Counsellor_Last_Name"]}", $"Card title is as expected: {cardSubtitle}", false);

                string cardCounsellorRegistrationNumber = baseStep.getText.Text(creditAccountsPage.CardAmount("debt-counselling", i)).Replace("(", "").Replace(")", "");
                string actualCardCounsellorRegistrationNumber = dic["Counsellor_Registration_Number"].ToString();
                validate.AssertEqualWithMessage(cardCounsellorRegistrationNumber, actualCardCounsellorRegistrationNumber, $"{cardSubtitle} Card Counsellor_Registration_Number is as expected: {cardCounsellorRegistrationNumber}", true);

                string cardDebtReviewStatus = baseStep.getText.Text(creditAccountsPage.CardDebtReviewStatus(i));
                string actualCardDebtReviewStatus = dic["Debt_Review_Status"].ToString();
                validate.AssertEqualWithMessage(cardDebtReviewStatus, actualCardDebtReviewStatus, $"{cardSubtitle} Card Debt_Review_Status Amount is as expected: {cardDebtReviewStatus}", true);

            }
        }

        #endregion
    }
}