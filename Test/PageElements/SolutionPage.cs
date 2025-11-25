namespace SanlamAutomation
{
    public class SolutionPage : WebDriverSession
    {
        public By solutionsicon = By.XPath("//a[text()=' Solutions']");
        public IWebElement SolutionsIcon => Driver.FindElement(solutionsicon);

        public By solutionsheading = By.XPath("(//h2[text()='Solutions for You'])[2]");
        public IWebElement SolutionsHeading => Driver.FindElement(solutionsheading);

        #region Credit Section

        public By splviewoffer = By.XPath("//*[@id=\"Sanlam_Personal_loans_View_offer\"]");
        public IWebElement SplViewOffer => Driver.FindElement(splviewoffer);

        public By splviewoffer_qualified = By.XPath("//*[@id=\"Sanlam Personal loans\"]");
        public IWebElement SplViewOffer_Qualified => Driver.FindElement(splviewoffer_qualified);

        public By splspeaktocoach = By.XPath("//*[@id=\"SPLSpeaktoaCoachViewOffer\"]");
        public IWebElement SplSpeakToCoach => Driver.FindElement(splspeaktocoach);

        public By popupsplspeaktocoach = By.XPath("//*[@id=\"SPL_Decline_Speak_to_a_Coach\"]");
        public IWebElement PopUpSplSpeakToCoach => Driver.FindElement(popupsplspeaktocoach);

        public By spl_viewofferpopup_applynowbtn = By.XPath("/html/body/ngb-modal-window/div/div/div[1]/div[2]/div/div[2]/div[2]/div/button");
        public IWebElement Spl_ViewOffePopup_ApplyNowBtn => Driver.FindElement(spl_viewofferpopup_applynowbtn);

        public By viewoffer_speaktocoach = By.XPath("//*[@id=\"SPL_Decline_Speak_to_a_Coach\"]");
        public IWebElement ViewOffer_SpeakToCoach => Driver.FindElement(viewoffer_speaktocoach);

        public By popupsplspeaktocoachyesbtn = By.XPath("//*[@id=\"SPLDeclineYes\"]");
        public IWebElement PopUpSplSpeakToCoachYesBtn => Driver.FindElement(popupsplspeaktocoachyesbtn);

        public By freecallbackrequestyesbtn = By.XPath("//*[@id=\"SPLDeclineYes_New\"]");
        public IWebElement FreeCallBackRequestYesBtn => Driver.FindElement(freecallbackrequestyesbtn);

        public By freecallbackrequesttextmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement FreeCallBackRequestTextMsg => Driver.FindElement(freecallbackrequesttextmsg);

        public By freecallbackrequestpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement FreeCallBackRequestPopupCutBtn => Driver.FindElement(freecallbackrequestpopupcutbtn);

        public By splqualifymsg = By.XPath("//*[contains(text(),'Sanlam Personal Loans')]/parent::div/following-sibling::div/h4");
        public IWebElement SplQualifyMsg => Driver.FindElement(splqualifymsg);

        public By getmoneyfield = By.XPath("//*[contains(text(),'Credit')]/following-sibling::div");
        public IWebElement GetMoneyField => Driver.FindElement(getmoneyfield);

        public By splprequalifier = By.XPath("//li[@id='Personal Loans_Sanlam Personal Loans'] //article/div/div[2]/h4");
        public IWebElement SPLPrequalifier => Driver.FindElement(splprequalifier);

        public By splapplynowbtn = By.XPath("//a[@id='Apply Now SPL Static Offer UpFront']");
        public IWebElement SPLApplyNowBtn => Driver.FindElement(splapplynowbtn);

        public By splcallmebackbtn = By.XPath("(//a[@id='branch EZFlow ApplyNow'])[2]");
        public IWebElement SPLCallMeBackBtn => Driver.FindElement(splcallmebackbtn);

        public By splaccepttermchkbox = By.XPath("//input[@id='AcceptTerms_AutoReg']");
        public IWebElement SPLAcceptTermChkBox => Driver.FindElement(splaccepttermchkbox);

        public By sendotpbtn_spl = By.XPath("//button[@id='Send OTP_btn']");
        public IWebElement SendOtpBtn_SPL => Driver.FindElement(sendotpbtn_spl);

        public By enterotp_spl = By.XPath("//input[@id='otp1']");
        public IWebElement EnterOTP_SPL => Driver.FindElement(enterotp_spl);

        public By submitotp_spl = By.XPath("//button[@id='OTPSubmit_btn']");
        public IWebElement SubmitOTP_SPL => Driver.FindElement(submitotp_spl);

        public By callbackpopupheader_spl = By.XPath("//h4[text()='Expect a call back shortly']");
        public IWebElement CallBackPopupHeader_SPL => Driver.FindElement(callbackpopupheader_spl);

        public By callbackpopupbody_spl = By.XPath("//h4[text()='Expect a call back shortly']/parent::div/following-sibling::div/div");
        public IWebElement CallBackPopupBody_SPL => Driver.FindElement(callbackpopupbody_spl);

        public By callbackpopupclose_spl = By.XPath("//h4[text()='Expect a call back shortly']/following-sibling::button");
        public IWebElement CallBackPopupClose_SPL => Driver.FindElement(callbackpopupclose_spl);

        public By continueapplicationbtn = By.XPath("//a[@id='Continue Application SPL']");
        public IWebElement ContinueApplicationBtn => Driver.FindElement(continueapplicationbtn);

        // spl tile

        public By splapplynow_btn = By.XPath("//a[contains(@id,\"Apply Now SPL Static Offer\")]");
        public IWebElement SplApplyNow_Btn => Driver.FindElement(splapplynow_btn);

        // spl verified pop up

        public By applynowbtn = By.XPath("//*[@id=\"old-spl-popup\"]//button[contains(@id,\"Apply Now SPL Static Offer\")]");
        public IWebElement ApplyNowBtn => Driver.FindElement(applynowbtn);

        public By splverified_viewoffer_applynowbtn = By.XPath("(//*[@id=\"Apply Now SPL Static Offer PopUp\"])[1]");
        public IWebElement SplVerified_ViewOffer_ApplyNowBtn => Driver.FindElement(splverified_viewoffer_applynowbtn);

        // third party page 

        public By otptext = By.XPath("//*[contains(text(),'OTP validation')]");
        public IWebElement OTPText => Driver.FindElement(otptext);

        // credit card tile qualifier

        public By ccqualifier = By.XPath("//*[contains(@id,'Sanlam_Money_Saver_credit_card') or contains(@id,'Sanlam Money Saver credit card')  ]/preceding-sibling::div/h4");
        public IWebElement CCQualifier => Driver.FindElement(ccqualifier);

        public By cctile_findoutmore_btn = By.XPath("//*[@id=\"Sanlam Money Saver credit card\"]");
        public IWebElement CCTile_FindOutMore_Btn => Driver.FindElement(cctile_findoutmore_btn);

        public By cctile_thirdparty = By.XPath("//h4[text()='Sanlam Money Saver Credit Card']");
        public IWebElement CCTile_ThirdParty => Driver.FindElement(cctile_thirdparty);

        // MobiCred tile qualifier

        public By mobicredqualifier = By.XPath("//*[contains(@id,'Mobicred')]/preceding-sibling::div/h4");
        public IWebElement MobiCredQualifier => Driver.FindElement(mobicredqualifier);

        public By mobicred_applynow_btn = By.XPath("//*[@id=\"Mobicred\"]");
        public IWebElement MobiCred_ApplyNow_Btn => Driver.FindElement(mobicred_applynow_btn);

        public By mobicred_thirdparty = By.XPath("//*[contains(text(),' Personal Details ')]");
        public IWebElement MobiCred_ThirdParty => Driver.FindElement(mobicred_thirdparty);

        // Credit Consolidation tile qualifier

        public By creditconsolqualifier = By.XPath("//*[contains(@id,'DebtBusters Credit Consolidation')]/parent::div/preceding-sibling::div/h4");
        public IWebElement CreditConsolQualifier => Driver.FindElement(creditconsolqualifier);

        public By creditconsolviewoffer = By.XPath("//*[@id=\"DebtBusters Credit Consolidation\"]");
        public IWebElement CreditConsolViewOffer => Driver.FindElement(creditconsolviewoffer);

        public By creditconsolspeaktocaoch = By.XPath("//*[@id=\"CreditConsolidationViewOfferDecline_SpeaktoCoach\"]");
        public IWebElement CreditConsolSpeakToCoach => Driver.FindElement(creditconsolspeaktocaoch);

        public By creditconsolsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CreditConsolSuccessMsg => Driver.FindElement(creditconsolsuccessmsg);

        public By creditconsolpopupcutbtn = By.XPath("//*[contains(text(),'Free callback request')]/parent::h2/preceding-sibling::a");
        public IWebElement CreditConsolPopUpCutBtn => Driver.FindElement(creditconsolpopupcutbtn);

        public By monthlysavingtext_popup = By.XPath("//*[contains(text(),'Monthly saving')]/following-sibling::span");
        public IWebElement MonthlySavingText_PopUp => Driver.FindElement(monthlysavingtext_popup);

        public By closebtn_popup = By.XPath("//*[@aria-label=\"Close\" or @class=\"popupclose\"]");
        public IWebElement CloseBtn_PopUp => Driver.FindElement(closebtn_popup);

        public By creditconsolcallmeback_btn = By.XPath("//*[@id=\"btn_CreditConsolidationViewOfferSuccess_Callmeback\"]");
        public IWebElement CreditConsolCallMeBack_Btn => Driver.FindElement(creditconsolcallmeback_btn);

        // capfin tile

        public By capfinqualifier = By.XPath("//*[contains(@data-name,'Apply_now_Capfin')]/preceding-sibling::div//h4");
        public IWebElement CapfinQualifier => Driver.FindElement(capfinqualifier);

        public By capfintileapplynow_btn = By.XPath("//*[@id=\"Capfin Personal Loans\"]");
        public IWebElement CapfinTileApplyNow_Btn => Driver.FindElement(capfintileapplynow_btn);

        public By thirdpartycapfin_popupyes_btn = By.XPath("//*[contains(text(),'YES - LET')]/parent::button");
        public IWebElement ThirdPartyCapfin_PopupYes_Btn => Driver.FindElement(thirdpartycapfin_popupyes_btn);

        // call me back btn

        public By callmebackbtn = By.XPath("//*[@id=\"Call_Me_Back_Header_New_Credit_Management_Coach\"]");
        public IWebElement CallMeBackBtn => Driver.FindElement(callmebackbtn);

        public By callmebackyesbtn = By.XPath("//*[@id=\"SolutionCallBackYes\"]");
        public IWebElement CallMeBackYesBtn => Driver.FindElement(callmebackyesbtn);

        public By callmebackpopupcutbtn = By.XPath("//*[contains(text(),'call me back')]/preceding-sibling::a");
        public IWebElement CallMeBackPopupCutBtn => Driver.FindElement(callmebackpopupcutbtn);

        public By callmebackpopupsuccessmsg = By.XPath("//*[contains(text(), 'Our coach will contact you shortly')]");
        public IWebElement CallMeBackPopupSuccessMsg => Driver.FindElement(callmebackpopupsuccessmsg);

        //speak to a coack when spl is decline      

        public By callmebackyesbtn_spldecline_speaktocoach = By.XPath("//*[@id=\"SPLYes\"]");
        public IWebElement CallMeBackYesBtn_SplDecline_SpeakToCoach => Driver.FindElement(callmebackyesbtn_spldecline_speaktocoach);

        public By callmebackpopupcutbtn_spldecline_speaktocoach = By.XPath("/html/body/ngb-modal-window/div/div/div/header/a");
        public IWebElement CallMeBackPopupCutBtn_SplDecline_SpeakToCoach => Driver.FindElement(callmebackpopupcutbtn_spldecline_speaktocoach);

        // store card

        public By trueworthqualifier = By.XPath("//*[@id='btn_TruworthsVeryHigh_ProductPage']/preceding-sibling::div//h4");
        public IWebElement TrueworthQualifier => Driver.FindElement(trueworthqualifier);

        public By trueworthapplynow_btn = By.XPath("//*[@id=\"btn_TruworthsVeryHigh_ProductPage\"]");
        public IWebElement TrueworthApplyNow_Btn => Driver.FindElement(trueworthapplynow_btn);

        public By thirdpartypage = By.XPath("//*[@id=\"qwe\"]");
        public IWebElement ThirdPartyPage => Driver.FindElement(thirdpartypage);

        public By identityqualifier = By.XPath("//*[@id=\"btn_IdentityVeryHigh_ProductPage\"]/preceding-sibling::div//h4");
        public IWebElement IdentityQualifier => Driver.FindElement(identityqualifier);

        public By identityapplynow_btn = By.XPath("//*[@id=\"btn_IdentityVeryHigh_ProductPage\"]");
        public IWebElement IdentityApplyNow_Btn => Driver.FindElement(identityapplynow_btn);

        // Auto logout popup

        public By logoutpopupstay_btn = By.XPath("//*[contains(text(),'Stay')]");
        public IWebElement LogOutPopupStay_Btn => Driver.FindElement(logoutpopupstay_btn);

        // Finance 27 Short-term Loans

        public By finance27qualifier = By.XPath("//*[@id=\"Finance27 Personal Loans\"]/parent::div/preceding-sibling::div//h4");
        public IWebElement Finance27Qualifier => Driver.FindElement(finance27qualifier);

        public By finance27applynow_btn = By.XPath("//*[@id=\"Finance27 Personal Loans\"]");
        public IWebElement Finance27TileApplyNow_Btn => Driver.FindElement(finance27applynow_btn);

        // Health Tile

        public By healthsection = By.XPath("//h2[text()='Health']");
        public IWebElement HealthSection => Driver.FindElement(healthsection);

        public By medicalschemesolutiontile = By.XPath("//li[@id='Medical Scheme Solution_Affordable Medical Scheme options']");
        public IWebElement MedicalSchemeSolutionTile => Driver.FindElement(medicalschemesolutiontile);

        public By medicalschemesolutiontitle= By.XPath("//li[@id='Medical Scheme Solution_Affordable Medical Scheme options']/article/div/div/h3");
        public IWebElement MedicalSchemeSolutionTitle => Driver.FindElement(medicalschemesolutiontitle);

        public By medicalschemesolutionsubtitle = By.XPath("//li[@id='Medical Scheme Solution_Affordable Medical Scheme options']/article/div/div/h4");
        public IWebElement MedicalSchemeSolutionSubTitle => Driver.FindElement(medicalschemesolutionsubtitle);

        public By medicalschemesolutiondescription = By.XPath("//li[@id='Medical Scheme Solution_Affordable Medical Scheme options']/article/div/div/p");
        public IWebElement MedicalSchemeSolutionDescription => Driver.FindElement(medicalschemesolutiondescription);

        public By medicalschemesolution_findoutmore = By.XPath("//a[@id='Affordable Medical Scheme options']");
        public IWebElement MedicalSchemeSolution_FindOutMore => Driver.FindElement(medicalschemesolution_findoutmore);

        public By primaryhealthinsurancetile = By.XPath("//li[@id='Primary Health Insurance_EssentialMED Health Insurance']");
        public IWebElement PrimaryHealthInsuranceTile => Driver.FindElement(primaryhealthinsurancetile);

        public By primaryhealthinsurancetitle = By.XPath("//li[@id='Primary Health Insurance_EssentialMED Health Insurance']/article/div/div/h3");
        public IWebElement PrimaryHealthInsuranceTitle => Driver.FindElement(primaryhealthinsurancetitle);

        public By primaryhealthinsurancesubtitle = By.XPath("//li[@id='Primary Health Insurance_EssentialMED Health Insurance']/article/div/div/h4");
        public IWebElement PrimaryHealthInsuranceSubTitle => Driver.FindElement(primaryhealthinsurancesubtitle);

        public By primaryhealthinsurancedescription = By.XPath("//li[@id='Primary Health Insurance_EssentialMED Health Insurance']/article/div/div/p");
        public IWebElement PrimaryHealthInsuranceDescription => Driver.FindElement(primaryhealthinsurancedescription);

        public By primaryhealthinsurance_getaquote = By.XPath("//a[@id='EssentialMED Health Insurance']");
        public IWebElement PrimaryHealthInsurance_GetaQuote => Driver.FindElement(primaryhealthinsurance_getaquote);

        public By viewallproducts_health = By.XPath("//a[@id='ViewAllProducts_Health']");
        public IWebElement ViewAllProducts_Health => Driver.FindElement(viewallproducts_health);

        public By gapcovertile = By.XPath("//li[@id='Gap Cover_Sanlam Comprehensive Gap Cover']");
        public IWebElement GapCoverTile => Driver.FindElement(gapcovertile);

        public By gapcovertitle = By.XPath("//li[@id='Gap Cover_Sanlam Comprehensive Gap Cover']/article/div/div/h3");
        public IWebElement GapCoverTitle => Driver.FindElement(gapcovertitle);

        public By gapcoversubtitle = By.XPath("//li[@id='Gap Cover_Sanlam Comprehensive Gap Cover']/article/div/div/h4");
        public IWebElement GapCoverSubTitle => Driver.FindElement(gapcoversubtitle);

        public By gapcoverdescription = By.XPath("//li[@id='Gap Cover_Sanlam Comprehensive Gap Cover']/article/div/div/p");
        public IWebElement GapCoverDescription => Driver.FindElement(gapcoverdescription);

        public By gapcover_findoutmore = By.XPath("//a[@id='Sanlam Comprehensive Gap Cover']");
        public IWebElement GapCover_FindOutMore => Driver.FindElement(gapcover_findoutmore);

        // Save Money Tile

        public By savemoneysection = By.XPath("//h2[text()='Savings']");
        public IWebElement SaveMoenySection => Driver.FindElement(savemoneysection);

        public By viewallproducts_saving = By.XPath("//a[@id='ViewAllProducts_Savings']");
        public IWebElement ViewAllProducts_Saving => Driver.FindElement(viewallproducts_saving);

        public By rewardtile = By.XPath("//li[@id='Rewards_Sanlam Reality']");
        public IWebElement RewardTile => Driver.FindElement(rewardtile);

        public By rewardtitle = By.XPath("//li[@id='Rewards_Sanlam Reality']/article/div/div/h3");
        public IWebElement RewardTitle => Driver.FindElement(rewardtitle);

        public By rewardsubtitle = By.XPath("//li[@id='Rewards_Sanlam Reality']/article/div/div/h4");
        public IWebElement RewardSubTitle => Driver.FindElement(rewardsubtitle);

        public By rewarddescription = By.XPath("//li[@id='Rewards_Sanlam Reality']/article/div/div/p");
        public IWebElement RewardDescription => Driver.FindElement(rewarddescription);

        public By reward_joinnow = By.XPath("//a[@id='Sanlam Reality']");
        public IWebElement Reward_JoinNow => Driver.FindElement(reward_joinnow);

        public By taxfreesavingtile = By.XPath("//li[@id='Tax-free Savings_Save for your long-term goals']");
        public IWebElement TaxFreeSavingTile => Driver.FindElement(taxfreesavingtile);

        public By taxfreesavingtitle = By.XPath("//li[@id='Tax-free Savings_Save for your long-term goals']/article/div/div/h3");
        public IWebElement TaxFreeSavingTitle => Driver.FindElement(taxfreesavingtitle);

        public By taxfreesavingsubtitle = By.XPath("//li[@id='Tax-free Savings_Save for your long-term goals']/article/div/div/h4");
        public IWebElement TaxFreeSavingSubTitle => Driver.FindElement(taxfreesavingsubtitle);

        public By taxfreesavingdescription = By.XPath("//li[@id='Tax-free Savings_Save for your long-term goals']/article/div/div/p");
        public IWebElement TaxFreeSavingDescription => Driver.FindElement(taxfreesavingdescription);

        public By taxfreesaving_findoutmore = By.XPath("//a[@id='Save for your long-term goals']");
        public IWebElement TaxFreeSaving_FindOutMore => Driver.FindElement(taxfreesaving_findoutmore);

        public By investinsharestile = By.XPath("//li[@id='Invest in Shares_EasyEquities']");
        public IWebElement InvestInSharesTile => Driver.FindElement(investinsharestile);

        public By investinsharestitle = By.XPath("//li[@id='Invest in Shares_EasyEquities']/article/div/div/h3");
        public IWebElement InvestInSharesTitle => Driver.FindElement(investinsharestitle);

        public By investinsharessubtitle = By.XPath("//li[@id='Invest in Shares_EasyEquities']/article/div/div/h4");
        public IWebElement InvestInSharesSubTitle => Driver.FindElement(investinsharessubtitle);

        public By investinsharesdescription = By.XPath("//li[@id='Invest in Shares_EasyEquities']/article/div/div/p");
        public IWebElement InvestInSharesDescription => Driver.FindElement(investinsharesdescription);

        public By investinshares_joinnow = By.XPath("//a[@id='Easy Equities Vouchers']");
        public IWebElement InvestInShares_JoinNow => Driver.FindElement(investinshares_joinnow);

        public By retirementplantile = By.XPath("//li[@id='Retirement Plan_Sanlam Retirement Plan']");
        public IWebElement RetirementPlanTile => Driver.FindElement(retirementplantile);

        public By retirementplantitle = By.XPath("//li[@id='Retirement Plan_Sanlam Retirement Plan']/article/div/div/h3");
        public IWebElement RetirementPlanTitle => Driver.FindElement(retirementplantitle);

        public By retirementplansubtitle = By.XPath("//li[@id='Retirement Plan_Sanlam Retirement Plan']/article/div/div/h4");
        public IWebElement RetirementPlanSubTitle => Driver.FindElement(retirementplansubtitle);

        public By retirementplandescription = By.XPath("//li[@id='Retirement Plan_Sanlam Retirement Plan']/article/div/div/p");
        public IWebElement RetirementPlanDescription => Driver.FindElement(retirementplandescription);

        public By retirementplan_findoutmore = By.XPath("//a[@id='Sanlam Retirement Plan']");
        public IWebElement RetirementPlan_FindOutMore => Driver.FindElement(retirementplan_findoutmore);

        public By educationplantile = By.XPath("//li[@id='Education Planning_Sanlam Goal Manager']");
        public IWebElement EducationPlanTile => Driver.FindElement(educationplantile);

        public By educationplantitle = By.XPath("//li[@id='Education Planning_Sanlam Goal Manager']/article/div/div/h3");
        public IWebElement EducationPlanTitle => Driver.FindElement(educationplantitle);

        public By educationplansubtitle = By.XPath("//li[@id='Education Planning_Sanlam Goal Manager']/article/div/div/h4");
        public IWebElement EducationPlanSubTitle => Driver.FindElement(educationplansubtitle);

        public By educationplandescription = By.XPath("//li[@id='Education Planning_Sanlam Goal Manager']/article/div/div/p");
        public IWebElement EducationPlanDescription => Driver.FindElement(educationplandescription);

        public By educationplan_findoutmore = By.XPath("//a[@id='Sanlam Goal Manager']");
        public IWebElement EducationPlan_FindOutMore => Driver.FindElement(educationplan_findoutmore);

        public By unittrusttile = By.XPath("//li[@id='Unit Trusts_Sanlam Smart Invest']");
        public IWebElement UnitTrustTile => Driver.FindElement(unittrusttile);

        public By unittrusttitle = By.XPath("//li[@id='Unit Trusts_Sanlam Smart Invest']/article/div/div/h3");
        public IWebElement UnitTrustTitle => Driver.FindElement(unittrusttitle);

        public By unittrustsubtitle = By.XPath("//li[@id='Unit Trusts_Sanlam Smart Invest']/article/div/div/h4");
        public IWebElement UnitTrustSubTitle => Driver.FindElement(unittrustsubtitle);

        public By unittrustdescription = By.XPath("//li[@id='Unit Trusts_Sanlam Smart Invest']/article/div/div/p");
        public IWebElement UnitTrustDescription => Driver.FindElement(unittrustdescription);

        public By unittrust_findoutmore = By.XPath("//a[@id='Sanlam Smart Invest']");
        public IWebElement UnitTrust_FindOutMore => Driver.FindElement(unittrust_findoutmore);

        // Financial Planning Tile

        public By planningsection = By.XPath("//h2[text()='Planning']");
        public IWebElement PlanningSection => Driver.FindElement(planningsection);

        public By getadvicetile = By.XPath("//li[@id='Get Advice_Find the right financial planner']");
        public IWebElement GetAdviceTile => Driver.FindElement(getadvicetile);

        public By getadvicetitle = By.XPath("//li[@id='Get Advice_Find the right financial planner']/article/div/div/h3");
        public IWebElement GetAdviceTitle => Driver.FindElement(getadvicetitle);

        public By getadvicesubtitle = By.XPath("//li[@id='Get Advice_Find the right financial planner']/article/div/div/h4");
        public IWebElement GetAdviceSubTitle => Driver.FindElement(getadvicesubtitle);

        public By getadvicedescription = By.XPath("//li[@id='Get Advice_Find the right financial planner']/article/div/div/p");
        public IWebElement GetAdviceDescription => Driver.FindElement(getadvicedescription);

        public By getadvice_getintouch = By.XPath("//a[@id='Find the right financial planner']");
        public IWebElement GetAdvice_GetInTouch => Driver.FindElement(getadvice_getintouch);

        public By onlinewilltile = By.XPath("//li[@id='Online Will_Draft your will online in minutes']");
        public IWebElement OnlineWillTile => Driver.FindElement(onlinewilltile);

        public By onlinewilltitle = By.XPath("//li[@id='Online Will_Draft your will online in minutes']/article/div/div/h3");
        public IWebElement OnlineWillTitle => Driver.FindElement(onlinewilltitle);

        public By onlinewillsubtitle = By.XPath("//li[@id='Online Will_Draft your will online in minutes']/article/div/div/h4");
        public IWebElement OnlineWillSubTitle => Driver.FindElement(onlinewillsubtitle);

        public By onlinewilldescription = By.XPath("//li[@id='Online Will_Draft your will online in minutes']/article/div/div/p");
        public IWebElement OnlineWillDescription => Driver.FindElement(onlinewilldescription);

        public By onlinewill_draftonenow = By.XPath("//a[@id='Draft your will online in minutes']");
        public IWebElement OnlineWill_DraftOneNow => Driver.FindElement(onlinewill_draftonenow);
        #endregion

        #region Health Section

        public By viewallproductshealth_btn = By.XPath("//*[@id=\"ViewAllProducts_Health\"]");
        public IWebElement ViewAllProductsHealth_Btn => Driver.FindElement(viewallproductshealth_btn);

        #endregion

        #region Savings Section

        public By viewallproductssavings_btn = By.XPath("//*[@id=\"ViewAllProducts_Savings\"]");
        public IWebElement ViewAllProductsSavings_Btn => Driver.FindElement(viewallproductssavings_btn);

        #endregion

        // OOBA Home Loan

        public By oobahomeloanstile = By.XPath("//li[@id='Home Loans Prequalify_ooba Home Loans']");
        public IWebElement OobaHomeLoansTile => Driver.FindElement(oobahomeloanstile);

        public By oobahomeloantitle = By.XPath("//li[@id='Home Loans Prequalify_ooba Home Loans']/div/article/div/div/h3");
        public IWebElement OobaHomeLoansTitle => Driver.FindElement(oobahomeloantitle);

        public By oobahomeloansubtitle = By.XPath("//li[@id='Home Loans Prequalify_ooba Home Loans']/div/article/div/div/h4");
        public IWebElement OobaHomeLoansSubTitle => Driver.FindElement(oobahomeloansubtitle);

        public By oobahomeloandescription = By.XPath("//li[@id='Home Loans Prequalify_ooba Home Loans']/div/article/div/div/p");
        public IWebElement OobaHomeLoansDescription => Driver.FindElement(oobahomeloandescription);

        public By oobahomeloan_findoutmore = By.XPath("//a[@id='ooba Home Loans']");
        public IWebElement OobaHomeLoans_FindOutMore => Driver.FindElement(oobahomeloan_findoutmore);

        public By oobahomeloan_getprequalified = By.XPath("//a[@id='ooba Home Loans-Apply Now']");
        public IWebElement OobaHomeLoans_GetPrequalified => Driver.FindElement(oobahomeloan_getprequalified);

        public By oobahomeloan_startjourneybtn = By.XPath("//button[@id='OobaHomeLoanStartJourney']");
        public IWebElement OobaHomeLoans_StartJourneyBtn => Driver.FindElement(oobahomeloan_startjourneybtn);

        public By oobahomeloan_continuebtn = By.XPath("//button[@id='OobaHomeLoanRedirect_New']");
        public IWebElement OobaHomeLoans_ContinueBtn => Driver.FindElement(oobahomeloan_continuebtn);

        public By oobahomeloan_speaktocoachbtn = By.XPath("//a[@id='Ooba_Speak_to_a_Coach_New']");
        public IWebElement OobaHomeLoans_SpeakToCoachBtn => Driver.FindElement(oobahomeloan_speaktocoachbtn);

        public By oobahomeloan_speaktocoachpopup = By.XPath("//h2[text()='Speak to a Coach']");
        public IWebElement OobaHomeLoans_SpeakToCoachPopup => Driver.FindElement(oobahomeloan_speaktocoachpopup);

        public By oobahomeloan_speaktocoachpopupclose = By.XPath("//h2[text()='Speak to a Coach']/preceding-sibling::a");
        public IWebElement OobaHomeLoans_SpeakToCoachPopupClose => Driver.FindElement(oobahomeloan_speaktocoachpopupclose);

        public By oobahomeloan_errormessage = By.XPath("//div[text()=' Error: Please try after some time ']");
        public IWebElement OobaHomeLoans_ErrorMessage => Driver.FindElement(oobahomeloan_errormessage);

        public By oobahomeloanqualifier = By.XPath("(//*[contains(@id,'ooba Home Loans')]/div/article/div/div/div/h4)[1]");
        public IWebElement OOBAHomeLoanQualifier => Driver.FindElement(oobahomeloanqualifier);

        // OOBA Home Loan Advance

        public By oobahomeloansadvancetile = By.XPath("//li[@id='Home Loan Advance_ooba Home Loans Advance']");
        public IWebElement OobaHomeLoansAdvanceTile => Driver.FindElement(oobahomeloansadvancetile);

        public By oobahomeloanadvancetitle = By.XPath("//li[@id='Home Loan Advance_ooba Home Loans Advance']/div/article/div/div/h3");
        public IWebElement OobaHomeLoansAdvanceTitle => Driver.FindElement(oobahomeloanadvancetitle);

        public By oobahomeloanadvancesubtitle = By.XPath("//li[@id='Home Loan Advance_ooba Home Loans Advance']/div/article/div/div/h4");
        public IWebElement OobaHomeLoansAdvanceSubTitle => Driver.FindElement(oobahomeloanadvancesubtitle);

        public By oobahomeloanadvancedescription = By.XPath("//li[@id='Home Loan Advance_ooba Home Loans Advance']/div/article/div/div/p");
        public IWebElement OobaHomeLoansAdvanceDescription => Driver.FindElement(oobahomeloanadvancedescription);

        public By oobahomeloanadvance_findoutmore = By.XPath("//a[@id='ooba Home Loans Advance']");
        public IWebElement OobaHomeLoansAdvance_FindOutMore => Driver.FindElement(oobahomeloanadvance_findoutmore);

        public By oobahomeloanadvance_callmebtn = By.XPath("//button[@id='ooba_Home_Loans_Please_Call_me']");
        public IWebElement OobaHomeLoansAdvance_CallMeBtn => Driver.FindElement(oobahomeloanadvance_callmebtn);

        public By oobahomeloanadvance_callbackmsg = By.XPath("//div[text()=' Thank you – ooba Home Loans will be in contact with you shortly ']");
        public IWebElement OobaHomeLoansAdvance_CallBackMsg => Driver.FindElement(oobahomeloanadvance_callbackmsg);

        public By oobahomeloanadvance_popupclose = By.XPath("//h4[text()='See if you qualify']/following-sibling::button");
        public IWebElement OobaHomeLoansAdvance_PopupClose => Driver.FindElement(oobahomeloanadvance_popupclose);

        public By oobahomeloanadvance_errormessage = By.XPath("//div[text()=' Error: Please try after some time ']");
        public IWebElement OobaHomeLoansAdvance_ErrorMessage => Driver.FindElement(oobahomeloanadvance_errormessage);

        public By oobahomeloanadvancequalifier = By.XPath("//*[contains(@id,'ooba Home Loans Advance')]/div/article/div/div/div/h4");
        public IWebElement OOBAHomeLoanAdvanceQualifier => Driver.FindElement(oobahomeloanadvancequalifier);
        public bool IsElementClickable(By locator)
        {
            BaseStep baseStep = new BaseStep();
            try 
            {
                baseStep.wait.WaitForElementClickableLongWait(locator,10); 
                return true;
            }
            catch { return false; }
        }
    }
}