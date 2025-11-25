namespace SanlamAutomation
{
    public class HomePage : WebDriverSession
    {
        public By userwelcometexthomepage = By.XPath("//*[@id=\"UserWelcomeTextHomePage\"]");
        public IWebElement UserWelcomeTextHomePage => Driver.FindElement(userwelcometexthomepage);

        public By homepage = By.XPath("//a[@href='/portal/home']");
        public IWebElement HomePageVisible => Driver.FindElement(homepage);

        public By salarypopup = By.XPath("//*[@id=\"modal-basic-title\"]");
        public IWebElement SalaryPopUp => Driver.FindElement(salarypopup);

        public By takehomesalary = By.XPath("//*[@id=\"currency-number\"]");
        public IWebElement TakeHomeSalary => Driver.FindElement(takehomesalary);

        public By salarypopupsubmitbtn = By.XPath("//*[@id=\"btntSubmitTakeHomeSalaryHomePage\"]");
        public IWebElement SalaryPopUpSubmitBtn => Driver.FindElement(salarypopupsubmitbtn);

        // profileIcon

        public By profileicon = By.XPath("//*[@id=\"navbarDropdown\"]");
        public IWebElement ProfileIcon => Driver.FindElement(profileicon);

        public By profileoption = By.XPath("/html/body/app-root/app-layout/app-header/header/nav/div/div/div/ul/li[7]/ul/li[1]/a");
        public IWebElement ProfileOption => Driver.FindElement(profileoption);

        public By profilecurrencyfield = By.XPath("//*[@id=\"currency-number\"]");
        public IWebElement ProfileCurrencyField => Driver.FindElement(profilecurrencyfield);

        public By profileupdatebtn = By.XPath("//button[contains(text(),' Update profile ')]");
        public IWebElement ProfileUpdateBtn => Driver.FindElement(profileupdatebtn);

        public By profileupdatemsg = By.XPath("//*[text()=' Profile Updated Successfully. ']");
        public IWebElement ProfileUpdateMsg => Driver.FindElement(profileupdatemsg);

        public By logout = By.XPath("/html/body/app-root/app-layout/app-header/header/nav/div/div/div/ul/li[7]/ul/li[3]/a");
        public IWebElement LogOut => Driver.FindElement(logout);

        // Can You Afford Your Debt?

        public By dashboardicon = By.XPath("//a[text()='Dashboard']");
        public IWebElement DashboardIcon => Driver.FindElement(dashboardicon);

        public By repaymentscircle = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[7]/div/div[1]/div[1]/div/div[1]/div");
        public IWebElement RepaymentsCircle => Driver.FindElement(repaymentscircle);

        public By incomecircle = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[7]/div/div[1]/div[3]/div/div[1]/div");
        public IWebElement IncomeCircle => Driver.FindElement(incomecircle);

        public By debttoincomeratio = By.XPath("//*[@class='circle-svg-large']");
        public IWebElement DebtToIncomeRatio => Driver.FindElement(debttoincomeratio);

        public By debttoincomeratiotext = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[7]/div/div[1]/div[5]/div/div[1]/h3[1]");
        public IWebElement DebtToIncomeRatioText => Driver.FindElement(debttoincomeratiotext);

        // Understand Your Debt

        /***
         * noofopenaccounts
         * ***/

        public By noofopenaccountsforhome = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[2]/div/div[2]/p");
        public IWebElement NoOfOpenAccountsForHome => Driver.FindElement(noofopenaccountsforhome);

        public By noofopenaccountsforcar = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[3]/div/div[2]/p");
        public IWebElement NoOfOpenAccountsForCar => Driver.FindElement(noofopenaccountsforcar);

        public By noofopenaccountsforclothing = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[4]/div/div[2]/p");
        public IWebElement NoOfOpenAccountsForClothing => Driver.FindElement(noofopenaccountsforclothing);

        public By noofopenaccountsforcreditcard = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[5]/div/div[2]/p");
        public IWebElement NoOfOpenAccountsForCreditCard => Driver.FindElement(noofopenaccountsforcreditcard);

        public By noofopenaccountsforloans = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[6]/div/div[2]/p");
        public IWebElement NoOfOpenAccountsForLoans => Driver.FindElement(noofopenaccountsforloans);

        /***
         * 
         * Your debt is made up of
         * 
         * ***/

        public By yourdeptismadeupofhome = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[2]/div/div[2]/p/parent::div/following-sibling::div[1]");
        public IWebElement YourDeptIsMadeUpOfHome => Driver.FindElement(yourdeptismadeupofhome);

        public By yourdeptismadeupofcar = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[3]/div/div[2]/p/parent::div/following-sibling::div[1]");
        public IWebElement YourDeptIsMadeUpOfCar => Driver.FindElement(yourdeptismadeupofcar);

        public By yourdeptismadeupofclothing = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[4]/div/div[2]/p/parent::div/following-sibling::div[1]");
        public IWebElement YourDeptIsMadeUpOfClothing => Driver.FindElement(yourdeptismadeupofclothing);

        public By yourdeptismadeupofcreditcard = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[5]/div/div[2]/p/parent::div/following-sibling::div[1]");
        public IWebElement YourDeptIsMadeUpOfCreditCard => Driver.FindElement(yourdeptismadeupofcreditcard);

        public By yourdeptismadeupofloans = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[6]/div/div[2]/p/parent::div/following-sibling::div[1]");
        public IWebElement YourDeptIsMadeUpOfLoans => Driver.FindElement(yourdeptismadeupofloans);

        /***
         * 
         * How much have you paid-off
         * 
         * **/

        public By howmuchhaveyoupaidoffhome = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[2]/div/div[2]/p/parent::div/following-sibling::div[2]");
        public IWebElement HowMuchHaveYouPaidOffHome => Driver.FindElement(howmuchhaveyoupaidoffhome);

        public By howmuchhaveyoupaidoffcar = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[3]/div/div[2]/p/parent::div/following-sibling::div[2]");
        public IWebElement HowMuchHaveYouPaidOffCar => Driver.FindElement(howmuchhaveyoupaidoffcar);

        public By howmuchhaveyoupaidoffclothing = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[4]/div/div[2]/p/parent::div/following-sibling::div[2]");
        public IWebElement HowMuchHaveYouPaidOffClothing => Driver.FindElement(howmuchhaveyoupaidoffclothing);

        public By howmuchhaveyoupaidoffcreditcard = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[5]/div/div[2]/p/parent::div/following-sibling::div[2]");
        public IWebElement HowMuchHaveYouPaidOffCreditCard => Driver.FindElement(howmuchhaveyoupaidoffcreditcard);

        public By howmuchhaveyoupaidoffloans = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[1]/div[2]/div[6]/div/div[2]/p/parent::div/following-sibling::div[2]");
        public IWebElement HowMuchHaveYouPaidOffLoans => Driver.FindElement(howmuchhaveyoupaidoffloans);


        public By totalcurrentbalance = By.XPath("/html/body/app-root/app-layout/app-dashboard/section/section[6]/div/div/div/div[2]/div[2]/div[2]/div/h4");
        public IWebElement TotalCurrentBalance => Driver.FindElement(totalcurrentbalance);

        // Your Credit Score

        public By creditscore = By.XPath("//*[contains(text(),'credit score')]/following-sibling::span");
        public IWebElement CreditScore => Driver.FindElement(creditscore);

        public By creditscorestatus = By.XPath("//*[contains(text(),'All you need to know about your credit')]/parent::div/parent::div/following-sibling::div/div[2]/span");
        public IWebElement CreditScoreStatus => Driver.FindElement(creditscorestatus);

        public By creditscorecircle = By.XPath("(//*[@class='donut-hole'])[1]");
        public IWebElement CreditScoreCircle => Driver.FindElement(creditscorecircle);

        // Success Popup


        public By applynowbtn = By.XPath("//*[@id=\"ContinueSPLHome\"]");
        public IWebElement ApplyNowBtn => Driver.FindElement(applynowbtn);

        public By prequalifyloanpopup_cancelbtn = By.XPath("//*[contains(text(),'CANCEL') or contains(text(),'Cancel')]");
        public IWebElement PreQualifyLoanPopup_CancelBtn => Driver.FindElement(prequalifyloanpopup_cancelbtn);

        // call me back button

        public By callmebackbtn = By.XPath("//*[@id=\"CallMeBackHomepage\"]");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("//*[@id=\"CallMeBackYesHomePage\"]");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebacksuccesmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackSuccessMsg => Driver.FindElement(callmebacksuccesmsg);

        public By callmebackcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackCutBtn => Driver.FindElement(callmebackcutbtn);

        // Qualifier


        public By splqualifymsg = By.XPath("//*[text()='Sanlam Personal Loan']/parent::div/following-sibling::div/div/span");
        public IWebElement SplQualifyMsg => Driver.FindElement(splqualifymsg);

        public By ccqualifier = By.XPath("//*[text()='Sanlam Money Saver Credit Card']/parent::div/following-sibling::div/div/span");
        public IWebElement CCQualifier => Driver.FindElement(ccqualifier);

        public By creditconsolqualifier = By.XPath("//*[text()='DebtBusters Credit Consolidation']/parent::div/following-sibling::div/div/span");
        public IWebElement CreditConsolQualifier => Driver.FindElement(creditconsolqualifier);

        public By capfinqualifier = By.XPath("//*[text()='Capfin Personal Loans']/parent::div/following-sibling::div/div/span");
        public IWebElement CapfinQualifier => Driver.FindElement(capfinqualifier);

        public By capfinqualifier_agentui = By.XPath("//*[text()='Capfin Personal Loan']/parent::div/following-sibling::div/div/span");
        public IWebElement CapfinQualifier_AgentUi => Driver.FindElement(capfinqualifier_agentui);

        public By creditconsolqualifierarrow = By.XPath("//*[text()='DebtBusters Credit Consolidation']/parent::div/following-sibling::div/a");
        public IWebElement CreditConsolQualifier_Arrow => Driver.FindElement(creditconsolqualifierarrow);

        //New Update popup
        public By newupdatepopup_laterbtn = By.XPath("//*[contains(text(),'Later')]");
        public IWebElement NewUpdatePopUp_LaterBtn => Driver.FindElement(newupdatepopup_laterbtn);


        //wealth field

        public By wealthupdatefield = By.XPath("/html/body/app-root/app-layout/app-landing/div[1]/section[1]/div/div/div[2]/div[2]/div/h3");
        public IWebElement WealthUpdateField => Driver.FindElement(wealthupdatefield);

        public By wealthupdatebtn = By.XPath("//*[@id=\"wealth_ScoreCapture_Redirect\"]");
        public IWebElement WealthUpdateBtn => Driver.FindElement(wealthupdatebtn);

        public By wealthupdatebtn_firsttimelogin = By.XPath("//*[@id=\"wealth_Score_Capture_Redirect\"]");
        public IWebElement WealthUpdateBtn_FirstTimeLogin => Driver.FindElement(wealthupdatebtn_firsttimelogin);

        //FAQButton
        public By fAQButton = By.XPath("//*[@id='FAQForYouHomePage']");
        public IWebElement FAQButton => Driver.FindElement(fAQButton);

        //DashboardButtons
        public By viewsolutionsbutton = By.XPath("//*[@id=\"SolutionsForYouHomePage\"]");
        public IWebElement ViewSolutionsButton => Driver.FindElement(viewsolutionsbutton);

        public By viewcreditinsightsbutton = By.XPath("//*[@id=\"DashbaordForYouHomePage\"]");
        public IWebElement ViewCreditInsightsButton => Driver.FindElement(viewcreditinsightsbutton);

        public By viewsolutionspagetext = By.XPath("//*[@id='old-sol-design']//*[contains(text(),'Solutions for You')]");
        public IWebElement ViewSolutionsPageText => Driver.FindElement(viewsolutionspagetext);
        
        public By recommendedsolutionsforyoutiles (int i) => By.XPath($"(//*[@id=\"viewSolution\"]//a[@href='/portal/offers'])[{i}]");
        public IWebElement RecommendedSolutionsForYouTiles(int i) => Driver.FindElement(recommendedsolutionsforyoutiles(i));

        public By takehomesalarytowardsdebtpercentage = By.XPath("//*[text()='Take home salary towards debt']/parent::div/preceding-sibling::div");
        public IWebElement TakeHomeSalaryTowardsDebtPercentage => Driver.FindElement(takehomesalarytowardsdebtpercentage);

        public By takehomesalarytowardsdebttext = By.XPath("//*[text()='Take home salary towards debt']/following-sibling::span");
        public IWebElement TakeHomeSalaryTowardsDebtText => Driver.FindElement(takehomesalarytowardsdebttext);

        public By overdueamount = By.XPath("//*[text()='Overdue amount']/parent::div/preceding-sibling::div");
        public IWebElement OverdueAmount => Driver.FindElement(overdueamount);

        public By overdueamounttext = By.XPath("//*[text()='Overdue amount']/following-sibling::span");
        public IWebElement OverdueAmountText => Driver.FindElement(overdueamounttext);
        public bool IsHomePageDisplayed()
        {
            bool stat = false;
            try
            {
                if (HomePageVisible.Displayed)
                {
                    stat = true;
                    return stat;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return stat;
            }
        }
        public bool IsUserWelcomeTextHomePage()
        {
            bool stat = false;
            try
            {
                if (UserWelcomeTextHomePage.Displayed)
                {
                    stat = true;
                    return stat;
                }
                else
                {
                    return false;
                }

            }
            catch 
            {
                return stat;
            }
        }
    }
}
    
