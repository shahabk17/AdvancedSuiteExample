namespace SanlamAutomation.Test.Pages
{
    public class BudgetPage : WebDriverSession
    {
        public By budgeticon = By.XPath("//*[text()=' Budget']");
        public IWebElement BudgetIcon => Driver.FindElement(budgeticon);

        public By callmebackbtn = By.XPath("//*[@id=\"Call Me Back Budget\"]");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("//*[@id=\"BudgetCallBack_Yes\"]");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn => Driver.FindElement(callmebackpopupcutbtn);

        public By callmebackpopupsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg => Driver.FindElement(callmebackpopupsuccessmsg);

        // budget tool - bank account page

        public By linkaccountfield = By.XPath("//h2[@class=\"sol-caption pt-4\"]");
        public IWebElement LinkAccountField => Driver.FindElement(linkaccountfield);

        public By linkaccountbtn = By.XPath("//*[@id=\"Link to Stitch Account\"]");
        public IWebElement LinkAccountBtn => Driver.FindElement(linkaccountbtn);

        public By linkaccountaddbtn = By.XPath("//*[contains(@value,\"Link an Account\")]");
        public IWebElement LinkAccountAddBtn => Driver.FindElement(linkaccountaddbtn);

        public By stitchcontinuebtn = By.XPath("//*[@id=\"primary-btn\"]");
        public IWebElement StitchContinueBtn => Driver.FindElement(stitchcontinuebtn);

        public By fnbbtn = By.XPath("//*[contains(text(),\"First National Bank\")]/parent::a");
        public IWebElement FnbBtn => Driver.FindElement(fnbbtn);

        public By fillbtn = By.XPath("//*[@id=\"test-credentials-snackbar-dismiss-btn\"]");
        public IWebElement FillBtn => Driver.FindElement(fillbtn);

        public By loginbtn = By.XPath("//*[@id=\"submit-credentials-btn\"]");
        public IWebElement LoginBtn => Driver.FindElement(loginbtn);

        public By callmebackbtn_linkaccount = By.XPath("//*[@id=\"CallMeBackLinkedaccounts\"]");
        public IWebElement CallMeBackBtn_LinkAccount => Driver.FindElement(callmebackbtn_linkaccount);

        public By callmebackyesbtn_linkaccount = By.XPath("//*[@id=\"BudgetCallBackYes\"]");
        public IWebElement CallMeBackYesBtn_LinkAccount => Driver.FindElement(callmebackyesbtn_linkaccount);

        public By callmebackpopupcutbtn_linkaccount = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn_LinkAccount => Driver.FindElement(callmebackpopupcutbtn_linkaccount);

        public By callmebackpopupsuccessmsg_linkaccount = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg_LinkAccount => Driver.FindElement(callmebackpopupsuccessmsg_linkaccount);

        public By yourbudgettext = By.XPath("//*[@id=\"backtohome\"]/following-sibling::div/h2");
        public IWebElement YourBudgetText => Driver.FindElement(yourbudgettext);

        #region Budget Score

        public By budgetscore_updatebtn = By.XPath("//*[@id=\"BudgetForYouHomePage\" or @id='Budget_For_You_Home_Page']");
        public IWebElement BudgetScore_UpdateBtn => Driver.FindElement(budgetscore_updatebtn);

        public By moneyin_takehomesalary = By.XPath("//*[contains(text(),'Money in')]/following-sibling::div//*[@id=\"homeSalary\"]");
        public IWebElement MoneyIn_TakeHomeSalary => Driver.FindElement(moneyin_takehomesalary);

        public By moneyin_otherincome = By.XPath("//*[contains(text(),'Money in')]/following-sibling::div//*[@id=\"otherIncome\"]");
        public IWebElement MoneyIn_OtherIncome => Driver.FindElement(moneyin_otherincome);

        public By moneyout_updatelivingexpenses = By.XPath("//*[@id=\"Update living expenses\"]");
        public IWebElement MoneyOut_UpdateLivingExpenses => Driver.FindElement(moneyout_updatelivingexpenses);

        public By moneyout_viewcreditinstalments = By.XPath("//*[@id=\"View credit instalments\"]");
        public IWebElement MoneyOut_ViewCreditInstalments => Driver.FindElement(moneyout_viewcreditinstalments);

        public By moneyout_foodandgroceries = By.XPath("//*[contains(text(),'Food & Groceries')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_FoodAndGroceries => Driver.FindElement(moneyout_foodandgroceries);

        public By moneyout_rental = By.XPath("//*[contains(text(),'Rental')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_Rental => Driver.FindElement(moneyout_rental);

        public By moneyout_water_elec = By.XPath("//*[contains(text(),'Water, Electricity')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_Water_Elec => Driver.FindElement(moneyout_water_elec);

        public By moneyout_vehicle_household = By.XPath("//*[contains(text(),'Vehicle & Household')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_Vehicle_Household => Driver.FindElement(moneyout_vehicle_household);

        public By moneyout_transport = By.XPath("//*[contains(text(),'Transport')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_Transport => Driver.FindElement(moneyout_transport);

        public By moneyout_cellphone = By.XPath("//*[contains(text(),'Cellphone & Internet')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_Cellphone => Driver.FindElement(moneyout_cellphone);

        public By moneyout_medicalaid = By.XPath("//*[contains(text(),'Medical Aid')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_MedicalAid => Driver.FindElement(moneyout_medicalaid);

        public By moneyout_schoolfees = By.XPath("//*[contains(text(),'School Fees')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_SchoolFees => Driver.FindElement(moneyout_schoolfees);

        public By moneyout_savings = By.XPath("//*[contains(text(),'Savings')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_Savings => Driver.FindElement(moneyout_savings);

        public By moneyout_other = By.XPath("//*[contains(text(),'Other')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_Other => Driver.FindElement(moneyout_other);

        public By moneyout_homeloans = By.XPath("//*[contains(text(),'Home Loans')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_HomeLoans => Driver.FindElement(moneyout_homeloans);

        public By moneyout_vehicleloans = By.XPath("//*[contains(text(),'Vehicle Loans')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_VehicleLoans => Driver.FindElement(moneyout_vehicleloans);

        public By moneyout_personalloans = By.XPath("//*[contains(text(),'Personal Loans')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_PersonalLoans => Driver.FindElement(moneyout_personalloans);

        public By moneyout_creditcards = By.XPath("//*[contains(text(),'Credit Cards')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_CreditCards => Driver.FindElement(moneyout_creditcards);

        public By moneyout_retailaccounts = By.XPath("//*[contains(text(),'Retail Accounts')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_RetailAccounts => Driver.FindElement(moneyout_retailaccounts);

        public By moneyout_insurance = By.XPath("//*[contains(text(),'Insurance')]/following-sibling::div/div/div/input");
        public IWebElement MoneyOut_Insurance => Driver.FindElement(moneyout_insurance);

        public By monthlylivingexpenses = By.XPath("//*[contains(text(),'Monthly living expenses')]/following-sibling::div/input");
        public IWebElement MonthlyLivingExpenses => Driver.FindElement(monthlylivingexpenses);

        public By monthlycreditinstalments = By.XPath("//*[contains(text(),'Monthly credit instalments')]/following-sibling::div/input");
        public IWebElement MonthlyCreditInstalments => Driver.FindElement(monthlycreditinstalments);

        public By viewbudgetscore_button = By.XPath("//*[@id=\"View Budget Score\"]");
        public IWebElement ViewBudgetScore_Button => Driver.FindElement(viewbudgetscore_button);

        public By budgetscore = By.XPath("(//*[text()='Your Budget Score']/following-sibling::div/div/span)[1]");
        public IWebElement BudgetScore => Driver.FindElement(budgetscore);

        public By budgetscoretext = By.XPath("(//*[text()='Your Budget Score']/following-sibling::div/div/span)[2]");
        public IWebElement BudgetScoreText => Driver.FindElement(budgetscoretext);

        #endregion Budget Score

        #region Budget Score Calculator

        public By budgetcalculatorpopup_cutbutton = By.XPath("//a[@class='popupclose']");
        public IWebElement BudgetCalculatorPopup_CutButton => Driver.FindElement(budgetcalculatorpopup_cutbutton);

        #endregion
    }
}
