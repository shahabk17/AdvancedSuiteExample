namespace SanlamAutomation.Test.Steps
{
    /// <summary>
    /// Handles all settings page related test actions and validations
    /// </summary>
    public class SettingsPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep;
        private readonly Validate validate;
        private readonly GenericUtils genericUtils;
        private readonly SettingsPage settingsPage;
        private readonly ProfilePage profilePage;
        private readonly DBCreditCoach dbCreditCoach;

        public SettingsPageSteps()
        {
            baseStep = new BaseStep();
            validate = new Validate();
            genericUtils = new GenericUtils();
            settingsPage = new SettingsPage();
            profilePage = new ProfilePage();
            dbCreditCoach = new DBCreditCoach();
        }

        /// <summary>
        /// Verifies Call Me Back functionality on settings page
        /// </summary>
        /// <param name="IdNumber">User's ID number for validation</param>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackSettingsPage(string IdNumber)
        {
            NavigateToSettingsPage();
            InitiateCallMeBack();
            ValidateCallMeBackSuccess();
            dbCreditCoach.GetCampaignSourceValidate(IdNumber, "Client Settings Page");
        }

        /// <summary>
        /// Method is used to check all fields on a page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyFieldsForTracking(string idnumber)
        {
            NavigateToSettingsPage();
            MultipleClickOnElement(idnumber, "//input", 1);
            MultipleClickOnElement(idnumber, "//button", 0);
        }

        #region Private Helper Methods

        private void MultipleClickOnElement(string idnumber, string elementType, int fieldIndex)
        {
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>Checking for {elementType} on Settings Page<<<<<<<<<<<");
            HomePage homePage = new();
            DBQueries dBQueries = new();
            AppInsights appInsights = new();
            IList<IWebElement> totalFields = Driver.FindElements(By.XPath(elementType));
            int j = 0;
            List<Task> logTasks = new List<Task>();

            for (int i = fieldIndex; i < totalFields.Count; i++)
            {
                IWebElement element = totalFields[i];
                if (validate.IsElementClickable(element, 2))
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

        private void NavigateToSettingsPage()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(profilePage.profileicon, 60);
            baseStep.ScrollToElement(profilePage.ProfileIcon);
            baseStep.Click(profilePage.ProfileIcon);
            baseStep.wait.WaitForElementClickableLongWait(settingsPage.settingicon, 60);
            baseStep.Click(settingsPage.SettingIcon);
        }

        private void InitiateCallMeBack()
        {
            baseStep.wait.WaitTillPageLoad();
            do { genericUtils.ScrollTillHalfPage(); }
            while (!settingsPage.CallMeBackBtn.Displayed);

            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(settingsPage.CallMeBackBtn);
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(settingsPage.CallMeBackBtn);
        }

        private void ValidateCallMeBackSuccess()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(settingsPage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(settingsPage.CallMeBackYesBtn);

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(settingsPage.callmebackpopupsuccessmsg, 60);
            string ccSuccessMsg = baseStep.getText.Text(settingsPage.CallMeBackPopupSuccessMsg);
            Assert.That(settingsPage.CallMeBackPopupSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, "Success Message is Visible with text " + ccSuccessMsg);
            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(settingsPage.CallMeBackPopupCutBtn);
        }

        #endregion
    }
}