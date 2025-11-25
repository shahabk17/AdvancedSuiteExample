namespace SanlamAutomation.Test.Pages
{
    public class CreditInsightsPage : WebDriverSession
    {
        public By creditinsights_creditscore = By.XPath("//h2[text()='Your credit score']");
        public IWebElement CreditInsights_CreditScore => Driver.FindElement(creditinsights_creditscore);

        public By creditinsightsicon = By.XPath("//*[text()='Credit Insights' or text()=' Credit Insights']");
        public IWebElement CreditInsightsIcon => Driver.FindElement(creditinsightsicon);

        public By creditconsolidationbtn = By.XPath("//*[@id=\"Credit Consolidation\"]");
        public IWebElement CreditConsolidationBtn => Driver.FindElement(creditconsolidationbtn);

        public By creditconsolidationyesbtn = By.XPath("//*[@id=\"CreditConsolidationYes\"]");
        public IWebElement CreditConsolidationYesBtn => Driver.FindElement(creditconsolidationyesbtn);

        public By ccsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement ccSuccessMsg => Driver.FindElement(ccsuccessmsg);

        public By ccpopupcutbtn = By.XPath("//*[@id=\"CreditConsolidationCancel\"]");
        public IWebElement CCPopupCutBtn => Driver.FindElement(ccpopupcutbtn);

        // call me back

        public By callmebackbtn = By.XPath("//*[@id=\"CallMeBackCreditInsightsPage\"]");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("//*[@id=\"CallMeBackYesCreditInsights\"]");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn => Driver.FindElement(callmebackpopupcutbtn);

        // budget button 

        public By yourbudgetbtn = By.XPath("//*[@id='btnBudgetLinkFromDashBoard']");
        public IWebElement YourBudgetBtn => Driver.FindElement(yourbudgetbtn);

        public By downloadcreditreportbutton = By.XPath("//*[@id=\"Download Credit Report\"]");
        public IWebElement DownloadCreditReportButton => Driver.FindElement(downloadcreditreportbutton);

        public By solutionsforyou = By.XPath("//*[@id=\"SolutionsForYouDashboardPage\"]");
        public IWebElement SolutionForYou => Driver.FindElement(solutionsforyou);

        public By callbackrequesttext = By.XPath("//*[@id=\"CreditConsolidationCancel\"]/following-sibling::h2");
        public IWebElement CallbackRequestText => Driver.FindElement(callbackrequesttext);

        #region How you measure up

        public By scale_yourscore = By.XPath("//*[text()='Your score']/parent::span");
        public IWebElement Scale_YourScore => Driver.FindElement(scale_yourscore);

        public By scale_agegroup = By.XPath("//*[text()='Average for your age group']/parent::span");
        public IWebElement Scale_AgeGroup => Driver.FindElement(scale_agegroup);

        #endregion How you measure up

        #region Factors affecting your score

        public By takehomesalarytowarddebt_arrow = By.XPath("//*[@id='Take-home salary toward debt']");
        public IWebElement TakeHomeSalaryTowardDebt_Arrow => Driver.FindElement(takehomesalarytowarddebt_arrow);

        public By takehomesalarytowarddebt_text = By.XPath("//*[@id='Take-home salary toward debt']/following-sibling::div//h4");
        public IWebElement TakeHomeSalaryTowardDebt_Text => Driver.FindElement(takehomesalarytowarddebt_text);

        public By takehomesalarytowarddebt_percentage = By.XPath("//*[@id='Take-home salary toward debt']/following-sibling::div//span[contains(text(),'%')]");
        public IWebElement TakeHomeSalaryTowardDebt_Percentage => Driver.FindElement(takehomesalarytowarddebt_percentage);

        public By overdueamount_arrow = By.XPath("//*[@id='Overdue amount']");
        public IWebElement OverdueAmount_Arrow => Driver.FindElement(overdueamount_arrow);

        public By overdueamount_text = By.XPath("//*[@id='Overdue amount']/following-sibling::div//h4");
        public IWebElement OverdueAmount_Text => Driver.FindElement(overdueamount_text);

        public By overdueamount = By.XPath("//*[@id='Overdue amount']/following-sibling::div//span[contains(text(),'R')]");
        public IWebElement OverdueAmount => Driver.FindElement(overdueamount);

        public By moneyleftforexpenses_arrow = By.XPath("//*[@id='Money left for expenses']");
        public IWebElement MoneyLeftForExpenses_Arrow => Driver.FindElement(moneyleftforexpenses_arrow);

        public By moneyleftforexpenses_text = By.XPath("//*[@id='Money left for expenses']/following-sibling::div//h4");
        public IWebElement MoneyLeftForExpenses_Text => Driver.FindElement(moneyleftforexpenses_text);

        public By moneyleftforexpenses_amount = By.XPath("//*[@id='Money left for expenses']/following-sibling::div//span[contains(text(),'R')]");
        public IWebElement MoneyLeftForExpenses_Amount => Driver.FindElement(moneyleftforexpenses_amount);

        public By estimatedmonthlyinterestpayments_arrow = By.XPath("//*[@id='Estimated Monthly Interest Payments']");
        public IWebElement EstimatedMonthlyInterestPayments_Arrow => Driver.FindElement(estimatedmonthlyinterestpayments_arrow);

        public By estimatedmonthlyinterestpayments_text = By.XPath("//*[@id='Estimated Monthly Interest Payments']/following-sibling::div//h4");
        public IWebElement EstimatedMonthlyInterestPayments_Text => Driver.FindElement(estimatedmonthlyinterestpayments_text);

        public By estimatedmonthlyinterestpayments_amount = By.XPath("//*[@id='Estimated Monthly Interest Payments']/following-sibling::div//span[contains(text(),'R')]");
        public IWebElement EstimatedMonthlyInterestPayments_Amount => Driver.FindElement(estimatedmonthlyinterestpayments_amount);

        #endregion Factors affecting your score

        #region Score trend

        public By scoretrend_tab = By.XPath("//*[@id=\"ScoreTrend\"]");
        public IWebElement ScoreTrend_Tab => Driver.FindElement(scoretrend_tab);

        public By scoretrend_message = By.XPath("//*[@class='dummytext']");
        public IWebElement ScoreTrend_Message => Driver.FindElement(scoretrend_message);

        public By scorehistory = By.XPath("//*[@id=\"month-three\"]");
        public IWebElement ScoreHistory => Driver.FindElement(scorehistory);

        #endregion Score trend

        #region Your credit summary

        public By creditsummary_takehomesalary_amount = By.XPath("//*[text()='Take-home Salary']/parent::div/following-sibling::div");
        public IWebElement CreditSummary_TakeHomeSalary_Amount => Driver.FindElement(creditsummary_takehomesalary_amount);

        public By totalcurrentbalance_amount = By.XPath("//*[text()='Total Current Balance']/parent::div/following-sibling::div");
        public IWebElement TotalCurrentBalance_Amount => Driver.FindElement(totalcurrentbalance_amount);

        public By totalmonthlypayments_amount = By.XPath("//*[text()='Total Monthly Payments']/parent::div/following-sibling::div");
        public IWebElement TotalMonthlyPayments_Amount => Driver.FindElement(totalmonthlypayments_amount);

        #endregion Your credit summary

        #region Your credit breakdown

        public By learnaboutmoney_button = By.XPath("//*[@id=\"LearnaboutMoneyDashboardPage\"]");
        public IWebElement LearnAboutMoney_Button => Driver.FindElement(learnaboutmoney_button);

        public By viewfullcredit_button = By.XPath("//*[@id=\"CreditProfileDashboardPage\"]");
        public IWebElement ViewFullCredit_Button => Driver.FindElement(viewfullcredit_button);

        public By accountsummary_tab = By.XPath("//*[@id=\"AccountsSummary\"]");
        public IWebElement AccountSummary_Tab => Driver.FindElement(accountsummary_tab);

        public By yourcreditbreakdown_homeloan = By.XPath("//*[contains(text(),'Home loan')]/parent::div/following-sibling::div");
        public IWebElement YourCreditBreakdown_HomeLoan => Driver.FindElement(yourcreditbreakdown_homeloan);

        public By yourcreditbreakdown_vehiclefinance = By.XPath("//*[contains(text(),'Vehicle finance')]/parent::div/following-sibling::div");
        public IWebElement YourCreditBreakdown_VehicleFinance => Driver.FindElement(yourcreditbreakdown_vehiclefinance);

        public By yourcreditbreakdown_retailaccount = By.XPath("//*[contains(text(),'Retail accounts')]/parent::div/following-sibling::div");
        public IWebElement YourCreditBreakdown_RetailAccount => Driver.FindElement(yourcreditbreakdown_retailaccount);

        public By yourcreditbreakdown_creditcard = By.XPath("//*[contains(text(),'Credit card payments')]/parent::div/following-sibling::div");
        public IWebElement YourCreditBreakdown_CreditCard => Driver.FindElement(yourcreditbreakdown_creditcard);

        public By yourcreditbreakdown_personalloans = By.XPath("//*[contains(text(),'Personal loans')]/parent::div/following-sibling::div");
        public IWebElement YourCreditBreakdown_PersonalLoans => Driver.FindElement(yourcreditbreakdown_personalloans);

        public By amountsettled_tab = By.XPath("//a[contains(@id,'tab-selectbyid2')]");
        public IWebElement AmountSettled_Tab => Driver.FindElement(amountsettled_tab);

        #endregion Your credit breakdown

    }
}
