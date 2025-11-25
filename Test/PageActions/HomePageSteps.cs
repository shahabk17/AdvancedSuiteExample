using RazorEngine;

namespace SanlamAutomation
{
    public class HomePageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly DBCreditCoach dBCreditCoach = new();

        /// <summary>
        /// Validates the welcome text is displayed on homepage after login
        /// </summary>
        [Author("Shahab Khan")]
        public void IsUserWelcomeTextHomePage()
        {
            try
            {
                HomePage dashboardPage = new HomePage();
                baseStep.wait.WaitForElementClickableLongWait(dashboardPage.userwelcometexthomepage, 20);
                validate.AssertEquals(true, dashboardPage.IsUserWelcomeTextHomePage(), "Welcome Text is not visible at the time of assert", false);
                baseStep.MultipleClick(dashboardPage.userwelcometexthomepage, 5);
            }
            catch
            {
                Report.ChildLog.Log(Status.Info, "User didnt login first time");
            }

        }

        /// <summary>
        /// Verifies if dashboard page is loaded and visible
        /// </summary>
        [Author("Shahab Khan")]
        public void IsdashboardPageDispalyed()
        {
            HomePage dashboardPage = new HomePage();
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(dashboardPage.homepage, 20);
            baseStep.wait.GenericWait(2000);
            Assert.That(dashboardPage.IsHomePageDisplayed());
        }

        /// <summary>
        /// Handles the salary popup dialog and enters the provided salary
        /// </summary>
        [Author("Shahab Khan")]
        public void WaitTillSalaryPopUpIsDisplayed(string enterSalary)
        {
            HomePage dashboardPage = new HomePage();
            baseStep.wait.WaitForElementVisibilityLongWait(dashboardPage.salarypopup, 20);
            string salaryPopUpText = baseStep.getText.Text(dashboardPage.SalaryPopUp);
            Report.ChildLog.Log(Status.Info, "Salary Pop Text - " + salaryPopUpText);
            baseStep.SendKeys(dashboardPage.TakeHomeSalary, enterSalary);
            validate.TakeStepFullScreenShot("Take Home Salary Entered ", Status.Pass);
            baseStep.Click(dashboardPage.SalaryPopUpSubmitBtn);
        }

        /// <summary>
        /// Enters salary information after login and updates profile if needed
        /// </summary>
        [Author("Shahab Khan")]
        public void EnterSalaryAndRepaymentValueAfterLogin(String IdNumber, String salary)
        {
            HomePage dashboardPage = new HomePage();
            IsdashboardPageDispalyed();
            try
            {
                baseStep.wait.WaitForElementVisibilityLongWait(dashboardPage.salarypopup, 20);

                /*
                 * As per Dev team Repayment value can only be calculated from bureau end
                 */
                //  enterRepaymentValue(IdNumber, expectedRepaymentValue);

                string salaryPopUpText = baseStep.getText.Text(dashboardPage.SalaryPopUp);
                Report.ChildLog.Log(Status.Info, "Salary Pop Text - " + salaryPopUpText);
                baseStep.SendKeys(dashboardPage.TakeHomeSalary, salary);
                validate.TakeStepFullScreenShot("Take Home Salary Entered ", Status.Pass);
                baseStep.Click(dashboardPage.SalaryPopUpSubmitBtn);

            }
            catch (Exception e)
            {
                Report.ChildLog.Log(Status.Info, "No Salary Popup is visible due to " + e);
                baseStep.wait.WaitForElementVisibilityLongWait(dashboardPage.homepage, 60);
                /*
                 * As per Dev team Repayment value can only be calculated from bureau end
                 */

                // enterRepaymentValue(IdNumber,expectedRepaymentValue);

                baseStep.wait.GenericWait(5000);
                baseStep.Click(dashboardPage.ProfileIcon);
                baseStep.wait.WaitForElementVisibility(dashboardPage.profileoption);
                baseStep.Click(dashboardPage.ProfileOption);

                baseStep.wait.WaitForElementVisibilityLongWait(dashboardPage.profilecurrencyfield, 60);
                baseStep.ClearAndSendKeys(dashboardPage.ProfileCurrencyField, salary);
                baseStep.ScrollToElement(dashboardPage.ProfileUpdateBtn);

                if (dashboardPage.ProfileUpdateBtn.Enabled)
                {
                    baseStep.Click(dashboardPage.ProfileUpdateBtn);
                    baseStep.wait.WaitForElementVisibilityLongWait(dashboardPage.profileupdatemsg, 60);
                    baseStep.wait.GenericWait(5000);
                }
                else
                {
                    baseStep.ClearAndSendKeys(dashboardPage.ProfileCurrencyField, salary);
                    baseStep.Click(dashboardPage.ProfileUpdateBtn);
                    baseStep.wait.WaitForElementVisibilityLongWait(dashboardPage.profileupdatemsg, 60);
                    baseStep.wait.GenericWait(5000);
                }

                validate.TakeStepFullScreenShot("Take Home Salary Entered is " + salary, Status.Pass);

                baseStep.Click(dashboardPage.DashboardIcon);
            }
        }

