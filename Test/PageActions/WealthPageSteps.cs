namespace SanlamAutomation.Test.Steps
{
    /// <summary>
    /// Handles all wealth page related test actions and validations
    /// </summary>
    public class WealthPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly WealthPage wealthPage = new();
        private readonly HomePage homePage = new();
        private readonly DBCreditCoach dbCreditCoach = new();

        /// <summary>
        /// Verifies Call Me Back functionality on wealth page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackWealthPage(string IdNumber)
        {
            HandleWealthUpdate();
            InitiateCallMeBack();
            ProcessCallMeBackRequest(IdNumber);
        }

        /// <summary>
        /// Verifies Call Me Back functionality on wealth home page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackWealthHomePage(string IdNumber)
        {
            NavigateToHomePage();
            HandleWealthScoreCallMeBack();
            ProcessCallMeBackRequest(IdNumber, "Client Wealth Balance");
        }

        /// <summary>
        /// Updates wealth score with provided values
        /// </summary>
        [Author("Shahab Khan")]
        public void UpdateWealthScore()
        {
            HandleInitialWealthUpdate();
            UpdateWealthFields();
            ValidateWealthScore();
        }

        /// <summary>
        /// Verifies wealth score calculation
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyWealthScoreCalculation(string IdNumber)
        {
            UpdateWealthScore();
            ValidateWealthScoreCalculation(IdNumber);
        }

        #region Private Helper Methods

        private void HandleWealthUpdate()
        {
            baseStep.wait.WaitTillPageLoad();
            try
            {
                HandleFirstTimeLogin();
            }
            catch
            {
                HandleRegularLogin();
            }
        }

        private void HandleFirstTimeLogin()
        {
            baseStep.wait.WaitForElementClickableLongWait(wealthPage.wealthupdatebtn_firsttimelogin, 60);
            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(wealthPage.WealthUpdateBtn_FirstTimeLogin);
            baseStep.Click(wealthPage.WealthUpdateBtn_FirstTimeLogin);
        }

        private void HandleRegularLogin()
        {
            baseStep.wait.WaitForElementClickableLongWait(wealthPage.wealthupdatebtn, 10);
            baseStep.ScrollToElement(wealthPage.WealthUpdateField);
            baseStep.Click(wealthPage.WealthUpdateBtn);
        }

        private void InitiateCallMeBack()
        {
            baseStep.wait.WaitTillPageLoad();
            do { genericUtils.ScrollTillHalfPage(); }
            while (!wealthPage.CallMeBackBtn.Displayed);

            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(wealthPage.CallMeBackBtn);
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(wealthPage.CallMeBackBtn);
        }

        private void ProcessCallMeBackRequest(string IdNumber, string source = "Client Wealth Page")
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(wealthPage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(wealthPage.CallMeBackYesBtn);

            ValidateCallMeBackSuccess();
            dbCreditCoach.GetCampaignSourceValidate(IdNumber, source);
        }

        private void ValidateCallMeBackSuccess()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(wealthPage.callmebackpopupsuccessmsg, 60);
            string ccSuccessMsg = baseStep.getText.Text(wealthPage.CallMeBackPopupSuccessMsg);
            Assert.That(validate.IsElementDisplayed(wealthPage.callmebackpopupsuccessmsg));
            Report.ChildLog.Log(Status.Info, "Success Message is Visible with text " + ccSuccessMsg);
            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(wealthPage.CallMeBackPopupCutBtn);
        }

        private void NavigateToHomePage()
        {
            baseStep.Click(homePage.HomePageVisible);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.GenericWait(2000);
        }

        private void HandleWealthScoreCallMeBack()
        {
            if (validate.IsElementDisplayed(wealthPage.callmebackbtn_wealthscore))
            {
                InitiateWealthScoreCallMeBack();
            }
            else
            {
                RefreshAndInitiateWealthScoreCallMeBack();
            }
        }

        private void InitiateWealthScoreCallMeBack()
        {
            baseStep.ScrollToElement(wealthPage.CallMeBackBtn_WealthScore);
            baseStep.Click(wealthPage.CallMeBackBtn_WealthScore);
        }

        private void RefreshAndInitiateWealthScoreCallMeBack()
        {
            RefreshPage();
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(wealthPage.callmebackbtn_wealthscore, 20);
            baseStep.ScrollToElement(wealthPage.CallMeBackBtn_WealthScore);
            baseStep.Click(wealthPage.CallMeBackBtn_WealthScore);
        }

        private void HandleInitialWealthUpdate()
        {
            try
            {
                HandleFirstTimeLogin();
            }
            catch
            {
                HandleRegularLogin();
            }
        }

        private void UpdateWealthFields()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(wealthPage.propertyfield, 10);

            EnterValueOfWealthPageField(wealthPage.PropertyField, "100000");
            EnterValueOfWealthPageField(wealthPage.VehicleField, "20000");
            EnterValueOfWealthPageField(wealthPage.RetirementField, "500");
            EnterValueOfWealthPageField(wealthPage.InvestAndSavingField, "100");
        }

        private void ValidateWealthScore()
        {
            baseStep.ScrollToElement(wealthPage.ViewWealthScore_Btn);
            baseStep.Click(wealthPage.ViewWealthScore_Btn);
            baseStep.wait.WaitTillPageLoad();

            ValidateWealthScoreText();
            ValidateBalanceCalculation();
        }

        private void ValidateWealthScoreText()
        {
            string wealthScoreText = baseStep.getText.Text(wealthPage.WealthScoreText);
            Assert.That(wealthPage.WealthScoreText.Displayed);
            validate.AssertEqualWithMessage(false, string.IsNullOrEmpty(wealthScoreText), $"WealthScore is updated: {wealthScoreText}", false);
        }

        private void ValidateBalanceCalculation()
        {
            int totalAssets = int.Parse(genericUtils.SplitString(baseStep.getText.Text(wealthPage.TotalAssets), " ", 1).Replace(",", ""));
            int totalLiabilities = int.Parse(genericUtils.SplitString(baseStep.getText.Text(wealthPage.Liabilities), " ", 1).Replace(",", ""));
            int balance = int.Parse(baseStep.getText.Text(wealthPage.Balance).Replace("R ", "").Replace(",", ""));
            int actualBalance = totalAssets - totalLiabilities;

            validate.AssertEqualWithMessage(balance, actualBalance, $"Balance is matched", true);
        }

        private void ValidateWealthScoreCalculation(string IdNumber)
        {
            int liabilities = int.Parse(genericUtils.SplitString(baseStep.getText.Text(wealthPage.Liabilities), " ", 1).Replace(",", ""));
            double wealthScore = dbCreditCoach.CalculateWealthScore(IdNumber, liabilities);
            string expectedWealthScoreText = GetExpectedWealthScoreText(wealthScore);
            string actualWealthScoreText = baseStep.getText.Text(wealthPage.WealthScoreText);

            validate.AssertEqualWithMessage(expectedWealthScoreText.ToLower(), actualWealthScoreText.ToLower(), "expectedCreditScoreStatus is equal to actual", false);
        }

        private string GetExpectedWealthScoreText(double wealthScore)
        {
            return wealthScore switch
            {
                > 0.1 => "Low risk",
                >= -0.1 and <= 0.1 => "Medium risk",
                < -0.1 => "High risk",
                _ => "Invalid Credit Score"
            };
        }

        private void RefreshPage()
        {
            Driver.Navigate().Refresh();
            baseStep.wait.WaitTillPageLoad();
        }

        private void EnterValueOfWealthPageField(IWebElement field, string value)
        {
            baseStep.ClearAndSendKeys(field, value);
            Report.ChildLog.Log(Status.Info, $"Value entered in field: {value}");
            validate.TakeStepFullScreenShot($"Field updated with value {value}", Status.Info);
            baseStep.wait.GenericWait(2000);
        }
        #endregion
    }
}
