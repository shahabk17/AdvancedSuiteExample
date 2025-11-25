namespace SanlamAutomation
{
    /// <summary>
    /// Handles all solution page related test actions and validations
    /// </summary>
    public class SolutionPageSteps : WebDriverSession
    {
        private readonly BaseStep baseStep = new();
        private readonly Validate validate = new();
        private readonly GenericUtils genericUtils = new();
        private readonly SolutionPage solutionPage = new();
        private readonly DBCreditCoach dbCreditCoach = new();
        private readonly HomePageSteps homePageSteps = new();
        private readonly AzureTables azureTables = new();

        /// <summary>
        /// Verifies broken links under Get Money tab
        /// </summary>
        [Author("Shahab Khan")]
        public void BrokenLinkUnderGetMoneyTab()
        {
            NavigateToSolutionsPage();
            ValidateLinks();
        }

        /// <summary>
        /// Handles View Offer and Speak to Coach functionality
        /// </summary>
        [Author("Shahab Khan")]
        public void ViewOfferandSpeaktoCoach(string idNumber)
        {
            UpdateAndVerifySPLStatus(idNumber);
            HandleSPLViewOffer(idNumber);
            ProcessSpeakToCoach();
        }

        /// <summary>
        /// Handles View Offer for qualified SPL users
        /// </summary>
        [Author("Shahab Khan")]
        public void ViewOffer_SplQualifiedUser(string IdNumber)
        {
            UpdateQualifiedUserStatus(IdNumber);
            ProcessQualifiedUserViewOffer();
        }

        /// <summary>
        /// Verifies SPL tile qualifier status
        /// </summary>
        /// <param name="Idnumber"></param>
        /// <param name="les_decision"></param>
        /// <param name="isQualifiedSPL"></param>
        [Author("Shahab Khan")]
        public void VerifySPLTile(string Idnumber, string les_decision, bool isQualifiedSPL)
        {
            NavigateAndUpdateSPLStatus(Idnumber, les_decision, isQualifiedSPL);
            ValidateSPLQualifier(Idnumber, isQualifiedSPL);
        }

        /// <summary>
        /// Verifies Credit Card and MobiCred tile qualifiers
        /// </summary>
        [Author("Shahab Khan")]
        public string VerifyCreditCardAndMobiCredTileQualifier(string Idnumber)
        {
            NavigateToSolutionsPage();
            baseStep.wait.WaitTillPageLoad();
            string keyVal = dbCreditCoach.KeynameValuefromCreditHistory(Idnumber, "CreditCoachScore_CreditCard");
            ValidateCreditCardQualifiers(Idnumber);
            return keyVal;
        }

        /// <summary>
        /// Verifies Credit Consolidation tile qualifier
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyCreditConsolidationTileQualifier(string Idnumber)
        {
            NavigateToSolutionsPage();
            ScrollToAndValidateQualifier(Idnumber);
        }

        /// <summary>
        /// Verifies Capfin tile qualifier
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyCapfinTileQualifier(string Idnumber)
        {
            NavigateToSolutionsPage();
            baseStep.wait.WaitTillPageLoad();
            string keyVal = dbCreditCoach.KeynameValuefromCreditHistory(Idnumber, "CreditCoachScorePerc");
            ValidateCapfinQualifier(Idnumber);
        }

        /// <summary>
        /// Verifies Call Me Back functionality on solution page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackSolutionPage(string IdNumber)
        {
            InitiateCallMeBack();
            ProcessCallMeBackRequest(IdNumber);
        }

        /// <summary>
        /// Updates a credit score based on qualifier type ("likely", "moderately likely", or "unlikely"); logs an info message for invalid input.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="QualifierType"></param>
        [Author("Jashan Kumar")]
        public void CheckAndUpdateCapfinQualifier(string idNumber, string QualifierType)
        {
            if (QualifierType.ToLower() == "likely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscoreperc", "61");
            }
            else if (QualifierType.ToLower() == "moderately likely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscoreperc", "41");
            }
            else if (QualifierType.ToLower() == "unlikely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscoreperc", "39");
            }
            else
            {
                Report.ChildLog.Log(Status.Info, "Invalid qualifier inserted");
            }
        }

        /// <summary>
        /// Updates Finance27 credit score based on qualifier type; logs an info message if the qualifier input is invalid.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="QualifierType"></param>
        [Author("Jashan Kumar")]
        public void CheckAndUpdatefinance27Qualifier(string idNumber, string QualifierType)
        {
            if (QualifierType.ToLower() == "likely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscoreperc", "61");
            }
            else if (QualifierType.ToLower() == "moderately likely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscoreperc", "41");
            }
            else if (QualifierType.ToLower() == "unlikely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscoreperc", "39");
            }
            else
            {
                Report.ChildLog.Log(Status.Info, "Invalid qualifier inserted");
            }
        }

        /// <summary>
        /// Updates Mobicredit or Moneysaver credit card score based on qualifier type; logs an info message for invalid qualifiers.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="QualifierType"></param>
        /// <returns></returns>
        [Author("Jashan Kumar")]
        public void CheckAndUpdateMobicreditAndMoneysaverQualifier(string idNumber, string QualifierType)
        {
            if (QualifierType.ToLower() == "likely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscore_creditcard", "65");
            }
            else if (QualifierType.ToLower() == "moderately likely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscore_creditcard", "31");
            }
            else if (QualifierType.ToLower() == "unlikely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscore_creditcard", "30");
            }
            else if (QualifierType.ToLower() == "very unlikely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditcoachscore_creditcard", "24");
            }
            else
            {
                Report.ChildLog.Log(Status.Info, "Invalid qualifier inserted");
            }
        }

        /// <summary>
        /// Determines and returns expected qualifier text for SPL based on decision results and qualification status from database values.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="isQualifiedSPL"></param>
        /// <returns></returns>
        [Author("Shahab Khan")]
        public string ReturnExpectedQualifierTextForSPL(string IdNumber, bool isQualifiedSPL)
        {
            string expectedQualifier = null;
            string LesDecision = dbCreditCoach.LesDecisionFromUserSPLQualificationDecision(IdNumber);
            string jsonDecision = dbCreditCoach.JsonDecisionFromUserSPLQualificationDecision(IdNumber);

            if (LesDecision.ToLower() == "approve")
            {
                expectedQualifier = "You are likely to qualify";
            }
            else if (LesDecision.ToLower() == "maybe" && isQualifiedSPL)
            {
                expectedQualifier = "You are likely to qualify";
            }
            else if (LesDecision.ToLower() == "maybe")
            {
                expectedQualifier = "You are unlikely to qualify";
            }
            else if (LesDecision.ToLower() == "decline" && isQualifiedSPL && jsonDecision == "qualified")
            {
                expectedQualifier = "You are likely to qualify";
            }
            else if (LesDecision.ToLower() == "decline")
            {
                expectedQualifier = "You are not likely to qualify";
            }

            return expectedQualifier;
        }

        /// <summary>
        /// Returns expected qualifier text and logs qualifier color based on credit card score and CSS color value from a web element.
        /// </summary>
        /// <param name="Idnumber"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        [Author("Shahab Khan")]
        public string ReturnExpectedQualifierTextForCC(string Idnumber, IWebElement element)
        {
            string expectedQualifier = null;
            string QualifierColor;
            int CreditCoachScore_CreditCard = int.Parse(dbCreditCoach.KeynameValuefromCreditHistory(Idnumber, "CreditCoachScore_CreditCard"));

            if (CreditCoachScore_CreditCard <= 24)
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(255, 0, 0, 1)"))
                {
                    QualifierColor = "Red";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "You are very unlikely to qualify";
            }
            else if (CreditCoachScore_CreditCard >= 24 && CreditCoachScore_CreditCard <= 30)
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(255, 129, 5, 1)"))
                {
                    QualifierColor = "Orange";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "You are unlikely to qualify";
            }
            else if (CreditCoachScore_CreditCard >= 31 && CreditCoachScore_CreditCard <= 64)
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(213, 213, 61, 1)"))
                {
                    QualifierColor = "Yellow";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "You are moderately likely to qualify";
            }
            else if (CreditCoachScore_CreditCard >= 65 && CreditCoachScore_CreditCard < 85)
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(103, 225, 103, 1)"))
                {
                    QualifierColor = "Light green";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "You are likely to qualify";
            }
            else if (CreditCoachScore_CreditCard >= 85)
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(0, 128, 0, 1)"))
                {
                    QualifierColor = "green";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "You are very likely to qualify";
            }

            return expectedQualifier;
        }

        /// <summary>
        /// Returns expected qualifier text and logs color based on client conversion data, element color, and optional monthly saving amount.
        /// </summary>
        /// <param name="Idnumber"></param>
        /// <param name="element"></param>
        /// <param name="monthlySaving"></param>
        /// <returns></returns>
        [Author("Shahab Khan")]
        public string ReturnExpectedQualifierTextForCreditConsolidation(string Idnumber, IWebElement element, string monthlySaving)
        {
            string expectedQualifier = null;
            string QualifierColor;
            IDictionary<string, string> DBC = dbCreditCoach.DBC_Client_ConversionFromDB(Idnumber);

            string dbClient = DBC["dbClient"];
            string dbC_Conversion = DBC["dbC_Conversion"];

            if (dbClient.ToLower() == "yes" && dbC_Conversion.ToLower() == "no" ||
                dbClient.ToLower() == "no" && dbC_Conversion.ToLower() == "no" ||
                dbClient.ToLower() == "yes" && dbC_Conversion.ToLower() == "yes")
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(255, 129, 5, 1)"))
                {
                    QualifierColor = "Orange";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "An unlikely solution for you";
            }
            else if (dbClient.ToLower() == "no" && dbC_Conversion.ToLower() == "yes" && monthlySaving != null)
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(107, 226, 107, 1)"))
                {
                    QualifierColor = "Green";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = $"You are likely to save {monthlySaving} p.m";
            }
            else if (dbClient.ToLower() == "no" && dbC_Conversion.ToLower() == "yes")
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(107, 226, 107, 1)"))
                {
                    QualifierColor = "Green";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "You are likely to save money";
            }

            return expectedQualifier;
        }

        /// <summary>
        /// Returns expected Capfin qualifier text and logs color based on the credit coach score and web element’s CSS color value.
        /// </summary>
        /// <param name="Idnumber"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        [Author("Shahab Khan")]
        public string ReturnExpectedQualifierTextForCapfin(string Idnumber, IWebElement element)
        {
            string expectedQualifier = null;
            string QualifierColor;
            int CreditCoachScorePerc = int.Parse(dbCreditCoach.GetCreditCoachScore(Idnumber));

            if (CreditCoachScorePerc == 1000)
            {
                Report.ChildLog.Log(Status.Info, "value is NULL then no qualifier should be display on capfin tile ");
                expectedQualifier = "";
            }
            else if (CreditCoachScorePerc >= 0 && CreditCoachScorePerc < 40)
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(255, 0, 0, 1)"))
                {
                    QualifierColor = "red";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "You are Less likely to qualify";
            }
            else if (CreditCoachScorePerc >= 40 && CreditCoachScorePerc <= 60)
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(255, 129, 5, 1)"))
                {
                    QualifierColor = "Orange";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "You are moderately likely to qualify";
            }
            else if (CreditCoachScorePerc > 60)
            {
                string color = element.GetCssValue("color");
                if (color.Contains("(0, 128, 0, 1)"))
                {
                    QualifierColor = "Green";
                    Report.ChildLog.Log(Status.Info, "Qualifier color is " + QualifierColor);
                }
                expectedQualifier = "You are highly likely to qualify";
            }

            return expectedQualifier;
        }

        /// <summary>
        /// Handles SPL tile scenarios by checking LMS requirement; processes either a decline case or a normal case based on input.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="lmsRequired"></param>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackSPLTile(string IdNumber, string lmsRequired)
        {
            if (lmsRequired.ToLower() == "spl decline")
            {
                HandleSplDeclineScenario(IdNumber);
            }
            else
            {
                HandleNormalSplScenario(IdNumber);
            }
        }

        /// <summary>
        /// This method is for verifying the view offer - speak to coach button
        /// </summary>
        /// <param name="IdNumber"></param>
        [Author("Shahab Khan")]
        public void VerifyCallMeBackCreditConsolidationTile(string IdNumber)
        {
            baseStep.ScrollToElement(solutionPage.CreditConsolQualifier);
            string CreditConsolQualifier = baseStep.getText.Text(solutionPage.CreditConsolQualifier);

            if (CreditConsolQualifier.ToLower().Equals("an unlikely solution for you"))
            {
                baseStep.wait.WaitTillPageLoad();
                baseStep.wait.WaitForElementClickableLongWait(solutionPage.creditconsolviewoffer, 60);
                baseStep.Click(solutionPage.CreditConsolViewOffer);

                baseStep.wait.WaitTillPageLoad();
                baseStep.wait.WaitForElementClickableLongWait(solutionPage.creditconsolspeaktocaoch, 60);
                validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
                baseStep.Click(solutionPage.CreditConsolSpeakToCoach);

                baseStep.wait.WaitTillPageLoad();
                baseStep.wait.WaitForElementClickableLongWait(solutionPage.callmebackpopupsuccessmsg, 60);
                string ccSuccessMsg = baseStep.getText.Text(solutionPage.CallMeBackPopupSuccessMsg);
                Assert.That(validate.IsElementDisplayed(solutionPage.callmebackpopupsuccessmsg));
                Report.ChildLog.Log(Status.Info, "Success Message is Visible with text " + ccSuccessMsg);
                validate.TakeStepFullScreenShot("Success Message", Status.Info);
                baseStep.Click(solutionPage.CreditConsolPopUpCutBtn);
                dbCreditCoach.GetCampaignSourceValidate(IdNumber, "Credit Consolidation Decline");
            }
        }

        /// <summary>
        /// Verifies StoreCards -Trueworth and Identity tile qualifier
        /// </summary>
        [Author("Shahab Khan")]
        public string VerifyStoreCardTileQualifier(string IdNumber)
        {
            NavigateToSolutionsPage();
            baseStep.wait.WaitTillPageLoad();
            string keyVal = dbCreditCoach.KeynameValuefromCreditHistory(IdNumber, "CreditCoachScore_StoreCard");
            ValidateStoreCardsQualifier(keyVal);
            return keyVal;
        }

        /// <summary>
        /// this is for return expected StoreCard tile qualifier
        /// </summary>
        /// <param name="creditCoachScore_Storecard"></param>
        /// <returns></returns>
        [Author("Shahab Khan")]
        public string ReturnExpectedQualifierTextForStoreCard(string creditCoachScore_Storecard)
        {
            int score_Storecard = int.Parse(creditCoachScore_Storecard);
            string expectedQualifier = score_Storecard switch
            {
                <= 5 => "You are unlikely to qualify",
                > 5 and <= 69 => "You are likely to qualify",
                > 69 => "You are very likely to qualify",
            };
            return expectedQualifier;
        }

        /// <summary>
        /// Updates the Storecard credit score based on qualifier type ("very likely", "likely", or "unlikely"); logs info for invalid input.
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="QualifierType"></param>
        [Author("Jashan Kumar")]
        public void CheckAndUpdateStorecardQualifier(string idNumber, string QualifierType)
        {
            if (QualifierType.ToLower() == "very likely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditCoachScore_Storecard", "70");
            }
            else if (QualifierType.ToLower() == "likely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditCoachScore_Storecard", "68");
            }
            else if (QualifierType.ToLower() == "unlikely")
            {
                dbCreditCoach.UpdateCreditCoachScore_PersonalLoan(idNumber, "creditCoachScore_Storecard", "5");
            }
            else
            {
                Report.ChildLog.Log(Status.Info, "Invalid qualifier inserted");
            }
        }

        /// <summary>
        /// Verifies SPL tile LMS status as per conditions
        /// </summary>
        /// <param name="Idnumber"></param>
        /// <param name="expectedCampaign"></param>
        [Author("Shahab Khan")]
        public void ValidateSPLTileLMS(string Idnumber, string expectedCampaign)
        {
            dbCreditCoach.GetCampaignSourceValidate(Idnumber, expectedCampaign);
        }

        /// <summary>
        /// Verify the SPL tiles button with different conditions
        /// </summary>
        /// <param name="Idnumber"></param>
        /// <param name="les_decision"></param>
        /// <param name="isSplQualified"></param>
        [Author("Shahab Khan")]
        public void VerifySplTileButtonAndLogs(string Idnumber, string les_decision, bool isSplQualified)
        {
            Report.ChildLog.Log(Status.Info, $"Method:- {MethodBase.GetCurrentMethod().Name}");
            if (isSplQualified || les_decision.ToLower() == "approve")
            {
                HandleQualifiedSPLUserFlow(Idnumber, les_decision, isSplQualified);
            }
            else
            {
                HandleNonQualifiedSPLUserFlow(Idnumber, les_decision, isSplQualified);
            }
        }

        /// <summary>
        /// This for checking button functionality
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifyCapfinTileButtonAndLogs()
        {
            Report.ChildLog.Log(Status.Info, $"Method:- {MethodBase.GetCurrentMethod().Name}");
            HandleCapfinUserFlow();
        }

        /// <summary>
        /// this for checking buttons on CreditCard And MobiCred Tile 
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifyCreditCardAndMobiCredTileButtons(string Idnumber, string creditcoachscore_creditcard)
        {
            Report.ChildLog.Log(Status.Info, $"Method:- {MethodBase.GetCurrentMethod().Name}");
            ValidateCreditCardTileButton();
            if (int.Parse(creditcoachscore_creditcard) < 30)
            {
                HandleAutoLogoutWhileWaiting(300000);
                dbCreditCoach.GetCampaignSourceValidate(Idnumber, "Credit Card Decline");
            }
            ValidateMobiCredTileButton();
            if (int.Parse(creditcoachscore_creditcard) < 30)
            {
                baseStep.wait.GenericWait(300000);
                dbCreditCoach.GetCampaignSourceValidate(Idnumber, "Credit Card Decline");
            }
        }

        /// <summary>
        /// Method used for checking qualifer on CreditCard Tile on Home page
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifyCreditConsolidationTileQualifiersOnHomePage(string Idnumber)
        {
            var homePage = new HomePage();
            var homePageSteps = new HomePageSteps();

            NavigateToHomePage(homePage);
            Report.ChildLog.Log(Status.Info, $"****Verify CreditConsolidationTile On HomePage****");
            string monthlySaving = null;
            string expectedCreditConsolQualifier = ReturnExpectedQualifierTextForCreditConsolidation(Idnumber, homePage.CreditConsolQualifier, monthlySaving);
            baseStep.wait.WaitTillPageLoad();
            homePageSteps.VerifySingleTileQualifierOnHomePage(homePage.CreditConsolQualifier, expectedCreditConsolQualifier);
        }

        /// <summary>
        /// Method used to check button actions and logs for Credit Consolidation Tile
        /// </summary>
        /// <param name="Idnumber"></param>
        /// <param name="dBClient"></param>
        /// <param name="dBC_Conversion"></param>
        [Author("Shahab Khan")]
        public bool VerifyCreditConsolTileButtonAndLogs(string Idnumber, string dBClient, string dBC_Conversion)
        {
            Report.ChildLog.Log(Status.Info, $"Method:- {MethodBase.GetCurrentMethod().Name}");
            bool isQualified = ReturnCreditConsolidationTileQualification(dBClient, dBC_Conversion) == "Qualified" ? true : false;
            Report.ChildLog.Log(Status.Info, $"User is qualified {isQualified}");
            if (isQualified)
            {
                DateTime requestTime = DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                HandleCreditConsolTileCallMeBackBtn();
                ValidateCreditConsolidateLogsInExternalCommLog(Idnumber, requestTime).GetAwaiter().GetResult();
                ValidateCreditConsolidationLogsInSql(Idnumber);
            }
            else
            {
                VerifyCallMeBackCreditConsolidationTile(Idnumber);
            }
            return isQualified;
        }

        /// <summary>
        /// Verifies Personal Finance 27 tile qualifier
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyPersonalFinance27TileQualifier(string Idnumber)
        {
            Report.ChildLog.Log(Status.Info, $"Method:- {MethodBase.GetCurrentMethod().Name}");
            NavigateToSolutionsPage();
            baseStep.wait.WaitTillPageLoad();
            string keyVal = dbCreditCoach.KeynameValuefromCreditHistory(Idnumber, "CreditCoachScorePerc");
            ValidatePersonalFinance27Qualifier(Idnumber);
        }

        /// <summary>
        /// This for checking button functionality
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifyPersonalFinance27TileButtonAndLogs(string Idnumber)
        {
            Report.ChildLog.Log(Status.Info, $"Method:- {MethodBase.GetCurrentMethod().Name}");
            ValidatePersonalFinance27TileApplyNowButton();
            ValidateFinance27ExternalCommLog(Idnumber);
        }

        /// <summary>
        /// Method is to verify the Trueworth and identify tile buttons
        /// </summary>
        /// <param name="Idnumber"></param>
        [Author("Shahab Khan")]
        public void VerifySoreCardsTileButtonAndLogs()
        {
            Report.ChildLog.Log(Status.Info, $"Method:- {MethodBase.GetCurrentMethod().Name}");
            HandleStoreCardUserFlow();
        }

        /// <summary>
        /// This method validates health tiles by navigating to the solutions page, ensuring visibility, scrolling, clicking "View All Products," and verifying medical scheme, health insurance, and gap cover tiles.
        /// </summary>
        [Author("Piyush Sharma")]
        public void ValidateHealthTiles()
        {
            NavigateToSolutionsPage();
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.healthsection, 10);
            baseStep.ScrollToElement(solutionPage.ViewAllProducts_Health);
            baseStep.Click(solutionPage.ViewAllProducts_Health);
            CheckMedicalSchemeSolutionTile();
            CheckPrimaryHealthInsuranceTile();
            CheckGapCoverTile();
        }

        /// <summary>
        /// This method validates save money tiles by navigating to the solutions page, ensuring visibility, scrolling, clicking "View All Products," and verifying various savings-related tiles.
        /// </summary>
        [Author("Piyush Sharma")]
        public void ValidateSaveMoneyTiles()
        {
            NavigateToSolutionsPage();
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.savemoneysection, 10);
            baseStep.ScrollToElement(solutionPage.ViewAllProducts_Saving);
            baseStep.Click(solutionPage.ViewAllProducts_Saving);
            CheckRewardsTile();
            CheckTaxFreeSavingTile();
            InvestAndSharesTile();
            RetirementPlanTile();
            EducationPlanTile();
            UnitTrustTile();
        }

        /// <summary>
        /// This method validates financial planning tiles by navigating to the solutions page, ensuring visibility, scrolling, and verifying the "Get Advice" and "Online Will" tiles.
        /// </summary>
        [Author("Piyush Sharma")]
        public void ValidateFinancialPlanningTiles()
        {
            NavigateToSolutionsPage();
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.planningsection, 10);
            baseStep.ScrollToElement(solutionPage.PlanningSection);
            CheckGetAdviceTile();
            CheckOnlineWillTile();
        }

        /// <summary>
        /// This method checks and updates the SPL qualification decision, ensuring it's set to "Approve" if previously "Decline" or "Maybe," then verifies the prequalification message.
        /// </summary>
        /// <param name="IdNumber"></param>
        [Author("Piyush Sharma")]
        public void CheckSPLQualificationDecision(string IdNumber)
        {
            NavigateToSolutionsPage();

            var splQualificationDecisionInfo = dbCreditCoach.FetchSPLQualificationDecision(IdNumber);
            string Decision = splQualificationDecisionInfo["Decision"].ToString();

            if (Decision == "Decline" || Decision == "Maybe")
            {
                dbCreditCoach.UpdateLesDecision(IdNumber, "Approve");
            }
            else
            {
                Report.ChildLog.Log(Status.Info, "SPL Qualification Decision is already set to Approve");
            }

            RefreshPage();

            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.splprequalifier, 10);
            validate.AssertEquals("You are likely to qualify", baseStep.getText.Text(solutionPage.SPLPrequalifier), "Prequalifier is mismatch", true);

            baseStep.wait.WaitTillPageLoad();
        }

        /// <summary>
        /// This method automates the SPL application process by verifying UI elements, handling OTP validation, and ensuring correct page redirection and logs for the given user.
        /// </summary>
        /// <param name="cellPhoneNumber"></param>
        /// <param name="idNumber"></param>
        /// <param name="urlparameters"></param>
        [Author("Piyush Sharma")]
        public void SPLApplyProcess(string cellPhoneNumber, string idNumber, string urlparameters)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.splaccepttermchkbox, 10);

            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.splapplynowbtn, 10);
            validate.AssertEquals("Apply Now", baseStep.getText.Text(solutionPage.SPLApplyNowBtn), "Apply Now button text is mismatch", true);

            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.splcallmebackbtn, 10);
            validate.AssertEquals("Call me back", baseStep.getText.Text(solutionPage.SPLCallMeBackBtn), "Call Me Back button text is mismatch", true);

            baseStep.Click(solutionPage.SPLAcceptTermChkBox);
            baseStep.Click(solutionPage.SPLApplyNowBtn);
            baseStep.wait.WaitTillPageLoad();

            validate.AssertEquals(true, Driver.Url.Contains("/branch-spl-otp"), "User didn't redirect to OTP screen", true);
            baseStep.Click(solutionPage.SendOtpBtn_SPL);
            baseStep.wait.WaitTillPageLoad();

            var otpDetails = dbCreditCoach.getCellphoneNumberfromOTPSTable(cellPhoneNumber);

            string otpObjectiveId = otpDetails["OTPObjectiveId"].ToString();
            validate.AssertEquals("BranchSplConsent", otpObjectiveId, "OTP Objective ID is incorrct", true);

            string pin = otpDetails["Pin"].ToString();
            baseStep.SendKeys(solutionPage.EnterOTP_SPL, pin);
            baseStep.wait.GenericWait(2000);
            baseStep.Click(solutionPage.SubmitOTP_SPL);
            baseStep.wait.GenericWait(10000);
            ValidateSPLPageRedirectionAndLogs(idNumber, urlparameters);

            baseStep.wait.GenericWait(3000);
            baseStep.Click(solutionPage.SPLAcceptTermChkBox);
            baseStep.Click(solutionPage.SPLApplyNowBtn);
            baseStep.wait.GenericWait(10000);
            ValidateSPLPageRedirectionAndLogs(idNumber, urlparameters);
        }

        /// <summary>
        /// This method automates the SPL "Call Me Back" process, verifies the callback popup, closes it, and checks the database for a communication log within a timeout period.
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="externalCommLogTypeId"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public bool SPLCallMeBackProcess(string IdNumber, int externalCommLogTypeId, int timeoutInSeconds = 60)
        {
            baseStep.Click(solutionPage.SPLCallMeBackBtn);
            baseStep.wait.WaitTillPageLoad();

            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.callbackpopupheader_spl, 10);
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.callbackpopupbody_spl, 10);

            baseStep.Click(solutionPage.CallBackPopupClose_SPL);

            DateTime timeout = DateTime.UtcNow.AddSeconds(timeoutInSeconds);

            while (DateTime.UtcNow < timeout)
            {
                var daLesLoginLog = dbCreditCoach.FetchExternalCommLogInfo(IdNumber, externalCommLogTypeId, 0);
                if (daLesLoginLog.Count >= 1)
                    return true;

                baseStep.wait.GenericWait(5000);
            }
            return false;

        }

        /// <summary>
        /// The method verifies the OOBA home loan process by navigating, validating UI elements, handling popups, switching windows, and checking communication logs.
        /// </summary>
        /// <param name="idNumber"></param>
        [Author("Piyush Sharma")]
        public void VerifyOOBAHomeLoan(string idNumber)
        {
            NavigateToSolutionsPage();
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloanstile, 10);
            baseStep.ScrollToElement(solutionPage.OobaHomeLoansTile);

            validate.AssertEquals("Home Loans Prequalify", baseStep.getText.Text(solutionPage.OobaHomeLoansTitle), "Title is mismatch", true);
            validate.AssertEquals("ooba Home Loans", baseStep.getText.Text(solutionPage.OobaHomeLoansSubTitle), "Sub-Title is mismatch", true);
            validate.AssertEquals("Calculate the home loan amount you could qualify for and get your prequalification certificate.", baseStep.getText.Text(solutionPage.OobaHomeLoansDescription), "Description is mismatch", true);

            baseStep.Click(solutionPage.OobaHomeLoans_FindOutMore);

            HandleWindows("Prequalify for a Home Loan | ooba | Sanlam Credit Solutions");

            baseStep.Click(solutionPage.OobaHomeLoans_GetPrequalified);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloan_startjourneybtn, 10);
            baseStep.Click(solutionPage.OobaHomeLoans_StartJourneyBtn);
            baseStep.wait.WaitTillPageLoad();

            var extCommLog = WaitForExternalCommLog(idNumber, 19, 0);
            string responseData = extCommLog["ResponseData"].ToString();
            try
            {
                JObject data = JObject.Parse(responseData);
                string statusCode = data["status_code"].ToString();

                if (data["status_code"].ToString() == "200")
                {
                    baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloan_continuebtn, 10);
                    baseStep.Click(solutionPage.OobaHomeLoans_ContinueBtn);
                    baseStep.wait.WaitTillPageLoad();

                    HandleWindows("ooba - Bond Indicator");
                }
                else if (data["status_code"].ToString() == "422")
                {
                    baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloan_speaktocoachbtn, 10);
                    baseStep.Click(solutionPage.OobaHomeLoans_SpeakToCoachBtn);
                    baseStep.wait.WaitTillPageLoad();
                    baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloan_speaktocoachpopup, 10);
                    baseStep.Click(solutionPage.OobaHomeLoans_SpeakToCoachPopupClose);

                    WaitForExternalCommLog(idNumber, 5, 0);
                    WaitForExternalCommLog(idNumber, 5, 1);
                }
            }
            catch
            {
                Report.ChildLog.Log(Status.Info, "Status code is Null");
                baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloan_errormessage, 10);
                validate.TakeStepFullScreenShot("Error Message", Status.Info);
            }
        }

        /// <summary>
        /// The method verifies the OOBA Home Loan Advance process by navigating, validating UI elements, handling interactions, and checking external communication logs.
        /// </summary>
        /// <param name="idNumber"></param>
        [Author("Piyush Sharma")]
        public void VerifyOOBAHomeLoanAdvance(string idNumber)
        {
            NavigateToSolutionsPage();
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloansadvancetile, 10);
            baseStep.ScrollToElement(solutionPage.OobaHomeLoansAdvanceTile);

            validate.AssertEquals("Home Loan Advance", baseStep.getText.Text(solutionPage.OobaHomeLoansAdvanceTitle), "Title is mismatch", true);
            validate.AssertEquals("ooba Home Loans Advance", baseStep.getText.Text(solutionPage.OobaHomeLoansAdvanceSubTitle), "Sub-Title is mismatch", true);
            validate.AssertEquals("Finance renovations cost effectively, increasing the value of your property. Repay existing debt at a lower interest rate.", baseStep.getText.Text(solutionPage.OobaHomeLoansAdvanceDescription), "Description is mismatch", true);

            baseStep.Click(solutionPage.OobaHomeLoansAdvance_FindOutMore);
            baseStep.wait.WaitTillPageLoad();

            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloanadvance_callmebtn, 10);
            baseStep.Click(solutionPage.OobaHomeLoansAdvance_CallMeBtn);
            baseStep.wait.WaitTillPageLoad();

            var extcommlog_16 = ValidateExternalCommlogInfoFromStorageTable(idNumber, 16, 1);
            var extcommlog_38 = ValidateExternalCommlogInfoFromStorageTable(idNumber, 38, 1);

            try
            {
                if (extcommlog_16.ResponseCode == 201 && extcommlog_38.ResponseCode == 200)
                {
                    baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloanadvance_callbackmsg, 10);
                    baseStep.Click(solutionPage.OobaHomeLoansAdvance_PopupClose);
                }
            }
            catch
            {
                baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.oobahomeloanadvance_errormessage, 10);
                Report.ChildLog.Log(Status.Info, "Status code is Null");
                validate.TakeStepFullScreenShot("Error Message", Status.Info);
                baseStep.Click(solutionPage.OobaHomeLoansAdvance_PopupClose);
            }
        }

        /// <summary>
        /// Method is used to check all fields on a page
        /// </summary>
        [Author("Shahab Khan")]
        public void VerifyFieldsForTracking(string idnumber)
        {
            baseStep.wait.WaitForElementClickableLongWait(solutionPage.solutionsicon, 10);
            baseStep.Click(solutionPage.SolutionsIcon);
            baseStep.wait.WaitTillPageLoad();
            MultipleClickOnElement(idnumber, "//button", 6);
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(solutionPage.ViewAllProductsHealth_Btn);
            baseStep.Click(solutionPage.ViewAllProductsHealth_Btn);
            baseStep.ScrollToElement(solutionPage.ViewAllProductsSavings_Btn);
            baseStep.Click(solutionPage.ViewAllProductsSavings_Btn);
            MultipleClickOnElement(idnumber, "//a", 37);
        }

        /// <summary>
        /// The method validates API responses for various financial products by comparing qualification statuses and UI elements.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="IdNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateMacronAPIResponse(string content, string IdNumber)
        {
            baseStep.wait.WaitForElementClickableLongWait(solutionPage.solutionsicon, 30);
            baseStep.ScrollToElement(solutionPage.SolutionsIcon);
            baseStep.Click(solutionPage.SolutionsIcon);
            baseStep.wait.WaitTillPageLoad();

            JObject parsedJSON = JObject.Parse(content);

            #region Validate OOBA Home Loan

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.oobahomeloanqualifier, 30);
            baseStep.ScrollToElement(solutionPage.OOBAHomeLoanQualifier);

            string oobaHomeLoan_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "ooba Home Loans")?["QualificationStatus"]?.ToString();

            int keyVal_HomeLoan = int.Parse(dbCreditCoach.KeynameValuefromCreditHistory(IdNumber, "CreditCoachScore_HomeLoan"));

            if (keyVal_HomeLoan > 65)
            {
                validate.AssertEquals("You are highly likely to qualify", oobaHomeLoan_QualificationStatus, "OOBA Home Loan Qualification Status is mismatch", true);
                validate.AssertEquals(oobaHomeLoan_QualificationStatus, baseStep.getText.Text(solutionPage.OOBAHomeLoanQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#008000", genericUtils.RgbToHex(solutionPage.OOBAHomeLoanQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_HomeLoan >= 60 && keyVal_HomeLoan <= 65)
            {
                validate.AssertEquals("You are moderately likely to qualify", oobaHomeLoan_QualificationStatus, "OOBA Home Loan Qualification Status is mismatch", true);
                validate.AssertEquals(oobaHomeLoan_QualificationStatus, baseStep.getText.Text(solutionPage.OOBAHomeLoanQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.OOBAHomeLoanQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_HomeLoan < 60)
            {
                validate.AssertEquals("You are unlikely to qualify", oobaHomeLoan_QualificationStatus, "OOBA Home Loan Qualification Status is mismatch", true);
                validate.AssertEquals(oobaHomeLoan_QualificationStatus, baseStep.getText.Text(solutionPage.OOBAHomeLoanQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF0000", genericUtils.RgbToHex(solutionPage.OOBAHomeLoanQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }

            #endregion

            #region Validate OOBA Home Loan Advance

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.oobahomeloanadvancequalifier, 30);
            baseStep.ScrollToElement(solutionPage.OOBAHomeLoanAdvanceQualifier);

            string oobaHomeLoanAdvance_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "ooba Home Loans Advance")?["QualificationStatus"]?.ToString();

            if (keyVal_HomeLoan > 65)
            {
                validate.AssertEquals("You are highly likely to qualify", oobaHomeLoanAdvance_QualificationStatus, "OOBA Home Loan Advance Qualification Status is mismatch", true);
                validate.AssertEquals(oobaHomeLoanAdvance_QualificationStatus, baseStep.getText.Text(solutionPage.OOBAHomeLoanAdvanceQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#008000", genericUtils.RgbToHex(solutionPage.OOBAHomeLoanAdvanceQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_HomeLoan >= 60 && keyVal_HomeLoan <= 65)
            {
                validate.AssertEquals("You are moderately likely to qualify", oobaHomeLoanAdvance_QualificationStatus, "OOBA Home Loan Advance Qualification Status is mismatch", true);
                validate.AssertEquals(oobaHomeLoanAdvance_QualificationStatus, baseStep.getText.Text(solutionPage.OOBAHomeLoanAdvanceQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.OOBAHomeLoanAdvanceQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_HomeLoan < 60)
            {
                validate.AssertEquals("You are unlikely to qualify", oobaHomeLoanAdvance_QualificationStatus, "OOBA Home Loan Advance Qualification Status is mismatch", true);
                validate.AssertEquals(oobaHomeLoanAdvance_QualificationStatus, baseStep.getText.Text(solutionPage.OOBAHomeLoanAdvanceQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF0000", genericUtils.RgbToHex(solutionPage.OOBAHomeLoanAdvanceQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }

            #endregion

            #region Validate Mobicred Account

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.mobicredqualifier, 30);
            baseStep.ScrollToElement(solutionPage.MobiCredQualifier);

            string mobicredAccount_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "Mobicred Account")?["QualificationStatus"]?.ToString();

            int keyVal_CreditCard = int.Parse(dbCreditCoach.KeynameValuefromCreditHistory(IdNumber, "Creditcoachscore_creditcard"));

            if (keyVal_CreditCard < 60)
            {
                validate.AssertEquals("You are unlikely to qualify", mobicredAccount_QualificationStatus, "Mobicred Account Qualification Status is mismatch", true);
                validate.AssertEquals(mobicredAccount_QualificationStatus, baseStep.getText.Text(solutionPage.MobiCredQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.MobiCredQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCard >= 60 && keyVal_CreditCard <= 80)
            {
                validate.AssertEquals("You are moderately likely to qualify", mobicredAccount_QualificationStatus, "Mobicred Account Qualification Status is mismatch", true);
                validate.AssertEquals(mobicredAccount_QualificationStatus, baseStep.getText.Text(solutionPage.MobiCredQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#D5D53D", genericUtils.RgbToHex(solutionPage.MobiCredQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCard > 80)
            {
                validate.AssertEquals("You are likely to qualify", mobicredAccount_QualificationStatus, "Mobicred Account Qualification Status is mismatch", true);
                validate.AssertEquals(mobicredAccount_QualificationStatus, baseStep.getText.Text(solutionPage.MobiCredQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#67E167", genericUtils.RgbToHex(solutionPage.MobiCredQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }

            #endregion

            #region Validate Sanlam Money Saver Credit Card

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.ccqualifier, 30);
            baseStep.ScrollToElement(solutionPage.CCQualifier);

            string sanlamMoneySaverCreditCard_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "Sanlam Money Saver Credit card")?["QualificationStatus"]?.ToString();

            if (keyVal_CreditCard <= 24)
            {
                validate.AssertEquals("You are very unlikely to qualify", sanlamMoneySaverCreditCard_QualificationStatus, "Sanlam Money Saver Credit Card Qualification Status is mismatch", true);
                validate.AssertEquals(sanlamMoneySaverCreditCard_QualificationStatus, baseStep.getText.Text(solutionPage.CCQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF0000", genericUtils.RgbToHex(solutionPage.CCQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCard >= 25 && keyVal_CreditCard <= 30)
            {
                validate.AssertEquals("You are unlikely to qualify", sanlamMoneySaverCreditCard_QualificationStatus, "Sanlam Money Saver Credit Card Qualification Status is mismatch", true);
                validate.AssertEquals(sanlamMoneySaverCreditCard_QualificationStatus, baseStep.getText.Text(solutionPage.CCQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.CCQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCard >= 31 && keyVal_CreditCard <= 64)
            {
                validate.AssertEquals("You are moderately likely to qualify", sanlamMoneySaverCreditCard_QualificationStatus, "Sanlam Money Saver Credit Card Qualification Status is mismatch", true);
                validate.AssertEquals(sanlamMoneySaverCreditCard_QualificationStatus, baseStep.getText.Text(solutionPage.CCQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#D5D53D", genericUtils.RgbToHex(solutionPage.CCQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCard >= 65 && keyVal_CreditCard <= 85)
            {
                validate.AssertEquals("You are likely to qualify", sanlamMoneySaverCreditCard_QualificationStatus, "Sanlam Money Saver Credit Card Qualification Status is mismatch", true);
                validate.AssertEquals(sanlamMoneySaverCreditCard_QualificationStatus, baseStep.getText.Text(solutionPage.CCQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#67E167", genericUtils.RgbToHex(solutionPage.CCQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCard >= 86)
            {
                validate.AssertEquals("You are very likely to qualify", sanlamMoneySaverCreditCard_QualificationStatus, "Sanlam Money Saver Credit Card Qualification Status is mismatch", true);
                validate.AssertEquals(sanlamMoneySaverCreditCard_QualificationStatus, baseStep.getText.Text(solutionPage.CCQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#008000", genericUtils.RgbToHex(solutionPage.CCQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }

            #endregion

            #region Validate Truworths Store Account

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.trueworthqualifier, 30);
            baseStep.ScrollToElement(solutionPage.TrueworthQualifier);

            string truworthsStoreAccount_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "Truworths Store Account")?["QualificationStatus"]?.ToString();

            int keyVal_StoreCard = int.Parse(dbCreditCoach.KeynameValuefromCreditHistory(IdNumber, "CreditCoachScore_StoreCard"));

            if (keyVal_StoreCard < 60)
            {
                validate.AssertEquals("You are unlikely to qualify", truworthsStoreAccount_QualificationStatus, "Truworths Store Account Qualification Status is mismatch", true);
                validate.AssertEquals(truworthsStoreAccount_QualificationStatus, baseStep.getText.Text(solutionPage.TrueworthQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF0000", genericUtils.RgbToHex(solutionPage.TrueworthQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_StoreCard >= 60 && keyVal_StoreCard <= 70)
            {
                validate.AssertEquals("You are likely to qualify", truworthsStoreAccount_QualificationStatus, "Truworths Store Account Qualification Status is mismatch", true);
                validate.AssertEquals(truworthsStoreAccount_QualificationStatus, baseStep.getText.Text(solutionPage.TrueworthQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#67E167", genericUtils.RgbToHex(solutionPage.TrueworthQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_StoreCard > 70)
            {
                validate.AssertEquals("You are very likely to qualify", truworthsStoreAccount_QualificationStatus, "Truworths Store Account Qualification Status is mismatch", true);
                validate.AssertEquals(truworthsStoreAccount_QualificationStatus, baseStep.getText.Text(solutionPage.TrueworthQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#008000", genericUtils.RgbToHex(solutionPage.TrueworthQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }

            #endregion

            #region Validate Identity Account Card

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.identityqualifier, 30);
            baseStep.ScrollToElement(solutionPage.IdentityQualifier);

            string identityAccountCard_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "Identity Account Card")?["QualificationStatus"]?.ToString();

            if (keyVal_StoreCard < 60)
            {
                validate.AssertEquals("You are unlikely to qualify", identityAccountCard_QualificationStatus, "Identity Account Card Qualification Status is mismatch", true);
                validate.AssertEquals(identityAccountCard_QualificationStatus, baseStep.getText.Text(solutionPage.IdentityQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF0000", genericUtils.RgbToHex(solutionPage.IdentityQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_StoreCard >= 60 && keyVal_StoreCard <= 70)
            {
                validate.AssertEquals("You are likely to qualify", identityAccountCard_QualificationStatus, "Identity Account Card Qualification Status is mismatch", true);
                validate.AssertEquals(identityAccountCard_QualificationStatus, baseStep.getText.Text(solutionPage.IdentityQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#67E167", genericUtils.RgbToHex(solutionPage.IdentityQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_StoreCard > 70)
            {
                validate.AssertEquals("You are very likely to qualify", identityAccountCard_QualificationStatus, "Identity Account Card Qualification Status is mismatch", true);
                validate.AssertEquals(identityAccountCard_QualificationStatus, baseStep.getText.Text(solutionPage.IdentityQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#008000", genericUtils.RgbToHex(solutionPage.IdentityQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }

            #endregion

            #region Validate Capfin Personal Loans

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.capfinqualifier, 30);
            baseStep.ScrollToElement(solutionPage.CapfinQualifier);

            string capfinPersonalLoans_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "Capfin Personal Loans")?["QualificationStatus"]?.ToString();

            int keyVal_CreditCoachScorePerc = int.Parse(dbCreditCoach.KeynameValuefromCreditHistory(IdNumber, "CreditCoachScorePerc"));

            if (keyVal_CreditCoachScorePerc < 60)
            {
                validate.AssertEquals("You are less likely to qualify", capfinPersonalLoans_QualificationStatus, "Capfin Personal Loans Qualification Status is mismatch", true);
                validate.AssertEquals(capfinPersonalLoans_QualificationStatus, baseStep.getText.Text(solutionPage.CapfinQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF0000", genericUtils.RgbToHex(solutionPage.CapfinQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCoachScorePerc >= 60 && keyVal_CreditCoachScorePerc <= 70)
            {
                validate.AssertEquals("You are moderately likely to qualify", capfinPersonalLoans_QualificationStatus, "Capfin Personal Loans Qualification Status is mismatch", true);
                validate.AssertEquals(capfinPersonalLoans_QualificationStatus, baseStep.getText.Text(solutionPage.CapfinQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.CapfinQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCoachScorePerc > 70)
            {
                validate.AssertEquals("You are likely to qualify", capfinPersonalLoans_QualificationStatus, "Capfin Personal Loans Qualification Status is mismatch", true);
                validate.AssertEquals(capfinPersonalLoans_QualificationStatus, baseStep.getText.Text(solutionPage.CapfinQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#008000", genericUtils.RgbToHex(solutionPage.CapfinQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }

            #endregion

            #region Validate Finance 27 Short-term Loans

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.finance27qualifier, 30);
            baseStep.ScrollToElement(solutionPage.Finance27Qualifier);

            string finance27ShortTermLoans_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "Finance 27 Short-term Loans")?["QualificationStatus"]?.ToString();

            if (keyVal_CreditCoachScorePerc < 70)
            {
                validate.AssertEquals("You are less likely to qualify", finance27ShortTermLoans_QualificationStatus, "Finance 27 Short-term Loans Qualification Status is mismatch", true);
                validate.AssertEquals(finance27ShortTermLoans_QualificationStatus, baseStep.getText.Text(solutionPage.Finance27Qualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF0000", genericUtils.RgbToHex(solutionPage.Finance27Qualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCoachScorePerc >= 70 && keyVal_CreditCoachScorePerc <= 80)
            {
                validate.AssertEquals("You are moderately likely to qualify", finance27ShortTermLoans_QualificationStatus, "Finance 27 Short-term Loans Qualification Status is mismatch", true);
                validate.AssertEquals(finance27ShortTermLoans_QualificationStatus, baseStep.getText.Text(solutionPage.Finance27Qualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.Finance27Qualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (keyVal_CreditCoachScorePerc > 80)
            {
                validate.AssertEquals("You are highly likely to qualify", finance27ShortTermLoans_QualificationStatus, "Finance 27 Short-term Loans Qualification Status is mismatch", true);
                validate.AssertEquals(finance27ShortTermLoans_QualificationStatus, baseStep.getText.Text(solutionPage.Finance27Qualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#008000", genericUtils.RgbToHex(solutionPage.Finance27Qualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }

            #endregion

            #region Validate DebtBusters Credit Consolidation

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.creditconsolqualifier, 30);
            baseStep.ScrollToElement(solutionPage.CreditConsolQualifier);

            var debtBusters = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "DebtBusters Credit Consolidation");
            string debtBustersCreditConsolidation_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "DebtBusters Credit Consolidation")?["QualificationStatus"]?.ToString();
            string dbClient = debtBusters?["DCQuote"]?["DBClient"]?.ToString();
            string dbcConversion = debtBusters?["DCQuote"]?["DBC_Conversion"]?.ToString();

            var dcQuoteInfo = dbCreditCoach.FetchDCQuoteInfo(IdNumber);
            string DBClient = dcQuoteInfo["DBClient"].ToString();
            string DBC_Conversion = dcQuoteInfo["DBC_Conversion"].ToString();
            double MonthlyInstallment = double.Parse(dcQuoteInfo["MonthlyInstallment"].ToString());
            double DBC_DCRS = double.Parse(dcQuoteInfo["DBC_DCRS"].ToString());

            if (DBClient == "Yes" && DBC_Conversion == "Yes")
            {
                validate.AssertEquals("An unlikely solution for you", debtBustersCreditConsolidation_QualificationStatus, "DebtBusters Credit Consolidation Qualification Status is mismatch", true);
                validate.AssertEquals("Yes", dbClient, "DBClient is mismatch", true);
                validate.AssertEquals("Yes", dbcConversion, "DBCConversion is mismatch", true);
                validate.AssertEquals(debtBustersCreditConsolidation_QualificationStatus, baseStep.getText.Text(solutionPage.CreditConsolQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.CreditConsolQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (DBClient == "Yes" && DBC_Conversion == "No")
            {
                validate.AssertEquals("An unlikely solution for you", debtBustersCreditConsolidation_QualificationStatus, "DebtBusters Credit Consolidation Qualification Status is mismatch", true);
                validate.AssertEquals("Yes", dbClient, "DBClient is mismatch", true);
                validate.AssertEquals("No", dbcConversion, "DBCConversion is mismatch", true);
                validate.AssertEquals(debtBustersCreditConsolidation_QualificationStatus, baseStep.getText.Text(solutionPage.CreditConsolQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.CreditConsolQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (DBClient == "No" && DBC_Conversion == "Yes")
            {
                validate.AssertEquals($"You are likely to save R {(int)Math.Round(MonthlyInstallment - DBC_DCRS)} p.m", debtBustersCreditConsolidation_QualificationStatus, "DebtBusters Credit Consolidation Qualification Status is mismatch", true);
                validate.AssertEquals("No", dbClient, "DBClient is mismatch", true);
                validate.AssertEquals("Yes", dbcConversion, "DBCConversion is mismatch", true);
                validate.AssertEquals(debtBustersCreditConsolidation_QualificationStatus, baseStep.getText.Text(solutionPage.CreditConsolQualifier).Replace(",", ""), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#6BE26B", genericUtils.RgbToHex(solutionPage.CreditConsolQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (DBClient == "No" && DBC_Conversion == "No")
            {
                validate.AssertEquals("An unlikely solution for you", debtBustersCreditConsolidation_QualificationStatus, "DebtBusters Credit Consolidation Qualification Status is mismatch", true);
                validate.AssertEquals("No", dbClient, "DBClient is mismatch", true);
                validate.AssertEquals("No", dbcConversion, "DBCConversion is mismatch", true);
                validate.AssertEquals(debtBustersCreditConsolidation_QualificationStatus, baseStep.getText.Text(solutionPage.CreditConsolQualifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.CreditConsolQualifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }

            #endregion

            #region Validate Sanlam Personal Loans

            if (baseStep.IsElementDisplayed(solutionPage.splprequalifier))
            {
                baseStep.ScrollToElement(solutionPage.SPLPrequalifier);
            }
            else if (baseStep.IsElementDisplayed(solutionPage.continueapplicationbtn))
            {
                baseStep.ScrollToElement(solutionPage.ContinueApplicationBtn);
            }

            var sanlam = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "Sanlam Personal Loans");
            string sanlamPersonalLoans_QualificationStatus = parsedJSON["ProductsQualification"].FirstOrDefault(p => p["Name"]?.ToString() == "Sanlam Personal Loans")?["QualificationStatus"]?.ToString();
            string decision = sanlam?["SplQuote"]?["Decision"]?.ToString();
            string decisionReasons = sanlam?["SplQuote"]?["DecisionReasons"]?.ToString();

            var splQualificationDecision = dbCreditCoach.FetchSPLQualificationDecision(IdNumber);
            string splDecision = splQualificationDecision["Decision"].ToString();
            string splDecisionReasons = splQualificationDecision["JsonDecisionReasons"].ToString();

            var splLESInformation = dbCreditCoach.FetchSPLLESInformation(IdNumber);
            int STTS_LTST_AL_AL_DTRV_AL = int.Parse(splLESInformation["STTS_LTST_AL_AL_DTRV_AL"].ToString());
            int creditCoachScore_PersonalLoan = int.Parse(splLESInformation["CreditCoachScore_PersonalLoan"].ToString());
            int AGEY_BRTH_AL_AL_ALLT_AL = int.Parse(splLESInformation["AGEY_BRTH_AL_AL_ALLT_AL"].ToString());
            int NUMB_LTST_AL_AL_ALLT_C9 = int.Parse(splLESInformation["NUMB_LTST_AL_AL_ALLT_C9"].ToString());
            int NUMB_LTST_AL_AL_ALLT_6p = int.Parse(splLESInformation["NUMB_LTST_AL_AL_ALLT_6p"].ToString());
            int AGEM_OLDT_AL_AL_ALLT_AL = int.Parse(splLESInformation["AGEM_OLDT_AL_AL_ALLT_AL"].ToString());
            int TTBL_OPNG_AL_AL_ALLT_AL = int.Parse(splLESInformation["TTBL_OPNG_AL_AL_ALLT_AL"].ToString());

            bool conditions =
                    STTS_LTST_AL_AL_DTRV_AL == -2 &&
                    creditCoachScore_PersonalLoan >= 70 &&
                    AGEY_BRTH_AL_AL_ALLT_AL >= 19 && AGEY_BRTH_AL_AL_ALLT_AL <= 65 &&
                    NUMB_LTST_AL_AL_ALLT_C9 < 1 &&
                    NUMB_LTST_AL_AL_ALLT_6p < 2 &&
                    AGEM_OLDT_AL_AL_ALLT_AL > 5 &&
                    TTBL_OPNG_AL_AL_ALLT_AL >= 5000;

            if (splDecision == "Approve")
            {
                validate.AssertEquals("You are likely to qualify", sanlamPersonalLoans_QualificationStatus, "Sanlam Personal Loans Qualification Status is mismatch", true);
                validate.AssertEquals("Approve", decision, "Decision is mismatch", true);
                validate.AssertEquals(sanlamPersonalLoans_QualificationStatus, baseStep.getText.Text(solutionPage.SPLPrequalifier), "Pre-Qualifier Text is mismatch", true);
                validate.AssertEquals("#67E167", genericUtils.RgbToHex(solutionPage.SPLPrequalifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
            }
            else if (splDecision == "Maybe")
            {
                if (conditions)
                {
                    validate.AssertEquals("You are likely to qualify", sanlamPersonalLoans_QualificationStatus, "Sanlam Personal Loans Qualification Status is mismatch", true);
                    validate.AssertEquals("Maybe", decision, "Decision is mismatch", true);
                    validate.AssertEquals(sanlamPersonalLoans_QualificationStatus, baseStep.getText.Text(solutionPage.SPLPrequalifier), "Pre-Qualifier Text is mismatch", true);
                    validate.AssertEquals("#67E167", genericUtils.RgbToHex(solutionPage.SPLPrequalifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
                }
                else
                {
                    validate.AssertEquals("You are unlikely to qualify", sanlamPersonalLoans_QualificationStatus, "Sanlam Personal Loans Qualification Status is mismatch", true);
                    validate.AssertEquals("Maybe", decision, "Decision is mismatch", true);
                    validate.AssertEquals(sanlamPersonalLoans_QualificationStatus, baseStep.getText.Text(solutionPage.SPLPrequalifier), "Pre-Qualifier Text is mismatch", true);
                    validate.AssertEquals("#D5D53D", genericUtils.RgbToHex(solutionPage.SPLPrequalifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
                }
            }
            else if (splDecision == "Decline")
            {
                if (splDecisionReasons == "[\"LES: Unable to retrieve customer data\"]" && conditions == true)
                {
                    validate.AssertEquals("You are likely to qualify", sanlamPersonalLoans_QualificationStatus, "Sanlam Personal Loans Qualification Status is mismatch", true);
                    validate.AssertEquals("Decline", decision, "Decision is mismatch", true);
                    validate.AssertEquals(splDecisionReasons, decisionReasons, "Decision Reason is mismatch", true);
                    validate.AssertEquals(sanlamPersonalLoans_QualificationStatus, baseStep.getText.Text(solutionPage.SPLPrequalifier), "Pre-Qualifier Text is mismatch", true);
                    validate.AssertEquals("#67E167", genericUtils.RgbToHex(solutionPage.SPLPrequalifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
                }
                else if (splDecisionReasons == "[\"LES: Customer does not meet company age requirement\",\"LES: Active Application Status Decline Result\"]")
                {
                    validate.AssertEquals("Decline", decision, "Decision is mismatch", true);
                    validate.AssertEquals(splDecisionReasons, decisionReasons, "Decision Reason is mismatch", true);
                    validate.AssertEquals(true, baseStep.IsElementDisplayed(solutionPage.continueapplicationbtn), "Continue Application button is not displayed", true);
                }
                else
                {
                    validate.AssertEquals("You are not likely to qualify", sanlamPersonalLoans_QualificationStatus, "Sanlam Personal Loans Qualification Status is mismatch", true);
                    validate.AssertEquals("Decline", decision, "Decision is mismatch", true);
                    validate.AssertEquals(splDecisionReasons, decisionReasons, "Decision Reason is mismatch", true);
                    validate.AssertEquals(sanlamPersonalLoans_QualificationStatus, baseStep.getText.Text(solutionPage.SPLPrequalifier), "Pre-Qualifier Text is mismatch", true);
                    validate.AssertEquals("#FF8105", genericUtils.RgbToHex(solutionPage.SPLPrequalifier.GetCssValue("color")), "Pre-Qualifier Colour is mismatch", true);
                }
            }

            #endregion
        }

        /// <summary>
        /// Validates external communication log for a given ID number by checking matching log type ID, endpoint, and log type values.
        /// </summary>
        /// <param name="idNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateMacronAPILogInExternalCommLog(string idNumber)
        {
            var extcommlog_37 = ValidateExternalCommlogInfoFromStorageTable(idNumber, 37, 1);

            validate.AssertEquals("37", extcommlog_37.LogTypeId.ToString(), "ExternalCommlogId is mismatch", true);
            validate.AssertEquals("EligibleCreditProductsFunction", extcommlog_37.Endpoint.ToString(), "Endpoint is mismatch", true);
            validate.AssertEquals("ChatBotLog", extcommlog_37.LogType.ToString(), "Endpoint is mismatch", true);
        }

        /// <summary>
        /// Validates API response for credit data, score, risk status, dates, and chatbot log by comparing it with database values.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="IdNumber"></param>
        [Author("Piyush Sharma")]
        public void ValidateSalnamOnlineAPIResponse(string content, string IdNumber)
        {
            #region Validate CreditData

            JObject parsedJSON = JObject.Parse(content);
            string creditData_APIResponse = parsedJSON["CreditData"].ToString();

            var creditHistoryInfo = dbCreditCoach.FetchCreditHistory(IdNumber);
            string externalCommLogInfo = creditHistoryInfo["ExternalCommLogId"].ToString();

            string creditData_CreditHistoryV2LogData = azureTables.ReadBlobFileData(externalCommLogInfo, "scs-logs", "CreditHistoryV2Log/");

            if (string.IsNullOrEmpty(creditData_CreditHistoryV2LogData))
            {
                string creditData_VccbCreditHistoryLogData = azureTables.ReadBlobFileData(externalCommLogInfo, "scs-logs", "VccbCreditHistoryLog/");
                validate.AssertEquals(creditData_APIResponse, creditData_VccbCreditHistoryLogData, "Credit Data is Mismatch", true);
            }
            else
            {
                validate.AssertEquals(creditData_APIResponse, creditData_CreditHistoryV2LogData, "Credit Data is Mismatch", true);
            }

            JObject creditDataObject = JObject.Parse(creditData_APIResponse);
            string creditData_APIResponse_idNumber = creditDataObject["crContactDataResponse"]?[0]?["id_number"]?.ToString();

            validate.AssertEquals(creditData_APIResponse_idNumber, IdNumber, "Id Number is Mismatch", true);

            #endregion

            #region Validate Score Information

            var creditScoreInfo = dbCreditCoach.FetchScoreInformationTable(IdNumber);
            string score = creditScoreInfo["Score"].ToString();
            string scorePercent = creditScoreInfo["ScorePercent"].ToString();
            string scoreAge = creditScoreInfo["ScoreAge"].ToString();

            validate.AssertEquals(score, parsedJSON["Score"].ToString(), "Credit Score Mismatch", true);
            validate.AssertEquals(scorePercent, parsedJSON["ScorePercent"].ToString(), "Score Percent Mismatch", true);
            validate.AssertEquals(scoreAge, parsedJSON["ScoreAge"].ToString(), "Score Age Mismatch", true);

            var creditHealthInfo = dbCreditCoach.CreditHealthInfoTable(IdNumber);
            string monthlyInterestPayment = creditHealthInfo["MonthlyInterestPayment"].ToString();

            validate.AssertEquals(monthlyInterestPayment, parsedJSON["MonthlyInterestPayment"].ToString(), "Monthly Interest Payment is Mismatch", true);

            #endregion

            #region Validate Risk Status

            if (int.Parse(scorePercent) >= 1 && int.Parse(scorePercent) <= 20)
            {
                validate.AssertEquals("Very High Risk", parsedJSON["RiskStatus"].ToString(), "Risk Status is Mismatch", true);
            }
            else if (int.Parse(scorePercent) >= 21 && int.Parse(scorePercent) <= 40)
            {
                validate.AssertEquals("High Risk", parsedJSON["RiskStatus"].ToString(), "Risk Status is Mismatch", true);
            }
            else if (int.Parse(scorePercent) >= 41 && int.Parse(scorePercent) <= 60)
            {
                validate.AssertEquals("Medium Risk", parsedJSON["RiskStatus"].ToString(), "Risk Status is Mismatch", true);
            }
            else if (int.Parse(scorePercent) >= 61 && int.Parse(scorePercent) <= 80)
            {
                validate.AssertEquals("Low Risk", parsedJSON["RiskStatus"].ToString(), "Risk Status is Mismatch", true);
            }
            else if (int.Parse(scorePercent) >= 81 && int.Parse(scorePercent) <= 100)
            {
                validate.AssertEquals("Very Low Risk", parsedJSON["RiskStatus"].ToString(), "Risk Status is Mismatch", true);
            }

            #endregion

            #region Validate Credit History Dates

            string creditHistory_CreatedDate = creditHistoryInfo["CreatedDate"].ToString();
            string LastUpdateDate_APIResponse = parsedJSON["LastUpdateDate"].ToString();

            validate.AssertEquals(creditHistory_CreatedDate, LastUpdateDate_APIResponse, "Credit History Created Date is Mismatch", true);

            DateTime oneMonthAheadDate = DateTime.Parse(creditHistory_CreatedDate).Date.AddMonths(1);
            DateTime currentDate = DateTime.Now.Date;
            int numberOfDays = (oneMonthAheadDate - currentDate).Days;
            DateTime creditHistory_NextUpdateDate = DateTime.Parse(creditHistory_CreatedDate).AddDays(numberOfDays);

            DateTime formattedDate = DateTime.Parse(parsedJSON["NextUpdateDate"].ToString());
            string nextUpdateDate = formattedDate.ToString("yyyy-MM-dd"); 

            validate.AssertEquals(nextUpdateDate, creditHistory_NextUpdateDate.ToString("yyyy-MM-dd"), "Next Update Date for credit report is Mismatch", true);

            #endregion

            #region Fetch and Validate ChatBot Log

            var ExternalCommLogList = azureTables.GetExternalCommLogInfo(IdNumber, 37, 1);

            string idNumber_AzureExt = ExternalCommLogList.IdNumber;
            validate.AssertEquals(idNumber_AzureExt, IdNumber, "Id Number is Mismatch", true);

            string endPoint_AzureExt = ExternalCommLogList.Endpoint;
            validate.AssertEquals("GetCreditScoreAndRiskDetailFunction", endPoint_AzureExt, "End Point name is Mismatch", true);

            string logType_AzureExt = ExternalCommLogList.LogType;
            validate.AssertEquals("ChatBotLog", logType_AzureExt, "Log Type name is Mismatch", true);

            string userId_AzureExt = ExternalCommLogList.UserId.ToString();
            var userDetails = dbCreditCoach.FetchUserDetailsFromUserTable(IdNumber);
            validate.AssertEquals(userDetails["Id"].ToString(), userId_AzureExt, "User Id name is Mismatch", true);

            string requestParam_AzureExt = ExternalCommLogList.RequestParam;
            JObject requestParam = JObject.Parse(requestParam_AzureExt);
            string idNumber_requestParam = requestParam["idNumber"].ToString();
            validate.AssertEquals(IdNumber, idNumber_requestParam, "IdNumber in Request Param is Mismatch", true);

            #endregion
        }

        #region Private Helper Methods

        private void HandleWindows(string windowTitle)
        {
            var currentWindow = Driver.CurrentWindowHandle;
            var newWindow = Driver.WindowHandles;
            Driver.SwitchTo().Window(newWindow[1]);
            validate.AssertEquals(windowTitle, Driver.Title, $"Page didn't redirected to {windowTitle}", true);
            validate.TakeStepFullScreenShot("New Window Screenshot", Status.Info);
            Driver.Close();
            Driver.SwitchTo().Window(currentWindow);
            baseStep.wait.WaitTillPageLoad();
        }

        private StorageBrowserTable ValidateExternalCommlogInfoFromStorageTable(string idNumber, int logTypeId, int platformId, int timeoutInSeconds = 60)
        {
            DateTime timeout = DateTime.UtcNow.AddSeconds(timeoutInSeconds);
            StorageBrowserTable ExternalCommLogList = null;

            while (DateTime.UtcNow < timeout)
            {
                ExternalCommLogList = azureTables.GetExternalCommLogInfo(idNumber, logTypeId, platformId);
                if (ExternalCommLogList != null)
                    break;

                baseStep.wait.GenericWait(5000);
            }
            return ExternalCommLogList;
        }

        private Dictionary<string, object> WaitForExternalCommLog(string idNumber, int externalCommLogTypeId, int index, int timeoutInSeconds = 60)
        {
            DateTime timeout = DateTime.UtcNow.AddSeconds(timeoutInSeconds);
            Dictionary<string, object> extCommLog = null;

            while (DateTime.UtcNow < timeout)
            {
                extCommLog = dbCreditCoach.FetchExternalCommLogInfo(idNumber, externalCommLogTypeId, index);
                if (extCommLog != null && extCommLog.Count >= 1)
                    break;

                baseStep.wait.GenericWait(5000);
            }
            return extCommLog;
        }

        private void ValidateSPLPageRedirectionAndLogs(string idNumber, string urlparameters, int timeoutInSeconds = 60)
        {
            var currentWindow = Driver.CurrentWindowHandle;
            var newWindow = Driver.WindowHandles;
            if (newWindow.Count > 1)
            {
                try
                {
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("Sanlam Personal Loans", Driver.Title, "Page didn't redirected to Sanlam Personal Loans", true);
                    validate.TakeStepFullScreenShot("Sanlam Personal Loans", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                catch
                {
                    Report.ChildLog.Log(Status.Info, "Sanlam Personal Loans popup is blocked");
                }
            }
            else
            {
                Report.ChildLog.Log(Status.Info, "Sanlam Personal Loans popup is blocked");
            }

            DateTime timeout = DateTime.UtcNow.AddSeconds(timeoutInSeconds);
            Dictionary<string, object> idpAPILog = null;

            while (DateTime.UtcNow < timeout)
            {
                idpAPILog = dbCreditCoach.FetchExternalCommLogInfo(idNumber, 20, 0);
                if (idpAPILog != null && idpAPILog.Count >= 1)
                    break;

                baseStep.wait.GenericWait(5000);
            }

            string RequestParam = idpAPILog["RequestParam"].ToString();
            var JsonRequestParam = JObject.Parse(RequestParam);
            validate.AssertEquals("True", JsonRequestParam["IsConsentAlreadyGiven"].ToString(), "IsConsentAlreadyGiven is set as False", true);
            validate.AssertEquals("4", JsonRequestParam["SibsStrategyId"].ToString(), "SibsStrategyId is mismatch", true);
            validate.AssertEquals("36", JsonRequestParam["SibsSupplierSourceId"].ToString(), "SibsSupplierSourceId is mismatch", true);
            validate.AssertEquals(true, urlparameters.Contains(JsonRequestParam["SibsBranchCode"].ToString()), "SibsBranchCode is mismatch", true);
        }

        private void MultipleClickOnElement(string idnumber, string elementType, int fieldIndex)
        {
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>Checking for {elementType} on Solution Page<<<<<<<<<<<");
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
                        WaitTillPageLoad(20);
                        Report.ChildLog.Log(Status.Info, $"Click on Element with attribute [{attributeKey}={attributeValue}]");
                        DBCreditCoach dBCreditCoach = new DBCreditCoach();
                        string userId = dBCreditCoach.GetUserId(idnumber);
                        string query = dBQueries.FetchCustomEvents(id, userId, currentDateTime);
                        logTasks.Add(Task.Run(() => appInsights.GetLogsFromAppInsights(query, attributeKey, attributeValue, currentDateTime)));

                        if (validate.IsElementDisplayed(solutionPage.callmebackpopupcutbtn))
                        {
                            baseStep.Click(solutionPage.CallMeBackPopupCutBtn);
                        }
                        if (validate.IsElementDisplayed(solutionPage.closebtn_popup))
                        {
                            baseStep.Click(solutionPage.CloseBtn_PopUp);
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
                if (validate.IsElementClickable(solutionPage.solutionsicon))
                {
                    WaitTillPageLoad();
                    baseStep.ScrollToElement(solutionPage.SolutionsIcon);
                    baseStep.wait.WaitForElementClickableLongWait(solutionPage.solutionsicon, 10);
                    baseStep.Click(solutionPage.SolutionsIcon);
                    baseStep.wait.WaitTillPageLoad();
                }
                totalFields = Driver.FindElements(By.XPath(elementType));
            }
            Task.WhenAll(logTasks).GetAwaiter().GetResult();
            appInsights.PrintCollectedLogs();
            Report.ChildLog.Log(Status.Pass, $">>>>>>>>>>>>>Checked total fields: {j} of tag {elementType} and failure is not occur for user {idnumber}<<<<<<<<<<<<");
        }

        private void WaitTillPageLoad(int waitInSeconds = 180)
        {
            WebDriverWait webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitInSeconds));
            By spinnerLocator = By.XPath("//ngx-spinner");

            try
            {
                // Store the current window before checking for new tabs
                string originalWindow = Driver.CurrentWindowHandle;
                var allWindows = base.Driver.WindowHandles;

                // If a new tab opens, switch to the latest one
                if (allWindows.Count > 1)
                {
                    base.Driver.SwitchTo().Window(allWindows.Last());
                }

                // Wait for the new page in the active tab to fully load
                webDriverWait.Until(driver =>
                    ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString().Equals("complete"));

                // Wait for all AJAX requests to complete
                webDriverWait.Until(driver =>
                    (bool)((IJavaScriptExecutor)driver).ExecuteScript("return window.jQuery ? jQuery.active == 0 : true"));

                // Wait for the spinner to disappear (only if still on the same tab)
                if (base.Driver.CurrentWindowHandle == originalWindow)
                {
                    webDriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(spinnerLocator));
                }

                // Switch back to the original tab if needed
                if (base.Driver.CurrentWindowHandle != originalWindow)
                {
                    base.Driver.SwitchTo().Window(originalWindow);
                }
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"Timeout while waiting for page load: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in WaitTillPageLoad: {ex.Message}");
            }
        }

        private void CheckGetAdviceTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.getadvicetile))
            {
                validate.AssertEquals("Get Advice", baseStep.getText.Text(solutionPage.GetAdviceTitle), "Title is Mismatch", true);
                validate.AssertEquals("Find the right financial planner", baseStep.getText.Text(solutionPage.GetAdviceSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("Sanlam advisers aim to provide you with professional advice and can help you shape a complete financial plan.", baseStep.getText.Text(solutionPage.GetAdviceDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.getadvice_getintouch))
                {
                    baseStep.ScrollToElement(solutionPage.GetAdvice_GetInTouch);
                    baseStep.Click(solutionPage.GetAdvice_GetInTouch);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("\r\n\tFind a Financial Adviser | Contact Sanlam", Driver.Title, "Page didn't redirected to Get Advice", true);
                    validate.TakeStepFullScreenShot("Get Advice", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Get in Touch button is not available for Get Advice", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Get Advice product tile is not available", Status.Info);
            }
        }

        private void CheckOnlineWillTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.onlinewilltile))
            {
                validate.AssertEquals("Online Will", baseStep.getText.Text(solutionPage.OnlineWillTitle), "Title is Mismatch", true);
                validate.AssertEquals("Draft your will online in minutes", baseStep.getText.Text(solutionPage.OnlineWillSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("A will stipulates how your property, possessions, money and other assets will be distributed when you pass away.", baseStep.getText.Text(solutionPage.OnlineWillDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.onlinewill_draftonenow))
                {
                    baseStep.ScrollToElement(solutionPage.OnlineWill_DraftOneNow);
                    baseStep.Click(solutionPage.OnlineWill_DraftOneNow);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("Financial Planning | Wills, Trusts and Estates | Wills", Driver.Title, "Page didn't redirected to Wills", true);
                    validate.TakeStepFullScreenShot("Wills", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Draft One Now button is not available for Wills", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Wills product tile is not available", Status.Info);
            }
        }

        private void CheckRewardsTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.rewardtile))
            {
                validate.AssertEquals("Rewards", baseStep.getText.Text(solutionPage.RewardTitle), "Title is Mismatch", true);
                validate.AssertEquals("Sanlam Reality", baseStep.getText.Text(solutionPage.RewardSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("A member on Reality Plus saves R 12,501 with wealth, travel, entertainment and gym benefits a year!", baseStep.getText.Text(solutionPage.RewardDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.reward_joinnow))
                {
                    baseStep.ScrollToElement(solutionPage.Reward_JoinNow);
                    baseStep.Click(solutionPage.Reward_JoinNow);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("Join Now | Sanlam Reality", Driver.Title, "Page didn't redirected to Sanlam Reality", true);
                    validate.TakeStepFullScreenShot("Sanlam Reality", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Join Now button is not available for Sanlam Reality", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Sanlam Reality product tile is not available", Status.Info);
            }
        }

        private void CheckTaxFreeSavingTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.taxfreesavingtile))
            {
                validate.AssertEquals("Tax-free Savings", baseStep.getText.Text(solutionPage.TaxFreeSavingTitle), "Title is Mismatch", true);
                validate.AssertEquals("Save for your long-term goals", baseStep.getText.Text(solutionPage.TaxFreeSavingSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("Save for your long-term goals without having to pay tax on interest, dividends, and capital gains.", baseStep.getText.Text(solutionPage.TaxFreeSavingDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.taxfreesaving_findoutmore))
                {
                    baseStep.ScrollToElement(solutionPage.TaxFreeSaving_FindOutMore);
                    baseStep.Click(solutionPage.TaxFreeSaving_FindOutMore);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("Personal Investment TFSA", Driver.Title, "Page didn't redirected to Personal Investment TFSA", true);
                    validate.TakeStepFullScreenShot("Personal Investment TFSA", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Find Out More button is not available for Personal Investment TFSA", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Personal Investment TFSA product tile is not available", Status.Info);
            }
        }

        private void InvestAndSharesTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.investinsharestile))
            {
                validate.AssertEquals("Invest in Shares", baseStep.getText.Text(solutionPage.InvestInSharesTitle), "Title is Mismatch", true);
                validate.AssertEquals("EasyEquities", baseStep.getText.Text(solutionPage.InvestInSharesSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("Invest with no minimums, in local and offshore shares and ETFs, property, crypto and much more.\r\nSign up and get R50 investment voucher", baseStep.getText.Text(solutionPage.InvestInSharesDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.investinshares_joinnow))
                {
                    baseStep.ScrollToElement(solutionPage.InvestInShares_JoinNow);
                    baseStep.Click(solutionPage.InvestInShares_JoinNow);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("EasyEquities - Sanlam Credit Solutions Home", Driver.Title, "Page didn't redirected to EasyEquities", true);
                    validate.TakeStepFullScreenShot("EasyEquities - Sanlam Credit Solutions Home", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Join Now button is not available for EasyEquities", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("EasyEquities product tile is not available", Status.Info);
            }
        }

        private void RetirementPlanTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.retirementplantile))
            {
                validate.AssertEquals("Retirement Plan", baseStep.getText.Text(solutionPage.RetirementPlanTitle), "Title is Mismatch", true);
                validate.AssertEquals("Sanlam Retirement Plan", baseStep.getText.Text(solutionPage.RetirementPlanSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("Boost your retirement savings with a retirement plan that pays a bonus. The longer you save, the bigger the bonus.", baseStep.getText.Text(solutionPage.RetirementPlanDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.retirementplan_findoutmore))
                {
                    baseStep.ScrollToElement(solutionPage.RetirementPlan_FindOutMore);
                    baseStep.Click(solutionPage.RetirementPlan_FindOutMore);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("Saving-for-Retirement", Driver.Title, "Page didn't redirected to Saving for Retirement", true);
                    validate.TakeStepFullScreenShot("Saving for Retirement", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Find Out More button is not available for Saving for Retirement", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Saving for Retirement product tile is not available", Status.Info);
            }
        }

        private void EducationPlanTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.educationplantile))
            {
                validate.AssertEquals("Education Planning", baseStep.getText.Text(solutionPage.EducationPlanTitle), "Title is Mismatch", true);
                validate.AssertEquals("Sanlam Goal Manager", baseStep.getText.Text(solutionPage.EducationPlanSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("It’s never too late to set a savings goal to start saving for your child’s tertiary education.", baseStep.getText.Text(solutionPage.EducationPlanDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.educationplan_findoutmore))
                {
                    baseStep.ScrollToElement(solutionPage.EducationPlan_FindOutMore);
                    baseStep.Click(solutionPage.EducationPlan_FindOutMore);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("\r\n\tEducation Planning | Policies & Plans | Sanlam", Driver.Title, "Page didn't redirected to Saving for Education Planning", true);
                    validate.TakeStepFullScreenShot("Education Planning", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Find Out More button is not available for Education Planning", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Education Planning product tile is not available", Status.Info);
            }
        }

        private void UnitTrustTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.unittrusttile))
            {
                validate.AssertEquals("Unit Trusts", baseStep.getText.Text(solutionPage.UnitTrustTitle), "Title is Mismatch", true);
                validate.AssertEquals("Sanlam Smart Invest", baseStep.getText.Text(solutionPage.UnitTrustSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("Smart Invest is an online tool that helps you invest in unit trusts from R500 per month.", baseStep.getText.Text(solutionPage.UnitTrustDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.unittrust_findoutmore))
                {
                    baseStep.ScrollToElement(solutionPage.UnitTrust_FindOutMore);
                    baseStep.Click(solutionPage.UnitTrust_FindOutMore);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("Smart Invest", Driver.Title, "Page didn't redirected to Saving for Smart Invest", true);
                    validate.TakeStepFullScreenShot("Smart Invest", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Find Out More button is not available for Smart Invest", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Smart Invest product tile is not available", Status.Info);
            }
        }

        private void CheckMedicalSchemeSolutionTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.medicalschemesolutiontile))
            {
                validate.AssertEquals("Medical Scheme Solution", baseStep.getText.Text(solutionPage.MedicalSchemeSolutionTitle), "Title is Mismatch", true);
                validate.AssertEquals("Affordable Medical Scheme options", baseStep.getText.Text(solutionPage.MedicalSchemeSolutionSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("Switch and save – find the right medical scheme for you", baseStep.getText.Text(solutionPage.MedicalSchemeSolutionDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.medicalschemesolution_findoutmore))
                {
                    baseStep.ScrollToElement(solutionPage.MedicalSchemeSolution_FindOutMore);
                    baseStep.Click(solutionPage.MedicalSchemeSolution_FindOutMore);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("Sanlam Health Solutions", Driver.Title, "Page didn't redirected to Sanlam Health Solutions", true);
                    validate.TakeStepFullScreenShot("Sanlam Health Solutions", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Find Out More button is not available for Medical Scheme Solution", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Medical Scheme Solution product tile is not available", Status.Info);
            }
        }

        private void CheckPrimaryHealthInsuranceTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.primaryhealthinsurancetile))
            {
                validate.AssertEquals("Primary Health Insurance", baseStep.getText.Text(solutionPage.PrimaryHealthInsuranceTitle), "Title is Mismatch", true);
                validate.AssertEquals("EssentialMED Health Insurance", baseStep.getText.Text(solutionPage.PrimaryHealthInsuranceSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("Choose your own benefits and save on your premiums. An affordable alternative to a Medical Aid.", baseStep.getText.Text(solutionPage.PrimaryHealthInsuranceDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.primaryhealthinsurance_getaquote))
                {
                    baseStep.ScrollToElement(solutionPage.PrimaryHealthInsurance_GetaQuote);
                    baseStep.Click(solutionPage.PrimaryHealthInsurance_GetaQuote);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("EssentialMED | Sanlam Credit Solutions | Episodic", Driver.Title, "Page didn't redirected to Essential MED", true);
                    validate.TakeStepFullScreenShot("Primary Health Insurance", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Get a Quote button is not available for Primary Health Insurance", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Primary Health Insurance product tile is not available", Status.Info);
            }
        }

        private void CheckGapCoverTile()
        {
            if (baseStep.IsElementDisplayed(solutionPage.gapcovertile))
            {
                validate.AssertEquals("Gap Cover", baseStep.getText.Text(solutionPage.GapCoverTitle), "Title is Mismatch", true);
                validate.AssertEquals("Sanlam Comprehensive Gap Cover", baseStep.getText.Text(solutionPage.GapCoverSubTitle), "Sub-Title is Mismatch", true);
                validate.AssertEquals("Cover the difference between what your medical scheme pays and the rates charged by medical specialists.", baseStep.getText.Text(solutionPage.GapCoverDescription), "Description is Mismatch", true);

                if (baseStep.IsElementDisplayed(solutionPage.gapcover_findoutmore))
                {
                    baseStep.ScrollToElement(solutionPage.GapCover_FindOutMore);
                    baseStep.Click(solutionPage.GapCover_FindOutMore);

                    var currentWindow = Driver.CurrentWindowHandle;
                    var newWindow = Driver.WindowHandles;
                    Driver.SwitchTo().Window(newWindow[1]);
                    validate.AssertEquals("Sanlam Medical Gap Cover Insurance | Sanlam Health Solutions", Driver.Title, "Page didn't redirected to Medical Gap Cover", true);
                    validate.TakeStepFullScreenShot("Medical Gap Cover", Status.Info);
                    Driver.Close();
                    Driver.SwitchTo().Window(currentWindow);
                    baseStep.wait.WaitTillPageLoad();
                }
                else
                {
                    validate.TakeFullPageScreenShot("Find Out More button is not available for Medical Gap Cover", Status.Info);
                }
            }
            else
            {
                validate.TakeFullPageScreenShot("Medical Gap Cover product tile is not available", Status.Info);
            }
        }

        private void ValidateCreditConsolidationLogsInSql(string Idnumber)
        {
            var externalCommLogs = dbCreditCoach.FetchExternalCommLogTable(Idnumber, 25);
            validate.AssertEqualWithMessage(true, !string.IsNullOrEmpty(externalCommLogs["RequestParam"].ToString()), $"RequestParam is: {externalCommLogs["RequestParam"].ToString()}", true);
            validate.AssertEqualWithMessage(true, !string.IsNullOrEmpty(externalCommLogs["ResponseData"].ToString()), $"ResponseData is: {externalCommLogs["ResponseData"].ToString()}", true);
        }

        private void NavigateToSolutionsPage()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.GenericWait(2000);
            baseStep.Click(solutionPage.SolutionsIcon);
            validate.TakeStepFullScreenShot("Solution Page is Visible", Status.Info);
            baseStep.wait.WaitTillPageLoad();
        }

        private void ValidateLinks()
        {
            IWebElement myElement = solutionPage.GetMoneyField;
            ReadOnlyCollection<IWebElement> links = myElement.FindElements(By.TagName("a"));
            using (HttpClient httpClient = new HttpClient())
            {
                foreach (IWebElement link in links)
                {
                    ValidateSingleLink(link, httpClient);
                }
            }
        }

        private void ValidateSingleLink(IWebElement link, HttpClient httpClient)
        {
            string href = link.GetAttribute("href");
            string linkText = link.Text;

            if (!href.StartsWith("javascript:"))
            {
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(href).Result;
                    int statusCode = (int)response.StatusCode;

                    if (statusCode >= 400)
                    {
                        Report.ChildLog.Log(Status.Info, $"Broken link found: {linkText} ({href})");
                    }
                    Report.ChildLog.Log(Status.Info, $"Button are working fine for {linkText} ({href})");
                    response.Dispose();
                }
                catch (WebException)
                {
                    Report.ChildLog.Log(Status.Info, $"Broken link found: {linkText} ({href})");
                }
            }
        }

        private void UpdateAndVerifySPLStatus(string IdNumber)
        {
            dbCreditCoach.UpdateLesDecision(IdNumber, "Decline");
            string status = dbCreditCoach.VerifySPLLESInformation(IdNumber);
            string jsonDecision = dbCreditCoach.JsonDecisionFromUserSPLQualificationDecision(IdNumber);
            Driver.Navigate().Refresh();
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.GenericWait(3000);
        }

        private void HandleSPLViewOffer(string idNumber)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            do
            {
                try
                {
                    baseStep.ScrollToElement(solutionPage.SplQualifyMsg);
                    string actualSplQualifyMsg = (string)js.ExecuteScript("return arguments[0].textContent;", solutionPage.SplQualifyMsg);
                    ValidateSPLQualifyMessage(actualSplQualifyMsg, idNumber);
                    genericUtils.ScrollTillHalfPage();
                    genericUtils.ScrollAtTopOfThePage();
                    baseStep.ScrollToElement(solutionPage.SplViewOffer);
                    baseStep.wait.GenericWait(2000);
                    js.ExecuteScript("arguments[0].Click();", solutionPage.SplViewOffer);
                }
                catch
                {
                    RefreshAndUpdateSPLDecision(idNumber);
                }
            }
            while (solutionPage.IsElementClickable(solutionPage.splviewoffer));
        }

        private void ProcessSpeakToCoach()
        {
            validate.TakeStepFullScreenShot("SplViewOffer Popup is Visible", Status.Info);
            baseStep.wait.WaitTillPageLoad();
            baseStep.Click(solutionPage.PopUpSplSpeakToCoach);
            HandleCallbackRequest();
        }

        private void HandleCallbackRequest()
        {
            baseStep.wait.WaitTillPageLoad();
            validate.TakeStepFullScreenShot("Free CallBack Request Yes Btn", Status.Info);
            baseStep.Click(solutionPage.PopUpSplSpeakToCoachYesBtn);

            baseStep.wait.WaitTillPageLoad();
            string FreeCallBackRequestTextMsg = baseStep.getText.Text(solutionPage.FreeCallBackRequestTextMsg);
            Assert.That(solutionPage.FreeCallBackRequestTextMsg.Displayed);
            validate.TakeStepFullScreenShot("Free CallBack Request Text Msg", Status.Info);
            Report.ChildLog.Log(Status.Info, " SPL not qualified user request for call back done and text visible " + FreeCallBackRequestTextMsg);
            baseStep.Click(solutionPage.FreeCallBackRequestPopupCutBtn);
        }

        private void UpdateQualifiedUserStatus(string IdNumber)
        {
            baseStep.wait.WaitTillPageLoad();
            dbCreditCoach.UpdateSplQualifiedUser(IdNumber, "Approve");
            Driver.Navigate().Refresh();
            baseStep.wait.GenericWait(5000);
            baseStep.wait.WaitTillPageLoad();
            ValidateQualifiedStatus();
        }

        private void ProcessQualifiedUserViewOffer()
        {
            baseStep.Click(solutionPage.SplViewOffer);
            validate.TakeStepFullScreenShot("SplViewOffer Popup is Visible", Status.Info);

            baseStep.wait.GenericWait(3000);
            baseStep.Click(solutionPage.SplVerified_ViewOffer_ApplyNowBtn);
            baseStep.wait.GenericWait(3000);
            HandleThirdPartyPage(solutionPage.otptext);
        }

        private void NavigateAndUpdateSPLStatus(string Idnumber, string les_decision, bool isQualifiedSPL)
        {
            baseStep.wait.WaitTillPageLoad();
            homePageSteps.IsUserWelcomeTextHomePage();
            dbCreditCoach.UpdateSPLLESInformation(Idnumber, les_decision, isQualifiedSPL);
            baseStep.wait.GenericWait(2000);
            dbCreditCoach.UpdateLesDecision(Idnumber, les_decision);
            RefreshPage();
            NavigateToSolutionsPage();
        }

        private void ScrollToAndValidateQualifier(string Idnumber)
        {
            baseStep.wait.GenericWait(3000);
            baseStep.ScrollToElement(solutionPage.CreditConsolQualifier);
            baseStep.wait.GenericWait(5000);
            string actualQualifier;
            string monthlySaving;
            do
            {
                actualQualifier = baseStep.getText.Text(solutionPage.CreditConsolQualifier).Trim();
                monthlySaving = ClickOnViewOfferCreditConsolidation() ?? "";
            } while (!actualQualifier.Contains(monthlySaving));
            ValidateCreditConsolQualifier(Idnumber, actualQualifier, monthlySaving);
        }

        private void InitiateCallMeBack()
        {
            baseStep.Click(solutionPage.SolutionsIcon);
            baseStep.wait.WaitTillPageLoad();
            do
            {
                genericUtils.ScrollTillFullPage();
                baseStep.wait.GenericWait(2000);
            }
            while (!solutionPage.CallMeBackBtn.Displayed);
        }

        private void ProcessCallMeBackRequest(string IdNumber)
        {
            validate.TakeStepFullScreenShot("CallMeBack Btn", Status.Info);
            baseStep.Click(solutionPage.CallMeBackBtn);
            HandleCallMeBackConfirmation();
            dbCreditCoach.GetCampaignSourceValidate(IdNumber, "Client Solutions Page");
        }

        private void ValidateSPLQualifyMessage(string actualSplQualifyMsg, string IdNumber)
        {
            string status = dbCreditCoach.VerifySPLLESInformation(IdNumber);
            string jsonDecision = dbCreditCoach.JsonDecisionFromUserSPLQualificationDecision(IdNumber);
            string expectedSplQualifyMsg;

            if (status == "qualified" && jsonDecision == "qualified")
            {
                expectedSplQualifyMsg = "You are likely to qualify";
            }
            else
            {
                expectedSplQualifyMsg = "You are not likely to qualify";
            }

            validate.AssertEquals(expectedSplQualifyMsg, actualSplQualifyMsg, "Spl user is not updated successfully", false);
        }

        private void RefreshAndUpdateSPLDecision(string IdNumber)
        {
            dbCreditCoach.UpdateLesDecision(IdNumber, "Decline");
            Driver.Navigate().Refresh();
        }

        private void ValidateQualifiedStatus()
        {
            string actualSplQualifyMsg = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].textContent;", solutionPage.SplQualifyMsg);
            string expectedSplQualifyMsg = "You are likely to qualify";
            validate.AssertEquals(expectedSplQualifyMsg, actualSplQualifyMsg, "Spl user is not updated successfully", false);
        }

        private void HandleThirdPartyPage(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
                wait.Until(Driver => Driver.WindowHandles.Count > 1);
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                bool pageDisplayed = false;
                try
                {
                    // Wait for the element to be displayed
                    validate.wait.WaitForElementVisibilityLongWait(locator, 60);
                    pageDisplayed = wait.Until(Driver => Driver.FindElement(locator).Displayed);
                }
                catch (WebDriverTimeoutException)
                {
                    // Element is not displayed within the wait time, setting to false
                    pageDisplayed = false;
                    validate.TakeStepFullScreenShot("Page is still loading after 20 secs or error occurred", Status.Info);
                }
                validate.AssertEqualWithMessage(true, pageDisplayed, "Third Party Page is visible", false);
                Driver.Close();
                Driver.SwitchTo().Window(Driver.WindowHandles.First());
            }
            catch (Exception e)
            {
                validate.TakeStepFullScreenShot("Third Party Page is visible", Status.Info);
                if (Driver.WindowHandles.Count > 0)
                {
                    Driver.Close();
                    Driver.SwitchTo().Window(Driver.WindowHandles.First());
                }
            }
        }

        private void ValidateSPLQualifier(string Idnumber, bool isQualifiedSPL)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.splqualifymsg, 20);
            string actualQualifier = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].textContent;", solutionPage.SplQualifyMsg);
            string expectedSplQualifier = ReturnExpectedQualifierTextForSPL(Idnumber, isQualifiedSPL);
            validate.AssertEqualWithMessage(expectedSplQualifier, actualQualifier, "Qualifier text as expected", false);
            Report.ChildLog.Log(Status.Info, "SPL Qualifier is visible " + actualQualifier);
        }

        private void ValidateCapfinQualifier(string Idnumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.capfinqualifier, 20);
            baseStep.ScrollToElement(solutionPage.CapfinQualifier);
            string actualQualifier = baseStep.getText.Text(solutionPage.CapfinQualifier);
            string expectedCapfinQualifier = ReturnExpectedQualifierTextForCapfin(Idnumber, solutionPage.CapfinQualifier);
            validate.AssertEqualWithMessage(expectedCapfinQualifier, actualQualifier, "Capfin Qualifier text as expected", false);
            Report.ChildLog.Log(Status.Info, "Capfin Qualifier is visible " + actualQualifier);
        }

        private string ClickOnViewOfferCreditConsolidation()
        {
            genericUtils.ScrollAtTopOfThePage();
            baseStep.ScrollToElement(solutionPage.CreditConsolViewOffer);
            baseStep.ClickByJsExecutor(solutionPage.CreditConsolViewOffer);
            baseStep.wait.WaitTillPageLoad();
            string monthlySaving = null;
            try
            {
                monthlySaving = baseStep.getText.Text(solutionPage.MonthlySavingText_PopUp);
            }

            catch { Console.WriteLine("No monthlysaving text visible, user is not eligible"); }
            baseStep.Click(solutionPage.CloseBtn_PopUp);
            return monthlySaving;
        }

        private void ValidateCreditCardQualifiers(string Idnumber)
        {
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.ccqualifier, 20);
            baseStep.ScrollToElement(solutionPage.CCQualifier);
            string actualQualifier = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].textContent;", solutionPage.CCQualifier);
            string MobiCredQualifier = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].textContent;", solutionPage.MobiCredQualifier);
            string expectedCCQualifier = ReturnExpectedQualifierTextForCC(Idnumber, solutionPage.CCQualifier);
            validate.AssertEquals(expectedCCQualifier, actualQualifier, "CC Qualifier text is not as per expected", false);
            validate.AssertEquals(MobiCredQualifier, actualQualifier, "Mobi Cred Qualifier text is not as per expected", false);
            Report.ChildLog.Log(Status.Info, "Money Saver Credit Card Qualifier is visible " + actualQualifier + " and Mobi Cred Qualifier is visible " + MobiCredQualifier);
        }


        private void ValidateCreditConsolQualifier(string Idnumber, string actualQualifier, string monthlySaving)
        {
            string expectedCreditConsolQualifier = ReturnExpectedQualifierTextForCreditConsolidation(Idnumber, solutionPage.CreditConsolQualifier, monthlySaving);
            validate.AssertEqualWithMessage(expectedCreditConsolQualifier, actualQualifier, "Credit Consolidation Qualifier text is expected", false);
            Report.ChildLog.Log(Status.Info, "CreditConsolidation Qualifier is visible " + actualQualifier);
        }

        private void RefreshPage()
        {
            Driver.Navigate().Refresh();
            baseStep.wait.WaitTillPageLoad();
        }

        private void HandleCallMeBackConfirmation()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(solutionPage.callmebackyesbtn, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(solutionPage.CallMeBackYesBtn);

            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(solutionPage.callmebackpopupsuccessmsg, 60);
            string ccSuccessMsg = baseStep.getText.Text(solutionPage.CallMeBackPopupSuccessMsg);
            Assert.That(solutionPage.CallMeBackPopupSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, "Success Message is Visible with text " + ccSuccessMsg);
            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(solutionPage.CallMeBackPopupCutBtn);
        }

        private void HandleSplDeclineScenario(string IdNumber)
        {
            dbCreditCoach.UpdateSplQualifiedUser(IdNumber, "Decline");
            baseStep.Click(solutionPage.SolutionsIcon);
            baseStep.wait.WaitTillPageLoad();

            Actions actions = new Actions(Driver);
            try
            {
                baseStep.wait.WaitForElementClickableLongWait(solutionPage.splviewoffer, 10);
                actions.Click(solutionPage.SplViewOffer).Perform();
            }
            catch
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("arguments[0].Click();", solutionPage.SplViewOffer);
            }

            ProcessSplDeclineCallBack();
            dbCreditCoach.GetCampaignSourceValidate(IdNumber, "SPL decline");
        }

        private void HandleNormalSplScenario(string IdNumber)
        {
            baseStep.wait.WaitTillPageLoad();
            Actions actions = new Actions(Driver);
            RefreshPage();
            try
            {
                baseStep.wait.WaitForElementClickableLongWait(solutionPage.splspeaktocoach, 10);
                actions.Click(solutionPage.SplSpeakToCoach).Perform();
            }
            catch
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("arguments[0].Click();", solutionPage.SplSpeakToCoach);
            }

            ProcessNormalSplCallBack();
            dbCreditCoach.GetCampaignSourceValidate(IdNumber, "Personal Loan");
        }

        private void ProcessSplDeclineCallBack()
        {
            baseStep.wait.WaitForElementClickableLongWait(solutionPage.viewoffer_speaktocoach, 60);
            baseStep.Click(solutionPage.ViewOffer_SpeakToCoach);

            baseStep.wait.WaitForElementClickableLongWait(solutionPage.popupsplspeaktocoachyesbtn, 60);
            baseStep.Click(solutionPage.PopUpSplSpeakToCoachYesBtn);

            ValidateCallBackSuccess();
        }

        private void ProcessNormalSplCallBack()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(solutionPage.callmebackyesbtn_spldecline_speaktocoach, 60);
            validate.TakeStepFullScreenShot("CallMeBack Popup", Status.Info);
            baseStep.Click(solutionPage.CallMeBackYesBtn_SplDecline_SpeakToCoach);
            ValidateCallBackSuccess();
        }

        private void ValidateCallBackSuccess()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(solutionPage.callmebackpopupsuccessmsg, 60);
            string ccSuccessMsg = baseStep.getText.Text(solutionPage.CallMeBackPopupSuccessMsg);
            Assert.That(solutionPage.CallMeBackPopupSuccessMsg.Displayed);
            Report.ChildLog.Log(Status.Info, "Success Message is Visible with text " + ccSuccessMsg);
            validate.TakeStepFullScreenShot("Success Message", Status.Info);
            baseStep.Click(solutionPage.CallMeBackPopupCutBtn_SplDecline_SpeakToCoach);
        }

        private void ValidateStoreCardsQualifier(string creditCoachScore_Storecard)
        {
            SolutionPage solutionPage = new();
            validate.wait.WaitForElementVisibilityLongWait(solutionPage.trueworthqualifier, 10);
            baseStep.ScrollToElement(solutionPage.TrueworthQualifier);
            string actualTrueworthQualifier = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].textContent;", solutionPage.TrueworthQualifier);
            string actualIdentityQualifier = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].textContent;", solutionPage.IdentityQualifier);
            string expectedStoreCardsQualifier = ReturnExpectedQualifierTextForStoreCard(creditCoachScore_Storecard);
            validate.AssertEqualWithMessage(expectedStoreCardsQualifier, actualTrueworthQualifier, "Trueworth Qualifier text as per expected", false);
            baseStep.ScrollToElement(solutionPage.IdentityQualifier);
            validate.AssertEqualWithMessage(expectedStoreCardsQualifier, actualIdentityQualifier, "Identity Qualifier text as per expected", false);
        }

        private void ValidateSPLExternalCommLog(string Idnumber)
        {
            var externalCommLogs = dbCreditCoach.FetchExternalCommLogTable(Idnumber, 20);
            JObject requestParam = JObject.Parse(externalCommLogs["RequestParam"].ToString());
            string sibsSupplierSourceId = requestParam["SibsSupplierSourceId"].ToString();
            string sibsBranchCode = requestParam["SibsBranchCode"].ToString();
            string branchStaffId = requestParam["BranchStaffId"].ToString();
            string umid = requestParam["Umid"].ToString();
            string otpSubmission = requestParam["OtpSubmission"].ToString();
            validate.AssertEqualWithMessage("11", sibsSupplierSourceId, "SibsSupplierSourceId as per expected", true);
            validate.AssertEqualWithMessage("", sibsBranchCode, "SibsBranchCode as per expected", true);
            validate.AssertEqualWithMessage("", branchStaffId, "BranchStaffId as per expected", true);
            validate.AssertEqualWithMessage("", umid, "Umid as per expected", true);
            validate.AssertEqualWithMessage("", otpSubmission.ToLower(), "OtpSubmission as per expected", true);
        }

        private void ValidateSplTileApplyNowButton()
        {
            baseStep.ScrollToElement(solutionPage.SplApplyNow_Btn);
            baseStep.Click(solutionPage.SplApplyNow_Btn);
            baseStep.wait.GenericWait(5000);
            HandleThirdPartyPage(solutionPage.otptext);
        }

        private void ValidateSplTileViewOfferButton(string lesDecision, bool isSplQualified)
        {
            if (isSplQualified || lesDecision.ToLower() == "approve")
            {
                ProcessQualifiedUserViewOffer();
            }
            else
            {
                baseStep.ScrollToElement(solutionPage.SplViewOffer);
                baseStep.Click(solutionPage.SplViewOffer);
                ProcessSpeakToCoach();
            }
        }

        private void ValidateSplTileSpeakToCoachButton()
        {
            baseStep.Click(solutionPage.SplSpeakToCoach);
            ProcessNormalSplCallBack();
        }

        private void HandleQualifiedSPLUserFlow(string Idnumber, string les_decision, bool isSplQualified)
        {
            ValidateSplTileApplyNowButton();
            ValidateSPLExternalCommLog(Idnumber);
            ValidateSplTileViewOfferButton(les_decision, isSplQualified);
            ValidateSPLExternalCommLog(Idnumber);
        }

        private void HandleNonQualifiedSPLUserFlow(string Idnumber, string les_decision, bool isSplQualified)
        {
            ValidateSplTileViewOfferButton(les_decision, isSplQualified);
            ValidateSPLTileLMS(Idnumber, "SPL decline");
            ValidateSplTileSpeakToCoachButton();
            ValidateSPLTileLMS(Idnumber, "Personal Loan");
        }

        private void HandleCapfinUserFlow()
        {
            ValidateCapfinTileApplyNowButton();
        }

        private void ValidateCapfinTileApplyNowButton()
        {
            baseStep.ScrollToElement(solutionPage.CapfinTileApplyNow_Btn);
            baseStep.Click(solutionPage.CapfinTileApplyNow_Btn);
            baseStep.wait.GenericWait(5000);
            HandleThirdPartyPage(solutionPage.thirdpartycapfin_popupyes_btn);
        }

        private void ValidateCreditCardTileButton()
        {
            baseStep.ScrollToElement(solutionPage.CCTile_FindOutMore_Btn);
            baseStep.Click(solutionPage.CCTile_FindOutMore_Btn);
            baseStep.wait.GenericWait(5000);
            HandleThirdPartyPage(solutionPage.cctile_thirdparty);
        }

        private void ValidateMobiCredTileButton()
        {
            baseStep.ScrollToElement(solutionPage.MobiCred_ApplyNow_Btn);
            baseStep.Click(solutionPage.MobiCred_ApplyNow_Btn);
            baseStep.wait.GenericWait(5000);
            HandleThirdPartyPage(solutionPage.mobicred_thirdparty);
        }

        private void HandleAutoLogoutWhileWaiting(int waitTimeMillis)
        {
            int elapsedTime = 0;
            int checkInterval = 5000; // Check every 5 seconds
            IWebDriver localDriver = Driver;

            Task.Run(() =>
            {
                while (elapsedTime < waitTimeMillis)
                {
                    try
                    {
                        var stayButton = localDriver.FindElement(solutionPage.logoutpopupstay_btn);

                        if (stayButton.Displayed)
                        {
                            stayButton.Click();
                            Report.ChildLog.Log(Status.Info, "Auto-logout popup appeared. Clicked 'Stay' button.");
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("No PopUp Visible");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error while checking for Stay button: {ex.Message}");
                    }

                    baseStep.wait.GenericWait(checkInterval);
                    elapsedTime += checkInterval;
                }
            });

            baseStep.wait.GenericWait(waitTimeMillis);
        }

        private void NavigateToHomePage(HomePage homePage)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.ScrollToElement(homePage.HomePageVisible);
            baseStep.wait.GenericWait(2000);
            baseStep.Click(homePage.HomePageVisible);
            baseStep.wait.WaitTillPageLoad();
            validate.TakeStepFullScreenShot("Home Page is Visible", Status.Info);
        }

        private async Task ValidateCreditConsolidateLogsInExternalCommLog(string idNumber, DateTime currentTimeUtc)
        {
            AzureTables storageBrowser = new AzureTables();
            var tableEntities = await ValidateLogsInExternalCommLog(storageBrowser, idNumber, currentTimeUtc);
            JObject json = JObject.Parse(tableEntities.RequestParam);
            string campaignSource = json["CampaignSource"].ToString();
            validate.AssertEqualWithMessage("Credit Consolidation Solution", campaignSource, "campaignSource is expected", true);
            validate.AssertEqualWithMessage(idNumber, json["IdNumber"].ToString(), "IdNumber is expected", true);
            validate.AssertEqualWithMessage("Sanlam", json["Referrer"].ToString(), "Referrer is expected", true);
            validate.AssertEqualWithMessage(false, string.IsNullOrEmpty(json["Cellphone"].ToString()), $"Cellphone is {json["Cellphone"].ToString()}", true);
            validate.AssertEqualWithMessage(false, string.IsNullOrEmpty(json["FirstName"].ToString()), $"FirstName is {json["FirstName"].ToString()}", true);
        }

        private async Task<StorageBrowserTable> ValidateLogsInExternalCommLog(AzureTables storageBrowser, string idNumber, DateTime currentTimeUtc)
        {
            baseStep.wait.GenericWait(2000);
            baseStep.wait.WaitTillPageLoad();
            var sortedEntities = await storageBrowser.GetExternalCommLogTableEntries(idNumber, currentTimeUtc);
            var getTableEntries = sortedEntities.Where(x => x.LogTypeId == 25).FirstOrDefault();
            Report.ChildLog.Log(Status.Info, "ResponseData present in storage table for ID " + idNumber + " is " + getTableEntries.ResponseData);
            return getTableEntries;
        }

        private string ReturnCreditConsolidationTileQualification(string dbClient, string dbC_Conversion)
        {
            string condition = dbClient.ToLower() + "-" + dbC_Conversion.ToLower();

            switch (condition)
            {
                case "no-yes":
                    return "Qualified";

                case "yes-no":
                case "no-no":
                case "yes-yes":
                    return "Not Qualified";

                default:
                    return "Invalid input. Please enter 'yes' or 'no' only.";
            }
        }

        private void HandleCreditConsolTileCallMeBackBtn()
        {
            baseStep.Click(solutionPage.CreditConsolCallMeBack_Btn);
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(solutionPage.closebtn_popup, 10);
            baseStep.Click(solutionPage.CloseBtn_PopUp);
        }

        private void ValidatePersonalFinance27Qualifier(string Idnumber)
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementVisibilityLongWait(solutionPage.finance27qualifier, 20);
            baseStep.ScrollToElement(solutionPage.Finance27Qualifier);
            string actualQualifier = baseStep.getText.Text(solutionPage.Finance27Qualifier);
            string expectedCapfinQualifier = ReturnExpectedQualifierTextForCapfin(Idnumber, solutionPage.Finance27Qualifier);
            validate.AssertEqualWithMessage(expectedCapfinQualifier.ToLower(), actualQualifier.ToLower(), "Personal Finance 27 Qualifier text as expected", false);
            Report.ChildLog.Log(Status.Info, "Personal Finance 27 Qualifier is visible " + actualQualifier);
        }

        private void ValidatePersonalFinance27TileApplyNowButton()
        {
            baseStep.ScrollToElement(solutionPage.Finance27TileApplyNow_Btn);
            baseStep.Click(solutionPage.Finance27TileApplyNow_Btn);
            baseStep.wait.GenericWait(5000);
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
                wait.Until(Driver => Driver.WindowHandles.Count > 1);
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                string url = Driver.Url;
                validate.AssertEqualWithMessage(true, url.Contains("finance.f27.me"), $"Third Party Page is visible and navigated url: {url}", false);
                Driver.Close();
                Driver.SwitchTo().Window(Driver.WindowHandles.First());
            }
            catch (Exception e)
            {
                validate.TakeStepFullScreenShot("Third Party Page is visible", Status.Info);
                if (Driver.WindowHandles.Count > 0)
                {
                    Driver.Close();
                    Driver.SwitchTo().Window(Driver.WindowHandles.First());
                }
            }
        }

        private void ValidateFinance27ExternalCommLog(string Idnumber)
        {
            var externalCommLogs = dbCreditCoach.FetchExternalCommLogTable(Idnumber, 36);
            validate.AssertEqualWithMessage(true, !string.IsNullOrEmpty(externalCommLogs["RequestParam"].ToString()), $"RequestParam is: {externalCommLogs["RequestParam"].ToString()}", true);
            validate.AssertEqualWithMessage(true, !string.IsNullOrEmpty(externalCommLogs["ResponseData"].ToString()), $"ResponseData is: {externalCommLogs["ResponseData"].ToString()}", true);
        }

        private void HandleStoreCardUserFlow()
        {
            ValidateTrueworthTileApplyNowButton();
            ValidateIdentifyTileApplyNowButton();
        }

        private void ValidateTrueworthTileApplyNowButton()
        {
            baseStep.ScrollToElement(solutionPage.TrueworthApplyNow_Btn);
            baseStep.Click(solutionPage.TrueworthApplyNow_Btn);
            baseStep.wait.GenericWait(5000);
            HandleThirdPartyPage(solutionPage.thirdpartypage);
        }

        private void ValidateIdentifyTileApplyNowButton()
        {
            baseStep.wait.WaitTillPageLoad();
            baseStep.wait.WaitForElementClickableLongWait(solutionPage.identityapplynow_btn, 20);
            baseStep.ScrollToElement(solutionPage.IdentityApplyNow_Btn);
            baseStep.Click(solutionPage.IdentityApplyNow_Btn);
            baseStep.wait.GenericWait(5000);
            HandleThirdPartyPage(solutionPage.thirdpartypage);
        }
        #endregion
    }
}