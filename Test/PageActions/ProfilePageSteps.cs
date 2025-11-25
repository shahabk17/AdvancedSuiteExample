using SanlamAutomation.Test.Pages;

namespace SanlamAutomation
{
    /// <summary>
    /// Handles all profile page related test actions and validations
    /// </summary>
    public class ProfilePageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly ProfilePage profilePage = new();
        private readonly HomePage homePage = new();
        private readonly DBCreditCoach dbCreditCoach = new();

        /// <summary>
        /// Updates the take home salary information on profile page
        /// </summary>
        /// <param name="salary">Take home salary value to be updated</param>
        [Author("Shahab Khan")]
        public void UpdateInfoOnProfilePage(string salary)
        {
            NavigateToProfilePage();
            UpdateSalaryInformation(salary);
            ValidateAndSubmitUpdate();
            validate.TakeFullPageScreenShot($"Take Home Salary Entered is {salary}", Status.Pass);
            ReturnToHomePage();
        }

        /// <summary>
        /// Verifies the Call Me Back functionality on profile page
        /// </summary>
        /// <param name="IdNumber">User's ID number for validation</param>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackProfilePage(string IdNumber)
        {
            NavigateToProfilePage();
            InitiateCallMeBack();
            ValidateCallMeBackSuccess();
            dbCreditCoach.GetCampaignSourceValidate(IdNumber, "Client Profile Page");
        }

        /// <summary>
        /// Verifies LMS Debt Counselling process
        /// </summary>
        /// <param name="IdNumber">User's ID number for validation</param>
        [Author("Shahab Khan")]
        public void VerifyLMSDebtCouncelling(string IdNumber)
        {
            NavigateToProfilePage();
            UpdateSalaryToZero();
            ProcessDebtCounselling(IdNumber);
        }

        /// <summary>
        /// Verifies validation for non-zero take home salary
        /// </summary>
        /// <param name="IdNumber">User's ID number for validation</param>
        [Author("Shahab Khan")]
        public void VerifyLMSNonZeroTakehomeSalary(string IdNumber)
        {
            NavigateToProfilePage();
            ValidateZeroSalaryInput();
            ReturnToHomePage();
        }