        /// <summary>
        /// Validates debt-to-income ratio and associated risk indicators
        /// </summary>
        [Author("Shahab Khan")]
        public void CheckDebt(string expectedIncomeValue)
        {
            HomePage dashboardPage = new HomePage();
            string expectedDebtToIncomeRatioText;
            baseStep.ScrollToElement(dashboardPage.RepaymentsCircle);
            string actualRepaymentValue = baseStep.getText.Text(dashboardPage.RepaymentsCircle);
            string actualRV = genericUtils.SplitString(actualRepaymentValue, " ", 1);
            string aRV;
            if (actualRV.Contains(","))
            {
                aRV = actualRV.Replace(",", "");
            }
            else
            {
                aRV = actualRV;
            }

            Report.ChildLog.Log(Status.Info, "RepaymentValue value is equal to " + aRV);

            string actualIncomeValue = baseStep.getText.Text(dashboardPage.IncomeCircle);
            string actualIV = genericUtils.SplitString(actualIncomeValue, " ", 1);
            validate.AssertEquals(expectedIncomeValue, actualIV, "Income Value is not matched", true);
            Report.ChildLog.Log(Status.Info, "actualIncomeValue value is equal to expectedIncomeValue");

            int ERV = int.Parse(aRV);
            int EIV = int.Parse(expectedIncomeValue);


            decimal per = ((decimal)ERV / (decimal)EIV) * 100;
            decimal debtPer = Math.Round(per);
            string DebtToIncomeRatioColor = null;



            if (debtPer <= 20)
            {
                string DebtToIncomeRatio = baseStep.getText.Text(dashboardPage.DebtToIncomeRatio);
                int DTIR = int.Parse(DebtToIncomeRatio.Replace("%", ""));
                Assert.That(DTIR <= 20);
                expectedDebtToIncomeRatioText = "Very High Chance";
                string actualDebtToIncomeRatioText = baseStep.getText.Text(dashboardPage.DebtToIncomeRatioText);
                string color = dashboardPage.DebtToIncomeRatioText.GetCssValue("color");
                if (color.Contains("(48, 149, 95, 1)"))
                {
                    DebtToIncomeRatioColor = "Green";
                }
                Assert.That(DebtToIncomeRatioColor == "Green");
                validate.AssertEquals(expectedDebtToIncomeRatioText, actualDebtToIncomeRatioText, "Debt-To-Income Ratio Text is not matched", false);
                Report.ChildLog.Log(Status.Pass, "User have DebtToIncomeRatio is below 20% and having " + actualDebtToIncomeRatioText + " and Text color is " + DebtToIncomeRatioColor);

            }
            else if (debtPer >= 21 && debtPer <= 40)
            {
                string DebtToIncomeRatio = baseStep.getText.Text(dashboardPage.DebtToIncomeRatio);
                int DTIR = int.Parse(DebtToIncomeRatio.Replace("%", ""));
                Assert.That(DTIR >= 21 && DTIR <= 40);
                expectedDebtToIncomeRatioText = "High Chance";
                string actualDebtToIncomeRatioText = baseStep.getText.Text(dashboardPage.DebtToIncomeRatioText);
                string color = dashboardPage.DebtToIncomeRatioText.GetCssValue("color");
                if (color.Contains("(165, 193, 94, 1)"))
                {
                    DebtToIncomeRatioColor = "Light Green";
                }
                validate.AssertEquals(expectedDebtToIncomeRatioText, actualDebtToIncomeRatioText, "Debt-To-Income Ratio Text is not matched", false);
                Report.ChildLog.Log(Status.Pass, "User have DebtToIncomeRatio is above 20% and below 40% and having " + actualDebtToIncomeRatioText + " and Text color is " + DebtToIncomeRatioColor);
            }
            else if (debtPer >= 41 && debtPer <= 60)
            {
                string DebtToIncomeRatio = baseStep.getText.Text(dashboardPage.DebtToIncomeRatio);
                int DTIR = int.Parse(DebtToIncomeRatio.Replace("%", ""));
                Assert.That(DTIR >= 41 && DTIR <= 60);
                expectedDebtToIncomeRatioText = "Moderate Chance";
                string actualDebtToIncomeRatioText = baseStep.getText.Text(dashboardPage.DebtToIncomeRatioText);
                string color = dashboardPage.DebtToIncomeRatioText.GetCssValue("color");
                if (color.Contains("(248, 217, 38, 1)"))
                {
                    DebtToIncomeRatioColor = "Yellow";
                }
                validate.AssertEquals(expectedDebtToIncomeRatioText, actualDebtToIncomeRatioText, "Debt-To-Income Ratio Text is not matched", false);
                Report.ChildLog.Log(Status.Pass, "User have DebtToIncomeRatio is above 40% and below 60% and having " + actualDebtToIncomeRatioText + " and Text color is " + DebtToIncomeRatioColor);
            }
            else if (debtPer >= 61 && debtPer <= 80)
            {
                string DebtToIncomeRatio = baseStep.getText.Text(dashboardPage.DebtToIncomeRatio);
                int DTIR = int.Parse(DebtToIncomeRatio.Replace("%", ""));
                Assert.That(DTIR >= 61 && DTIR <= 80);
                expectedDebtToIncomeRatioText = "Low Chance";
                string actualDebtToIncomeRatioText = baseStep.getText.Text(dashboardPage.DebtToIncomeRatioText);
                string color = dashboardPage.DebtToIncomeRatioText.GetCssValue("color");
                if (color.Contains("(241, 144, 68, 1)"))
                {
                    DebtToIncomeRatioColor = "Orange";
                }
                validate.AssertEquals(expectedDebtToIncomeRatioText, actualDebtToIncomeRatioText, "Debt-To-Income Ratio Text is not matched", false);
                Report.ChildLog.Log(Status.Pass, "User have DebtToIncomeRatio is above 60% and below 80% and having " + actualDebtToIncomeRatioText + " and Text color is " + DebtToIncomeRatioColor);
            }
            else if (debtPer >= 81)
            {
                string DebtToIncomeRatio = baseStep.getText.Text(dashboardPage.DebtToIncomeRatio);
                int DTIR = int.Parse(DebtToIncomeRatio.Replace("%", ""));
                Assert.That(DTIR >= 81);
                expectedDebtToIncomeRatioText = "Very Low Chance";
                string actualDebtToIncomeRatioText = baseStep.getText.Text(dashboardPage.DebtToIncomeRatioText);
                string color = dashboardPage.DebtToIncomeRatioText.GetCssValue("color");
                if (color.Contains("(246, 52, 52, 1)"))
                {
                    DebtToIncomeRatioColor = "Red";
                }
                validate.AssertEquals(expectedDebtToIncomeRatioText, actualDebtToIncomeRatioText, "Debt-To-Income Ratio Text is not matched", false);
                Report.ChildLog.Log(Status.Pass, "User have DebtToIncomeRatio is above 80% and having " + actualDebtToIncomeRatioText + " and Text color is " + DebtToIncomeRatioColor);
            }


        }

