namespace SanlamAutomation
{
    [Author("Shahab Khan")]
    public class CreditInsightsPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly DBCreditCoach dBCreditCoach = new();
        private readonly CreditInsightsPage creditInsightsPage = new();

        /// <summary>
        /// Verifies credit consolidation LMS functionality
        /// </summary>
        /// <param name="IdNumber">User ID number</param>
        /// <param name="salary">User salary information</param>
        [Author("Shahab Khan")]
        public void VerifyCreditConsolidationLMS(string IdNumber, string salary)
        {

            var homePageSteps = new HomePageSteps();

            InitializeAndNavigate(homePageSteps, salary);
            ProcessCreditConsolidation(IdNumber);
        }

        /// <summary>
        /// Verifies call me back functionality on Credit Insights page
        /// </summary>
        /// <param name="IdNumber">User ID number for campaign validation</param>
        [Author("Shahab Khan")]
        public void verifyCallMeBackCreditInsightsPage(string IdNumber)
        {

            NavigateToCreditInsights();
            InitiateCallMeBack();
            HandleCallMeBackConfirmation(IdNumber);
        }

        /// <summary>
        /// Verifies download credit report functionality
        /// </summary>
        /// <param name="idNumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyDownloadCreditReportButton(string idNumber)
        {
            var homePage = new HomePage();


            var expectedFileName = GenerateExpectedFileName(idNumber);
            InitiateDownload(homePage);
            ValidateFileDownload(expectedFileName);
        }

        /// <summary>
        /// Verifies Solutions For You button functionality
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifySolutionsForYouButton()
        {

            var homePage = new HomePage();

            NavigateToSolutions();
            ValidateSolutionsPage(homePage);
        }

        /// <summary>
        /// Verifies Your Budget button functionality
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyYourBudgetButton()
        {

            var budgetPage = new BudgetPage();

            NavigateToBudget();
            ValidateBudgetPage(budgetPage);
        }

        /// <summary>
        /// Verifies Credit Consolidation button functionality
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyCreditConsolidationButton()
        {

            NavigateToCreditConsolidation();
            ValidateCreditConsolidation();
        }

        /// <summary>
        /// Verifies How You Measure Up scale
        /// </summary>
        /// <param name="idNumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyHowYouMeasureUpScale(string idNumber)
        {

            NavigateToScale();
            ValidateScaleMetrics(idNumber);
        }

        /// <summary>
        /// Verifies Take Home Salary Toward Debt information
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyTakeHomeSalaryTowardDebt()
        {

            var homePage = new HomePage();

            ValidateSalaryTowardDebt(homePage);
        }

        /// <summary>
        /// Verifies Overdue Amount information
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyOverdueAmount()
        {

            var homePage = new HomePage();

            ValidateOverdueAmount(homePage);
        }

        /// <summary>
        /// Verifies Money Left For Expenses information
        /// </summary>
        /// <param name="idNumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyMoneyLeftForExpenses(string idNumber)
        {

            NavigateToMoneyLeft();
            ValidateMoneyLeftMetrics(idNumber);
        }

        /// <summary>
        /// Verifies Estimated Monthly Interest Payments
        /// </summary>
        /// <param name="idNumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyEstimatedMonthlyInterestPayments(string idNumber)
        {

            ValidateMonthlyInterestPayments(idNumber);
        }

        /// <summary>
        /// Verifies Credit Score Trend for three months
        /// </summary>
        /// <param name="idNumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyCreditScoreTrendForThreeMonths(string idNumber)
        {

            NavigateToScoreTrend();
            ValidateScoreHistory(idNumber);
        }

        /// <summary>
        /// Verifies Credit Score Trend for less than three months
        /// </summary>
        /// <param name="idNumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyCreditScoreTrendForLessThanThreeMonths(string idNumber)
        {

            NavigateToScoreTrend();
            ValidateScoreTrendMessage();
        }

        /// <summary>
        /// Verifies Your Credit Summary information
        /// </summary>
        /// <param name="idNumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyYourCreditSummary(string idNumber)
        {
            NavigateToCreditSummary();
            ValidateCreditSummaryDetails(idNumber);
        }

