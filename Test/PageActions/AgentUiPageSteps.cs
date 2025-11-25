namespace SanlamAutomation.Test.Steps
{
    [Author("Shahab Khan")]
    public class AgentUiPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly AgentUiPage agentUiPage = new();
        private readonly DBCreditCoach dBCreditCoach = new();

        public string expectedSplQualifier;
        public string expectedCCQualifier;
        public string expectedCreditConsolQualifier;
        public string expectedCapfinQualifier;
        public string expectedFinance27Qualifier;
        public string expectedStoreCardsQualifier;

        /// <summary>
        /// Performs login to Agent UI with specified credentials
        /// </summary>
        /// <param name="IdNumber">User ID number</param>
        /// <param name="emailId">Agent email ID</param>
        /// <param name="idPassword">Encrypted password</param>
        [Author("Shahab Khan")]
        public void LoginToAgentUI(string IdNumber, string emailId, string idPassword)
        {
            var password = genericUtils.Decrypt(idPassword, 3);
            baseStep.wait.WaitTillPageLoad();
            HandleAgentLogin(agentUiPage, emailId, password);
            SearchForUser(agentUiPage, IdNumber);
        }

        /// <summary>
        /// Activates or deactivates user based on provided flag
        /// </summary>
        /// <param name="activateUser">True to activate, False to deactivate</param>
        [Author("Shahab Khan")]
        public void ActivateOrDeactivateUserFromAgentUi(bool activateUser)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.GenericWait(2000);
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.activatebtn, 10);

            var activateBtnText = baseStep.getText.Text(agentUiPage.ActivateBtn);
            HandleUserActivation(agentUiPage, activateUser, activateBtnText);
        }

        /// <summary>
        /// Updates user's credit history through Agent UI
        /// </summary>
        [Author("Shahab Khan")]
        public void PullUserCreditHistory()
        {
            UpdateCreditReport(agentUiPage);
        }

        /// <summary>
        /// Verifies tile qualifiers for different products on Agent UI
        /// </summary>
        /// <param name="Idnumber">User ID number</param>
        /// <param name="isQualifiedSPL">SPL qualification status</param>
        [Author("Shahab Khan")]
        public void VerifyTileQualifiersOnAgentUi(string Idnumber, bool isQualifiedSPL)
        {
            var agentUiPage = new AgentUiPage();
            NavigateToCustomerDashboard(agentUiPage);
            VerifyAllQualifiers(Idnumber, isQualifiedSPL);
            VerifyTileQualifiersOnHomePage();
        }

        /// <summary>
        /// Verifies SPL tile qualifier on Agent UI
        /// </summary>
        /// <param name="Idnumber">User ID number</param>
        /// <param name="isQualifiedSPL">SPL qualification status</param>
        [Author("Shahab Khan")]
        public void VerifySPLTileQualifier(string Idnumber, bool isQualifiedSPL)
        {
            var solutionPageSteps = new SolutionPageSteps();

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.solutiontab, 10);
            baseStep.wait.GenericWait(5000);

            baseStep.ScrollToElement(agentUiPage.SolutionTab);
            baseStep.Click(agentUiPage.SolutionTab);
            validate.TakeStepFullScreenShot("Solution Page is Visible", Status.Info);

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.splqualifymsg, 10);

            baseStep.ScrollToElement(agentUiPage.SplQualifyMsg);
            baseStep.wait.GenericWait(3000);

            var actualQualifier = baseStep.getText.Text(agentUiPage.SplQualifyMsg);
            expectedSplQualifier = solutionPageSteps.ReturnExpectedQualifierTextForSPL(Idnumber, isQualifiedSPL);

            validate.AssertEquals(expectedSplQualifier, actualQualifier, "Qualifier text is not as per expected", false);
            Report.ChildLog.Log(Status.Info, $"SPL Qualifier is visible on Agent Ui Solution Page is {actualQualifier}");
        }

        /// <summary>
        /// Verifies Credit Card and MobiCred tile qualifiers
        /// </summary>
        /// <param name="Idnumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyCreditCardAndMobiCredTileQualifier(string Idnumber)
        {
            var solutionPageSteps = new SolutionPageSteps();

            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ccqualifier, 30);
            baseStep.ScrollToElement(agentUiPage.CCQualifier);
            var actualQualifier = baseStep.getText.Text(agentUiPage.CCQualifier);

            var mobiCredQualifier = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].textContent;", agentUiPage.MobiCredQualifier);
            expectedCCQualifier = solutionPageSteps.ReturnExpectedQualifierTextForCC(Idnumber, agentUiPage.CCQualifier);

            validate.AssertEquals(expectedCCQualifier, actualQualifier, "CC Qualifier text is not as per expected", false);
            validate.AssertEquals(mobiCredQualifier, actualQualifier, "Mobi Cred Qualifier text is not as per expected", false);

            Report.ChildLog.Log(Status.Info, $"Money Saver Credit Card Qualifier is visible on Agent Ui Solution Page is {actualQualifier} and Mobi Cred Qualifier is visible {mobiCredQualifier}");
        }

        /// <summary>
        /// Verifies Credit Consolidation tile qualifier
        /// </summary>
        /// <param name="Idnumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyCreditConsolidationTileQualifier(string Idnumber)
        {
            var agentUiPage = new AgentUiPage();
            var solutionPageSteps = new SolutionPageSteps();

            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.creditconsolqualifier, 20);
            baseStep.ScrollToElement(agentUiPage.CreditConsolQualifier);
            var actualQualifier = baseStep.getText.Text(agentUiPage.CreditConsolQualifier);
            string monthlySaving = null;

            expectedCreditConsolQualifier = solutionPageSteps.ReturnExpectedQualifierTextForCreditConsolidation(Idnumber, agentUiPage.CreditConsolQualifier, monthlySaving);
            validate.AssertEquals(expectedCreditConsolQualifier, actualQualifier, "CC Qualifier text is not as per expected", false);

            Report.ChildLog.Log(Status.Info, $"CreditConsolidation Qualifier is visible on Agent Ui Solution Page is {actualQualifier}");
        }

        /// <summary>
        /// Verifies Capfin tile qualifier
        /// </summary>
        /// <param name="Idnumber">User ID number</param>
        [Author("Shahab Khan")]
        public void VerifyCapfinTileQualifier(string Idnumber)
        {
            var solutionPageSteps = new SolutionPageSteps();

            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.capfinqualifier, 20);
            baseStep.ScrollToElement(agentUiPage.CapfinQualifier);
            var actualQualifier = baseStep.getText.Text(agentUiPage.CapfinQualifier);
            expectedCapfinQualifier = solutionPageSteps.ReturnExpectedQualifierTextForCapfin(Idnumber, agentUiPage.CapfinQualifier);

            validate.AssertEqualWithMessage(expectedCapfinQualifier, actualQualifier, "Capfin Qualifier text as expected", false);
            Report.ChildLog.Log(Status.Info, $"Capfin Qualifier is visible {actualQualifier}");
        }

        /// <summary>
        /// Verifies all tile qualifiers on home page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyTileQualifiersOnHomePage()
        {
            var homePage = new HomePage();
            var homePageSteps = new HomePageSteps();

            NavigateToHomePage(agentUiPage);
            VerifyHomePageQualifiers(homePage, homePageSteps);
        }

        /// <summary>
        /// Method is used to verify the spl tile on Agent homepage and Ui page 
        /// </summary>
        /// <param name="Idnumber"></param>
        /// <param name="isQualifiedSPL"></param>
        [Author("Shahab Khan")]
        public void VerifySPLTileQualifiersOnAgentUi(string Idnumber, bool isQualifiedSPL)
        {
            var agentUiPage = new AgentUiPage();
            NavigateToCustomerDashboard(agentUiPage);
            VerifySPLTileQualifier(Idnumber, isQualifiedSPL);
            VerifySPLTileQualifiersOnHomePage();
        }

        /// <summary>
        /// Method is used to verify comm logs on agent ui
        /// </summary>
        /// <param name="Idnumber"></param>
        /// <param name="isQualifiedSPL"></param>
        [Author("Shahab Khan")]
        public void VerifyCommLogsOnAgentUi(string expectedQualifier, string expectedTransfer, bool isQualifiedSPL = true)
        {
            if (isQualifiedSPL)
            {
                NavigateToCommunicationLogs();
                VerifyTileLogsInLogDetailsTable(expectedQualifier, expectedTransfer);
            }
        }

        /// <summary>
        /// Method is used to check the Capfin tile on Agent UI
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifyCapfinTileQualifiersOnAgentUi(string Idnumber)
        {
            var agentUiPage = new AgentUiPage();
            NavigateToCustomerDashboard(agentUiPage);
            NavigateToSolutionTab(agentUiPage);
            VerifyCapfinTileQualifier(Idnumber);
            VerifyCapfinTileQualifiersOnHomePage();
        }

        /// <summary>
        /// Method used to CreditCardAndMobiCredTile on homepage and soulution page
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifyCreditCardAndMobiCredTileQualifierOnAgentUI(string Idnumber)
        {
            var agentUiPage = new AgentUiPage();
            NavigateToCustomerDashboard(agentUiPage);
            NavigateToSolutionTab(agentUiPage);
            VerifyCreditCardAndMobiCredTileQualifier(Idnumber);
            VerifyCreditCardTileQualifiersOnHomePage();
        }

        /// <summary>
        /// To check the tile logs in the communcation logs table in Agent UI
        /// </summary>
        /// <param name="expectedQualifier"></param>
        /// <param name="expectedTransfer"></param>
        public void VerifyTileLogsInLogDetailsTable(string expectedQualifier, string expectedTransfer)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.logdetails_table, 30);
            IWebElement logsTable = agentUiPage.LogDetails_Table;
            IList<IWebElement> rows = logsTable.FindElements(By.TagName("tr"));
            string currentDate = DateTime.Now.ToString("dd/MM/yyyy");

            foreach (var row in rows)
            {
                IList<IWebElement> columns = row.FindElements(By.TagName("td"));
                IWebElement date = row.FindElement(By.TagName("th"));
                if (columns.Count > 0 && date != null)
                {
                    string dateText = date.Text;
                    string transfer = columns[3].Text;
                    if (dateText == currentDate && transfer == expectedTransfer)
                    {
                        validate.AssertEqualWithMessage(expectedQualifier.ToLower(), columns[2].Text.ToLower(), "Qualifier as expected", false);
                        Report.ChildLog.Log(Status.Info, $"Date: {dateText}, Type: {columns[0].Text}, Reason: {columns[1].Text}, Outcome: {columns[2].Text}, Transfer: {transfer}, CreatedBy: {columns[4].Text}");
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Methods is for checking the tile qualifiers on agent ui
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifyCreditConsolidationTileQualifierOnAgentUI(string Idnumber)
        {
            var agentUiPage = new AgentUiPage();
            NavigateToCustomerDashboard(agentUiPage);
            NavigateToSolutionTab(agentUiPage);
            VerifyCreditConsolidationTileQualifier(Idnumber);
            VerifyCreditConsolidationTileQualifiersOnHomePage();
        }

        /// <summary>
        /// Method is used to check the Personal Finance 27 tile on Agent UI
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifyPersonalFinance27TileQualifiersOnAgentUi(string Idnumber)
        {
            var agentUiPage = new AgentUiPage();
            NavigateToCustomerDashboard(agentUiPage);
            NavigateToSolutionTab(agentUiPage);
            VerifyPersonalFinance27TileQualifier(Idnumber);
        }

        /// <summary>
        /// This method verifies store card qualifiers on the Agent UI by navigating to the customer dashboard, validating qualifiers, and checking logs.
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifyStoreCardsTileQualifiersOnAgentUi(string creditCoachScore_Storecard)
        {
            var agentUiPage = new AgentUiPage();
            NavigateToCustomerDashboard(agentUiPage);
            NavigateToSolutionTab(agentUiPage);
            ValidateStoreCardsQualifier(creditCoachScore_Storecard);
            VerifyCommLogsOnAgentUi(expectedStoreCardsQualifier, "Store Card");
            VerifyTileLogsInLogDetailsTable(expectedStoreCardsQualifier, "Identity Store Card");
        }

        /// <summary>
        /// This method validates the communication log by checking the communication log table for entries related to "Digital" and "User."
        /// </summary>
        [Author("Piyush Sharma")]
        public void ValidateCommunicationLog()
        {
            ValidateCommunicationLogTable("Digital", "User");
        }

        /// <summary>
        /// The method validates customer dashboard elements by checking credit scores, dates, and status displays while capturing screenshots for verification purposes.
        /// </summary>
        [Author("Piyush Sharma")]
        public void VerifyCustomerDashboardUiAndResponse()
        {
            try
            {
                baseStep.wait.WaitForElementClickableLongWait(agentUiPage.customerdashboard_btn, 10);
                baseStep.ScrollToElement(agentUiPage.CustomerDashboard_Btn);
                baseStep.Click(agentUiPage.CustomerDashboard_Btn);
                baseStep.wait.WaitTillPageLoad();
                baseStep.wait.GenericWait(2000);

                baseStep.wait.WaitForElementClickableLongWait(agentUiPage.paneltogle_btn, 10);
                baseStep.Click(agentUiPage.PanelTogle_Btn);
                baseStep.wait.GenericWait(2000);

                baseStep.wait.WaitForElementClickableLongWait(agentUiPage.dashboardtab, 10);
                baseStep.Click(agentUiPage.DashboardTab);
                baseStep.wait.GenericWait(2000);

                baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.creditscore, 10);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.creditscore), $"Credit Score is {baseStep.getText.Text(agentUiPage.CreditScore)}", false);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.reportdate), $"Report Date is {baseStep.getText.Text(agentUiPage.ReportDate)}", true);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.yourscoreis), $"Your Score is {baseStep.getText.Text(agentUiPage.YourScoreIs)}", true);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.updatedon), $"Updated on {baseStep.getText.Text(agentUiPage.UpdatedOn)}", true);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.nextupdatein), $"Next update in {baseStep.getText.Text(agentUiPage.NextUpdateIn)}", true);
                validate.TakeFullPageScreenShot("Dashboard UI", Status.Info);
            }
            catch (Exception ex)
            {
                validate.TakeFullPageScreenShot("No Dashboard UI for this user", Status.Info);
                Report.ChildLog.Log(Status.Info, $"Error: {ex}");
            }
        }

        /// <summary>
        /// The method updates and validates credit history by triggering a bureau call, verifying credit scores, dates, and dashboard elements with comprehensive screenshots.
        /// </summary>
        [Author("Piyush Sharma")]
        public void VerifyUpdateCreditHistory()
        {
            try
            {
                baseStep.wait.WaitForElementClickableLongWait(agentUiPage.updatecreditreport_button, 10);
                baseStep.ScrollToElement(agentUiPage.UpdateCreditReport_Button);
                baseStep.Click(agentUiPage.UpdateCreditReport_Button);
                baseStep.wait.WaitTillPageLoad();
                baseStep.wait.WaitForElementClickableLongWait(agentUiPage.bureaucallpopup_yesbutton, 10);
                baseStep.Click(agentUiPage.BureauCallPopup_YesButton);
                baseStep.wait.WaitTillPageLoad();
                baseStep.wait.WaitForElementClickableLongWait(agentUiPage.paneltogle_btn, 10);
                baseStep.Click(agentUiPage.PanelTogle_Btn);
                baseStep.wait.WaitForElementClickableLongWait(agentUiPage.dashboardtab, 10);
                baseStep.Click(agentUiPage.DashboardTab);
                baseStep.wait.GenericWait(2000);

                baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.creditscore, 10);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.creditscore), $"Credit Score is {baseStep.getText.Text(agentUiPage.CreditScore)}", false);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.reportdate), $"Report Date is {baseStep.getText.Text(agentUiPage.ReportDate)}", true);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.yourscoreis), $"Your Score is {baseStep.getText.Text(agentUiPage.YourScoreIs)}", true);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.updatedon), $"Updated on {baseStep.getText.Text(agentUiPage.UpdatedOn)}", true);
                validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(agentUiPage.nextupdatein), $"Next update in {baseStep.getText.Text(agentUiPage.NextUpdateIn)}", true);
                validate.TakeFullPageScreenShot("Dashboard UI", Status.Info);
                baseStep.Click(agentUiPage.PanelTogle_Btn);
            }
            catch (Exception ex)
            {
                validate.TakeFullPageScreenShot("No Dashboard UI for this user", Status.Info);
                Report.ChildLog.Log(Status.Info, $"Error: {ex}");
            }
        }

        /// <summary>
        /// The method verifies customer dashboard visibility by checking credit history status, toggling panels, and validating display of all dashboard elements with screenshots.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="requiredStatus"></param>
        [Author("Piyush Sharma")]
        public void VerifyCustomerDashboard(string idNumber, string requiredStatus = "disable")
        {
            var expected = true;
            if (requiredStatus.ToLower() == "disable")
            {
                expected = false;
                int dateTimeDay = DateTime.Now.Day;
                string dateTimeMonth = DateTime.Now.Month.ToString();
                if (dateTimeDay < 19)
                    dateTimeMonth = (DateTime.Now.Month - 1).ToString();
                dBCreditCoach.UpdateAndDeleteTable(DBQueries.UpdateCreditHistoryIsActive(idNumber, 0, dateTimeMonth));
            }
            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.customerdashboard_btn, 10);
            baseStep.ScrollToElement(agentUiPage.CustomerDashboard_Btn);
            baseStep.Click(agentUiPage.CustomerDashboard_Btn);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.paneltogle_btn, 10);
            baseStep.Click(agentUiPage.PanelTogle_Btn);
            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.dashboardtab, 10);
            baseStep.Click(agentUiPage.DashboardTab);
            baseStep.wait.GenericWait(2000);

            validate.AssertEqualWithMessage(expected, validate.IsElementDisplayed(agentUiPage.creditscore), $"Credit Score is {baseStep.getText.Text(agentUiPage.CreditScore) ?? "not"} Visible", true);
            validate.AssertEqualWithMessage(expected, validate.IsElementDisplayed(agentUiPage.reportdate), $"Report Date is {baseStep.getText.Text(agentUiPage.CreditScore) ?? "not"} Visible", true);
            validate.AssertEqualWithMessage(expected, validate.IsElementDisplayed(agentUiPage.yourscoreis), $"Your Score is {baseStep.getText.Text(agentUiPage.CreditScore) ?? "not"} Visible", true);
            validate.AssertEqualWithMessage(expected, validate.IsElementDisplayed(agentUiPage.updatedon), $"Updated On is {baseStep.getText.Text(agentUiPage.CreditScore) ?? "not"} Visible", true);
            validate.AssertEqualWithMessage(expected, validate.IsElementDisplayed(agentUiPage.nextupdatein), $"Next update in {baseStep.getText.Text(agentUiPage.CreditScore) ?? "not"} Visible", true);
            validate.TakeFullPageScreenShot($"Dashboard UI is visible - {expected}", Status.Info);

            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.paneltogle_btn, 10);
            baseStep.Click(agentUiPage.PanelTogle_Btn);
        }

        /// <summary>
        /// The method creates and verifies communication logs by adding new entries, validating success messages, and confirming log details in the system table.
        /// </summary>
        [Author("Piyush Sharma")]
        public void CreateLogAndVerifyCommLog()
        {
            Report.ChildLog.Log(Status.Info, $"Method:- {MethodBase.GetCurrentMethod().Name}");

            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.commlogs_dropdown, 10);
            baseStep.ScrollToElement(agentUiPage.CommLogs_Dropdown);
            baseStep.Click(agentUiPage.CommLogs_Dropdown);
            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.lognew_btn, 10);
            baseStep.Click(agentUiPage.LogNew_Btn);
            baseStep.dropdown.SelectByText(agentUiPage.LogNewType_Dropdown, "Chat");
            baseStep.dropdown.SelectByText(agentUiPage.LogNewReason_Dropdown, "CR - Credit Score");
            baseStep.SendKeys(agentUiPage.LogNewOutcome_TextBox, "This log is created for Auto Regression Testing purpose");
            baseStep.dropdown.SelectByText(agentUiPage.LogNewTransfer_Dropdown, "CR - Credit Score");
            baseStep.Click(agentUiPage.LogNewSave_Btn);
            baseStep.wait.WaitTillPageLoad();
            validate.TakeStepFullScreenShot("Log New Popup", Status.Info);
            validate.AssertEquals(true, validate.IsElementDisplayed(agentUiPage.lognewsuccessmsg), "Success message is not displayed", false);
            baseStep.Click(agentUiPage.LogNewCut_Btn);

            baseStep.Click(agentUiPage.LogDetails_Btn);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibility(agentUiPage.logdetailstable_rows);
            int i = 1;
            foreach (IWebElement TableRow in agentUiPage.LogDetailsTable_Rows)
            {
                string type = baseStep.getText.Text(agentUiPage.LogDetailsType(i));
                if (type.Equals("Chat"))
                {
                    validate.AssertEquals("Chat", type, "Type is not matched", false);
                    string reason = baseStep.getText.Text(agentUiPage.LogDetailsReason(i));
                    string outcome = baseStep.getText.Text(agentUiPage.LogDetailsOutcome(i));
                    string transfer = baseStep.getText.Text(agentUiPage.LogDetailsTransfer(i));
                    validate.AssertEquals("CR - Credit Score", reason, "Reason is not matched", true);
                    validate.AssertEquals("This log is created for Auto Regression Testing purpose", outcome, "Outcome is not matched", true);
                    validate.AssertEquals("CR - Credit Score", transfer, "Transfer is not matched", true);
                    break;
                }
                else
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// This method verifies communication log entries for SPL by checking log details for a matching date, type, creator, and outcome.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="createdBy"></param>
        [Author("Piyush Sharma")]
        public void VerifyCommlogForSPL(string type, string createdBy)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(agentUiPage.CommLogs_Expand);
            baseStep.Click(agentUiPage.CommLogs_Expand);
            baseStep.ScrollToElement(agentUiPage.CommLogs_Expand);
            baseStep.Click(agentUiPage.LogDetails_Button);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.logdetails_table, 10);

            for (int a = 1; a < agentUiPage.CommLog_TableRow.Count + 1; a++)
            {
                if (agentUiPage.CommLogTable_Date(a).Text == DateTime.UtcNow.ToString("dd/MM/yyyy").Replace("-", "/"))
                {
                    if (agentUiPage.CommLogTable_Transfer(a).Text == "Transfer - SPL")
                    {
                        validate.AssertEquals(type, agentUiPage.CommLogTable_Type(a).Text, "Type is mismatch", true);
                        validate.AssertEquals(createdBy, agentUiPage.CommLogTable_CreatedBy(a).Text, "Created By is mismatch", true);
                        validate.AssertEquals("You are likely to qualify", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                    }
                }
            }
        }

        /// <summary>
        /// The method verifies a communication log for an OOBA home loan by checking date, type, creator, and outcome in a table.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="createdBy"></param>
        [Author("Piyush Sharma")]
        public void VerifyCommlogForOOBAHomeLoan(string type, string createdBy)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(agentUiPage.CommLogs_Expand);
            baseStep.Click(agentUiPage.CommLogs_Expand);
            baseStep.ScrollToElement(agentUiPage.CommLogs_Expand);
            baseStep.Click(agentUiPage.LogDetails_Button);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.logdetails_table, 10);

            for (int a = 1; a < agentUiPage.CommLog_TableRow.Count + 1; a++)
            {
                if (agentUiPage.CommLogTable_Date(a).Text == DateTime.UtcNow.ToString("dd/MM/yyyy").Replace("-", "/"))
                {
                    if (agentUiPage.CommLogTable_Transfer(a).Text == "Transfer - Ooba")
                    {
                        if (agentUiPage.CommLogTable_Outcome(a).Text == "Home Loans Prequalify")
                        {
                            validate.AssertEquals(type, agentUiPage.CommLogTable_Type(a).Text, "Type is mismatch", true);
                            validate.AssertEquals(createdBy, agentUiPage.CommLogTable_CreatedBy(a).Text, "Created By is mismatch", true);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The method verifies a communication log for an OOBA home loan advance by checking date, type, creator, and outcome in a table.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="createdBy"></param>
        [Author("Piyush Sharma")]
        public void VerifyCommlogForOOBAHomeLoanAdvance(string type, string createdBy)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(agentUiPage.CommLogs_Expand);
            baseStep.Click(agentUiPage.CommLogs_Expand);
            baseStep.ScrollToElement(agentUiPage.CommLogs_Expand);
            baseStep.Click(agentUiPage.LogDetails_Button);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.logdetails_table, 10);

            for (int a = 1; a < agentUiPage.CommLog_TableRow.Count + 1; a++)
            {
                if (agentUiPage.CommLogTable_Date(a).Text == DateTime.UtcNow.ToString("dd/MM/yyyy").Replace("-", "/"))
                {
                    if (agentUiPage.CommLogTable_Transfer(a).Text == "Transfer - Ooba")
                    {
                        if (agentUiPage.CommLogTable_Outcome(a).Text == "Home Loan Advance")
                        {
                            validate.AssertEquals(type, agentUiPage.CommLogTable_Type(a).Text, "Type is mismatch", true);
                            validate.AssertEquals(createdBy, agentUiPage.CommLogTable_CreatedBy(a).Text, "Created By is mismatch", true);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a registration campaign with options for ID number and URLs, fills the form, and returns the result as a dictionary.
        /// </summary>
        /// <param name="containIdNumber"></param>
        /// <param name="campaignPageURL"></param>
        /// <param name="campaignPageURL_Product"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> CreateRegistrationCampaign_ContainIdNumber(bool containIdNumber, bool campaignPageURL, bool campaignPageURL_Product)
        {
            RedirectToRegistrationCampaign();
            var registrationDataset = RegistrationCampaignFormFillup(containIdNumber, campaignPageURL, campaignPageURL_Product);
            return registrationDataset;
        }

        /// <summary>
        /// Validates a registration campaign's UI data against expected values, updates details, checks changes, and retrieves the campaign's login URL.
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="containIdNumber"></param>
        /// <param name="campaignPageURL"></param>
        /// <param name="campaignPageURL_Product"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public Dictionary<string, object> ValidateAndManageRegistrationCampaign(Dictionary<string, object> dataset, bool containIdNumber, bool campaignPageURL, bool campaignPageURL_Product)
        {
            string updateCampaignName = "Test_Name1";
            string updatePrimaryHeading = "Primary Heading1";
            string updateSecondaryHeading = "Secondary Heading1";

            baseStep.ScrollToElement(agentUiPage.UJC_OverviewTab);
            baseStep.Click(agentUiPage.UJC_OverviewTab);

            baseStep.Click(agentUiPage.UJC_RegistrationCampaignDetails);
            baseStep.wait.WaitTillPageLoad();

            for (int i = 0; i < agentUiPage.UJC_RegistrationDataList.Count; i++)
            {
                IWebElement registrationData = agentUiPage.UJC_RegistrationDataList[i];

                if (registrationData.Text.Contains(dataset["Campaign_UtmID"].ToString()))
                {
                    string campaignName = agentUiPage.UJC_RegistrationData_CampaignName(i + 1).Text;
                    validate.AssertEquals(campaignName, dataset["Campaign_Name"].ToString(), "Campaign Name is Mismatch", true);

                    string campaignUtmId = agentUiPage.UJC_RegistrationData_CampaignUtmId(i + 1).Text;
                    validate.AssertEquals(campaignUtmId, dataset["Campaign_UtmID"].ToString(), "Campaign UTM Id is Mismatch", true);

                    string campaignBeginDate = agentUiPage.UJC_RegistrationData_BeginDate(i + 1).Text;
                    string[] splittedDate_Begin = campaignBeginDate.Split("T");
                    string beginDate = splittedDate_Begin[0].ToString();
                    validate.AssertEquals(beginDate, DateTime.Now.Date.ToString("yyyy-MM-dd"), "Campaign Begin Date is Mismatch", true);

                    string campaignEndDate = agentUiPage.UJC_RegistrationData_EndDate(i + 1).Text;
                    string[] splittedDate_End = campaignBeginDate.Split("T");
                    string endDate = splittedDate_End[0].ToString();
                    validate.AssertEquals(endDate, DateTime.Now.Date.ToString("yyyy-MM-dd"), "Campaign End Date is Mismatch", true);

                    string campaignPrimaryHeading = agentUiPage.UJC_RegistrationData_PrimaryHeading(i + 1).Text;
                    validate.AssertEquals(campaignPrimaryHeading, dataset["Primary_Heading"].ToString(), "Primary Heading is Mismatch", true);

                    string campaignPrimaryContent = agentUiPage.UJC_RegistrationData_PrimaryContent(i + 1).Text;
                    validate.AssertEquals(campaignPrimaryContent, dataset["Primary_Content"].ToString(), "Primary Content is Mismatch", true);

                    string campaignSecondaryHeading = agentUiPage.UJC_RegistrationData_SecondaryHeading(i + 1).Text;
                    validate.AssertEquals(campaignSecondaryHeading, dataset["Secondary_Heading"].ToString(), "Secondary Heading is Mismatch", true);

                    string campaignSecondaryContent = agentUiPage.UJC_RegistrationData_SecondaryContent(i + 1).Text;
                    validate.AssertEquals(campaignSecondaryContent, dataset["Secondary_Content"].ToString(), "Secondary Content is Mismatch", true);

                    string campaignFirstButtonName = agentUiPage.UJC_RegistrationData_CampaignButtonName(i + 1).Text;
                    validate.AssertEquals(campaignFirstButtonName, dataset["First_Button_Name"].ToString(), "First Button Name is Mismatch", true);

                    if (!containIdNumber)
                    {
                        string campaignPrimaryHeadingSecondPage = agentUiPage.UJC_RegistrationData_PrimaryHeadingSecondPage(i + 1).Text;
                        validate.AssertEquals(campaignPrimaryHeadingSecondPage, dataset["Primary_Heading_Second_Page"].ToString(), "Primary Heading Second Page is Mismatch", true);

                        string campaignPrimaryContentSecondPage = agentUiPage.UJC_RegistrationData_PrimaryContentSecondPage(i + 1).Text;
                        validate.AssertEquals(campaignPrimaryContentSecondPage, dataset["Primary_Content_Second_Page"].ToString(), "Primary Content Second Page is Mismatch", true);

                        string campaignSecondButtonName = agentUiPage.UJC_RegistrationData_CampaignSecondButtonName(i + 1).Text;
                        validate.AssertEquals(campaignSecondButtonName, dataset["Second_Button_Name"].ToString(), "Second Button Name is Mismatch", true);
                    }

                    if (campaignPageURL)
                    {
                        string campaignLandingPageURL = agentUiPage.UJC_RegistrationData_LandingPageUrl(i + 1).Text;
                        validate.AssertEquals(campaignLandingPageURL, dataset["Landing_Page_URL"].ToString(), "Landing Page URL is Mismatch", true);
                    }
                    else
                    {
                        if (campaignPageURL_Product)
                        {
                            string campaignLandingPageURL = agentUiPage.UJC_RegistrationData_LandingPageUrl(i + 1).Text;
                            validate.AssertEquals(campaignLandingPageURL, dataset["Product_Page_URL"].ToString(), "Product Page URL is Mismatch", true);
                        }
                    }

                    #region Validate Campaign Preview Section

                    baseStep.ScrollToElement(agentUiPage.UJC_RegistrationData_Preview(i + 1));
                    baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_registrationdata_preview(i + 1), 5);
                    agentUiPage.UJC_RegistrationData_Preview(i + 1).Click();
                    baseStep.wait.WaitTillPageLoad();
                    baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_preview_primaryheader, 10);

                    validate.AssertEquals(agentUiPage.UJC_Preview_PrimaryHeader.Text, dataset["Primary_Heading"].ToString(), "Primary Heading in campaign preview is Mismatch", true);
                    validate.AssertEquals(agentUiPage.UJC_Preview_PrimaryContent.Text, dataset["Primary_Content"].ToString(), "Primary Content in campaign preview is Mismatch", true);

                    if (containIdNumber)
                    {
                        validate.AssertEquals(true, validate.IsElementDisplayed(agentUiPage.ujc_preview_inputfield), "Input Field in campaign preview is not displayed", true);
                    }

                    validate.AssertEquals(agentUiPage.UJC_Preview_FirstButtonName.Text, dataset["First_Button_Name"].ToString(), "First Button Name in campaign preview is Mismatch", true);
                    validate.AssertEquals(agentUiPage.UJC_Preview_SecondaryHeader.Text, dataset["Secondary_Heading"].ToString(), "Secondary Heading in campaign preview is Mismatch", true);
                    validate.AssertEquals(agentUiPage.UJC_Preview_SecondaryContent.Text, dataset["Secondary_Content"].ToString(), "Secondary Content in campaign preview is Mismatch", true);

                    baseStep.ScrollToElement(agentUiPage.UJC_Preview_Close);
                    agentUiPage.UJC_Preview_Close.Click();

                    #endregion

                    #region Validate Campaign Update Section

                    agentUiPage.UJC_RegistrationData_Update(i + 1).Click();
                    baseStep.wait.WaitTillPageLoad();
                    baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_update_campaignbegindatecalanderbutton, 10);

                    baseStep.ScrollToElement(agentUiPage.UJC_Update_CampaignBeginDateCalanderButton);
                    baseStep.Click(agentUiPage.UJC_Update_CampaignBeginDateCalanderButton);
                    baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_calanderpopup, 5);
                    baseStep.Click(agentUiPage.UJC_CalanderCurrentDate);

                    baseStep.ScrollToElement(agentUiPage.UJC_Update_CampaignEndDateCalanderButton);
                    baseStep.Click(agentUiPage.UJC_Update_CampaignEndDateCalanderButton);
                    baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_calanderpopup, 5);
                    UJC_SelectCalanderDate(DateTime.Now.AddDays(4).Day.ToString());

                    baseStep.ScrollToElement(agentUiPage.UJC_Update_CampaignName);
                    baseStep.ClearAndSendKeys(agentUiPage.UJC_Update_CampaignName, updateCampaignName);

                    baseStep.ScrollToElement(agentUiPage.UJC_Update_PrimaryHeading);
                    baseStep.ClearAndSendKeys(agentUiPage.UJC_Update_PrimaryHeading, updatePrimaryHeading);

                    baseStep.ScrollToElement(agentUiPage.UJC_Update_SecondaryHeading);
                    baseStep.ClearAndSendKeys(agentUiPage.UJC_Update_SecondaryHeading, updateSecondaryHeading);

                    baseStep.ScrollToElement(agentUiPage.UJC_Update_IsActive);
                    if (agentUiPage.UJC_Update_IsActive.Selected)
                    {
                        baseStep.Click(agentUiPage.UJC_Update_IsActive);
                    }

                    baseStep.ScrollToElement(agentUiPage.UJC_UpdateButton);
                    baseStep.Click(agentUiPage.UJC_UpdateButton);

                    baseStep.wait.WaitTillPageLoad();

                    baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_campaignsuccessmessage, 10);
                    baseStep.ScrollToElement(agentUiPage.UJC_CampaignSuccessMessage);
                    validate.AssertEquals("Updated Successfully.", agentUiPage.UJC_CampaignSuccessMessage.Text, "Campaign Update Message is not displayed", true);

                    for (int a = 0; a < agentUiPage.UJC_RegistrationDataList.Count; a++)
                    {
                        IWebElement updatedRegistrationData = agentUiPage.UJC_RegistrationDataList[a];

                        if (updatedRegistrationData.Text.Contains(dataset["Campaign_UtmID"].ToString()))
                        {
                            Assert.Fail("Campaign is present in the registration log that shouldn't be present");
                        }
                    }

                    baseStep.ScrollToElement(agentUiPage.UJC_RecycleBinTab);
                    baseStep.Click(agentUiPage.UJC_RecycleBinTab);

                    baseStep.Click(agentUiPage.UJC_RegistrationCampaignBin_Open);
                    baseStep.wait.WaitTillPageLoad();
                    baseStep.Click(agentUiPage.UJC_RegistrationCampaignBin_Close);
                    baseStep.wait.WaitTillPageLoad();

                    for (int a = 0; a < agentUiPage.UJC_RegistrationDataList.Count; a++)
                    {
                        IWebElement updatedRegistrationData = agentUiPage.UJC_RegistrationDataList[a];

                        if (updatedRegistrationData.Text.Contains(dataset["Campaign_UtmID"].ToString()))
                        {
                            baseStep.ScrollToElement(agentUiPage.UJC_RegistrationData_Update(a + 1));
                            agentUiPage.UJC_DeleteData_Update(a + 1).Click();
                            baseStep.wait.WaitTillPageLoad();
                            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_update_campaignbegindatecalanderbutton, 10);

                            baseStep.ScrollToElement(agentUiPage.UJC_Update_IsActive);
                            if (!agentUiPage.UJC_Update_IsActive.Selected)
                            {
                                baseStep.Click(agentUiPage.UJC_Update_IsActive);
                            }

                            baseStep.ScrollToElement(agentUiPage.UJC_UpdateButton);
                            baseStep.Click(agentUiPage.UJC_UpdateButton);

                            baseStep.wait.WaitTillPageLoad();

                            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_campaignsuccessmessage, 10);
                            baseStep.ScrollToElement(agentUiPage.UJC_CampaignSuccessMessage);
                            validate.AssertEquals("Updated Successfully.", agentUiPage.UJC_CampaignSuccessMessage.Text, "Campaign Update Message is not displayed", true);
                        }
                    }

                    baseStep.ScrollToElement(agentUiPage.UJC_OverviewTab);
                    baseStep.Click(agentUiPage.UJC_OverviewTab);

                    baseStep.Click(agentUiPage.UJC_RegistrationCampaignDetails_Close);
                    baseStep.wait.WaitTillPageLoad();
                    baseStep.Click(agentUiPage.UJC_RegistrationCampaignDetails_Open);
                    baseStep.wait.WaitTillPageLoad();

                    #endregion

                    #region Validate and Fetch Login Link

                    for (int b = 0; b < agentUiPage.UJC_RegistrationDataList.Count; b++)
                    {
                        IWebElement updatedRegistrationData = agentUiPage.UJC_RegistrationDataList[b];

                        if (updatedRegistrationData.Text.Contains(dataset["Campaign_UtmID"].ToString()))
                        {
                            baseStep.ScrollToElement(agentUiPage.UJC_RegistrationData_GetLink(b + 1));
                            agentUiPage.UJC_RegistrationData_GetLink(b + 1).Click();
                            baseStep.wait.WaitTillPageLoad();

                            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_registrationdata_urllink, 5);
                            string ujc_URL = baseStep.getText.Text(agentUiPage.UJC_RegistrationData_UrlLink);

                            baseStep.Click(agentUiPage.UJC_UrlLink_PopupClose);

                            Dictionary<string, object> updateInfo = new Dictionary<string, object>
                            {
                                {"UJC_Url", ujc_URL},
                                {"updatedPrimaryHeading", updatePrimaryHeading},
                                {"updatedSecondaryHeading", updateSecondaryHeading},
                                {"updatedCampaignName", updateCampaignName}
                            };
                            return updateInfo;
                        }
                    }

                    #endregion
                }
            }

            return null;
        }

        /// <summary>
        /// This method validates the content of the registration campaign URL page, comparing UI values with expected data and handling ID number conditions.
        /// </summary>
        /// <param name="registrationDataset"></param>
        /// <param name="ujc_updatedInfo"></param>
        /// <param name="containIdNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateRegistrationURL(Dictionary<string, object> registrationDataset, Dictionary<string, object> ujc_updatedInfo, bool containIdNumber)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_registrationform_primaryheading, 10);

            validate.AssertEquals(ujc_updatedInfo["updatedPrimaryHeading"].ToString(), baseStep.getText.Text(agentUiPage.UJC_RegistrationForm_PrimaryHeading), "Primary Heading text is not matching", true);
            validate.AssertEquals(registrationDataset["Primary_Content"].ToString(), baseStep.getText.Text(agentUiPage.UJC_RegistrationForm_PrimaryContent), "Primary Content text is not matching", true);
            validate.AssertEquals(ujc_updatedInfo["updatedSecondaryHeading"].ToString(), baseStep.getText.Text(agentUiPage.UJC_RegistrationForm_SecondaryHeading), "Secondary Heading text is not matching", true);
            validate.AssertEquals(registrationDataset["Secondary_Content"].ToString(), baseStep.getText.Text(agentUiPage.UJC_RegistrationForm_SecondaryContent), "Secondary Content text is not matching", true);

            if (containIdNumber)
            {
                validate.AssertEquals(registrationDataset["First_Button_Name"].ToString(), baseStep.getText.Text(agentUiPage.UJC_RegistrationForm_SubmitButton), "Submit Button text is not matching", true);
            }
            else
            {
                validate.AssertEquals(registrationDataset["First_Button_Name"].ToString(), baseStep.getText.Text(agentUiPage.UJC_RegistrationForm_Button), "Registration Form Button text is not matching", true);
                baseStep.Click(agentUiPage.UJC_RegistrationForm_Button);
                baseStep.wait.WaitTillPageLoad();
                validate.AssertEquals(registrationDataset["Primary_Heading_Second_Page"].ToString(), baseStep.getText.Text(agentUiPage.UJC_RegistrationForm_PrimaryHeadingSecondPage), "Primary Heading Second Page text is not matching", true);
                validate.AssertEquals(registrationDataset["Primary_Content_Second_Page"].ToString(), baseStep.getText.Text(agentUiPage.UJC_RegistrationForm_PrimaryContentSecondPage), "Primary Content Second Page text is not matching", true);
                validate.AssertEquals(registrationDataset["Second_Button_Name"].ToString(), baseStep.getText.Text(agentUiPage.UJC_RegistrationForm_SubmitButton), "Submit Button text is not matching", true);
            }
        }

        /// <summary>
        /// This method verifies campaign details stored in the database against the expected UI and dataset values, including optional page and button validations.
        /// </summary>
        /// <param name="registrationDataset"></param>
        /// <param name="ujc_updatedInfo"></param>
        /// <param name="campaignPageURL"></param>
        /// <param name="containIdNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateCampaignOnDB(Dictionary<string, object> registrationDataset, Dictionary<string, object> ujc_updatedInfo, bool campaignPageURL, bool containIdNumber)
        {
            var CampaignInfo = dBCreditCoach.FetchCampaignLog(registrationDataset["Campaign_UtmID"].ToString());

            validate.AssertEquals(CampaignInfo["CampaignName"].ToString(), ujc_updatedInfo["updatedCampaignName"].ToString(), "Campaign Name is Mismatch", true);
            validate.AssertEquals(CampaignInfo["CampaignUtmId"].ToString(), registrationDataset["Campaign_UtmID"].ToString(), "Campaign UTM ID is Mismatch", true);
            validate.AssertEquals(CampaignInfo["PrimaryHeading"].ToString(), ujc_updatedInfo["updatedPrimaryHeading"].ToString(), "Primary Heading is Mismatch", true);
            validate.AssertEquals(CampaignInfo["PrimaryContent"].ToString(), registrationDataset["Primary_Content"].ToString(), "Primary Content is Mismatch", true);
            validate.AssertEquals(CampaignInfo["SecondaryHeading"].ToString(), ujc_updatedInfo["updatedSecondaryHeading"].ToString(), "Secondary Heading is Mismatch", true);
            validate.AssertEquals(CampaignInfo["SecondaryContent"].ToString(), registrationDataset["Secondary_Content"].ToString(), "Secondary Content is Mismatch", true);
            validate.AssertEquals(CampaignInfo["CampaignButtonName"].ToString(), registrationDataset["First_Button_Name"].ToString(), "Campaign Button Name is Mismatch", true);

            if (campaignPageURL)
            {
                if (CampaignInfo["CampaignLinkedPage"].ToString() == "/portal/account")
                {
                    validate.AssertEquals(CampaignInfo["CampaignLinkedPage"].ToString(), registrationDataset["Landing_Page_URL"].ToString(), "Campaign Linked Page is Mismatch", true);
                }
                else if (CampaignInfo["CampaignLinkedPage"].ToString() == "/portal/offers")
                {
                    validate.AssertEquals(CampaignInfo["CampaignLinkedPage"].ToString(), registrationDataset["Product_Page_URL"].ToString(), "Campaign Linked Page is Mismatch", true);
                } 
            }

            if (!containIdNumber)
            {
                validate.AssertEquals(CampaignInfo["CampaignButtonNameSecondPage"].ToString(), registrationDataset["Second_Button_Name"].ToString(), "Second Button Name is Mismatch", true);
                validate.AssertEquals(CampaignInfo["PrimaryHeadingSecondPage"].ToString(), registrationDataset["Primary_Heading_Second_Page"].ToString(), "Primary Heading Second Page is Mismatch", true);
                validate.AssertEquals(CampaignInfo["PrimaryContentSecondPage"].ToString(), registrationDataset["Primary_Content_Second_Page"].ToString(), "Primary Content Second Page is Mismatch", true);
            }

            DateTime beginDate = DateTime.Parse(CampaignInfo["BeginDate"].ToString());
            string db_BeginDate = beginDate.ToString("dd-MM-yyyy");
            string todaysDate = DateTime.Now.ToString("dd-MM-yyyy");

            DateTime endDate = DateTime.Parse(CampaignInfo["EndDate"].ToString());
            string db_endDate = endDate.ToString("dd-MM-yyyy");
            string futureDate = DateTime.Now.AddDays(4).ToString("dd-MM-yyyy");

            validate.AssertEquals(db_BeginDate, todaysDate, "Begin Date is Mismatch", true);
            validate.AssertEquals(db_endDate, futureDate, "End Date is Mismatch", true);
        }

        #region Private Helper Methods

        private void RedirectToRegistrationCampaign()
        {
            baseStep.wait.WaitTillPageLoad();

            if (baseStep.IsElementDisplayed(agentUiPage.ujc_button))
            {
                baseStep.ScrollToElement(agentUiPage.UJC_Button);
                baseStep.Click(agentUiPage.UJC_Button);
                baseStep.wait.WaitTillPageLoad();

                baseStep.IsElementDisplayed(agentUiPage.ujc_manageuserjourneybutton);
                baseStep.Click(agentUiPage.UJC_ManageUserJourneyButton);
                baseStep.wait.WaitTillPageLoad();
            }
            else
            {
                Assert.Fail("UJC Button is not displayed");
            }
        }

        private Dictionary<string, object> RegistrationCampaignFormFillup(bool containIdNumber, bool campaignPageURL, bool campaignPageURL_Product)
        {
            var imageLocation = genericUtils.GetDataPath("TestResources\\Image");
            string image = Path.Combine(imageLocation, "BannerImage.jpg");
            string campaignName = "Test_Name";
            string campaignUtmId = "TestUTMId" + genericUtils.RandomInteger(10000);
            string primaryHeading = "Primary Heading";
            string primaryContent = "Primary Content";
            string campaignButtonName = "Test Button";
            string campaignLandingPageUrl = "/portal/account";
            string campaignProductPageUrl = "/portal/offers";
            string campaignProduct = "Personal Loans--Sanlam Personal Loans";
            string secondaryHeading = "Secondary Heading";
            string secondaryContent = "Secondary Content";
            string primaryHeadingSecondPage = "Primary Heading Second Page";
            string primaryContentSecondPage = "Primary Content Second Page";
            string campaignButtonNameSecondPage = "Test Button Second Page";

            baseStep.Click(agentUiPage.UJC_RegistrationCampaignType);
            baseStep.SendKeys(agentUiPage.UJC_CampaignName, campaignName);
            baseStep.SendKeys(agentUiPage.UJC_CampaignUtmId, campaignUtmId);

            baseStep.ScrollToElement(agentUiPage.UJC_CampaignBeginDateCalanderButton);
            baseStep.Click(agentUiPage.UJC_CampaignBeginDateCalanderButton);
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_calanderpopup, 5);
            baseStep.Click(agentUiPage.UJC_CalanderCurrentDate);

            baseStep.ScrollToElement(agentUiPage.UJC_CampaignEndDateCalanderButton);
            baseStep.Click(agentUiPage.UJC_CampaignEndDateCalanderButton);
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_calanderpopup, 5);
            UJC_SelectCalanderDate(DateTime.Now.AddDays(3).Day.ToString());

            if (containIdNumber)
            {
                baseStep.ScrollToElement(agentUiPage.UJC_CampaignWithIdNumber);
                baseStep.Click(agentUiPage.UJC_CampaignWithIdNumber);
            }
            else
            {
                baseStep.ScrollToElement(agentUiPage.UJC_CampaignWithoutIdNumber);
                baseStep.Click(agentUiPage.UJC_CampaignWithoutIdNumber);
            }

            baseStep.ScrollToElement(agentUiPage.UJC_PrimaryHeading);
            baseStep.SendKeys(agentUiPage.UJC_PrimaryHeading, primaryHeading);

            HandleContentTextBox(0, primaryContent);

            baseStep.ScrollToElement(agentUiPage.UJC_CampaignButtonName);
            baseStep.SendKeys(agentUiPage.UJC_CampaignButtonName, campaignButtonName);

            if (campaignPageURL)
            {
                baseStep.dropdown.SelectByText(agentUiPage.UJC_CampaignLandingPageUrl, campaignLandingPageUrl);
            }
            else
            {
                if (campaignPageURL_Product)
                {
                    baseStep.dropdown.SelectByText(agentUiPage.UJC_CampaignLandingPageUrl, campaignProductPageUrl);
                    baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.ujc_campaignfeatureproduct, 10);
                    baseStep.ScrollToElement(agentUiPage.UJC_CampaignFeatureProduct);
                    baseStep.dropdown.SelectByText(agentUiPage.UJC_CampaignFeatureProduct, campaignProduct);
                }
            }

            baseStep.ScrollToElement(agentUiPage.UJC_SecondaryHeading);
            baseStep.SendKeys(agentUiPage.UJC_SecondaryHeading, secondaryHeading);

            HandleContentTextBox(1, secondaryContent);

            baseStep.ScrollToElement(agentUiPage.UJC_ImageUrl);
            baseStep.SendKeys(agentUiPage.UJC_ImageUrl, image);

            if (!containIdNumber)
            {
                baseStep.ScrollToElement(agentUiPage.UJC_PrimaryHeadingSecondPage);
                baseStep.SendKeys(agentUiPage.UJC_PrimaryHeadingSecondPage, primaryHeadingSecondPage);

                HandleContentTextBox(2, primaryContentSecondPage);

                baseStep.ScrollToElement(agentUiPage.UJC_CampaignButtonNameSecondPage);
                baseStep.SendKeys(agentUiPage.UJC_CampaignButtonNameSecondPage, campaignButtonNameSecondPage);
            }

            baseStep.ScrollToElement(agentUiPage.UJC_Publish);
            baseStep.Click(agentUiPage.UJC_Publish);

            baseStep.wait.WaitTillPageLoad();

            baseStep.ScrollToElement(agentUiPage.UJC_CampaignSuccessMessage);
            validate.AssertEquals("Added Successfully", agentUiPage.UJC_CampaignSuccessMessage.Text, "Campaign Success Message is not displayed", true);

            Dictionary<string, object> dataset = new Dictionary<string, object>
            {
                {"Campaign_Name", campaignName},
                {"Campaign_UtmID", campaignUtmId},
                {"Primary_Heading", primaryHeading},
                {"Primary_Content", primaryContent},
                {"Secondary_Heading", secondaryHeading},
                {"Secondary_Content", secondaryContent},
                {"First_Button_Name", campaignButtonName},
                {"Landing_Page_URL", campaignLandingPageUrl},
                {"Product_Page_URL", campaignProductPageUrl},
                {"Product", campaignProduct},
                {"Primary_Heading_Second_Page", primaryHeadingSecondPage},
                {"Primary_Content_Second_Page", primaryContentSecondPage},
                {"Second_Button_Name", campaignButtonNameSecondPage}
            };
            return dataset;
        }

        private void HandleContentTextBox(int frameIndex, string textMessage)
        {
            Driver.SwitchTo().Frame(frameIndex);

            IList<IWebElement> contentTextBox = agentUiPage.UJC_ContentTextbox;

            for (int a = 0; a < contentTextBox.Count; a++)
            {
                IWebElement content = contentTextBox[a];
                baseStep.ScrollToElement(content);
                baseStep.ClearAndSendKeys(content, textMessage);
            }

            Driver.SwitchTo().DefaultContent();
        }

        private void UJC_SelectCalanderDate(string date)
        {
            IList<IWebElement> calanderdates = agentUiPage.UJC_CalanderDates;

            for (int a = 0; a < calanderdates.Count; a++)
            {
                string dates = calanderdates[a].Text;

                if (dates.Contains(date))
                {
                    calanderdates[a].Click();
                    break;
                }
            }
        }

        private void ValidateCommunicationLogTable(string type, string createdBy)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(agentUiPage.CommLogs_Expand);
            baseStep.Click(agentUiPage.CommLogs_Expand);
            baseStep.ScrollToElement(agentUiPage.CommLogs_Expand);
            baseStep.Click(agentUiPage.LogDetails_Button);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.logdetails_table, 10);

            for (int a = 1; a < agentUiPage.CommLog_TableRow.Count + 1; a++)
            {
                if (agentUiPage.CommLogTable_Date(a).Text == DateTime.UtcNow.ToString("dd/MM/yyyy").Replace("-", "/"))
                {
                    if (agentUiPage.CommLogTable_Transfer(a).Text == "Transfer - SD - Medical")
                    {
                        validate.AssertEquals(type, agentUiPage.CommLogTable_Type(a).Text, "Type is mismatch", true);
                        validate.AssertEquals(createdBy, agentUiPage.CommLogTable_CreatedBy(a).Text, "Created By is mismatch", true);

                        switch (agentUiPage.CommLogTable_Outcome(a).Text)
                        {
                            case "Gap Cover":
                                validate.AssertEquals("Gap Cover", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;

                            case "Primary Health Insurance":
                                validate.AssertEquals("Primary Health Insurance", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;

                            case "Medical Scheme Solution":
                                validate.AssertEquals("Medical Scheme Solution", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;
                        }
                    }
                    else if (agentUiPage.CommLogTable_Transfer(a).Text == "Transfer - SD - Long Term" || agentUiPage.CommLogTable_Transfer(a).Text == "Sanlam Customer Care - Reality")
                    {
                        validate.AssertEquals(type, agentUiPage.CommLogTable_Type(a).Text, "Type is mismatch", true);
                        validate.AssertEquals(createdBy, agentUiPage.CommLogTable_CreatedBy(a).Text, "Created By is mismatch", true);

                        switch (agentUiPage.CommLogTable_Outcome(a).Text)
                        {
                            case "Rewards":
                                validate.AssertEquals("Rewards", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;

                            case "Tax-free Savings":
                                validate.AssertEquals("Tax-free Savings", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;

                            case "Invest in Shares":
                                validate.AssertEquals("Invest in Shares", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;

                            case "Retirement Plan":
                                validate.AssertEquals("Retirement Plan", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;

                            case "Education Planning":
                                validate.AssertEquals("Education Planning", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;

                            case "Unit Trusts":
                                validate.AssertEquals("Unit Trusts", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;
                        }
                    }
                    else if (agentUiPage.CommLogTable_Transfer(a).Text == "Transfer - SD - Income Protection")
                    {
                        validate.AssertEquals(type, agentUiPage.CommLogTable_Type(a).Text, "Type is mismatch", true);
                        validate.AssertEquals(createdBy, agentUiPage.CommLogTable_CreatedBy(a).Text, "Created By is mismatch", true);

                        switch (agentUiPage.CommLogTable_Outcome(a).Text)
                        {
                            case "Online Will":
                                validate.AssertEquals("Online Will", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;

                            case "Get Advice":
                                validate.AssertEquals("Get Advice", agentUiPage.CommLogTable_Outcome(a).Text, "Outcome is mismatch", true);
                                break;
                        }
                    }
                }
            }
        }

        private void HandleAgentLogin(AgentUiPage agentUiPage, string emailId, string password)
        {
            try
            {
                baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.signin, 10);
                baseStep.SendKeys(agentUiPage.SignIn, emailId);
                validate.TakeStepFullScreenShot("Sign in id is enter", Status.Info);
                baseStep.Click(agentUiPage.NextBtn);

                baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.enterpassword, 10);
                baseStep.SendKeys(agentUiPage.EnterPassword, password);
                validate.TakeStepFullScreenShot("password is enter", Status.Info);
                baseStep.Click(agentUiPage.NextBtn);

                baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.staysigninnobtn, 10);
                baseStep.Click(agentUiPage.StaySignInNoBtn);
            }
            catch
            {
                Report.ChildLog.Log(Status.Info, "Id enter in AgentUi");
            }
        }

        private void SearchForUser(AgentUiPage agentUiPage, string IdNumber)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.searchpanel, 10);
            baseStep.SendKeys(agentUiPage.SearchPanel, IdNumber);
            validate.TakeStepFullScreenShot("Id is enter", Status.Info);
            Report.ChildLog.Log(Status.Info, $"Id is enter - {IdNumber}");
            baseStep.Click(agentUiPage.SearchBtn);
        }

        private void HandleUserActivation(AgentUiPage agentUiPage, bool activateUser, string activateBtnText)
        {
            if ((activateUser && activateBtnText.ToLower() == "activate") ||
                activateBtnText.ToLower() == "deactivate")
            {
                ProcessActivation(agentUiPage);
            }
        }

        private void ProcessActivation(AgentUiPage agentUiPage)
        {
            baseStep.ScrollToElement(agentUiPage.ActivateBtn);
            validate.TakeStepFullScreenShot("Activation status change", Status.Info);
            baseStep.Click(agentUiPage.ActivateBtn);

            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.activatereasondrop, 10);
            baseStep.Click(agentUiPage.ActivateReasonDropOption);
            validate.TakeStepFullScreenShot("Activate reason option is selected", Status.Info);
            baseStep.Click(agentUiPage.ActivateReasonDropSaveBtn);

            ValidateActivationStatus(agentUiPage);
        }

        private void ValidateActivationStatus(AgentUiPage agentUiPage)
        {
            baseStep.wait.WaitTillPageLoad();
            try
            {
                baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.successmsg, 120);
                var successMsg = baseStep.getText.Text(agentUiPage.SuccessMsg);
                Report.ChildLog.Log(Status.Info, $"SuccessMsg is {successMsg}");
            }
            catch
            {
                var buttonText = baseStep.getText.Text(agentUiPage.ActivateBtn);
                Report.ChildLog.Log(Status.Info, $"SuccessMsg is not visible and button text is {buttonText}");
            }
        }

        private void UpdateCreditReport(AgentUiPage agentUiPage)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(agentUiPage.UpdateCreditReport);
            baseStep.Click(agentUiPage.UpdateCreditReport);
            baseStep.wait.WaitTillPageLoad();

            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.bureaupopupyesbtn, 60);
            validate.TakeStepFullScreenShot("Bureau Call Information Popup is Visible", Status.Info);
            baseStep.Click(agentUiPage.BureauPopupYesBtn);

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.GenericWait(3000);
            validate.TakeFullPageScreenShot("Active User history pull succesfully", Status.Info);
        }

        private void NavigateToCustomerDashboard(AgentUiPage agentUiPage)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(agentUiPage.CustomerDashboard);
            baseStep.Click(agentUiPage.CustomerDashboard);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.paneltogle, 10);
            baseStep.Click(agentUiPage.PanelTogle);
        }

        private void VerifyAllQualifiers(string Idnumber, bool isQualifiedSPL)
        {
            TryVerifyQualifier(() => VerifySPLTileQualifier(Idnumber, isQualifiedSPL), "SPLTile");
            TryVerifyQualifier(() => VerifyCreditCardAndMobiCredTileQualifier(Idnumber), "CreditCard And MobiCredTile");
            TryVerifyQualifier(() => VerifyCreditConsolidationTileQualifier(Idnumber), "CreditConsolidationTile");
            TryVerifyQualifier(() => VerifyCapfinTileQualifier(Idnumber), "CapfinTile");
        }

        private void TryVerifyQualifier(Action verificationAction, string qualifierName)
        {
            try
            {
                verificationAction();
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"{qualifierName} Qualifier is not verified due to error {ex}");
            }
        }

        private void NavigateToHomePage(AgentUiPage agentUiPage)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(agentUiPage.HomeTab);
            baseStep.wait.GenericWait(2000);
            baseStep.Click(agentUiPage.HomeTab);
            baseStep.wait.WaitTillPageLoad();
            validate.TakeStepFullScreenShot("Home Page is Visible", Status.Info);
        }

        private void VerifyHomePageQualifiers(HomePage homePage, HomePageSteps homePageSteps)
        {
            TryVerifyQualifier(() => homePageSteps.VerifySingleTileQualifierOnHomePage(homePage.SplQualifyMsg, expectedSplQualifier), "SPLTile");
            TryVerifyQualifier(() => homePageSteps.VerifySingleTileQualifierOnHomePage(homePage.CCQualifier, expectedCCQualifier), "CCQualifier");
            TryVerifyQualifier(() => homePageSteps.VerifySingleTileQualifierOnHomePage(homePage.CreditConsolQualifier, expectedCreditConsolQualifier), "CreditConsolidationTile");
            TryVerifyQualifier(() => homePageSteps.VerifySingleTileQualifierOnHomePage(homePage.CapfinQualifier_AgentUi, expectedCapfinQualifier), "CapfinTile");
        }

        private void VerifySPLTileQualifiersOnHomePage()
        {
            var homePage = new HomePage();
            var homePageSteps = new HomePageSteps();

            NavigateToHomePage(agentUiPage);
            homePageSteps.VerifySingleTileQualifierOnHomePage(homePage.SplQualifyMsg, expectedSplQualifier);
        }

        private void NavigateToCommunicationLogs()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.Click(agentUiPage.PanelTogle);
            baseStep.ScrollToElement(agentUiPage.CommLogs_Expand);
            baseStep.Click(agentUiPage.CommLogs_Expand);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.logdetails_button, 10);
            baseStep.ScrollToElement(agentUiPage.LogDetails_Button);
            baseStep.Click(agentUiPage.LogDetails_Button);
        }

        private void VerifyCapfinTileQualifiersOnHomePage()
        {
            var homePage = new HomePage();
            var homePageSteps = new HomePageSteps();

            NavigateToHomePage(agentUiPage);
            homePageSteps.VerifySingleTileQualifierOnHomePage(homePage.CapfinQualifier_AgentUi, expectedCapfinQualifier);
        }

        private void NavigateToSolutionTab(AgentUiPage agentUiPage)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(agentUiPage.solutiontab, 10);
            baseStep.wait.GenericWait(5000);

            try
            {
                baseStep.ScrollToElement(agentUiPage.SolutionTab);
                baseStep.Click(agentUiPage.SolutionTab);
            }
            catch (ElementClickInterceptedException)
            {
                baseStep.Click(agentUiPage.SolutionTab);
            }
            baseStep.wait.WaitTillPageLoad();
            validate.TakeStepFullScreenShot("Solution Page is Visible", Status.Info);
        }

        private void VerifyCreditCardTileQualifiersOnHomePage()
        {
            var homePage = new HomePage();
            var homePageSteps = new HomePageSteps();

            NavigateToHomePage(agentUiPage);
            homePageSteps.VerifySingleTileQualifierOnHomePage(homePage.CCQualifier, expectedCCQualifier);
        }

        private void VerifyCreditConsolidationTileQualifiersOnHomePage()
        {
            var homePage = new HomePage();
            var homePageSteps = new HomePageSteps();

            NavigateToHomePage(agentUiPage);
            homePageSteps.VerifySingleTileQualifierOnHomePage(homePage.CreditConsolQualifier, expectedCreditConsolQualifier);
        }

        private void VerifyPersonalFinance27TileQualifier(string Idnumber)
        {
            var solutionPageSteps = new SolutionPageSteps();

            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.finance27qualifier, 20);
            baseStep.ScrollToElement(agentUiPage.Finance27Qualifier);
            var actualQualifier = baseStep.getText.Text(agentUiPage.Finance27Qualifier);
            expectedFinance27Qualifier = solutionPageSteps.ReturnExpectedQualifierTextForCapfin(Idnumber, agentUiPage.Finance27Qualifier);
            validate.AssertEqualWithMessage(expectedFinance27Qualifier.ToLower(), actualQualifier.ToLower(), "Personal Finance 27 Qualifier text as expected", false);
            Report.ChildLog.Log(Status.Info, $"Personal Finance 27 Qualifier is visible {actualQualifier}");
        }

        private void ValidateStoreCardsQualifier(string creditCoachScore_Storecard)
        {
            var solutionPageSteps = new SolutionPageSteps();
            baseStep.wait.WaitForElementVisibilityLongWait(agentUiPage.trueworthqualifier, 20);
            baseStep.ScrollToElement(agentUiPage.TrueworthQualifier);
            string actualTrueworthQualifier = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].textContent;", agentUiPage.TrueworthQualifier);
            string actualIdentityQualifier = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].textContent;", agentUiPage.IdentityQualifier);
            expectedStoreCardsQualifier = solutionPageSteps.ReturnExpectedQualifierTextForStoreCard(creditCoachScore_Storecard);
            validate.AssertEqualWithMessage(expectedStoreCardsQualifier, actualTrueworthQualifier, "Trueworth Qualifier text as per expected", false);
            baseStep.ScrollToElement(agentUiPage.IdentityQualifier);
            validate.AssertEqualWithMessage(expectedStoreCardsQualifier, actualIdentityQualifier, "Identity Qualifier text as per expected", false);
        }
        #endregion
    }
}