        /// <summary>
        /// Retrieves and validates number of open accounts across different categories
        /// </summary>
        [Author("Shahab Khan")]
        public void CheckNoOfOpenAccounts()
        {
            HomePage dashboardPage = new HomePage();

            IsdashboardPageDispalyed();
            baseStep.wait.WaitForElementVisibilityLongWait(dashboardPage.noofopenaccountsforhome, 60);
            baseStep.ScrollToElement(dashboardPage.NoOfOpenAccountsForHome);
            baseStep.wait.GenericWait(2000);

            // Home Account

            string NoOfOpenAccountsForHome = baseStep.getText.Text(dashboardPage.NoOfOpenAccountsForHome);
            Report.ChildLog.Log(Status.Info, "No. Of Open Accounts for Home are " + NoOfOpenAccountsForHome);


            // Car Account

            string NoOfOpenAccountsForCar = baseStep.getText.Text(dashboardPage.NoOfOpenAccountsForHome);
            Report.ChildLog.Log(Status.Info, "No. Of Open Accounts for Car are " + NoOfOpenAccountsForCar);

            // Clothing Account

            string NoOfOpenAccountsForClothing = baseStep.getText.Text(dashboardPage.NoOfOpenAccountsForClothing);
            Report.ChildLog.Log(Status.Info, "No. Of Open Accounts for Clothing are " + NoOfOpenAccountsForClothing);

            // Credit Card

            string NoOfOpenAccountsForCreditCard = baseStep.getText.Text(dashboardPage.NoOfOpenAccountsForCreditCard);
            Report.ChildLog.Log(Status.Info, "No. Of Open Accounts for CreditCard are " + NoOfOpenAccountsForCreditCard);


            // Loans

            string NoOfOpenAccountsForLoans = baseStep.getText.Text(dashboardPage.NoOfOpenAccountsForLoans);
            Report.ChildLog.Log(Status.Info, "No. Of Open Accounts for Loans are " + NoOfOpenAccountsForLoans);

        }