        /// <summary>
        /// Verifies Your Credit Breakdown Buttons
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyYourCreditBreakdownButtons()
        {

            ValidateFullCreditButton();
            ValidateLearnAboutMoneyButton();
        }

        /// <summary>
        /// Verifies Your Credit Breakdown Fields
        /// </summary>
        /// <param name="idNumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyYourCreditBreakdownFields(string idNumber)
        {

            NavigateToCreditBreakdown();
            ValidateCreditBreakdownTabs(idNumber);
        }

        /// <summary>
        /// Method is used to check all fields on a page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyFieldsForTracking(string idnumber)
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditinsightsicon, 10);
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            MultipleClickOnElement(idnumber, "//button", 2);
            MultipleClickOnElement(idnumber, "//a", 17);            
        }

        #region Private Helper Methods

        private void MultipleClickOnElement(string idnumber, string elementType, int fieldIndex)
        {
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>Checking for {elementType} on Credit Insights Page<<<<<<<<<<<");
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
                        logTasks.Add(Task.Run(() => appInsights.GetLogsFromAppInsights(query, attributeKey, attributeValue, currentDateTime)));
                        if (validate.IsElementDisplayed(creditInsightsPage.callmebackpopupcutbtn))
                        {
                            baseStep.Click(creditInsightsPage.CallMeBackPopupCutBtn);
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
                if (validate.IsElementClickable(creditInsightsPage.creditinsightsicon))
                {
                    baseStep.ScrollToElement(creditInsightsPage.CreditInsightsIcon);
                    baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditinsightsicon, 10);
                    baseStep.Click(creditInsightsPage.CreditInsightsIcon);
                    baseStep.wait.WaitTillPageLoad();
                }
                totalFields = Driver.FindElements(By.XPath(elementType));
            }
            Task.WhenAll(logTasks).GetAwaiter().GetResult();
            appInsights.PrintCollectedLogs();
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>>>>Checked total fields: {j} of tag {elementType} and failure is not occur for user {idnumber}<<<<<<<<<<<<");
        }

        private void InitializeAndNavigate(HomePageSteps homePageSteps, string salary)
        {
            baseStep.wait.WaitTillPageLoad();
            homePageSteps.IsUserWelcomeTextHomePage();
            homePageSteps.WaitTillSalaryPopUpIsDisplayed(salary);
            baseStep.wait.WaitTillPageLoad();
            homePageSteps.IsdashboardPageDispalyed();
        }

        private void ProcessCreditConsolidation(string IdNumber)
        {
            NavigateToCreditInsights();
            InitiateCreditConsolidation();
            HandleCreditConsolidationConfirmation(IdNumber);
        }

        private void NavigateToCreditInsights()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
        }

        private void InitiateCreditConsolidation()
        {
            baseStep.wait.WaitForElementVisibilityLongWait(creditInsightsPage.creditconsolidationbtn, 60);
            baseStep.ScrollToElement(creditInsightsPage.YourBudgetBtn);
            validate.TakeStepFullScreenShot("CreditConsolidationBtn", Status.Info);
            baseStep.Click(creditInsightsPage.CreditConsolidationBtn);
        }

        private void HandleCreditConsolidationConfirmation(string IdNumber)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(creditInsightsPage.creditconsolidationyesbtn, 60);
            validate.TakeStepFullScreenShot("Credit Consolidation Yes Btn", Status.Info);
            baseStep.Click(creditInsightsPage.CreditConsolidationYesBtn);

            ValidateCreditConsolidationSuccess(IdNumber);
        }

        private void ValidateCreditConsolidationSuccess(string IdNumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.GenericWait(2000);
            baseStep.wait.WaitForElementVisibilityLongWait(creditInsightsPage.ccsuccessmsg, 60);

            var ccSuccessMsg = baseStep.getText.Text(creditInsightsPage.ccSuccessMsg);
            Assert.That(creditInsightsPage.ccSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, $"Credit Consolidation Success Message is Visible with text {ccSuccessMsg}");

            validate.TakeStepFullScreenShot("Credit Consolidation Success Message", Status.Info);
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.ccpopupcutbtn, 60);
            baseStep.Click(creditInsightsPage.CCPopupCutBtn);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Credit Consolidation");
        }

        private void InitiateCallMeBack()
        {
            do { genericUtils.ScrollTillHalfPage(); }
            while (!creditInsightsPage.CallMeBackBtn.Displayed);

            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(creditInsightsPage.CallMeBackBtn);
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(creditInsightsPage.CallMeBackBtn);
        }

        private string GenerateExpectedFileName(string idNumber)
        {
            var userName = dBCreditCoach.GetUserName(idNumber);
            return $"{DateTime.Now:yyyy-M-d}-{userName}-Your Summary Sanlam Credit Profile.pdf";
        }

        private void InitiateDownload(HomePage homePage)
        {
            baseStep.Click(homePage.ViewCreditInsightsButton);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.downloadcreditreportbutton, 10);
            validate.TakeStepFullScreenShot("Download Credit Report Button", Status.Info);
            baseStep.Click(creditInsightsPage.DownloadCreditReportButton);
        }

        private void ValidateFileDownload(string expectedFileName)
        {
            var isFileDownloaded = WaitForFileDownload(Properties.downloadFolder, expectedFileName, 30);
            validate.AssertEqualWithMessage(true, isFileDownloaded, "File is downloaded in Download folder", true);
        }

        private void NavigateToScale()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditinsightsicon, 20);
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
            EnsureCreditInsightsNavigation();
        }

        private void EnsureCreditInsightsNavigation()
        {
            if (!validate.IsElementClickable(creditInsightsPage.creditinsightsicon, 20))
            {
                baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            }
        }

        private void ValidateScaleMetrics(string idNumber)
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.scale_yourscore, 10);
            baseStep.ScrollToElement(creditInsightsPage.Scale_YourScore);

            var scaleInfo = dBCreditCoach.FetchScoreInformationTable(idNumber);
            ValidateScorePercentages(scaleInfo);
        }

        private void ValidateScorePercentages(Dictionary<string, object> scaleInfo)
        {
            var actualYourScore = genericUtils.SplitString(creditInsightsPage.Scale_YourScore.GetAttribute("style"), " ", 1).Replace("%;", "");
            var actualAverageAgeGroup = genericUtils.SplitString(creditInsightsPage.Scale_AgeGroup.GetAttribute("style"), " ", 1).Replace("%;", "");

            validate.AssertEqualWithMessage(scaleInfo["ScorePercent"], actualYourScore, "Your score is as expected", false);
            validate.AssertEqualWithMessage(scaleInfo["ScoreAge"], actualAverageAgeGroup, "Average age group is as expected", true);
        }

        private void NavigateToSolutions()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.downloadcreditreportbutton, 10);
            validate.TakeStepFullScreenShot("Solutions for you Button", Status.Info);
            baseStep.Click(creditInsightsPage.SolutionForYou);
        }

        private void ValidateSolutionsPage(HomePage homePage)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(homePage.viewsolutionspagetext, 20);
            baseStep.wait.WaitTillPageLoad();
            validate.AssertEqualWithMessage("Solutions for You", homePage.ViewSolutionsPageText.Text, "Successfully navigated to Solutions Page", false);
        }

        private void NavigateToBudget()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditinsightsicon, 20);
            baseStep.wait.GenericWait(2000);
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
            EnsureCreditInsightsNavigation();
            baseStep.Click(creditInsightsPage.YourBudgetBtn);
            baseStep.wait.WaitTillPageLoad();
        }

        private void ValidateBudgetPage(BudgetPage budgetPage)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(budgetPage.yourbudgettext, 20);
            validate.AssertEqualWithMessage("Your Budget", baseStep.getText.Text(budgetPage.YourBudgetText), "Successfully navigated to Your Budget Page", false);
        }

        private void NavigateToCreditConsolidation()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditinsightsicon, 20);
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
            EnsureCreditInsightsNavigation();
        }

        private void ValidateCreditConsolidation()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditconsolidationbtn, 10);
            validate.TakeStepFullScreenShot("Credit consolidation Button", Status.Info);
            baseStep.Click(creditInsightsPage.CreditConsolidationBtn);
            baseStep.wait.WaitTillPageLoad();

            baseStep.wait.WaitForElementVisibilityLongWait(creditInsightsPage.callbackrequesttext, 20);
            validate.AssertEqualWithMessage("Free callback request", baseStep.getText.Text(creditInsightsPage.CallbackRequestText), "Free callback request pop-up is visible", false);
            baseStep.Click(creditInsightsPage.CallMeBackPopupCutBtn);
        }

        private void ValidateSalaryTowardDebt(HomePage homePage)
        {
            baseStep.ScrollToElement(homePage.TakeHomeSalaryTowardsDebtPercentage);
            string expected_TakeHomeSalaryTowardDebt_Percentage = baseStep.getText.Text(homePage.TakeHomeSalaryTowardsDebtPercentage);
            string expected_TakeHomeSalaryTowardDebt_Text = baseStep.getText.Text(homePage.TakeHomeSalaryTowardsDebtText);

            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(creditInsightsPage.TakeHomeSalaryTowardDebt_Arrow);

            string actual_TakeHomeSalaryTowardDebt_Percentage = baseStep.getText.Text(creditInsightsPage.TakeHomeSalaryTowardDebt_Percentage);
            string actual_TakeHomeSalaryTowardDebt_Text = genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.TakeHomeSalaryTowardDebt_Text), "are ", 1);

            validate.AssertEqualWithMessage(expected_TakeHomeSalaryTowardDebt_Percentage, actual_TakeHomeSalaryTowardDebt_Percentage, "Take Home Salary Towards Debt Percentage is as expected.", false);
            validate.AssertEqualWithMessage(expected_TakeHomeSalaryTowardDebt_Text, actual_TakeHomeSalaryTowardDebt_Text, "Take Home Salary Towards Debt Text is as expected.", false);
        }

        private void ValidateOverdueAmount(HomePage homePage)
        {
            baseStep.Click(creditInsightsPage.OverdueAmount_Arrow);
            baseStep.wait.WaitForElementVisibilityLongWait(creditInsightsPage.overdueamount, 2);
            validate.TakeStepFullScreenShot("Overdue Amount");

            string actual_OverdueAmount = baseStep.getText.Text(creditInsightsPage.OverdueAmount);
            string actual_OverdueAmount_Text = genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.OverdueAmount_Text), "are ", 1);

            baseStep.Click(homePage.HomePageVisible);
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(homePage.OverdueAmount);

            string expected_OverdueAmount = baseStep.getText.Text(homePage.OverdueAmount);
            string expected_OverdueAmount_Text = baseStep.getText.Text(homePage.OverdueAmountText);

            validate.AssertEqualWithMessage(expected_OverdueAmount, actual_OverdueAmount, "Overdue Amount is as expected.", true);
            validate.AssertEqualWithMessage(expected_OverdueAmount_Text, actual_OverdueAmount_Text, "Overdue Amount Text is as expected.", true);
        }

        private bool WaitForFileDownload(string folderPath, string fileName, int timeoutInSeconds)
        {
            DateTime timeout = DateTime.Now.AddSeconds(timeoutInSeconds);
            while (DateTime.Now < timeout)
            {
                string check = Path.Combine(folderPath, fileName);
                if (File.Exists(Path.Combine(folderPath, fileName)))
                {
                    return true;
                }
                baseStep.wait.GenericWait(1000);
            }
            return false;
        }

        private void HandleCallMeBackConfirmation(string IdNumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(creditInsightsPage.CallMeBackYesBtn);

            ValidateCallMeBackSuccess(IdNumber);
        }

        private void NavigateToMoneyLeft()
        {
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(creditInsightsPage.MoneyLeftForExpenses_Arrow);
        }

        private void ValidateMoneyLeftMetrics(string idNumber)
        {
            baseStep.Click(creditInsightsPage.MoneyLeftForExpenses_Arrow);
            baseStep.wait.WaitForElementVisibilityLongWait(creditInsightsPage.moneyleftforexpenses_amount, 2);
            validate.TakeStepFullScreenShot("Money Left For Expenses");

            var actual_Amount = genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.MoneyLeftForExpenses_Amount), "R ", 1).Replace(",", "");
            var actual_Text = genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.MoneyLeftForExpenses_Text), "are ", 1);
            var grossSalary_Amount = genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.CreditSummary_TakeHomeSalary_Amount), "R ", 1).Replace(",", "");

            var expected_Amount = genericUtils.SplitString(dBCreditCoach.CreditHealthInfoTable(idNumber)["MoneyLeftForExpenses"].ToString(), ".00", 0);
            var moneyLeftPercentage = (int.Parse(actual_Amount) * 100) / (int.Parse(grossSalary_Amount));

            var expected_Text = GetMoneyLeftRiskLevel(moneyLeftPercentage);

            validate.AssertEqualWithMessage(expected_Amount, actual_Amount, "Money Left For Expenses Amount is as expected", true);
            validate.AssertEqualWithMessage(expected_Text, actual_Text, "Money Left For Expenses Text is as expected", true);
        }

        private string GetMoneyLeftRiskLevel(int percentage) => percentage switch
        {
            <= 25 => "Very High Risk",
            <= 50 => "High Risk",
            <= 75 => "Low Risk",
            <= 100 => "Very Low Risk",
            _ => "Unknown Risk Level"
        };

        private void ValidateMonthlyInterestPayments(string idNumber)
        {
            baseStep.Click(creditInsightsPage.EstimatedMonthlyInterestPayments_Arrow);
            baseStep.wait.WaitForElementVisibilityLongWait(creditInsightsPage.estimatedmonthlyinterestpayments_amount, 2);
            validate.TakeStepFullScreenShot("Estimated Monthly Interest Payments");

            var actual_Amount = genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.EstimatedMonthlyInterestPayments_Amount), "R ", 1).Replace(",", "");
            var actual_Text = genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.EstimatedMonthlyInterestPayments_Text), "are ", 1);

            var expected_Amount = genericUtils.SplitString(dBCreditCoach.CreditHealthInfoTable(idNumber)["MonthlyInterestPayment"].ToString(), ".00", 0);
            var loadCreditHealthInfo = baseStep.FetchDetailsFromSessionStorage("loadCreditHealthInfo");
            var expected_Text = loadCreditHealthInfo["creditHealthInfoUiTextResponse"]["monthlyInterestPayments"]["scoreType"].ToString();

            validate.AssertEqualWithMessage(expected_Amount, actual_Amount, "Monthly Interest Payment Amount is as expected", true);
            validate.AssertEqualWithMessage(expected_Text, actual_Text, "Monthly Interest Payment Text is as expected", true);
        }

        private void NavigateToScoreTrend()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditinsightsicon, 20);
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
            EnsureCreditInsightsNavigation();

            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.scoretrend_tab, 10);
            baseStep.Click(creditInsightsPage.ScoreTrend_Tab);
            baseStep.wait.WaitTillPageLoad();
        }

        private void ValidateScoreHistory(string idNumber)
        {
            dBCreditCoach.FetchIdnumberAvailableForMonths(idNumber);
            validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(creditInsightsPage.scorehistory),
                "Score history is displayed for three months", false);
        }

        private void ValidateScoreTrendMessage()
        {
            const string expectedMessage = "Score trend shows you how your credit score changes over time. Three months of data is needed to display this. Please visit again soon.";
            var actualMessage = baseStep.getText.Text(creditInsightsPage.ScoreTrend_Message).Trim();
            validate.AssertEqualWithMessage(expectedMessage, actualMessage,
                "Score trend message is visible for Ids having less than 3 months credit history", false);
        }

        private void NavigateToCreditSummary()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditinsightsicon, 20);
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
            EnsureCreditInsightsNavigation();
        }

        private void ValidateCreditSummaryDetails(string idNumber)
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditsummary_takehomesalary_amount, 10);
            baseStep.ScrollToElement(creditInsightsPage.CreditSummary_TakeHomeSalary_Amount);

            var metrics = GetActualCreditSummaryMetrics();
            var expectedMetrics = GetExpectedCreditSummaryMetrics(idNumber);

            ValidateCreditSummaryMetrics(metrics, expectedMetrics);
        }

        private void ValidateFullCreditButton()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.viewfullcredit_button, 10);
            baseStep.ScrollToElement(creditInsightsPage.ViewFullCredit_Button);
            baseStep.Click(creditInsightsPage.ViewFullCredit_Button);
            baseStep.wait.WaitTillPageLoad();
            validate.AssertEqualWithMessage(true, validate.IsElementClickable(creditInsightsPage.accountsummary_tab),
                "Account Summary page is visible", false);
        }

        private void ValidateLearnAboutMoneyButton()
        {
            NavigateBackToCreditInsights();
            InitiateLearnAboutMoney();
            ValidateFinancialPlanningPage();
        }

        private void NavigateToCreditBreakdown()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditinsightsicon, 20);
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
            EnsureCreditInsightsNavigation();
        }

        private void ValidateCreditBreakdownTabs(string idNumber)
        {

            VerifyYourCreditBreakdownTabs(idNumber, "Credit consists of");
            baseStep.ScrollToElement(creditInsightsPage.AmountSettled_Tab);
            baseStep.Click(creditInsightsPage.AmountSettled_Tab);
            VerifyYourCreditBreakdownTabs(idNumber, "Amount settled");
        }

        private void ValidateCallMeBackSuccess(string IdNumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.ccsuccessmsg, 60);

            var ccSuccessMsg = baseStep.getText.Text(creditInsightsPage.ccSuccessMsg);
            Assert.That(creditInsightsPage.ccSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, $"Success Message is Visible with text {ccSuccessMsg}");

            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(creditInsightsPage.CallMeBackPopupCutBtn);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Client Credit Insights Page");
        }

        private Dictionary<string, string> GetActualCreditSummaryMetrics()
        {
            return new Dictionary<string, string>
            {
                { "TakeHomeSalary", genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.CreditSummary_TakeHomeSalary_Amount), "R ", 1).Replace(",", "") },
                { "TotalCurrentBalance", genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.TotalCurrentBalance_Amount), "R ", 1).Replace(",", "") },
                { "TotalMonthlyPayments", genericUtils.SplitString(baseStep.getText.Text(creditInsightsPage.TotalMonthlyPayments_Amount), "R ", 1).Replace(",", "") }
            };
        }

        private Dictionary<string, string> GetExpectedCreditSummaryMetrics(string idNumber)
        {
            var creditHealthInfo = dBCreditCoach.CreditHealthInfoTable(idNumber);
            return new Dictionary<string, string>
            {
                { "TakeHomeSalary", Math.Round(double.Parse((string)creditHealthInfo["GrossIncome"])).ToString() },
                { "TotalCurrentBalance", Math.Round(double.Parse((string)creditHealthInfo["TotalCurrentBalance"])).ToString() },
                { "TotalMonthlyPayments", Math.Round(double.Parse((string)creditHealthInfo["TotalMonthlyInstalments"])).ToString() }
            };
        }

        private void ValidateCreditSummaryMetrics(Dictionary<string, string> actual, Dictionary<string, string> expected)
        {
            validate.AssertEqualWithMessage(expected["TakeHomeSalary"], actual["TakeHomeSalary"], "Take Home Salary is as expected", false);
            validate.AssertEqualWithMessage(expected["TotalCurrentBalance"], actual["TotalCurrentBalance"], "Total Current Balance is as expected", true);
            validate.AssertEqualWithMessage(expected["TotalMonthlyPayments"], actual["TotalMonthlyPayments"], "Total Monthly Payment is as expected", true);
        }

        private void NavigateBackToCreditInsights()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.creditinsightsicon, 20);
            baseStep.Click(creditInsightsPage.CreditInsightsIcon);
            baseStep.wait.WaitTillPageLoad();
        }

        private void InitiateLearnAboutMoney()
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.viewfullcredit_button, 10);
            baseStep.ScrollToElement(creditInsightsPage.LearnAboutMoney_Button);
            baseStep.Click(creditInsightsPage.LearnAboutMoney_Button);
            baseStep.wait.WaitTillPageLoad();
        }

        private void ValidateFinancialPlanningPage()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            validate.AssertEqualWithMessage("Financial Planning | Wills, Trusts & Advice | Sanlam",
                Driver.Title, "Financial Planning - Wills, Trusts & Advice - Sanlam page is visible", true);
            validate.TakeStepFullScreenShot("Financial Planning - Wills, Trusts & Advice - Sanlam page is visible", Status.Info);
        }

        private void VerifyYourCreditBreakdownTabs(string idNumber, string tabName)
        {
            baseStep.wait.WaitForElementClickableLongWait(creditInsightsPage.yourcreditbreakdown_homeloan, 20);
            baseStep.ScrollToElement(creditInsightsPage.YourCreditBreakdown_RetailAccount);

            var actualMetrics = GetActualCreditBreakdownMetrics();
            var expectedMetrics = GetExpectedCreditBreakdownMetrics(idNumber, tabName);

            ValidateCreditBreakdownMetrics(actualMetrics, expectedMetrics, tabName);
        }

        private Dictionary<string, string> GetActualCreditBreakdownMetrics()
        {
            return new Dictionary<string, string>
            {
                { "HomeLoan", baseStep.getText.Text(creditInsightsPage.YourCreditBreakdown_HomeLoan).Replace("%", "") },
                { "VehicleFinance", baseStep.getText.Text(creditInsightsPage.YourCreditBreakdown_VehicleFinance).Replace("%", "") },
                { "RetailAccount", baseStep.getText.Text(creditInsightsPage.YourCreditBreakdown_RetailAccount).Replace("%", "") },
                { "CreditCard", baseStep.getText.Text(creditInsightsPage.YourCreditBreakdown_CreditCard).Replace("%", "") },
                { "PersonalLoans", baseStep.getText.Text(creditInsightsPage.YourCreditBreakdown_PersonalLoans).Replace("%", "") }
            };
        }

        private Dictionary<string, string> GetExpectedCreditBreakdownMetrics(string idNumber, string tabName)
        {
            var creditHealthInfo = dBCreditCoach.CreditHealthInfoTable(idNumber);
            var metrics = tabName == "Credit consists of"
                ? GetCreditConsistsOfMetrics(creditHealthInfo)
                : GetAmountSettledMetrics(creditHealthInfo);

            return metrics.ToDictionary(
                kvp => kvp.Key,
                kvp => Math.Round(double.Parse((string)kvp.Value)).ToString()
            );
        }

        private Dictionary<string, object> GetCreditConsistsOfMetrics(Dictionary<string, object> creditHealthInfo)
        {
            return new Dictionary<string, object>
            {
                { "HomeLoan", creditHealthInfo["HomeLoanDebtBalancePercent"] },
                { "VehicleFinance", creditHealthInfo["CarLoanDebtBalancePercent"] },
                { "RetailAccount", creditHealthInfo["RetailerLoanDebtBalancePercent"] },
                { "CreditCard", creditHealthInfo["CreditLoanDebtBalancePercent"] },
                { "PersonalLoans", creditHealthInfo["PersonalLoanDebtBalancePercent"] }
            };
        }

        private Dictionary<string, object> GetAmountSettledMetrics(Dictionary<string, object> creditHealthInfo)
        {
            return new Dictionary<string, object>
            {
                { "HomeLoan", creditHealthInfo["HomeLoanTotalPaidOffPercent"] },
                { "VehicleFinance", creditHealthInfo["CarLoanTotalPaidOffPercent"] },
                { "RetailAccount", creditHealthInfo["RetailerLoanTotalPaidOffPercent"] },
                { "CreditCard", creditHealthInfo["CreditLoanTotalPaidOffPercent"] },
                { "PersonalLoans", creditHealthInfo["PersonalLoanTotalPaidOffPercent"] }
            };
        }

        private void ValidateCreditBreakdownMetrics(Dictionary<string, string> actual, Dictionary<string, string> expected, string tabName)
        {
            foreach (var (key, value) in actual)
            {
                validate.AssertEqualWithMessage(expected[key], value,
                    $"{key} under {tabName} tab is as expected", key == "HomeLoan" ? false : true);
            }
        }
        #endregion
    }
}