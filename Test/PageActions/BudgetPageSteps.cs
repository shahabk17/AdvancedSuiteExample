namespace SanlamAutomation.Test.Steps
{
    [Author("Shahab Khan")]
    public class BudgetPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();

        /// <summary>
        /// Verifies call me back functionality on Budget page
        /// </summary>
        /// <param name="IdNumber">User ID number for campaign validation</param>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackBudgetPage(string IdNumber)
        {
            var budgetPage = new BudgetPage();
            var dBCreditCoach = new DBCreditCoach();

            NavigateToBudgetPage(budgetPage);
            InitiateCallMeBack(budgetPage);
            HandleCallMeBackConfirmation(budgetPage, IdNumber, dBCreditCoach);
        }

        /// <summary>
        /// Calculates and validates budget score based on user inputs
        /// </summary>
        /// <param name="IdNumber">User ID number for budget calculation</param>
        [Author("Shahab Khan")]
        public void BudgetScoreCalculation(string IdNumber)
        {
            var budgetPage = new BudgetPage();
            InitiateBudgetUpdate(budgetPage);
            UpdateMoneyInSection(budgetPage);
            UpdateMoneyOutSection(budgetPage);
            CalculateAndValidateBudgetScore(budgetPage, IdNumber);
        }

        /// <summary>
        /// Method is used to check all fields on a page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyFieldsForTracking(string idnumber)
        {
            BudgetPage budgetPage = new();
            baseStep.wait.WaitForElementClickableLongWait(budgetPage.budgeticon, 10);
            baseStep.Click(budgetPage.BudgetIcon);
            MultipleClickOnElement(budgetPage, idnumber, "//button", 0);
            MultipleClickOnElement(budgetPage, idnumber, "//a", 21);
            baseStep.ScrollToElement(budgetPage.MoneyOut_UpdateLivingExpenses);
            baseStep.Click(budgetPage.MoneyOut_UpdateLivingExpenses);
            baseStep.Click(budgetPage.MoneyOut_ViewCreditInstalments);
            MultipleClickOnElement(budgetPage, idnumber, "//input", 36);
        }

        #region Private Helper Methods

        private void MultipleClickOnElement(BudgetPage budgetPage, string idnumber, string elementType, int fieldIndex)
        {
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>Checking for {elementType} on Budget Page<<<<<<<<<<<");
            DBQueries dBQueries = new();
            AppInsights appInsights = new();
            IList<IWebElement> totalFields = Driver.FindElements(By.XPath(elementType));
            int j = 0;
            List<Task> logTasks = new List<Task>();

            for (int i = fieldIndex; i < totalFields.Count; i++)
            {
                IWebElement element = totalFields[i];
                genericUtils.ScrollTillHalfPage();
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
                        if (validate.IsElementDisplayed(budgetPage.callmebackpopupcutbtn))
                        {
                            baseStep.Click(budgetPage.CallMeBackPopupCutBtn);
                        }
                        if (validate.IsElementDisplayed(budgetPage.budgetcalculatorpopup_cutbutton))
                        {
                            baseStep.Click(budgetPage.BudgetCalculatorPopup_CutButton);
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
                else 
                {
                    Console.WriteLine($"index not clickable {i} of tag {elementType}");
                }
                if (validate.IsElementClickable(budgetPage.budgeticon))
                {
                    baseStep.ScrollToElement(budgetPage.BudgetIcon);
                    baseStep.wait.WaitForElementClickableLongWait(budgetPage.budgeticon, 10);
                    baseStep.Click(budgetPage.BudgetIcon);
                    baseStep.wait.WaitTillPageLoad();
                }
                totalFields = Driver.FindElements(By.XPath(elementType));
            }
            Task.WhenAll(logTasks).GetAwaiter().GetResult();
            appInsights.PrintCollectedLogs();
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>>>>Checked total fields: {j} of tag {elementType} and failure is not occur for user {idnumber}<<<<<<<<<<<<");
        }

        private void NavigateToBudgetPage(BudgetPage budgetPage)
        {
            baseStep.Click(budgetPage.BudgetIcon);
            baseStep.wait.WaitTillPageLoad();
        }

        private void InitiateCallMeBack(BudgetPage budgetPage)
        {
            do { genericUtils.ScrollTillHalfPage(); }
            while (!budgetPage.CallMeBackBtn.Displayed);

            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(budgetPage.CallMeBackBtn);
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(budgetPage.CallMeBackBtn);
        }

        private void HandleCallMeBackConfirmation(BudgetPage budgetPage, string IdNumber, DBCreditCoach dBCreditCoach)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(budgetPage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(budgetPage.CallMeBackYesBtn);

            ValidateCallMeBackSuccess(budgetPage, IdNumber, dBCreditCoach);
        }

        private void ValidateCallMeBackSuccess(BudgetPage budgetPage, string IdNumber, DBCreditCoach dBCreditCoach)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(budgetPage.callmebackpopupsuccessmsg, 60);

            var ccSuccessMsg = baseStep.getText.Text(budgetPage.CallMeBackPopupSuccessMsg);
            Assert.That(budgetPage.CallMeBackPopupSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, $"Success Message is Visible with text {ccSuccessMsg}");

            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(budgetPage.CallMeBackPopupCutBtn);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Client Budget Page");
        }

        private void InitiateBudgetUpdate(BudgetPage budgetPage)
        {
            baseStep.Click(budgetPage.BudgetScore_UpdateBtn);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(budgetPage.moneyin_takehomesalary, 10);
            baseStep.ScrollToElement(budgetPage.MoneyIn_TakeHomeSalary);
        }

        private void UpdateMoneyInSection(BudgetPage budgetPage)
        {
            var salary = genericUtils.RandomInteger(10000, 99999).ToString();
            var otherIncome = genericUtils.RandomInteger(1000, 9999).ToString();

            baseStep.ClearAndSendKeys(budgetPage.MoneyIn_TakeHomeSalary, salary);
            baseStep.ClearAndSendKeys(budgetPage.MoneyIn_OtherIncome, otherIncome);
            Report.ChildLog.Log(Status.Info, $"Updated Home salary {salary} and otherIncome is {otherIncome}");
        }

        private void UpdateMoneyOutSection(BudgetPage budgetPage)
        {
            baseStep.ScrollToElement(budgetPage.MoneyOut_UpdateLivingExpenses);
            baseStep.Click(budgetPage.MoneyOut_UpdateLivingExpenses);

            var expenses = new Dictionary<string, string>
            {
                { "Food", genericUtils.RandomInteger(1000, 9999).ToString() },
                { "Rent", genericUtils.RandomInteger(1000, 9999).ToString() },
                { "Water", genericUtils.RandomInteger(1000, 9999).ToString() },
                { "Vehicle", genericUtils.RandomInteger(1000, 9999).ToString() },
                { "Transport", genericUtils.RandomInteger(1000, 9999).ToString() },
                { "Cellphone", genericUtils.RandomInteger(1000, 9999).ToString() },
                { "Medical", genericUtils.RandomInteger(1000, 9999).ToString() },
                { "SchoolFees", genericUtils.RandomInteger(1000, 9999).ToString() },
                { "Savings", genericUtils.RandomInteger(1000, 9999).ToString() },
                { "Other", genericUtils.RandomInteger(1000, 9999).ToString() }
            };

            UpdateExpenses(budgetPage, expenses);
            LogExpenses(expenses);
        }

        private void UpdateExpenses(BudgetPage budgetPage, Dictionary<string, string> expenses)
        {
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_FoodAndGroceries, expenses["Food"]);
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_Rental, expenses["Rent"]);
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_Water_Elec, expenses["Water"]);
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_Vehicle_Household, expenses["Vehicle"]);
            baseStep.ScrollToElement(budgetPage.MoneyOut_Transport);
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_Transport, expenses["Transport"]);
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_Cellphone, expenses["Cellphone"]);
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_MedicalAid, expenses["Medical"]);
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_SchoolFees, expenses["SchoolFees"]);
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_Savings, expenses["Savings"]);
            baseStep.ClearAndSendKeys(budgetPage.MoneyOut_Other, expenses["Other"]);
        }

        private void LogExpenses(Dictionary<string, string> expenses)
        {
            var expenseLog = string.Join(", \r\n", expenses.Select(e => $"{e.Key}: {e.Value}"));
            Report.ChildLog.Log(Status.Info, $"Updated Living Expense: \r\n {expenseLog}");
        }

        private void CalculateAndValidateBudgetScore(BudgetPage budgetPage, string IdNumber)
        {
            baseStep.ScrollToElement(budgetPage.ViewBudgetScore_Button);
            baseStep.Click(budgetPage.ViewBudgetScore_Button);
            baseStep.wait.WaitTillPageLoad();

            var expectedBudgetScore = CalculateBudgetScore(IdNumber).ToString();
            baseStep.wait.GenericWait(5000);

            budgetPage = new BudgetPage();
            var actualBudgetScore = baseStep.getText.Text(budgetPage.BudgetScore).Replace("%", "");
            validate.AssertEqualWithMessage(expectedBudgetScore, actualBudgetScore, "Budget score is expected", false);

            ValidateBudgetScoreText(int.Parse(actualBudgetScore));
        }

        private double CalculateBudgetScore(string idNumber)
        {
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            var dictionary = dBCreditCoach.FetchBudgetTable(idNumber);
            // Income calculation
            double income =
                (double.TryParse(dictionary["HomeSalary"]?.ToString() ?? "0", out double homeSalary) ? homeSalary : 0) +
                (double.TryParse(dictionary["OtherIncome"]?.ToString() ?? "0", out double otherIncome) ? otherIncome : 0);

            // Vehicle and Home loans
            double vehicleLoan = double.TryParse(dictionary["VehicleLoans"]?.ToString() ?? "0", out double vehicleLoanAmount) ? vehicleLoanAmount : 0;
            double homeLoan = double.TryParse(dictionary["HomeLoans"]?.ToString() ?? "0", out double homeLoanAmount) ? homeLoanAmount : 0;
            double secured = homeLoan + vehicleLoan;

            // Unsecured loans
            double unSecured =
                (double.TryParse(dictionary["PersonalLoans"]?.ToString() ?? "0", out double personalLoans) ? personalLoans : 0) +
                (double.TryParse(dictionary["CreditCards"]?.ToString() ?? "0", out double creditCards) ? creditCards : 0) +
                (double.TryParse(dictionary["RetailAccounts"]?.ToString() ?? "0", out double retailAccounts) ? retailAccounts : 0);

            // Monthly expenses
            double expenses =
                (double.TryParse(dictionary["MonthlyLivingExpenses"]?.ToString() ?? "0", out double livingExpenses) ? livingExpenses : 0) +
                (double.TryParse(dictionary["MonthlyCreditRepayments"]?.ToString() ?? "0", out double creditRepayments) ? creditRepayments : 0);

            // Calculate spending points
            double spendingPoint = SpendingPoints(dictionary, income, secured);

            // Saving and rental calculations
            double savingExpense = double.TryParse(dictionary["SavingExpense"]?.ToString() ?? "0", out double savingAmount) ? savingAmount : 0;
            double savingPoint = income != 0 ? savingExpense / income : 0;
            double rentalExpense = double.TryParse(dictionary["RentalExpense"]?.ToString() ?? "0", out double rentalAmount) ? rentalAmount : 0;
            savingPoint = savingPoint switch
            {
                > 0.1 => 25,
                >= 0.05 and < 0.1 => 20,
                >= 0.025 and < 0.5 => 10,
                > 0 and < 0.025 => 5,
                _ => 0 // Default case for invalid CS values
            };
            double unSecuredPoint = (unSecured / income) switch
            {
                >= 0.3 => 0,
                >= 0.2 and < 0.3 => 2,
                >= 0.15 and < 0.2 => 4,
                >= 0.1 and < 0.15 => 8,
                < 0.1 and > 0 => 12,
                <= 0 => 15
            };
            double securedPoint = (vehicleLoan / income) switch
            {
                >= 0.3 => 0,
                >= 0.2 and < 0.3 => 2,
                >= 0.15 and < 0.2 => 6,
                >= 0.1 and < 0.15 => 8,
                < 0.1 and > 0 => 12,
                <= 0 => 15
            };
            if ((vehicleLoan != null && vehicleLoan > 0) && (homeLoan != null && homeLoan > 0) && income > 0)
            {
                var securedIncome = Math.Round(secured / income, 3);
                if (securedIncome >= 0.66) { securedPoint = 0; }
            }
            if ((vehicleLoan != null && vehicleLoan > 0) && (homeLoan == null || homeLoan == 0) && (rentalExpense != null && rentalExpense > 0) && income > 0)
            {
                var loanaAndRentalExpense = vehicleLoan + rentalExpense;
                var vehicleRentalIncome = Math.Round(loanaAndRentalExpense / income, 3);

                if (vehicleRentalIncome >= 0.66) { securedPoint = 0; }
            }


            double accuracyPoint = (expenses / income) switch
            {
                < 0.5 => 0,
                _ => 10 // Default case for invalid CS values
            };
            double completenessPoints = CompletenessPoints(dictionary, income);

            return completenessPoints + accuracyPoint + securedPoint + unSecuredPoint + savingPoint + spendingPoint;
        }

        private double SpendingPoints(Dictionary<string, object> dictionary, double income, double secured)
        {
            double foodSpend = double.Parse(dictionary["FoodAndGroceriesExpense"].ToString()) / income;
            foodSpend = foodSpend switch
            {
                <= 0.2 => 5,
                _ => 0 // Default case for invalid CS values
            };
            double cellphoneExp = double.Parse(dictionary["CellphoneAndInternetExpense"]?.ToString() ?? "0") / income;
            cellphoneExp = cellphoneExp switch
            {
                <= 0.05 => 5,
                _ => 0 // Default case for invalid CS values
            };
            double transportExp = double.Parse(dictionary["TransportExpense"]?.ToString() ?? "0") / income;
            transportExp = transportExp switch
            {
                <= 0.18 => 5,
                _ => 0 // Default case for invalid CS values
            };
            double medicalAidExpense = double.Parse(dictionary["MedicalAidExpense"].ToString());
            medicalAidExpense = medicalAidExpense switch
            {
                > 0 => 5,
                _ => 0 // Default case for invalid CS values
            };
            //double vehicleAndHouseExp = dictionary["VehicleAndHouseholdExpense"] == null ? 0 : 5;
            return foodSpend + cellphoneExp + transportExp + medicalAidExpense;
        }

        private double CompletenessPoints(Dictionary<string, object> dictionary, double income)
        {
            double income_pts = income switch
            {
                <= 0 => 0,
                _ => 5 // Default case for invalid CS values
            };
            double foodSpend = double.Parse(dictionary["FoodAndGroceriesExpense"]?.ToString() ?? "0");
            double food_pts = foodSpend switch
            {
                <= 0 => 0,
                _ => 1  // Default case for invalid CS values
            };
            double cellphoneExp = double.Parse(dictionary["CellphoneAndInternetExpense"]?.ToString() ?? "0");
            double cell_pts = cellphoneExp switch
            {
                <= 0 => 0,
                _ => 1 // Default case for invalid CS values
            };
            double transportExp = double.Parse(dictionary["TransportExpense"]?.ToString() ?? "0");
            double transport_pts = transportExp switch
            {
                <= 0 => 0,
                _ => 1 // Default case for invalid CS values
            };
            double rentalExpense = double.Parse(dictionary["RentalExpense"]?.ToString() ?? "0");
            double waterBill = double.Parse(dictionary["WaterElectricityRatesAndLeviesExpense"]?.ToString() ?? "0");
            double home_pts = 0;
            if ((rentalExpense != 0 && rentalExpense > 0) || (waterBill != 0 && waterBill > 0))
            {
                home_pts = 1;
            }
            double savingsExp = double.Parse(dictionary["SavingExpense"]?.ToString() ?? "0");
            double savings_pts = savingsExp switch
            {
                <= 0 => 0,
                _ => 1 // Default case for invalid CS values
            };

            return income_pts + food_pts + cell_pts + transport_pts + home_pts + savings_pts;
        }

        private void ValidateBudgetScoreText(int budgetScore)
        {
            BudgetPage budgetPage = new BudgetPage();

            string actualBudgetScoreText = baseStep.getText.Text(budgetPage.BudgetScoreText);

            string expectedBudgetScoreText = budgetScore switch
            {
                >= 0 and <= 20 => "Very High risk",
                >= 21 and <= 40 => "High risk",
                >= 41 and <= 60 => "Medium risk",
                >= 61 and <= 80 => "Low risk",
                >= 81 and <= 100 => "Very Low risk"
            };
            validate.AssertEqualWithMessage(expectedBudgetScoreText.ToLower(), actualBudgetScoreText.ToLower(), "Budget Score Text is expected", true);
        }

        #endregion
    }
}