        /// <summary>
        /// Validates credit score and associated risk status
        /// </summary>
        [Author("Shahab Khan")]
        public void YourCreditScore(string IdNumber)
        {
            HomePage homePage = new HomePage();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            IsdashboardPageDispalyed();

            //Credit Score
            baseStep.wait.WaitForElementVisibilityLongWait(homePage.creditscore, 60);
            string expectedCreditScore = dBCreditCoach.GetCreditScoreFromDB(IdNumber);
            string actualCreditScoreAfter = baseStep.getText.Text(homePage.CreditScore).Replace("%", "");
            validate.AssertEqualWithMessage(expectedCreditScore, actualCreditScoreAfter, "Credit Coach score is updated", false);

            // Credit Score Status
            int CS = int.Parse(actualCreditScoreAfter);
            string expectedCreditScoreStatus = CS switch
            {
                0 => "You Don't Have A Credit Score",
                > 0 and <= 20 => "Very High Risk",
                > 20 and <= 40 => "High Risk",
                > 40 and <= 60 => "Medium Risk",
                > 60 and <= 80 => "Low Risk",
                > 80 and <= 100 => "Very Low Risk",
                _ => "Invalid Credit Score" // Default case for invalid CS values
            };
            string actualCreditScoreStatus = baseStep.getText.Text(homePage.CreditScoreStatus);
            validate.AssertEquals(expectedCreditScoreStatus.ToLower(), actualCreditScoreStatus.ToLower(), "expectedCreditScoreStatus is not equal to Actual", false);
            Report.ChildLog.Log(Status.Info, "expectedCreditScoreStatus i.e " + expectedCreditScoreStatus + " is eqaul to actualCreditScoreStatus i.e." + actualCreditScoreStatus);

        }

