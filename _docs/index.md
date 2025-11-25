# Introduction

The project is related to the regression test cases of Sanlam and number of test cases present in this projects are:- :-

  1. CC1845_PreventSameIDNumberFromAttemptingRegistrationtwice
  1. CC1911_AgentUi
  1. CC1914_ForgotPasswordPage
  1. CC1916_NewUserJourney
  1. CC2093_SPLRegistrationJourney
  1. CC3088_NewLMS_RegistrationInactiveSPLAutoreg
  1. CC3185_LMSOnSanlamCreditSolutionPlatform
  1. CC3214_AutoRegistrationProcess
  1. CC3244_NewLMS_RegistrationBudgetingAdvice
  1. CC3245_NewLMS_RegistrationNoneCoaching
  1. ValidateLMS_RegistrationFailedSecurityQuestions_RegistrationInactiveSPLIVRAutoreg
  1. ValidateRegistrationFailedOTP_NewLMS_RegistrationInactiveSPLAutoreg
  1. ValidateRegistrationInactiveSPL_NewLMS_RegistrationInactiveSPLAutoreg
  1. ValidateRegistrationInactiveSP_NewLMS_RegistrationInactiveSPLIVRAutoreg
  1. ValidateLMS_RegistrationInactiveForOOBA_NewLMS_RegistrationInactiveOOBAAutoreg
  1. CC3288_CreditScore
  1. CC2036_CC2029_UpdateWealthScore
  1. CC2186_WealthScoreCalculation
  1. CC2009_CC2033_BudgetScoreCalculation
  1. CC_FAQPage 
  1. CC_AllDashboardButtons
  1. CC_InsightsForYou
  1. CC_CreditInsightsPageButtons 
  1. CC_HowYouMeasureUpScale 
  1. CC2113_CreditScoreTrend
  1. CC_FactorsAffectingYourScore
  1. CC2748_YourCreditSummary
  1. CC_CreditAccountPageTabs
  1. ADO13374_SanlamPersonalLoanTile 
  1. ADO13375_CapfinPersonalLoanTile 
  1. ADO13643_CreditCardAndMobiCredTile 
  1. ADO13644_CreditConsolidationTile 
  1. ADO13376_PersonalLoanFinance27Tile 

In this test cases inputs are present in [automation-countainer](https://portal.azure.com/#view/Microsoft_Azure_Storage/ContainerMenuBlade/~/overview/storageAccountId/%2Fsubscriptions%2Fa0a76383-9cc3-4aa3-8693-284b5eafc757%2FresourceGroups%2FRG-SCS-PREPROD-001%2Fproviders%2FMicrosoft.Storage%2FstorageAccounts%2Fstscsdata001/path/automation-container/etag/%220x8DC522F22AC37A0%22/defaultEncryptionScope/%24account-encryption-key/denyEncryptionScopeOverride~/false/defaultId//publicAccessVal/None) file and derived from their and having same name as the test class name (test case name). After test run project uses extent report for reporting the test results and its steps screenshot.

Repos are using to store the code in it. Link of Sanlam Repo for Auto regression Test Project [Test Code Repo](https://dev.azure.com/IDMDebtBusters/_git/IDM.DigiTech.Platforms.SCS) 

## Folder Structure

**_docs** - Contains documentation for this project.

**External** - Contains actions classes required to connect with database, storage browser, Resource group etc and used for API tasks.

**Test** - This folder is design on the basis of POM (Page Object Model). In this three folders are available as per below:-

1. *Pages* - Conatins of xpath or css of elements as per the page name.
1. *Steps* - Contains method performing on the pages during test run.
1. *TestSanlem* - Contains Regression Test in it.

**TestResources** - Others things required by the projects are URL, Database connection strings, DB Quiries etc are available in this folder.

### Language

C#

### Tools

Selenium, Nunit, Extent Report, AzureDevops, RestSharp

### Database

SQL, AzureDevops (Blob-Containers, Storage Browser)

