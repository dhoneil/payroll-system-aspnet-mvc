
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/21/2022 21:42:10
-- Generated from EDMX file: D:\C# PROJECTS\mjl\mjl\mjl\Models\Database\dbPayroll.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbPayroll];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BillingAdjustments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillingAdjustments];
GO
IF OBJECT_ID(N'[dbo].[BillingSecurities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillingSecurities];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[CompanyHolidays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyHolidays];
GO
IF OBJECT_ID(N'[dbo].[CompensationHistories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompensationHistories];
GO
IF OBJECT_ID(N'[dbo].[DailyTimeRecords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DailyTimeRecords];
GO
IF OBJECT_ID(N'[dbo].[employee_master]', 'U') IS NOT NULL
    DROP TABLE [dbo].[employee_master];
GO
IF OBJECT_ID(N'[dbo].[Employee_Payslip]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Payslip];
GO
IF OBJECT_ID(N'[dbo].[Employee_Payslip_Adjustment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Payslip_Adjustment];
GO
IF OBJECT_ID(N'[dbo].[Employee_Payslip_Details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Payslip_Details];
GO
IF OBJECT_ID(N'[dbo].[Employee_Payslip_Government]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Payslip_Government];
GO
IF OBJECT_ID(N'[dbo].[Employee_Schedule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Schedule];
GO
IF OBJECT_ID(N'[dbo].[employee_timesheet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[employee_timesheet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeCharges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeCharges];
GO
IF OBJECT_ID(N'[dbo].[EmployeeDependents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeDependents];
GO
IF OBJECT_ID(N'[dbo].[EmployeeGovernments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeGovernments];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSpouses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSpouses];
GO
IF OBJECT_ID(N'[dbo].[Jobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Jobs];
GO
IF OBJECT_ID(N'[dbo].[SSS_Contribution]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SSS_Contribution];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[View_billingAdjustments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[View_billingAdjustments];
GO
IF OBJECT_ID(N'[dbo].[viewChargeReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[viewChargeReports];
GO
IF OBJECT_ID(N'[dbo].[viewCompanyHolidays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[viewCompanyHolidays];
GO
IF OBJECT_ID(N'[dbo].[viewEmployeeCharges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[viewEmployeeCharges];
GO
IF OBJECT_ID(N'[dbo].[ViewEmployeeV2]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ViewEmployeeV2];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BillingAdjustments'
CREATE TABLE [dbo].[BillingAdjustments] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [payslip_id] int  NULL,
    [name] varchar(50)  NULL,
    [amount] decimal(18,0)  NULL,
    [is_active] bit  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL
);
GO

-- Creating table 'BillingSecurities'
CREATE TABLE [dbo].[BillingSecurities] (
    [bill_id] int IDENTITY(1,1) NOT NULL,
    [bill_description] varchar(max)  NULL,
    [bill_amount] decimal(18,2)  NULL,
    [company_id] int  NULL,
    [date_from] datetime  NULL,
    [date_to] datetime  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [updated_by] int  NULL,
    [update_date] datetime  NULL,
    [status] datetime  NULL,
    [remarks] varchar(max)  NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [company_id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [type] varchar(50)  NULL,
    [owner_name] nvarchar(max)  NULL,
    [admin_fee] decimal(18,2)  NULL,
    [address] nvarchar(max)  NULL,
    [city] nvarchar(max)  NULL,
    [contact_number] nvarchar(max)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL,
    [is_active] bit  NULL
);
GO

-- Creating table 'CompanyHolidays'
CREATE TABLE [dbo].[CompanyHolidays] (
    [HolidayID] int IDENTITY(1,1) NOT NULL,
    [HolidayName] varchar(50)  NULL,
    [HolidayType] varchar(50)  NULL,
    [HolidayDate] datetime  NULL,
    [CompanyID] int  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'CompensationHistories'
CREATE TABLE [dbo].[CompensationHistories] (
    [HistoryID] int IDENTITY(1,1) NOT NULL,
    [EmployeeID] int  NULL,
    [CompensationType] varchar(50)  NULL,
    [ValidityStart] datetime  NULL,
    [CompensationRate] decimal(18,2)  NULL,
    [PreparedBy] int  NULL,
    [PreparedDate] datetime  NULL,
    [LastEditBy] int  NULL,
    [LastEditDate] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'DailyTimeRecords'
CREATE TABLE [dbo].[DailyTimeRecords] (
    [RecordID] int IDENTITY(1,1) NOT NULL,
    [EmployeeID] int  NOT NULL,
    [RecordDate] datetime  NOT NULL,
    [SystemEqDate] int  NOT NULL,
    [TimeLogIn] datetime  NULL,
    [TimeLogOut] datetime  NULL,
    [TimeLogType] int  NOT NULL,
    [PreparedBy] int  NOT NULL,
    [PreparedDate] datetime  NOT NULL,
    [LastEditBy] int  NOT NULL,
    [LastEditDate] datetime  NOT NULL,
    [Source] nvarchar(max)  NULL,
    [HasComputedPayslip] bit  NOT NULL,
    [total_duty_hrs] decimal(18,2)  NULL,
    [total_overtime_hrs] decimal(18,2)  NULL,
    [holiday_hrs] decimal(18,2)  NULL,
    [nigthdiff] decimal(18,2)  NULL,
    [lates] int  NULL,
    [undertime] int  NULL,
    [isHalfDay] bit  NULL
);
GO

-- Creating table 'employee_master'
CREATE TABLE [dbo].[employee_master] (
    [cust_acct_no] varchar(30)  NOT NULL,
    [countryid] int  NOT NULL,
    [comp_code] varchar(15)  NOT NULL,
    [franchise] int  NOT NULL,
    [store_code] varchar(10)  NOT NULL,
    [employee_no] decimal(10,0)  NOT NULL,
    [username] varchar(30)  NULL,
    [first_name] varchar(50)  NULL,
    [middle_name] varchar(50)  NULL,
    [last_name] varchar(50)  NULL,
    [birthday] datetime  NULL,
    [civil_id_no] varchar(30)  NULL,
    [civil_id_expired] datetime  NULL,
    [health_card_no] varchar(30)  NULL,
    [health_crd_exp] datetime  NULL,
    [level] int  NULL,
    [password] varchar(30)  NULL,
    [status] varchar(1)  NULL,
    [inac_date] datetime  NULL,
    [act_date] datetime  NULL,
    [passport] varchar(30)  NULL,
    [passport_expr] datetime  NULL,
    [address_flat] varchar(10)  NULL,
    [address_floor] varchar(10)  NULL,
    [address_censuno] varchar(20)  NULL,
    [address_buildingno] varchar(20)  NULL,
    [address_street] varchar(30)  NULL,
    [address_blockno] varchar(20)  NULL,
    [address_area] int  NULL,
    [address_city] int  NULL,
    [address_country] int  NULL,
    [origin_address] varchar(100)  NULL,
    [origin_city] varchar(30)  NULL,
    [origin_country] int  NULL,
    [first_name_a] varchar(50)  NULL,
    [middle_name_a] varchar(50)  NULL,
    [last_name_a] varchar(50)  NULL,
    [father_name_a] varchar(50)  NULL,
    [father_last_a] varchar(50)  NULL,
    [salary_rate] decimal(18,3)  NULL,
    [address_avenue] varchar(60)  NULL,
    [dept_code] varchar(10)  NULL,
    [sect_code] varchar(10)  NULL,
    [photo] varbinary(max)  NULL,
    [genderid] int  NULL,
    [maritalid] int  NULL,
    [nationalityid] int  NULL,
    [worksched] int  NULL,
    [driverlicense] varchar(50)  NULL,
    [driverexpired] datetime  NULL,
    [Contract] bit  NULL,
    [noyears] int  NULL,
    [contract_expired] datetime  NULL,
    [residency_no] varchar(50)  NULL,
    [residency_expired] datetime  NULL,
    [bankid] int  NULL,
    [bank_account_no] varchar(50)  NULL,
    [Passport_countryid] int  NULL,
    [passport_issuedat] varchar(50)  NULL,
    [datehired] datetime  NULL,
    [statusid] int  NULL,
    [photo_filename] varchar(300)  NULL,
    [photo_imagetype] varchar(50)  NULL,
    [phoneno] varchar(30)  NULL,
    [mobileno] varchar(30)  NULL,
    [email] varchar(60)  NULL,
    [compmobno] varchar(30)  NULL,
    [compemailadd] varchar(60)  NULL,
    [shift_code] varchar(2)  NULL,
    [clientassign] varchar(20)  NULL,
    [shift_type] int  NULL,
    [birthplace] varchar(100)  NULL,
    [salary_typeid] int  NULL,
    [salary_periodid] int  NULL,
    [height] int  NULL,
    [weight] int  NULL,
    [religionid] int  NULL,
    [spouse] varchar(120)  NULL,
    [spouse_occupation] varchar(80)  NULL,
    [fathers_name] varchar(150)  NULL,
    [fathers_occupation] varchar(120)  NULL,
    [mothers_name] varchar(120)  NULL,
    [mothers_occupation] varchar(150)  NULL,
    [parent_address] varchar(200)  NULL,
    [skillandlanguage] varchar(200)  NULL,
    [caseemergency] varchar(80)  NULL,
    [caseemergencycontact] varchar(50)  NULL,
    [caseemergencyaddress] varchar(200)  NULL,
    [sssno] varchar(50)  NULL,
    [pagibig] varchar(50)  NULL,
    [philhealth] varchar(50)  NULL,
    [tinno] varchar(50)  NULL,
    [probitionary] int  NULL,
    [noofmonths] int  NULL,
    [endprob] datetime  NULL,
    [housing] decimal(18,2)  NULL,
    [transpo] decimal(18,2)  NULL,
    [sil] int  NULL,
    [vl] int  NULL,
    [sl] int  NULL,
    [renewcontract] bit  NULL,
    [restday] int  NULL,
    [allowance_petrol] decimal(18,3)  NULL,
    [allowance_telephone] decimal(18,3)  NULL,
    [allowance_travel] decimal(18,3)  NULL,
    [allowance_car] decimal(18,3)  NULL,
    [allowance_job_nature] decimal(18,3)  NULL,
    [allowance_foods] decimal(18,3)  NULL,
    [allowance_others] decimal(18,3)  NULL,
    [cash_bond] decimal(18,3)  NULL,
    [pagibig_contr] decimal(18,3)  NULL,
    [philhealth_contr] decimal(18,3)  NULL,
    [fingerprintid] varchar(30)  NULL,
    [security_lisence] varchar(50)  NULL,
    [sl_dateexpire] datetime  NULL,
    [signaturephoto] varbinary(max)  NULL,
    [photos] bit  NULL,
    [sss_deduct] bit  NULL,
    [pagibig_deduct] bit  NULL,
    [philhealthdeduct] bit  NULL,
    [chargefee] decimal(18,3)  NULL,
    [runningbalance] decimal(18,3)  NULL,
    [defaulthrswork] decimal(18,3)  NULL,
    [chargepaydaydeduct] decimal(18,3)  NULL,
    [sprsdhrsprem] decimal(18,3)  NULL,
    [defaultothrswork] decimal(18,3)  NULL,
    [nsdhrswork] decimal(18,3)  NULL,
    [totalhrsworby8] bit  NULL,
    [billingfee] decimal(18,3)  NULL,
    [incentive] decimal(18,3)  NULL,
    [Gender] varchar(50)  NULL,
    [Nationality] varchar(50)  NULL
);
GO

-- Creating table 'Employee_Payslip'
CREATE TABLE [dbo].[Employee_Payslip] (
    [payslip_id] int IDENTITY(1,1) NOT NULL,
    [company_id] int  NULL,
    [employee_id] int  NULL,
    [employee_rate] decimal(18,2)  NULL,
    [date_from] datetime  NULL,
    [date_to] datetime  NULL,
    [total_additionals] decimal(18,2)  NULL,
    [total_less] decimal(18,2)  NULL,
    [total_deduction] decimal(18,2)  NULL,
    [gross_pay] decimal(18,2)  NULL,
    [net_pay] decimal(18,2)  NULL,
    [status] varchar(50)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [cancelled_by] int  NULL,
    [cancelled_date] datetime  NULL,
    [approved_by] int  NULL,
    [approved_date] datetime  NULL
);
GO

-- Creating table 'Employee_Payslip_Adjustment'
CREATE TABLE [dbo].[Employee_Payslip_Adjustment] (
    [payslip_adjustment_id] int IDENTITY(1,1) NOT NULL,
    [payslip_id] int  NULL,
    [name] varchar(50)  NULL,
    [amount] decimal(18,2)  NULL,
    [type] varchar(50)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [cancelled_by] int  NULL,
    [cancelled_date] datetime  NULL,
    [lastedit_by] int  NULL,
    [lastedit_date] datetime  NULL,
    [status] varchar(50)  NULL,
    [isCharges] bit  NULL
);
GO

-- Creating table 'Employee_Payslip_Details'
CREATE TABLE [dbo].[Employee_Payslip_Details] (
    [payslip_less_id] int IDENTITY(1,1) NOT NULL,
    [payslip_id] int  NULL,
    [name] varchar(50)  NULL,
    [days] decimal(18,2)  NULL,
    [hours] decimal(18,2)  NULL,
    [amount] decimal(18,2)  NULL,
    [type] varchar(50)  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL
);
GO

-- Creating table 'Employee_Payslip_Government'
CREATE TABLE [dbo].[Employee_Payslip_Government] (
    [payslip_government_id] int IDENTITY(1,1) NOT NULL,
    [payslip_id] int  NULL,
    [name] varchar(50)  NULL,
    [amount] decimal(18,2)  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL
);
GO

-- Creating table 'Employee_Schedule'
CREATE TABLE [dbo].[Employee_Schedule] (
    [ScheduleID] int IDENTITY(1,1) NOT NULL,
    [EmployeeID] int  NULL,
    [EffectiveDate] datetime  NULL,
    [ExpiryDate] datetime  NULL,
    [TimeIn] time  NULL,
    [TimeOut] time  NULL,
    [Restday] int  NULL,
    [Remarks] varchar(max)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [latest_edit_by] int  NULL,
    [latest_edit_date] datetime  NULL,
    [isOutOverDay] bit  NULL
);
GO

-- Creating table 'employee_timesheet'
CREATE TABLE [dbo].[employee_timesheet] (
    [emp_timesheet_id] int IDENTITY(1,1) NOT NULL,
    [employee_id] int  NULL,
    [date_from] datetime  NULL,
    [date_to] datetime  NULL,
    [regular_days] int  NULL,
    [holiday_days] int  NULL,
    [restday_days] int  NULL,
    [absent_days] int  NULL,
    [overtime_hours] decimal(18,2)  NULL,
    [lates] decimal(18,2)  NULL,
    [undertime] decimal(18,2)  NULL,
    [status] varchar(50)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [cancelled_by] int  NULL,
    [cancelled_date] datetime  NULL,
    [lastedit_by] int  NULL,
    [lastedit_date] datetime  NULL
);
GO

-- Creating table 'EmployeeCharges'
CREATE TABLE [dbo].[EmployeeCharges] (
    [ChargeID] int IDENTITY(1,1) NOT NULL,
    [ChargeName] varchar(50)  NULL,
    [EmployeeID] int  NULL,
    [date_from] datetime  NULL,
    [date_to] datetime  NULL,
    [status] varchar(50)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [lastedit_by] int  NULL,
    [lastedit_date] datetime  NULL,
    [ChargeAmount] decimal(18,2)  NULL,
    [Released_by] int  NULL,
    [Released_date] datetime  NULL,
    [Cancelled_by] int  NULL,
    [Cancelled_date] datetime  NULL,
    [payslip_adjustment_id] int  NULL,
    [payslip_id] int  NULL
);
GO

-- Creating table 'EmployeeDependents'
CREATE TABLE [dbo].[EmployeeDependents] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [emp_id] bigint  NULL,
    [name] varchar(50)  NULL,
    [relation] varchar(50)  NULL,
    [isActive] bit  NULL,
    [preparedBy] int  NULL,
    [preparedDate] datetime  NULL
);
GO

-- Creating table 'EmployeeGovernments'
CREATE TABLE [dbo].[EmployeeGovernments] (
    [goverment_deduction_id] bigint IDENTITY(1,1) NOT NULL,
    [emp_id] int  NULL,
    [with_sss] bit  NULL,
    [sss_type_deduction] varchar(50)  NULL,
    [sss_amount] decimal(18,2)  NULL,
    [with_philhealth] bit  NULL,
    [philhealth_type_deduction] varchar(50)  NULL,
    [philhealth_amount] decimal(18,2)  NULL,
    [with_pagibig] bit  NULL,
    [pagibig_type_deduction] varchar(50)  NULL,
    [pagibig_amount] decimal(18,2)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL,
    [with_tin] bit  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeID] int IDENTITY(1,1) NOT NULL,
    [employee_no] int  NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [MiddleName] nvarchar(max)  NULL,
    [DateHired] datetime  NULL,
    [Gender] nvarchar(max)  NULL,
    [Citizenship] nvarchar(max)  NULL,
    [BirthDate] datetime  NULL,
    [BirthPlace] nvarchar(max)  NULL,
    [JobID] int  NULL,
    [PreparedBy] int  NULL,
    [PreparedDate] datetime  NULL,
    [LastEditBy] int  NULL,
    [LastEditDate] datetime  NULL,
    [IsActive] bit  NULL,
    [company_id] int  NULL,
    [contact_no] varchar(50)  NULL,
    [birth_place] varchar(50)  NULL,
    [height] varchar(50)  NULL,
    [weight] varchar(50)  NULL,
    [probitionary] bit  NULL,
    [prob_no_of_months] int  NULL,
    [end_probitionary_date] datetime  NULL,
    [province_address] varchar(max)  NULL,
    [email] varchar(50)  NULL,
    [Current_country] varchar(50)  NULL,
    [MaritalStatus] varchar(50)  NULL,
    [religion] varchar(100)  NULL,
    [Current_region] varchar(50)  NULL,
    [Current_city_municipality] varchar(50)  NULL,
    [Current_street] varchar(50)  NULL,
    [Current_barangay] varchar(50)  NULL,
    [Current_sitio] varchar(50)  NULL,
    [provincial_country] varchar(50)  NULL,
    [provincial_region] varchar(50)  NULL,
    [provincial_city_municipality] varchar(50)  NULL,
    [provincial_street] varchar(50)  NULL,
    [provincial_barangay] varchar(50)  NULL,
    [provincial_sitio] varchar(50)  NULL,
    [renew_contract] bit  NULL,
    [SSS_Number] varchar(50)  NULL,
    [PagIbig_Number] varchar(50)  NULL,
    [PhilHealth_Number] varchar(50)  NULL,
    [TIN_Number] varchar(50)  NULL,
    [atm_number] varchar(50)  NULL
);
GO

-- Creating table 'EmployeeSpouses'
CREATE TABLE [dbo].[EmployeeSpouses] (
    [id] int IDENTITY(1,1) NOT NULL,
    [emp_id] varchar(50)  NULL,
    [firstname] varchar(50)  NULL,
    [middlename] varchar(50)  NULL,
    [lastname] varchar(50)  NULL,
    [preparedBy] int  NULL,
    [preparedDate] datetime  NULL,
    [isActive] bit  NULL
);
GO

-- Creating table 'Jobs'
CREATE TABLE [dbo].[Jobs] (
    [JobID] int IDENTITY(1,1) NOT NULL,
    [JobName] nvarchar(max)  NULL,
    [JobDescription] nvarchar(max)  NULL,
    [PreparedBy] int  NULL,
    [PreparedDate] datetime  NULL,
    [LastEditBy] int  NULL,
    [LastEditDate] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'SSS_Contribution'
CREATE TABLE [dbo].[SSS_Contribution] (
    [id] int IDENTITY(1,1) NOT NULL,
    [start_range] decimal(18,2)  NULL,
    [end_range] decimal(18,2)  NULL,
    [monthly_salary_credit] decimal(18,2)  NULL,
    [social_security_er] decimal(18,2)  NULL,
    [social_security_ee] decimal(18,2)  NULL,
    [social_security_total] decimal(18,2)  NULL,
    [ec_er] decimal(18,2)  NULL,
    [total_contribution_er] decimal(18,2)  NULL,
    [total_contribution_ee] decimal(18,2)  NULL,
    [grand_total_contribution] decimal(18,2)  NULL,
    [total_contribution] decimal(18,2)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'View_billingAdjustments'
CREATE TABLE [dbo].[View_billingAdjustments] (
    [id] bigint  NOT NULL,
    [payslip_id] int  NULL,
    [name] varchar(50)  NULL,
    [amount] decimal(18,0)  NULL,
    [is_active] bit  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL,
    [Expr1] int  NULL,
    [Expr5] int  NULL,
    [employee_id] int  NULL,
    [employee_rate] decimal(18,2)  NULL,
    [date_from] datetime  NULL,
    [date_to] datetime  NULL,
    [total_additionals] decimal(18,2)  NULL,
    [total_less] decimal(18,2)  NULL,
    [total_deduction] decimal(18,2)  NULL,
    [gross_pay] decimal(18,2)  NULL,
    [net_pay] decimal(18,2)  NULL,
    [status] varchar(50)  NULL,
    [Expr2] int  NULL,
    [Expr3] datetime  NULL,
    [cancelled_by] int  NULL,
    [cancelled_date] datetime  NULL,
    [approved_by] int  NULL,
    [approved_date] datetime  NULL,
    [fname] nvarchar(max)  NULL,
    [lname] nvarchar(max)  NULL,
    [fname_edit] nvarchar(max)  NULL,
    [lname_edit] nvarchar(max)  NULL,
    [e_ID] int  NULL
);
GO

-- Creating table 'viewChargeReports'
CREATE TABLE [dbo].[viewChargeReports] (
    [ChargeID] int  NOT NULL,
    [ChargeName] varchar(50)  NULL,
    [EmployeeID] int  NULL,
    [date_from] datetime  NULL,
    [date_to] datetime  NULL,
    [status] varchar(50)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [lastedit_by] int  NULL,
    [lastedit_date] datetime  NULL,
    [ChargeAmount] decimal(18,2)  NULL,
    [Released_by] int  NULL,
    [Released_date] datetime  NULL,
    [Cancelled_by] int  NULL,
    [Cancelled_date] datetime  NULL,
    [payslip_adjustment_id] int  NULL,
    [payslip_id] int  NULL,
    [Expr1] int  NOT NULL,
    [employee_no] int  NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [MiddleName] nvarchar(max)  NULL,
    [DateHired] datetime  NULL,
    [Gender] nvarchar(max)  NULL,
    [Citizenship] nvarchar(max)  NULL,
    [BirthDate] datetime  NULL,
    [BirthPlace] nvarchar(max)  NULL,
    [JobID] int  NULL,
    [PreparedBy] int  NULL,
    [PreparedDate] datetime  NULL,
    [LastEditBy] int  NULL,
    [LastEditDate] datetime  NULL,
    [IsActive] bit  NULL,
    [company_id] int  NULL,
    [contact_no] varchar(50)  NULL,
    [birth_place] varchar(50)  NULL,
    [probitionary] bit  NULL,
    [prob_no_of_months] int  NULL,
    [end_probitionary_date] datetime  NULL,
    [province_address] varchar(max)  NULL,
    [email] varchar(50)  NULL,
    [Current_country] varchar(50)  NULL,
    [MaritalStatus] varchar(50)  NULL,
    [religion] varchar(100)  NULL,
    [Current_region] varchar(50)  NULL,
    [Current_city_municipality] varchar(50)  NULL,
    [Current_street] varchar(50)  NULL,
    [Current_barangay] varchar(50)  NULL,
    [Current_sitio] varchar(50)  NULL,
    [provincial_country] varchar(50)  NULL,
    [provincial_region] varchar(50)  NULL,
    [provincial_city_municipality] nchar(10)  NULL,
    [provincial_street] varchar(50)  NULL,
    [provincial_barangay] varchar(50)  NULL,
    [provincial_sitio] varchar(50)  NULL,
    [renew_contract] bit  NULL,
    [SSS_Number] varchar(50)  NULL,
    [PagIbig_Number] varchar(50)  NULL,
    [PhilHealth_Number] varchar(50)  NULL,
    [TIN_Number] varchar(50)  NULL,
    [atm_number] varchar(50)  NULL,
    [Expr2] int  NOT NULL,
    [name] nvarchar(max)  NULL,
    [type] varchar(50)  NULL,
    [owner_name] nvarchar(max)  NULL,
    [admin_fee] decimal(18,2)  NULL,
    [address] nvarchar(max)  NULL,
    [city] nvarchar(max)  NULL,
    [contact_number] nvarchar(max)  NULL,
    [Expr3] int  NULL,
    [Expr4] datetime  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL,
    [is_active] bit  NULL,
    [weight] varchar(50)  NULL,
    [height] varchar(50)  NULL
);
GO

-- Creating table 'viewCompanyHolidays'
CREATE TABLE [dbo].[viewCompanyHolidays] (
    [HolidayID] int  NOT NULL,
    [HolidayName] varchar(50)  NULL,
    [HolidayType] varchar(50)  NULL,
    [HolidayDate] datetime  NULL,
    [CompanyID] int  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL,
    [IsActive] bit  NULL,
    [company_id] int  NOT NULL,
    [name] nvarchar(max)  NULL,
    [owner_name] nvarchar(max)  NULL,
    [address] nvarchar(max)  NULL,
    [city] nvarchar(max)  NULL,
    [contact_number] nvarchar(max)  NULL,
    [Expr1] int  NULL,
    [Expr2] datetime  NULL,
    [Expr3] int  NULL,
    [Expr4] datetime  NULL,
    [is_active] bit  NULL
);
GO

-- Creating table 'viewEmployeeCharges'
CREATE TABLE [dbo].[viewEmployeeCharges] (
    [ChargeID] int  NOT NULL,
    [ChargeName] varchar(50)  NULL,
    [EmployeeID] int  NULL,
    [date_from] datetime  NULL,
    [date_to] datetime  NULL,
    [status] varchar(50)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [lastedit_by] int  NULL,
    [lastedit_date] datetime  NULL,
    [ChargeAmount] decimal(18,2)  NULL,
    [Released_by] int  NULL,
    [Released_date] datetime  NULL,
    [Cancelled_by] int  NULL,
    [Cancelled_date] datetime  NULL,
    [payslip_adjustment_id] int  NULL,
    [payslip_id] int  NULL,
    [Expr1] int  NOT NULL,
    [employee_no] int  NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [MiddleName] nvarchar(max)  NULL,
    [DateHired] datetime  NULL,
    [Gender] nvarchar(max)  NULL,
    [Citizenship] nvarchar(max)  NULL,
    [BirthDate] datetime  NULL,
    [BirthPlace] nvarchar(max)  NULL,
    [JobID] int  NULL,
    [PreparedBy] int  NULL,
    [PreparedDate] datetime  NULL,
    [LastEditBy] int  NULL,
    [LastEditDate] datetime  NULL,
    [IsActive] bit  NULL,
    [company_id] int  NULL,
    [contact_no] varchar(50)  NULL,
    [birth_place] varchar(50)  NULL,
    [probitionary] bit  NULL,
    [prob_no_of_months] int  NULL,
    [end_probitionary_date] datetime  NULL,
    [province_address] varchar(max)  NULL,
    [email] varchar(50)  NULL,
    [Current_country] varchar(50)  NULL,
    [MaritalStatus] varchar(50)  NULL,
    [religion] varchar(100)  NULL,
    [Current_region] varchar(50)  NULL,
    [Current_city_municipality] varchar(50)  NULL,
    [Current_street] varchar(50)  NULL,
    [Current_barangay] varchar(50)  NULL,
    [Current_sitio] varchar(50)  NULL,
    [provincial_country] varchar(50)  NULL,
    [provincial_region] varchar(50)  NULL,
    [provincial_city_municipality] nchar(10)  NULL,
    [provincial_street] varchar(50)  NULL,
    [provincial_barangay] varchar(50)  NULL,
    [provincial_sitio] varchar(50)  NULL,
    [renew_contract] bit  NULL,
    [height] varchar(50)  NULL,
    [weight] varchar(50)  NULL
);
GO

-- Creating table 'ViewEmployeeV2'
CREATE TABLE [dbo].[ViewEmployeeV2] (
    [EmployeeID] int  NOT NULL,
    [employee_no] int  NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [MiddleName] nvarchar(max)  NULL,
    [DateHired] datetime  NULL,
    [Gender] nvarchar(max)  NULL,
    [Citizenship] nvarchar(max)  NULL,
    [BirthDate] datetime  NULL,
    [BirthPlace] nvarchar(max)  NULL,
    [JobID] int  NULL,
    [PreparedBy] int  NULL,
    [PreparedDate] datetime  NULL,
    [LastEditBy] int  NULL,
    [LastEditDate] datetime  NULL,
    [IsActive] bit  NULL,
    [company_id] int  NULL,
    [contact_no] varchar(50)  NULL,
    [birth_place] varchar(50)  NULL,
    [probitionary] bit  NULL,
    [prob_no_of_months] int  NULL,
    [end_probitionary_date] datetime  NULL,
    [province_address] varchar(max)  NULL,
    [email] varchar(50)  NULL,
    [Current_country] varchar(50)  NULL,
    [MaritalStatus] varchar(50)  NULL,
    [religion] varchar(100)  NULL,
    [Current_region] varchar(50)  NULL,
    [Current_city_municipality] varchar(50)  NULL,
    [Current_street] varchar(50)  NULL,
    [Current_barangay] varchar(50)  NULL,
    [Current_sitio] varchar(50)  NULL,
    [provincial_country] varchar(50)  NULL,
    [provincial_region] varchar(50)  NULL,
    [provincial_city_municipality] nchar(10)  NULL,
    [provincial_street] varchar(50)  NULL,
    [provincial_barangay] varchar(50)  NULL,
    [provincial_sitio] varchar(50)  NULL,
    [renew_contract] bit  NULL,
    [SSS_Number] varchar(50)  NULL,
    [PagIbig_Number] varchar(50)  NULL,
    [PhilHealth_Number] varchar(50)  NULL,
    [TIN_Number] varchar(50)  NULL,
    [Expr1] int  NULL,
    [JobName] nvarchar(max)  NULL,
    [JobDescription] nvarchar(max)  NULL,
    [Expr2] int  NULL,
    [Expr3] datetime  NULL,
    [Expr4] int  NULL,
    [Expr5] datetime  NULL,
    [Expr6] bit  NULL,
    [HistoryID] int  NULL,
    [Expr7] int  NULL,
    [CompensationType] varchar(50)  NULL,
    [ValidityStart] datetime  NULL,
    [CompensationRate] decimal(18,2)  NULL,
    [Expr8] int  NULL,
    [Expr9] datetime  NULL,
    [Expr10] int  NULL,
    [Expr11] datetime  NULL,
    [Expr12] bit  NULL,
    [goverment_deduction_id] bigint  NULL,
    [emp_id] int  NULL,
    [with_sss] bit  NULL,
    [sss_type_deduction] varchar(50)  NULL,
    [sss_amount] decimal(18,2)  NULL,
    [with_philhealth] bit  NULL,
    [philhealth_type_deduction] varchar(50)  NULL,
    [philhealth_amount] decimal(18,2)  NULL,
    [with_pagibig] bit  NULL,
    [pagibig_type_deduction] varchar(50)  NULL,
    [pagibig_amount] decimal(18,2)  NULL,
    [prepared_by] int  NULL,
    [prepared_date] datetime  NULL,
    [last_edit_by] int  NULL,
    [last_edit_date] datetime  NULL,
    [Expr13] int  NULL,
    [name] nvarchar(max)  NULL,
    [type] varchar(50)  NULL,
    [owner_name] nvarchar(max)  NULL,
    [admin_fee] decimal(18,2)  NULL,
    [address] nvarchar(max)  NULL,
    [city] nvarchar(max)  NULL,
    [contact_number] nvarchar(max)  NULL,
    [Expr14] int  NULL,
    [Expr15] datetime  NULL,
    [Expr16] int  NULL,
    [Expr17] datetime  NULL,
    [is_active] bit  NULL,
    [with_tin] bit  NULL,
    [atm_number] varchar(50)  NULL,
    [height] varchar(50)  NULL,
    [weight] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'BillingAdjustments'
ALTER TABLE [dbo].[BillingAdjustments]
ADD CONSTRAINT [PK_BillingAdjustments]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [bill_id] in table 'BillingSecurities'
ALTER TABLE [dbo].[BillingSecurities]
ADD CONSTRAINT [PK_BillingSecurities]
    PRIMARY KEY CLUSTERED ([bill_id] ASC);
GO

-- Creating primary key on [company_id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([company_id] ASC);
GO

-- Creating primary key on [HolidayID] in table 'CompanyHolidays'
ALTER TABLE [dbo].[CompanyHolidays]
ADD CONSTRAINT [PK_CompanyHolidays]
    PRIMARY KEY CLUSTERED ([HolidayID] ASC);
GO

-- Creating primary key on [HistoryID] in table 'CompensationHistories'
ALTER TABLE [dbo].[CompensationHistories]
ADD CONSTRAINT [PK_CompensationHistories]
    PRIMARY KEY CLUSTERED ([HistoryID] ASC);
GO

-- Creating primary key on [RecordID] in table 'DailyTimeRecords'
ALTER TABLE [dbo].[DailyTimeRecords]
ADD CONSTRAINT [PK_DailyTimeRecords]
    PRIMARY KEY CLUSTERED ([RecordID] ASC);
GO

-- Creating primary key on [cust_acct_no], [countryid], [comp_code], [franchise], [store_code], [employee_no] in table 'employee_master'
ALTER TABLE [dbo].[employee_master]
ADD CONSTRAINT [PK_employee_master]
    PRIMARY KEY CLUSTERED ([cust_acct_no], [countryid], [comp_code], [franchise], [store_code], [employee_no] ASC);
GO

-- Creating primary key on [payslip_id] in table 'Employee_Payslip'
ALTER TABLE [dbo].[Employee_Payslip]
ADD CONSTRAINT [PK_Employee_Payslip]
    PRIMARY KEY CLUSTERED ([payslip_id] ASC);
GO

-- Creating primary key on [payslip_adjustment_id] in table 'Employee_Payslip_Adjustment'
ALTER TABLE [dbo].[Employee_Payslip_Adjustment]
ADD CONSTRAINT [PK_Employee_Payslip_Adjustment]
    PRIMARY KEY CLUSTERED ([payslip_adjustment_id] ASC);
GO

-- Creating primary key on [payslip_less_id] in table 'Employee_Payslip_Details'
ALTER TABLE [dbo].[Employee_Payslip_Details]
ADD CONSTRAINT [PK_Employee_Payslip_Details]
    PRIMARY KEY CLUSTERED ([payslip_less_id] ASC);
GO

-- Creating primary key on [payslip_government_id] in table 'Employee_Payslip_Government'
ALTER TABLE [dbo].[Employee_Payslip_Government]
ADD CONSTRAINT [PK_Employee_Payslip_Government]
    PRIMARY KEY CLUSTERED ([payslip_government_id] ASC);
GO

-- Creating primary key on [ScheduleID] in table 'Employee_Schedule'
ALTER TABLE [dbo].[Employee_Schedule]
ADD CONSTRAINT [PK_Employee_Schedule]
    PRIMARY KEY CLUSTERED ([ScheduleID] ASC);
GO

-- Creating primary key on [emp_timesheet_id] in table 'employee_timesheet'
ALTER TABLE [dbo].[employee_timesheet]
ADD CONSTRAINT [PK_employee_timesheet]
    PRIMARY KEY CLUSTERED ([emp_timesheet_id] ASC);
GO

-- Creating primary key on [ChargeID] in table 'EmployeeCharges'
ALTER TABLE [dbo].[EmployeeCharges]
ADD CONSTRAINT [PK_EmployeeCharges]
    PRIMARY KEY CLUSTERED ([ChargeID] ASC);
GO

-- Creating primary key on [id] in table 'EmployeeDependents'
ALTER TABLE [dbo].[EmployeeDependents]
ADD CONSTRAINT [PK_EmployeeDependents]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [goverment_deduction_id] in table 'EmployeeGovernments'
ALTER TABLE [dbo].[EmployeeGovernments]
ADD CONSTRAINT [PK_EmployeeGovernments]
    PRIMARY KEY CLUSTERED ([goverment_deduction_id] ASC);
GO

-- Creating primary key on [EmployeeID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC);
GO

-- Creating primary key on [id] in table 'EmployeeSpouses'
ALTER TABLE [dbo].[EmployeeSpouses]
ADD CONSTRAINT [PK_EmployeeSpouses]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [JobID] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [PK_Jobs]
    PRIMARY KEY CLUSTERED ([JobID] ASC);
GO

-- Creating primary key on [id] in table 'SSS_Contribution'
ALTER TABLE [dbo].[SSS_Contribution]
ADD CONSTRAINT [PK_SSS_Contribution]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [id] in table 'View_billingAdjustments'
ALTER TABLE [dbo].[View_billingAdjustments]
ADD CONSTRAINT [PK_View_billingAdjustments]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [ChargeID], [Expr1], [Expr2] in table 'viewChargeReports'
ALTER TABLE [dbo].[viewChargeReports]
ADD CONSTRAINT [PK_viewChargeReports]
    PRIMARY KEY CLUSTERED ([ChargeID], [Expr1], [Expr2] ASC);
GO

-- Creating primary key on [HolidayID], [company_id] in table 'viewCompanyHolidays'
ALTER TABLE [dbo].[viewCompanyHolidays]
ADD CONSTRAINT [PK_viewCompanyHolidays]
    PRIMARY KEY CLUSTERED ([HolidayID], [company_id] ASC);
GO

-- Creating primary key on [ChargeID], [Expr1] in table 'viewEmployeeCharges'
ALTER TABLE [dbo].[viewEmployeeCharges]
ADD CONSTRAINT [PK_viewEmployeeCharges]
    PRIMARY KEY CLUSTERED ([ChargeID], [Expr1] ASC);
GO

-- Creating primary key on [EmployeeID] in table 'ViewEmployeeV2'
ALTER TABLE [dbo].[ViewEmployeeV2]
ADD CONSTRAINT [PK_ViewEmployeeV2]
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------