        /// <summary>
        /// Verifies success popup for special qualified users
        /// </summary>
        [Author("Shahab Khan")]
        public void SuccessPopup(bool splqualifieduser)
        {

            HomePage homePage = new HomePage();
            SolutionPage SolutionPage = new SolutionPage();
            baseStep.wait.WaitTillPageLoad();

            if (splqualifieduser)
            {
                if (validate.IsElementDisplayed(homePage.applynowbtn))
                {
                    baseStep.MultipleClick(homePage.applynowbtn, 2);
                }
                else
                {
                    baseStep.Click(SolutionPage.SolutionsIcon);
                }
                baseStep.wait.WaitTillPageLoad();
                baseStep.wait.WaitForElementVisibilityLongWait(SolutionPage.applynowbtn, 60);
                baseStep.Click(SolutionPage.ApplyNowBtn);
                baseStep.wait.GenericWait(3000);
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                wait.Until(Driver => Driver.WindowHandles.Count > 1);
                try
                {
                    Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                    baseStep.wait.WaitForElementVisibilityLongWait(SolutionPage.otptext, 100);
                    Assert.That(validate.IsElementDisplayed(SolutionPage.otptext));
                    string OTPText = baseStep.getText.Text(SolutionPage.OTPText);
                    validate.TakeStepFullScreenShot("Third Party Page is visible to enter OTP", Status.Info);
                    Report.ChildLog.Log(Status.Info, "Second Page is visible with Text " + OTPText);
                    Driver.Close();
                    Driver.SwitchTo().Window(Driver.WindowHandles.First());
                }
                catch
                {
                    Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                    validate.TakeStepFullScreenShot("Third Party Page is visible to enter OTP", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(Driver.WindowHandles.First());
                }
            }

        }

        /// <summary>
        /// Validates call me back functionality on homepage
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackHomePage(string IdNumber)
        {
            HomePage HomePage = new HomePage();
            baseStep.wait.WaitTillPageLoad();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            do { genericUtils.ScrollTillHalfPage(); }
            while (!HomePage.CallMeBackBtn.Displayed);
            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(HomePage.CallMeBackBtn);
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(HomePage.CallMeBackBtn);

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(HomePage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(HomePage.CallMeBackYesBtn);

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(HomePage.callmebacksuccesmsg, 60);
            string ccSuccessMsg = baseStep.getText.Text(HomePage.CallMeBackSuccessMsg);
            Assert.That(HomePage.CallMeBackSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, "Success Message is Visible with text " + ccSuccessMsg);
            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(HomePage.CallMeBackCutBtn);
            dBCreditCoach.GetCampaignSourceValidate(IdNumber, "Client Home Page");
        }

        /// <summary>
        /// Verifies qualification status for different product tiles
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyTileQualifiersOnHomePage(string Idnumber, bool isQualifiedSPL)
        {
            HomePage HomePage = new HomePage();
            SolutionPageSteps solutionPageSteps = new SolutionPageSteps();
            baseStep.wait.WaitTillPageLoad();

            baseStep.Click(HomePage.HomePageVisible);
            baseStep.wait.WaitTillPageLoad();
            validate.TakeStepFullScreenShot("Home Page is Visible", Status.Info);
            try
            {
                Report.ChildLog.Log(Status.Info, $"****Verify SPLTile On HomePage****");
                string expectedSplQualifier = solutionPageSteps.ReturnExpectedQualifierTextForSPL(Idnumber, isQualifiedSPL);
                VerifySingleTileQualifierOnHomePage(HomePage.SplQualifyMsg, expectedSplQualifier);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"SPLTile Qualifier is not verified due to error {ex}");
            }
            try
            {
                Report.ChildLog.Log(Status.Info, $"****Verify CCQualifier On HomePage****");
                string expectedCCQualifier = solutionPageSteps.ReturnExpectedQualifierTextForCC(Idnumber, HomePage.CCQualifier);
                VerifySingleTileQualifierOnHomePage(HomePage.CCQualifier, expectedCCQualifier);

            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"CCQualifier is not verified due to error {ex}");
            }
            try
            {
                Report.ChildLog.Log(Status.Info, $"****Verify CapfinTile On HomePage****");
                string expectedCapfinQualifier = solutionPageSteps.ReturnExpectedQualifierTextForCapfin(Idnumber, HomePage.CapfinQualifier);
                VerifySingleTileQualifierOnHomePage(HomePage.CapfinQualifier, expectedCapfinQualifier);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"CapfinTile Qualifier is not verified due to error {ex}");
            }
            try
            {
                Report.ChildLog.Log(Status.Info, $"****Verify CreditConsolidationTile On HomePage****");
                string monthlySaving = null;
                string expectedCreditConsolQualifier = solutionPageSteps.ReturnExpectedQualifierTextForCreditConsolidation(Idnumber, HomePage.CreditConsolQualifier, monthlySaving);
                baseStep.wait.WaitTillPageLoad();
                VerifySingleTileQualifierOnHomePage(HomePage.CreditConsolQualifier, expectedCreditConsolQualifier);
            }
            catch (Exception ex)
            {
                Report.ChildLog.Log(Status.Info, $"CreditConsolidationTile Qualifier is not verified due to error {ex}");
            }

        }

        /// <summary>
        /// Method is used to check tile qualifier on homepage
        /// </summary>
        /// <param name="element"></param>
        /// <param name="expectedQualifier"></param>
        [Author("Shahab Khan")]
        public void VerifySingleTileQualifierOnHomePage(IWebElement element, string expectedQualifier)
        {
            baseStep.ScrollToElement(element);
            baseStep.wait.GenericWait(3000);
            validate.TakeStepFullScreenShot("Qualifier is Visible", Status.Info);
            string actualQualifier = baseStep.getText.Text(element);
            validate.AssertEqualWithMessage(expectedQualifier, actualQualifier, "Qualifier text is as per expected", false);
        }

        [Author("Shahab Khan")]
        public void HandlePreQualifyLoanPopup()
        {
            HomePage homePage = new HomePage();
            if (validate.IsElementClickable(homePage.prequalifyloanpopup_cancelbtn, 10))
            {
                baseStep.MultipleClick(homePage.prequalifyloanpopup_cancelbtn, 3);
            }
        }

        /// <summary>
        /// Validates wealth score information and updates
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyWealthScore()
        {
            HomePage homePage = new HomePage();
            WealthPage wealthPage = new WealthPage();

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(homePage.wealthupdatebtn_firsttimelogin, 60);
            baseStep.wait.GenericWait(2000);
            baseStep.ScrollToElement(homePage.WealthUpdateBtn_FirstTimeLogin);
            baseStep.wait.WaitTillPageLoad();
            baseStep.Click(homePage.WealthUpdateBtn_FirstTimeLogin);

            baseStep.wait.WaitTillPageLoad();
            validate.AssertEqualWithMessage(true, validate.IsElementDisplayed(wealthPage.vehiclefield), "Wealth page is displayed", false);

            baseStep.Click(homePage.HomePageVisible);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(homePage.wealthupdatebtn_firsttimelogin, 60);
            Report.ChildLog.Log(Status.Info, "user has not updated the wealth score and value of Wealth score details not display");
        }


        /// <summary>
        /// Validates view solutions button functionality
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyViewSolutionsButton()
        {
            HomePage homePage = new HomePage();
            validate.TakeStepFullScreenShot("View Solutions Button", Status.Info);
            baseStep.Click(homePage.ViewSolutionsButton);
            baseStep.wait.WaitForElementVisibilityLongWait(homePage.viewsolutionspagetext, 20);
            validate.AssertEqualWithMessage("Solutions for You", homePage.ViewSolutionsPageText.Text, "Successfully navigated to Solutions Page", false);
        }

        /// <summary>
        /// Validates credit insights button functionality
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyViewCreditInsightsButton()
        {
            HomePage homePage = new HomePage();
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(homePage.homepage, 20);
            baseStep.Click(homePage.HomePageVisible);
            baseStep.wait.WaitTillPageLoad();
            if (!validate.IsElementClickable(homePage.viewcreditinsightsbutton, 20))
            {
                baseStep.Click(homePage.HomePageVisible);
            }
            validate.TakeStepFullScreenShot("View Credit Insights Button", Status.Info);
            baseStep.Click(homePage.ViewCreditInsightsButton);
            baseStep.wait.WaitTillPageLoad();
            String viewCreditInsightsActualTitle = Driver.Title;
            validate.AssertEqualWithMessage("Sanlam Credit Solutions", viewCreditInsightsActualTitle, "Successfully navigated to View Credit Insights", false);
        }

        /// <summary>
        /// Verifies recommended solutions tiles and their navigation
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyRecommendeSolutionsForYouTiles()
        {
            HomePage homePage = new HomePage();
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(homePage.homepage, 20);
            baseStep.Click(homePage.HomePageVisible);
            baseStep.wait.WaitTillPageLoad();

            for (int i = 1; i <= 5; i++)
            {
                genericUtils.ScrollTillHalfPage();
                baseStep.wait.GenericWait(2000);
                baseStep.ScrollToElement(homePage.RecommendedSolutionsForYouTiles(i));
                validate.TakeStepFullScreenShot($"Recommended Solutions For You Tile {i}", Status.Info);
                baseStep.Click(homePage.RecommendedSolutionsForYouTiles(i));
                baseStep.wait.WaitForElementVisibilityLongWait(homePage.viewsolutionspagetext, 60);
                validate.AssertEqualWithMessage("Solutions for You", homePage.ViewSolutionsPageText.Text, "Successfully navigated to Solutions Page", true);

                baseStep.wait.WaitForElementClickableLongWait(homePage.homepage, 20);
                baseStep.Click(homePage.HomePageVisible);
                baseStep.wait.WaitTillPageLoad();
                if (!validate.IsElementClickable(homePage.recommendedsolutionsforyoutiles(i), 20))
                {
                    baseStep.Click(homePage.HomePageVisible);
                }
            }
        }

        /// <summary>
        /// Validates take home salary debt ratio and associated risk level
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyTakeHomeSalaryTowardsDebt(string idNumber)
        {
            HomePage homePage = new HomePage();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();
            string expectedPercent = dBCreditCoach.SalaryGoingTowardDebtPercent(idNumber).Replace(".00", "");
            baseStep.ScrollToElement(homePage.TakeHomeSalaryTowardsDebtPercentage);
            string actualPercentage = baseStep.getText.Text(homePage.TakeHomeSalaryTowardsDebtPercentage).Replace("%", "");
            validate.AssertEqualWithMessage(expectedPercent, actualPercentage, "Take Home Salary Towards Debt is as expected", false);

            string expectedText = int.Parse(actualPercentage) switch
            {
                >= 81 and <= 999999 => "Very High Risk",
                >= 61 and <= 80 => "High Risk",
                >= 41 and <= 60 => "Medium Risk",
                >= 21 and <= 40 => "Low Risk",
                >= 0 and <= 20 => "Very Low Risk"
            };
            string actualText = baseStep.getText.Text(homePage.TakeHomeSalaryTowardsDebtText);
            validate.AssertEqualWithMessage(expectedText.ToLower(), actualText.ToLower(), "Take HomeSalary Towards Debt Text is as expected", true);
        }

        /// <summary>
        /// Validates overdue amount and associated risk indicators
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyOverdueAmount(string idNumber)
        {
            HomePage homePage = new HomePage();
            DBCreditCoach dBCreditCoach = new DBCreditCoach();

            string expectedOverdueAmount = dBCreditCoach.TotalOverdueAmount(idNumber).Replace(".00", "");
            baseStep.ScrollToElement(homePage.OverdueAmount);
            string actualOverdueAmount = genericUtils.SplitString(baseStep.getText.Text(homePage.OverdueAmount), " ", 1).Replace(",", "");
            validate.AssertEqualWithMessage(expectedOverdueAmount, actualOverdueAmount, "Total Overdue Amount is as expected", false);

            string expectedOverdueAmountText = int.Parse(actualOverdueAmount) switch
            {
                >= 502 and <= 99999999 => "Very High Risk",
                >= 1 and <= 501 => "High Risk",
                0 => "Low Risk"
            };
            string actualOverdueAmountText = baseStep.getText.Text(homePage.OverdueAmountText);
            validate.AssertEqualWithMessage(expectedOverdueAmountText.ToLower(), actualOverdueAmountText.ToLower(), "Overdue Amount Text is as expected", true);
        }

        /// <summary>
        /// This method validates database entries after branch registration by checking user and branch details, login timestamps, branch code, and log existence.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="urlParameter"></param>
        [Author("Piyush Sharma")]
        public void ValidateDBPostBranchRegistration(string IdNumber, string urlParameter)
        {
            var userDetails = dBCreditCoach.FetchUserDetailsFromUserTable(IdNumber);
            string branchId = userDetails["BranchId"].ToString();

            var branchDetails = dBCreditCoach.FetchBranchDetails(branchId);
            string branchCode = branchDetails["Code"].ToString();

            string currentDateTimeString = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss");
            DateTime currentDateTime = DateTime.Parse(currentDateTimeString);

            string firstLoginDateTimeString = userDetails["FirstLoginDateTime"].ToString();
            DateTime firstLoginDateTime = DateTime.Parse(firstLoginDateTimeString);

            string currentDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
            string firstLoginDate = firstLoginDateTime.ToString("dd-MM-yyyy");

            validate.AssertEquals(true, urlParameter.Contains(branchCode), "Branch Code is Mismatch", true);
            validate.AssertEquals(true, currentDateTime > firstLoginDateTime, "First Login Date & Time is incorrect", true);
            validate.AssertEquals(true, currentDate == firstLoginDate, "First Login Date is incorrect", true);
            validate.AssertEquals("True", userDetails["IsBranchUser"].ToString(), "IsBranch User validation is False", true);
            validate.AssertEquals(true, ValidateDaLesLog(IdNumber, 41, 0), "DaLes doesn't have any Log", true);
        }

        /// <summary>
        /// This method checks for a DaLes log entry by repeatedly querying the database within a timeout period, returning true if found, otherwise false.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="externalCommLogTypeId"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public bool ValidateDaLesLog(string IdNumber, int externalCommLogTypeId, int index, int timeoutInSeconds = 60)
        {
            DateTime timeout = DateTime.UtcNow.AddSeconds(timeoutInSeconds);

            while (DateTime.UtcNow < timeout)
            {
                var daLesLoginLog = dBCreditCoach.FetchExternalCommLogInfo(IdNumber, externalCommLogTypeId, index);
                if (daLesLoginLog.Count >= 1)
                    return true;

                baseStep.wait.GenericWait(5000);
            }
            return false;
        }

        /// <summary>
        ///  Method is used to check all fields on a page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyDashboardFieldsTracking(string idnumber)
        {
            MultipleClickOnDashboardElement(idnumber, "//a", 17);
            MultipleClickOnDashboardElement(idnumber, "//button", 0);
        }

        #region Private helper method

        private void MultipleClickOnDashboardElement(string idnumber, string elementType, int fieldIndex)
        {
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>Checking for {elementType} on Dashboard Page<<<<<<<<<<<");
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
                if (validate.IsElementClickable(homePage.homepage))
                {
                    baseStep.ScrollToElement(homePage.HomePageVisible);
                    baseStep.wait.WaitForElementClickableLongWait(homePage.homepage, 10);
                    baseStep.Click(homePage.HomePageVisible);
                    baseStep.wait.WaitTillPageLoad();
                }
                totalFields = Driver.FindElements(By.XPath(elementType));
            }
            Task.WhenAll(logTasks).GetAwaiter().GetResult();
            appInsights.PrintCollectedLogs();
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>>>>Checked total fields: {j} of tag {elementType} and failure is not occur for user {idnumber}<<<<<<<<<<<<");
        }

        #endregion
    }
}