        /// <summary>
        /// Performs logout operation
        /// </summary>
        [Author("Shahab Khan")]
        public void LogOut()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(profilePage.profileicon, 60);
            baseStep.ScrollToElement(profilePage.ProfileIcon);
            baseStep.Click(profilePage.ProfileIcon);
            baseStep.Click(profilePage.LogOut);
            baseStep.wait.WaitTillPageLoad();
        }

        /// <summary>
        /// Method is used to check all fields on a page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyFieldsForTracking(string idnumber)
        {
            NavigateToProfilePage();
            MultipleClickOnElement(idnumber, "//a", 18);
            MultipleClickOnElement(idnumber, "//input", 0);
            MultipleClickOnElement(idnumber, "//button", 0);
        }

        #region Private Helper Methods
        private void MultipleClickOnElement(string idnumber, string elementType, int fieldIndex)
        {
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>Checking for {elementType} on Profile Page<<<<<<<<<<<");
            HomePage homePage = new();
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
                        if (validate.IsElementDisplayed(profilePage.callmebackpopupcutbtn))
                        {
                            baseStep.Click(profilePage.CallMeBackPopupCutBtn);
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
                totalFields = Driver.FindElements(By.XPath(elementType));
            }
            Task.WhenAll(logTasks).GetAwaiter().GetResult();
            appInsights.PrintCollectedLogs();
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>>>>Checked total fields: {j} of tag {elementType} and failure is not occur for user {idnumber}<<<<<<<<<<<<");
        }

        private void NavigateToProfilePage()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(profilePage.profileicon, 60);
            baseStep.ScrollToElement(profilePage.ProfileIcon);
            baseStep.Click(profilePage.ProfileIcon);
            baseStep.wait.WaitForElementClickableLongWait(profilePage.profileoption, 60);
            baseStep.Click(profilePage.ProfileOption);
        }

        private void UpdateSalaryInformation(string salary)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(profilePage.profilecurrencyfield, 60);
            validate.TakeStepFullScreenShot("ProfilePage", Status.Info);
            baseStep.ClearAndSendKeys(profilePage.ProfileCurrencyField, salary);
            baseStep.ScrollToElement(profilePage.ProfileUpdateBtn);
        }

        private void ValidateAndSubmitUpdate()
        {
            if (profilePage.ProfileUpdateBtn.Enabled)
            {
                SubmitProfileUpdate();
            }
            else
            {
                RetryProfileUpdate();
            }
        }

        private void SubmitProfileUpdate()
        {
            validate.TakeStepFullScreenShot("ProfileUpdateBtn", Status.Info);
            baseStep.wait.GenericWait(2000);
            baseStep.Click(profilePage.ProfileUpdateBtn);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(profilePage.profileupdatemsg, 60);
            validate.TakeStepFullScreenShot("profileupdatemsg", Status.Info);
            baseStep.wait.GenericWait(3000);
        }

        private void RetryProfileUpdate()
        {
            baseStep.Click(profilePage.ProfileUpdateBtn);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(profilePage.profileupdatemsg, 60);
            baseStep.wait.GenericWait(5000);
        }

        private void ReturnToHomePage()
        {
            baseStep.Click(profilePage.HomeIcon);
            baseStep.wait.WaitTillPageLoad();
            validate.TakeStepFullScreenShot("HomePage", Status.Info);
        }

        private void InitiateCallMeBack()
        {
            do { genericUtils.ScrollTillFullPage(); }
            while (!profilePage.CallMeBackBtn.Displayed);

            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(profilePage.CallMeBackBtn);
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(profilePage.CallMeBackBtn);
        }

        private void ValidateCallMeBackSuccess()
        {
            baseStep.wait.WaitForElementClickableLongWait(profilePage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(profilePage.CallMeBackYesBtn);

            baseStep.wait.WaitForElementClickableLongWait(profilePage.callmebackpopupsuccessmsg, 60);
            string ccSuccessMsg = baseStep.getText.Text(profilePage.CallMeBackPopupSuccessMsg);
            Assert.That(profilePage.CallMeBackPopupSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, "Success Message is Visible with text " + ccSuccessMsg);
            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(profilePage.CallMeBackPopupCutBtn);
        }

        private void UpdateSalaryToZero()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ClearAndSendKeys(profilePage.ProfileCurrencyField, "0");
            baseStep.ScrollToElement(profilePage.ProfileUpdateBtn);

            if (profilePage.ProfileUpdateBtn.Enabled)
            {
                validate.TakeStepFullScreenShot("ProfileUpdateBtn", Status.Info);
                baseStep.wait.GenericWait(2000);
                baseStep.Click(profilePage.ProfileUpdateBtn);
                baseStep.wait.WaitTillPageLoad();
                baseStep.wait.WaitForElementVisibilityLongWait(profilePage.profileupdatemsg, 60);
                validate.TakeFullPageScreenShot("Take Home Salary Entered is 0", Status.Pass);
            }
            else
            {
                RetryProfileUpdate();
                validate.TakeFullPageScreenShot("Take Home Salary Entered is 0", Status.Pass);
            }
        }

        private void ProcessDebtCounselling(string IdNumber)
        {
            baseStep.Click(homePage.HomePageVisible);
            baseStep.wait.WaitTillPageLoad();

            dbCreditCoach.UpdateTotalCurrentBalance(IdNumber, "30000");
            string salary = (dbCreditCoach.SalaryTowardsDebt(IdNumber, 0.7)).ToString();

            baseStep.wait.WaitForElementVisibilityLongWait(homePage.salarypopup, 60);
            string salaryPopUpText = baseStep.getText.Text(homePage.SalaryPopUp);
            Report.ChildLog.Log(Status.Info, "Salary Pop Text - " + salaryPopUpText);

            try
            {
                do
                {
                    baseStep.wait.GenericWait(3000);
                    baseStep.ClearAndSendKeys(homePage.TakeHomeSalary, salary);
                    validate.TakeStepFullScreenShot("Take Home Salary Entered ", Status.Pass);
                    baseStep.Click(homePage.SalaryPopUpSubmitBtn);
                    baseStep.wait.WaitTillPageLoad();
                } while (validate.IsElementDisplayed(homePage.takehomesalary));
            }
            catch
            {
                Report.ChildLog.Log(Status.Info, $"salary entered: {salary}");
            }

            dbCreditCoach.GetCampaignSourceValidate(IdNumber, "Registration: Debt Counselling");
        }

        private void ValidateZeroSalaryInput()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ClearAndSendKeys(profilePage.ProfileCurrencyField, "0");
            baseStep.ScrollToElement(profilePage.ProfileUpdateBtn);
            validate.TakeStepFullScreenShot("ProfileUpdateBtn", Status.Info);
            baseStep.wait.GenericWait(2000);
            baseStep.Click(profilePage.ProfileUpdateBtn);
            baseStep.wait.WaitForElementVisibilityLongWait(profilePage.profilecurrencyfieldmsg, 10);
            baseStep.ScrollToElement(profilePage.ProfileCurrencyFieldMsg);
            validate.TakeFullPageScreenShot("Take Home Salary Entered is 0", Status.Pass);
        }

        #endregion
    }
}