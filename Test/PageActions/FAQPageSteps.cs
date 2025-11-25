using SanlamAutomation.Test.Pages;

namespace SanlamAutomation.Test.Steps
{
    [Author("Shahab Khan")]
    public class FAQPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly AppInsights appInsights = new AppInsights();

        /// <summary>
        /// Verifies call me back functionality on FAQ page
        /// </summary>
        /// <param name="IdNumber">User ID number for campaign validation</param>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackFAQPage(string IdNumber)
        {
            var fAQPage = new FAQPage();
            var profilePage = new ProfilePage();
            var dBCreditCoach = new DBCreditCoach();

            NavigateToFAQ(profilePage, fAQPage);
            InitiateCallMeBack(fAQPage);
            HandleCallMeBackConfirmation(fAQPage, IdNumber, dBCreditCoach);
        }

        /// <summary>
        /// Validates FAQ page content against database
        /// </summary>
        [Author("Shahab Khan")]
        public void ValidateFAQPage()
        {
            var homePage = new HomePage();
            var fAQPage = new FAQPage();
            var dBCreditCoach = new DBCreditCoach();

            baseStep.ScrollToElement(homePage.FAQButton);
            homePage.FAQButton.Click();
            baseStep.wait.WaitTillPageLoad();

            ValidateFAQContent(fAQPage, dBCreditCoach.FAQQuestions());
        }

        /// <summary>
        /// Method is used to check all fields on a page 
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyFieldsForTracking(string idnumber)
        {
            var fAQPage = new FAQPage();
            var profilePage = new ProfilePage();
            NavigateToFAQ(profilePage, fAQPage);
            MultipleClickOnElement(idnumber, "//button", 0);
        }

        #region Private Helper Methods

        private void MultipleClickOnElement(string idnumber, string elementType, int fieldIndex)
        {
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>Checking for {elementType} on FAQs Page<<<<<<<<<<<");
            HomePage homePage = new();
            DBQueries dBQueries = new();
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
                        appInsights.CaptureClickLog($"Info\t{DateTime.UtcNow}\tClick on Element with attribute [{attributeKey}={attributeValue}]");
                        DBCreditCoach dBCreditCoach = new DBCreditCoach();
                        string userId = dBCreditCoach.GetUserId(idnumber);
                        string query = dBQueries.FetchCustomEvents(id, userId, currentDateTime);
                        logTasks.Add(Task.Run(() => appInsights.GetLogsFromAppInsights(query, attributeKey, attributeValue, currentDateTime))); 
                        if (validate.IsElementDisplayed(homePage.callmebackcutbtn))
                        {
                            baseStep.Click(homePage.CallMeBackCutBtn);
                        }
                        j++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(element.GetDomProperty);
                        Console.WriteLine(ex);
                    }
                }
                totalFields = Driver.FindElements(By.XPath(elementType));
            }
            Task.WhenAll(logTasks).GetAwaiter().GetResult();
            appInsights.PrintCollectedLogs();
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>>>>Checked total fields: {j} of tag {elementType} and failure is not occur for user {idnumber}<<<<<<<<<<<<");
        }

        private void NavigateToFAQ(ProfilePage profilePage, FAQPage fAQPage)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(profilePage.profileicon, 60);
            baseStep.ScrollToElement(profilePage.ProfileIcon);
            baseStep.Click(profilePage.ProfileIcon);
            baseStep.wait.WaitForElementClickableLongWait(fAQPage.faqicon, 60);
            baseStep.Click(fAQPage.FAQIcon);
            baseStep.wait.WaitTillPageLoad();
        }

        private void InitiateCallMeBack(FAQPage fAQPage)
        {
            do { genericUtils.ScrollTillHalfPage(); }
            while (!fAQPage.CallMeBackBtn.Displayed);

            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(fAQPage.CallMeBackBtn);
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(fAQPage.CallMeBackBtn);
        }

        private void HandleCallMeBackConfirmation(FAQPage fAQPage, string IdNumber, DBCreditCoach dBCreditCoach)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(fAQPage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(fAQPage.CallMeBackYesBtn);

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(fAQPage.callmebackpopupsuccessmsg, 60);

            var ccSuccessMsg = baseStep.getText.Text(fAQPage.CallMeBackPopupSuccessMsg);
            Assert.That(fAQPage.CallMeBackPopupSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, $"Success Message is Visible with text {ccSuccessMsg}");

            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(fAQPage.CallMeBackPopupCutBtn);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Client FAQ Page");
        }

        private void ValidateFAQContent(FAQPage fAQPage, Dictionary<string, string> fAQDictionary)
        {
            int questionNumber = 1;
            foreach (IWebElement question in fAQPage.FAQQuestions)
            {
                baseStep.ScrollToElement(question);
                var uiQuestion = baseStep.getText.Text(question);
                validate.AssertEqualWithMessage(true, fAQDictionary.ContainsKey(uiQuestion), "FAQ question in UI is active in DB", true);

                var dbAnswer = Regex.Replace(fAQDictionary[uiQuestion].ToString().Trim().Replace(" ", "").ToLower(), "<.*?>", "");
                baseStep.Click(question);
                baseStep.wait.WaitForElementVisibility(fAQPage.faqanswer);

                var uiAnswer = Regex.Replace(baseStep.getText.Text(fAQPage.FAQAnswer).Trim().Replace(" ", "").ToLower(), @"\s+", "");
                validate.AssertEqualWithMessage(true, uiAnswer.Contains(dbAnswer), "FAQ answer in UI is active in DB", true);

                validate.TakeStepFullScreenShot($"Question{questionNumber}", Status.Info);
                questionNumber++;
            }
        }

        #endregion
    }
}