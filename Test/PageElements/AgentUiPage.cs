namespace SanlamAutomation.Test.Pages
{
    public class AgentUiPage : WebDriverSession
    {

        public By signin = By.XPath("//*[@id=\"i0116\"]");
        public IWebElement SignIn => Driver.FindElement(signin);

        public By nextbtn = By.XPath("//*[@id=\"idSIButton9\"]");
        public IWebElement NextBtn => Driver.FindElement(nextbtn);

        public By enterpassword = By.XPath("//*[@id=\"i0118\"]");
        public IWebElement EnterPassword => Driver.FindElement(enterpassword);

        public By staysigninnobtn = By.XPath("//*[@id=\"idBtn_Back\"]");
        public IWebElement StaySignInNoBtn => Driver.FindElement(staysigninnobtn);

        public By searchpanel = By.XPath("//*[@id=\"serchpanel\"]/div/form/div/input");
        public IWebElement SearchPanel => Driver.FindElement(searchpanel);

        public By searchbtn = By.XPath("//*[@id=\"Capa_1\"]");
        public IWebElement SearchBtn => Driver.FindElement(searchbtn);

        public By activatebtn = By.XPath("//*[@id=\"collapseOne\"]/div/form/div[4]/button[2]");
        public IWebElement ActivateBtn => Driver.FindElement(activatebtn);

        //Manual Verification Done 

        public By activatereasondrop = By.XPath("//*[@id=\"accordionExample\"]/div[6]/button[2]");
        public IWebElement ActivateReasonDrop => Driver.FindElement(activatereasondrop);

        public By activatereasondropoption = By.XPath("/html/body/ngb-modal-window/div/div/div[2]/app-audit-log/form/div[2]/div/div[1]/select/option[1]");
        public IWebElement ActivateReasonDropOption => Driver.FindElement(activatereasondropoption);

        public By activatereasondropsavebtn = By.XPath("/html/body/ngb-modal-window/div/div/div[2]/app-audit-log/form/div[4]/button");
        public IWebElement ActivateReasonDropSaveBtn => Driver.FindElement(activatereasondropsavebtn);

        public By successmsg = By.XPath("//*[@id=\"collapseOne\"]/div/div/div");
        public IWebElement SuccessMsg => Driver.FindElement(successmsg);

        public By updatecreditreport = By.XPath("//*[@id=\"accordionExample\"]/div[6]/button[2]");
        public IWebElement UpdateCreditReport => Driver.FindElement(updatecreditreport);

        public By bureaupopupyesbtn = By.XPath("/html/body/ngb-modal-window/div/div/div[3]/div/button[1]");
        public IWebElement BureauPopupYesBtn => Driver.FindElement(bureaupopupyesbtn);

        //customer Dashboard- Solution Page

        public By customerdashboard = By.XPath("//*[@id=\"bind-to-dashboard\"]");
        public IWebElement CustomerDashboard => Driver.FindElement(customerdashboard);

        public By solutiontab = By.CssSelector("#solution-tab");
        public IWebElement SolutionTab => Driver.FindElement(solutiontab);

        public By splqualifymsg = By.XPath("//*[text()='Personal Loans']/parent::div/following-sibling::div[2]/h4");
        public IWebElement SplQualifyMsg => Driver.FindElement(splqualifymsg);

        public By ccqualifier = By.XPath("//*[text()='Money Saver Credit Card']/following-sibling::div/h4");
        public IWebElement CCQualifier => Driver.FindElement(ccqualifier);

        public By creditconsolqualifier = By.XPath("//*[text()='Credit Consolidation']/following-sibling::h4");
        public IWebElement CreditConsolQualifier => Driver.FindElement(creditconsolqualifier);

        public By mobicredqualifier = By.XPath("//*[text()='Mobicred Account']/following-sibling::div");
        public IWebElement MobiCredQualifier => Driver.FindElement(mobicredqualifier);

        public By capfinqualifier = By.XPath("//*[text()='Capfin Personal Loans']/parent::div/div");
        public IWebElement CapfinQualifier => Driver.FindElement(capfinqualifier);

        public By creditconsolviewoffer = By.XPath("(//*[@id=\"DebtBusters Credit Consolidation\"])[2]");
        public IWebElement CreditConsolViewOffer => Driver.FindElement(creditconsolviewoffer);

        public By monthlysavingtext = By.XPath("//*[contains(text(),'Estimated Monthly Savings')]/following-sibling::p");
        public IWebElement MonthlySavingText => Driver.FindElement(monthlysavingtext);

        public By closebtn_popup = By.XPath("//*[@class=\"popupclose\"]");
        public IWebElement CloseBtn_PopUp => Driver.FindElement(closebtn_popup);

        public By finance27qualifier = By.XPath("//*[contains(text(),'Finance 27 Short-term Loans')]/following-sibling::div");
        public IWebElement Finance27Qualifier => Driver.FindElement(finance27qualifier);

        public By trueworthqualifier = By.XPath("//*[contains(text(),'Truworths Store Account')]/following-sibling::div");
        public IWebElement TrueworthQualifier => Driver.FindElement(trueworthqualifier);

        public By identityqualifier = By.XPath("//*[contains(text(),'Identity Account Card')]/following-sibling::div");
        public IWebElement IdentityQualifier => Driver.FindElement(identityqualifier);

        public By paneltogle = By.XPath("//*[@class='panel-toogl']");
        public IWebElement PanelTogle => Driver.FindElement(paneltogle);

        //customer Dashboard- Home Page

        public By hometab = By.XPath("//*[@id=\"landing-tab\"]");
        public IWebElement HomeTab => Driver.FindElement(hometab);

        #region Dashboard Ui

        public By customerdashboard_btn = By.XPath("//*[contains(text(),'Customer Dashboard')]");
        public IWebElement CustomerDashboard_Btn => Driver.FindElement(customerdashboard_btn);

        public By paneltogle_btn = By.XPath("//*[contains(@class,'panel-toogl')]");
        public IWebElement PanelTogle_Btn => Driver.FindElement(paneltogle_btn);

        public By dashboardtab = By.XPath("//a[@id='dashbord-tab']");
        public IWebElement DashboardTab => Driver.FindElement(dashboardtab);

        public By creditscore = By.XPath("//*[contains(text(),'Credit Score')]/following-sibling::span");
        public IWebElement CreditScore => Driver.FindElement(creditscore);

        public By reportdate = By.XPath("//*[contains(text(),'Report Date')]/following-sibling::span");
        public IWebElement ReportDate => Driver.FindElement(reportdate);

        public By yourscoreis = By.XPath("//*[contains(text(),'Your score is')]/following-sibling::span");
        public IWebElement YourScoreIs => Driver.FindElement(yourscoreis);

        public By updatedon = By.XPath("//*[contains(text(),'Updated on')]/following-sibling::span");
        public IWebElement UpdatedOn => Driver.FindElement(updatedon);

        public By nextupdatein = By.XPath("//*[contains(text(),'Next update in')]/following-sibling::span");
        public IWebElement NextUpdateIn => Driver.FindElement(nextupdatein);

        public By vehiclefinancescore = By.XPath("//*[contains(text(),'Vehicle Finance Score')]/following-sibling::span[1]");
        public IWebElement VehicleFinanceScore => Driver.FindElement(vehiclefinancescore);

        public By vehiclefinancescorestatus = By.XPath("//*[contains(text(),'Vehicle Finance Score')]/following-sibling::span[2]");
        public IWebElement VehicleFinanceScoreStatus => Driver.FindElement(vehiclefinancescorestatus);

        #endregion Dashboard Ui

        #region Update Credit Report

        public By updatecreditreport_button = By.XPath("//*[contains(text(),'Update Credit Report')]");
        public IWebElement UpdateCreditReport_Button => Driver.FindElement(updatecreditreport_button);

        public By bureaucallpopup_yesbutton = By.XPath("//*[contains(text(),'Yes')]");
        public IWebElement BureauCallPopup_YesButton => Driver.FindElement(bureaucallpopup_yesbutton);

        public By bureaucallpopup_cutbutton = By.XPath("//*[contains(text(),'Bureau Call Information')]/following-sibling::button");
        public IWebElement BureauCallPopup_CutButton => Driver.FindElement(bureaucallpopup_cutbutton);

        #endregion Update Credit Report

        //communication logs - Menu

        public By commlogs_expand = By.XPath("//*[contains(text(),'Communication Logs')]");
        public IWebElement CommLogs_Expand => Driver.FindElement(commlogs_expand);

        public By logdetails_button = By.XPath("//*[contains(text(),'Log Details')]");
        public IWebElement LogDetails_Button => Driver.FindElement(logdetails_button);

        public By logdetails_table = By.XPath("//tbody");
        public IWebElement LogDetails_Table => Driver.FindElement(logdetails_table);

        public By commlog_tablerow = By.XPath("//table/tbody/tr");
        public IList<IWebElement> CommLog_TableRow => Driver.FindElements(commlog_tablerow);

        public By commlogtable_date(int index) => By.XPath($"//table/tbody/tr[{index}]/th");
        public IWebElement CommLogTable_Date(int index) => Driver.FindElement(commlogtable_date(index));

        public By commlogtable_type(int index) => By.XPath($"//table/tbody/tr[{index}]/td[1]");
        public IWebElement CommLogTable_Type(int index) => Driver.FindElement(commlogtable_type(index));

        public By commlogtable_outcome(int index) => By.XPath($"//table/tbody/tr[{index}]/td[3]");
        public IWebElement CommLogTable_Outcome(int index) => Driver.FindElement(commlogtable_outcome(index));

        public By commlogtable_transfer(int index) => By.XPath($"//table/tbody/tr[{index}]/td[4]");
        public IWebElement CommLogTable_Transfer(int index) => Driver.FindElement(commlogtable_transfer(index));

        public By commlogtable_createdby(int index) => By.XPath($"//table/tbody/tr[{index}]/td[5]");
        public IWebElement CommLogTable_CreatedBy(int index) => Driver.FindElement(commlogtable_createdby(index));

        #region Communication Logs

        public By commlogs_dropdown = By.XPath("//*[contains(text(),'Communication Log')]");
        public IWebElement CommLogs_Dropdown => Driver.FindElement(commlogs_dropdown);

        public By lognew_btn = By.XPath("//*[contains(text(),'Log New')]");
        public IWebElement LogNew_Btn => Driver.FindElement(lognew_btn);

        public By logdetails_btn = By.XPath("//*[contains(text(),'Log Details')]");
        public IWebElement LogDetails_Btn => Driver.FindElement(logdetails_btn);

        public By lognewtype_dropdown = By.XPath("//*[contains(text(),'Type')]/preceding-sibling::div/select");
        public IWebElement LogNewType_Dropdown => Driver.FindElement(lognewtype_dropdown);

        public By lognewreason_dropdown = By.XPath("//*[contains(text(),'Reason')]/preceding-sibling::div/select");
        public IWebElement LogNewReason_Dropdown => Driver.FindElement(lognewreason_dropdown);

        public By lognewoutcome_textbox = By.XPath("//*[contains(text(),'Outcome')]/preceding-sibling::input");
        public IWebElement LogNewOutcome_TextBox => Driver.FindElement(lognewoutcome_textbox);

        public By lognewtransfer_dropdown = By.XPath("//*[contains(text(),'Transfer')]/preceding-sibling::div/select");
        public IWebElement LogNewTransfer_Dropdown => Driver.FindElement(lognewtransfer_dropdown);

        public By lognewsave_btn = By.XPath("//button[contains(text(),'Save')]");
        public IWebElement LogNewSave_Btn => Driver.FindElement(lognewsave_btn);

        public By lognewsuccessmsg = By.XPath("//*[contains(text(),'Log Added Successfully.')]");
        public IWebElement LogNewSuccessMsg => Driver.FindElement(lognewsuccessmsg);

        public By lognewcut_btn = By.XPath("//h5[contains(text(),'Log New')]/following-sibling::button");
        public IWebElement LogNewCut_Btn => Driver.FindElement(lognewcut_btn);

        public By logdetailstable_rows = By.XPath("//tbody/tr");
        public IList<IWebElement> LogDetailsTable_Rows => Driver.FindElements(logdetailstable_rows);
        public By logdetailtype(int i) => By.XPath($"//tbody/tr[{i}]/td[1]");
        public IWebElement LogDetailsType(int i) => Driver.FindElement(logdetailtype(i));
        public By logdetailreason(int i) => By.XPath($"//tbody/tr[{i}]/td[2]");
        public IWebElement LogDetailsReason(int i) => Driver.FindElement(logdetailreason(i));
        public By logdetailoutcome(int i) => By.XPath($"//tbody/tr[{i}]/td[3]");
        public IWebElement LogDetailsOutcome(int i) => Driver.FindElement(logdetailoutcome(i));
        public By logdetailtransfer(int i) => By.XPath($"//tbody/tr[{i}]/td[4]");
        public IWebElement LogDetailsTransfer(int i) => Driver.FindElement(logdetailtransfer(i));
        public By logdetaildate(int i) => By.XPath($"//tbody/tr[{i}]/th");
        public IWebElement LogDetailsDate(int i) => Driver.FindElement(logdetaildate(i));
        public By communicationlogbtn => By.XPath("//button[text()='Communication Logs']");
        public IWebElement CommunicationLogBtn => Driver.FindElement(communicationlogbtn);
        public By communicationlogsection_header => By.XPath("(//*[contains(text(),'Communication Log')])[3]");
        public IWebElement CommunicationLogSection_Header => Driver.FindElement(communicationlogsection_header);
        public By calanderbtn_fromdate => By.XPath("//input[@id='fromDate']/following-sibling::div/button");
        public IWebElement CalanderBtn_FromDate => Driver.FindElement(calanderbtn_fromdate);
        public By calanderbtn_todate => By.XPath("//input[@id='toDate']/following-sibling::div/button");
        public IWebElement CalanderBtn_ToDate => Driver.FindElement(calanderbtn_todate);
        public By calanderdates => By.XPath("//div[@class='btn-light ng-star-inserted']");
        public IList<IWebElement> CalanderDates => Driver.FindElements(calanderdates);
        public By calanderinputfield => By.XPath("//input[@id='fromDate']");
        public IWebElement CalanderInputField => Driver.FindElement(calanderinputfield);

        #endregion Communication Logs

        #region User Customization Journey

        public By ujc_button = By.XPath("//button[text()='User Journey Customization']");
        public IWebElement UJC_Button => Driver.FindElement(ujc_button);

        public By ujc_manageuserjourneybutton = By.XPath("//button[text()=' Manage User Journeys/Campaigns ']");
        public IWebElement UJC_ManageUserJourneyButton => Driver.FindElement(ujc_manageuserjourneybutton);

        public By ujc_manageuserjourneylink = By.XPath("//a[text()='Manage User Journeys/Campaigns']");
        public IWebElement UJC_ManageUserJourneyLink => Driver.FindElement(ujc_manageuserjourneylink);

        public By ujc_registrationcampaigntype = By.XPath("//input[@value='registration']");
        public IWebElement UJC_RegistrationCampaignType => Driver.FindElement(ujc_registrationcampaigntype);

        public By ujc_logincampaigntype = By.XPath("//input[@value='login']");
        public IWebElement UJC_LoginCampaignType => Driver.FindElement(ujc_logincampaigntype);

        public By ujc_campaignname = By.XPath("//input[@id='campaignName']");
        public IWebElement UJC_CampaignName => Driver.FindElement(ujc_campaignname);

        public By ujc_campaignutmid = By.XPath("//input[@id='campaignUtmId']");
        public IWebElement UJC_CampaignUtmId => Driver.FindElement(ujc_campaignutmid);

        public By ujc_campaignbegindatecalanderbutton = By.XPath("//input[@id='beginDate']/following-sibling::div/button");
        public IWebElement UJC_CampaignBeginDateCalanderButton => Driver.FindElement(ujc_campaignbegindatecalanderbutton);

        public By ujc_campaignenddatecalanderbutton = By.XPath("//input[@id='endDate']/following-sibling::div/button");
        public IWebElement UJC_CampaignEndDateCalanderButton => Driver.FindElement(ujc_campaignenddatecalanderbutton);

        public By ujc_calanderpopup = By.XPath("//div[@class='ngb-dp-week ngb-dp-weekdays ng-star-inserted']");
        public IWebElement UJC_CalanderPopup => Driver.FindElement(ujc_calanderpopup);

        public By ujc_calandercurrentdate = By.XPath("//div[@class='ngb-dp-day ngb-dp-today ng-star-inserted']");
        public IWebElement UJC_CalanderCurrentDate => Driver.FindElement(ujc_calandercurrentdate);

        public By ujc_calanderdates = By.XPath("//div[@class='btn-light ng-star-inserted']");
        public IList<IWebElement> UJC_CalanderDates => Driver.FindElements(ujc_calanderdates);

        public By ujc_campaignwithidnumber = By.XPath("//input[@value='campaignwithid']");
        public IWebElement UJC_CampaignWithIdNumber => Driver.FindElement(ujc_campaignwithidnumber);

        public By ujc_campaignwithoutidnumber = By.XPath("//input[@value='campaignwithoutid']");
        public IWebElement UJC_CampaignWithoutIdNumber => Driver.FindElement(ujc_campaignwithoutidnumber);

        public By ujc_primaryheading = By.XPath("//input[@id='primaryHeading']");
        public IWebElement UJC_PrimaryHeading => Driver.FindElement(ujc_primaryheading);

        public By ujc_contenttextbox = By.XPath("//body[@class='cke_editable cke_editable_themed cke_contents_ltr cke_show_borders']");
        public IList<IWebElement> UJC_ContentTextbox => Driver.FindElements(ujc_contenttextbox);

        public By ujc_campaignbuttonname = By.XPath("//input[@id='campaignButtonName']");
        public IWebElement UJC_CampaignButtonName => Driver.FindElement(ujc_campaignbuttonname);

        public By ujc_campaignlandingpageurl = By.XPath("(//select[@placeholder='Select Options'])[1]");
        public IWebElement UJC_CampaignLandingPageUrl => Driver.FindElement(ujc_campaignlandingpageurl);

        public By ujc_campaignfeatureproduct = By.XPath("(//select[@placeholder='Select Options'])[2]");
        public IWebElement UJC_CampaignFeatureProduct => Driver.FindElement(ujc_campaignfeatureproduct);

        public By ujc_secondaryheading = By.XPath("//input[@id='secondaryHeading']");
        public IWebElement UJC_SecondaryHeading => Driver.FindElement(ujc_secondaryheading);

        public By ujc_imageurl = By.XPath("//input[@id='imageUrl']");
        public IWebElement UJC_ImageUrl => Driver.FindElement(ujc_imageurl);

        public By ujc_publish = By.XPath("(//button[text()='Publish'])[1]");
        public IWebElement UJC_Publish => Driver.FindElement(ujc_publish);

        public By ujc_campaignsuccessmessage = By.XPath("//div[@class='alert alert-success ng-star-inserted']");
        public IWebElement UJC_CampaignSuccessMessage => Driver.FindElement(ujc_campaignsuccessmessage);

        public By ujc_overviewtab = By.XPath("//a[@id='overview-tab']");
        public IWebElement UJC_OverviewTab => Driver.FindElement(ujc_overviewtab);

        public By ujc_registrationcampaigndetails = By.XPath("//h2[text()=' Registration Campaign Details ' and @aria-expanded='false']");
        public IWebElement UJC_RegistrationCampaignDetails => Driver.FindElement(ujc_registrationcampaigndetails);

        public By ujc_registrationdatalist = By.XPath("//table/tbody/tr");
        public IList<IWebElement> UJC_RegistrationDataList => Driver.FindElements(ujc_registrationdatalist);
        public By ujc_registrationdata_campaignname(int index) => By.XPath($"(//table/tbody/tr/td[2])[{index}]");
        public IWebElement UJC_RegistrationData_CampaignName(int index) => Driver.FindElement(ujc_registrationdata_campaignname(index));
        public By ujc_registrationdata_campaignutmid(int index) => By.XPath($"(//table/tbody/tr/td[3])[{index}]");
        public IWebElement UJC_RegistrationData_CampaignUtmId(int index) => Driver.FindElement(ujc_registrationdata_campaignutmid(index));
        public By ujc_registrationdata_begindate(int index) => By.XPath($"(//table/tbody/tr/td[5])[{index}]");
        public IWebElement UJC_RegistrationData_BeginDate(int index) => Driver.FindElement(ujc_registrationdata_begindate(index));
        public By ujc_registrationdata_enddate(int index) => By.XPath($"(//table/tbody/tr/td[6])[{index}]");
        public IWebElement UJC_RegistrationData_EndDate(int index) => Driver.FindElement(ujc_registrationdata_enddate(index));
        public By ujc_registrationdata_primaryheading(int index) => By.XPath($"(//table/tbody/tr/td[7])[{index}]");
        public IWebElement UJC_RegistrationData_PrimaryHeading(int index) => Driver.FindElement(ujc_registrationdata_primaryheading(index));
        public By ujc_registrationdata_primaryheadingsecondpage(int index) => By.XPath($"(//table/tbody/tr/td[12])[{index}]");
        public IWebElement UJC_RegistrationData_PrimaryHeadingSecondPage(int index) => Driver.FindElement(ujc_registrationdata_primaryheadingsecondpage(index));
        public By ujc_registrationdata_primarycontent(int index) => By.XPath($"(//table/tbody/tr/td[8])[{index}]");
        public IWebElement UJC_RegistrationData_PrimaryContent(int index) => Driver.FindElement(ujc_registrationdata_primarycontent(index));
        public By ujc_registrationdata_primarycontentsecondpage(int index) => By.XPath($"(//table/tbody/tr/td[13])[{index}]");
        public IWebElement UJC_RegistrationData_PrimaryContentSecondPage(int index) => Driver.FindElement(ujc_registrationdata_primarycontentsecondpage(index));
        public By ujc_registrationdata_secondaryheading(int index) => By.XPath($"(//table/tbody/tr/td[9])[{index}]");
        public IWebElement UJC_RegistrationData_SecondaryHeading(int index) => Driver.FindElement(ujc_registrationdata_secondaryheading(index));
        public By ujc_registrationdata_secondarycontent(int index) => By.XPath($"(//table/tbody/tr/td[10])[{index}]");
        public IWebElement UJC_RegistrationData_SecondaryContent(int index) => Driver.FindElement(ujc_registrationdata_secondarycontent(index));
        public By ujc_registrationdata_campaignbuttonname(int index) => By.XPath($"(//table/tbody/tr/td[11])[{index}]");
        public IWebElement UJC_RegistrationData_CampaignButtonName(int index) => Driver.FindElement(ujc_registrationdata_campaignbuttonname(index));
        public By ujc_registrationdata_campaignsecondbuttonname(int index) => By.XPath($"(//table/tbody/tr/td[14])[{index}]");
        public IWebElement UJC_RegistrationData_CampaignSecondButtonName(int index) => Driver.FindElement(ujc_registrationdata_campaignsecondbuttonname(index));
        public By ujc_registrationdata_landingpageurl(int index) => By.XPath($"(//table/tbody/tr/td[15])[{index}]");
        public IWebElement UJC_RegistrationData_LandingPageUrl(int index) => Driver.FindElement(ujc_registrationdata_landingpageurl(index));
        public By ujc_registrationdata_preview(int index) => By.XPath($"(//table/tbody/tr/td[21]/a)[{index}]");
        public IWebElement UJC_RegistrationData_Preview(int index) => Driver.FindElement(ujc_registrationdata_preview(index));
        public By ujc_preview_primaryheader => By.XPath("//div[@class='form-group fullwidth previewMessage']/h2");
        public IWebElement UJC_Preview_PrimaryHeader => Driver.FindElement(ujc_preview_primaryheader);
        public By ujc_preview_primarycontent => By.XPath("//div[@class='form-group fullwidth previewMessage']/p");
        public IWebElement UJC_Preview_PrimaryContent => Driver.FindElement(ujc_preview_primarycontent);
        public By ujc_preview_firstbuttonname => By.XPath("//div[@class='form-group fullwidth previewMessage']/div/div/button");
        public IWebElement UJC_Preview_FirstButtonName => Driver.FindElement(ujc_preview_firstbuttonname);
        public By ujc_preview_inputfield => By.XPath("//div[@class='form-group fullwidth previewMessage']/div[1]/div[1]/div/input");
        public IWebElement UJC_Preview_InputField => Driver.FindElement(ujc_preview_inputfield);
        public By ujc_preview_secondaryheader => By.XPath("//div[@class='form-group fullwidth previewMessage']/div[2]/div/div/h2");
        public IWebElement UJC_Preview_SecondaryHeader => Driver.FindElement(ujc_preview_secondaryheader);
        public By ujc_preview_secondarycontent => By.XPath("//div[@class='form-group fullwidth previewMessage']/div[2]/div/div/p");
        public IWebElement UJC_Preview_SecondaryContent => Driver.FindElement(ujc_preview_secondarycontent);
        public By ujc_preview_close => By.XPath("//button[@class='close']");
        public IWebElement UJC_Preview_Close => Driver.FindElement(ujc_preview_close);
        public By ujc_registrationdata_update(int index) => By.XPath($"(//table/tbody/tr/td[18]/a)[{index}]");
        public IWebElement UJC_RegistrationData_Update(int index) => Driver.FindElement(ujc_registrationdata_update(index));
        public By ujc_deletedata_update(int index) => By.XPath($"(//table/tbody/tr/td[18]/a)[{index}]");
        public IWebElement UJC_DeleteData_Update(int index) => Driver.FindElement(ujc_deletedata_update(index));

        public By ujc_update_campaignbegindatecalanderbutton = By.XPath("(//input[@id='beginDate']/following-sibling::div/button)[2]");
        public IWebElement UJC_Update_CampaignBeginDateCalanderButton => Driver.FindElement(ujc_update_campaignbegindatecalanderbutton);

        public By ujc_update_campaignenddatecalanderbutton = By.XPath("(//input[@id='endDate']/following-sibling::div/button)[2]");
        public IWebElement UJC_Update_CampaignEndDateCalanderButton => Driver.FindElement(ujc_update_campaignenddatecalanderbutton);

        public By ujc_update_campaignname = By.XPath("(//input[@id='campaignName'])[2]");
        public IWebElement UJC_Update_CampaignName => Driver.FindElement(ujc_update_campaignname);

        public By ujc_update_primaryheading = By.XPath("(//input[@id='primaryHeading'])[2]");
        public IWebElement UJC_Update_PrimaryHeading => Driver.FindElement(ujc_update_primaryheading);

        public By ujc_update_secondaryheading = By.XPath("(//input[@id='secondaryHeading'])[2]");
        public IWebElement UJC_Update_SecondaryHeading => Driver.FindElement(ujc_update_secondaryheading);

        public By ujc_update_isactive = By.XPath("//input[@id='isActive']");
        public IWebElement UJC_Update_IsActive => Driver.FindElement(ujc_update_isactive);

        public By ujc_updatebutton = By.XPath("//button[@name='btnUpdate']");
        public IWebElement UJC_UpdateButton => Driver.FindElement(ujc_updatebutton);

        public By ujc_recycleBinTab = By.XPath("//a[@id='recycleBin-tab']");
        public IWebElement UJC_RecycleBinTab => Driver.FindElement(ujc_recycleBinTab);

        public By ujc_registrationcampaignbin_open = By.XPath("//h2[text()=' Registration Campaign Bin ' and @aria-expanded='true']");
        public IWebElement UJC_RegistrationCampaignBin_Open => Driver.FindElement(ujc_registrationcampaignbin_open);

        public By ujc_registrationcampaignbin_close = By.XPath("//h2[text()=' Registration Campaign Bin ' and @aria-expanded='false']");
        public IWebElement UJC_RegistrationCampaignBin_Close => Driver.FindElement(ujc_registrationcampaignbin_close);

        public By ujc_registrationcampaigndetails_open = By.XPath("//h2[text()=' Registration Campaign Details ' and @aria-expanded='false']");
        public IWebElement UJC_RegistrationCampaignDetails_Open => Driver.FindElement(ujc_registrationcampaigndetails_open);

        public By ujc_registrationcampaigndetails_close = By.XPath("//h2[text()=' Registration Campaign Details ' and @aria-expanded='true']");
        public IWebElement UJC_RegistrationCampaignDetails_Close => Driver.FindElement(ujc_registrationcampaigndetails_close);
        public By ujc_registrationdata_getlink(int index) => By.XPath($"(//table/tbody/tr/td[20]/a)[{index}]");
        public IWebElement UJC_RegistrationData_GetLink(int index) => Driver.FindElement(ujc_registrationdata_getlink(index));
        public By ujc_registrationdata_urllink => By.XPath("//td[@id='html-content']");
        public IWebElement UJC_RegistrationData_UrlLink => Driver.FindElement(ujc_registrationdata_urllink);
        public By ujc_urllink_popupclose => By.XPath("//button[@class='close']");
        public IWebElement UJC_UrlLink_PopupClose => Driver.FindElement(ujc_urllink_popupclose);
        public By ujc_registrationform_primaryheading => By.XPath("//form[@id='id-number-campaign-form']/h2");
        public IWebElement UJC_RegistrationForm_PrimaryHeading => Driver.FindElement(ujc_registrationform_primaryheading);
        public By ujc_registrationform_primarycontent => By.XPath("//form[@id='id-number-campaign-form']/p");
        public IWebElement UJC_RegistrationForm_PrimaryContent => Driver.FindElement(ujc_registrationform_primarycontent);
        public By ujc_registrationform_secondaryheading => By.XPath("//form[@id='id-number-campaign-form']/div[2]/div/div/h2");
        public IWebElement UJC_RegistrationForm_SecondaryHeading => Driver.FindElement(ujc_registrationform_secondaryheading);
        public By ujc_registrationform_secondarycontent => By.XPath("//form[@id='id-number-campaign-form']/div[2]/div/div/p");
        public IWebElement UJC_RegistrationForm_SecondaryContent => Driver.FindElement(ujc_registrationform_secondarycontent);
        public By ujc_registrationform_idnumber => By.XPath("//input[@id='IdNumber']");
        public IWebElement UJC_RegistrationForm_IdNumber => Driver.FindElement(ujc_registrationform_idnumber);
        public By ujc_registrationform_submitbutton => By.XPath("//form[@id='id-number-campaign-form']/div/div[2]/button");
        public IWebElement UJC_RegistrationForm_SubmitButton => Driver.FindElement(ujc_registrationform_submitbutton);
        public By ujc_registrationform_button => By.XPath("(//button[@name='btnRegisterRegisterPage'])[3]");
        public IWebElement UJC_RegistrationForm_Button => Driver.FindElement(ujc_registrationform_button);

        public By ujc_primaryheadingsecondpage = By.XPath("//input[@id='primaryHeadingSecondPage']");
        public IWebElement UJC_PrimaryHeadingSecondPage => Driver.FindElement(ujc_primaryheadingsecondpage);

        public By ujc_campaignbuttonnamesecondpage = By.XPath("//input[@id='campaignButtonNameSecondPage']");
        public IWebElement UJC_CampaignButtonNameSecondPage => Driver.FindElement(ujc_campaignbuttonnamesecondpage);
        public By ujc_registrationform_primaryheadingsecondpage => By.XPath("//form[@id='id-number-campaign-form']/h2");
        public IWebElement UJC_RegistrationForm_PrimaryHeadingSecondPage => Driver.FindElement(ujc_registrationform_primaryheadingsecondpage);
        public By ujc_registrationform_primarycontentsecondpage => By.XPath("//form[@id='id-number-campaign-form']/p");
        public IWebElement UJC_RegistrationForm_PrimaryContentSecondPage => Driver.FindElement(ujc_registrationform_primarycontentsecondpage);

        #endregion
    }
}