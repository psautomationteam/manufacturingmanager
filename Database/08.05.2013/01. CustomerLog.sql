/*
   Sunday, May 05, 201310:20:12 PM
   User: 
   Server: KHANGUYEN-PC\SQLEXPRESS
   Database: BaoHienCompany
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CustomerLog
	DROP CONSTRAINT FK_CustomerLog_Customer
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Customer', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Customer', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Customer', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CustomerLog
	DROP CONSTRAINT DF_CustomerLog_BeforeDebit
GO
ALTER TABLE dbo.CustomerLog
	DROP CONSTRAINT DF_CustomerLog_Amount
GO
ALTER TABLE dbo.CustomerLog
	DROP CONSTRAINT DF_CustomerLog_AfterDebit
GO
CREATE TABLE dbo.Tmp_CustomerLog
	(
	Id int NOT NULL IDENTITY (1, 1),
	CustomerId int NOT NULL,
	RecordCode varchar(25) NOT NULL,
	BeforeDebit float(53) NOT NULL,
	Amount float(53) NOT NULL,
	AfterDebit float(53) NOT NULL,
	Status int NOT NULL,
	CreatedDate datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CustomerLog ADD CONSTRAINT
	DF_CustomerLog_BeforeDebit DEFAULT ((0)) FOR BeforeDebit
GO
ALTER TABLE dbo.Tmp_CustomerLog ADD CONSTRAINT
	DF_CustomerLog_Amount DEFAULT ((0)) FOR Amount
GO
ALTER TABLE dbo.Tmp_CustomerLog ADD CONSTRAINT
	DF_CustomerLog_AfterDebit DEFAULT ((0)) FOR AfterDebit
GO
ALTER TABLE dbo.Tmp_CustomerLog ADD CONSTRAINT
	DF_CustomerLog_Status DEFAULT 0 FOR Status
GO
SET IDENTITY_INSERT dbo.Tmp_CustomerLog ON
GO
IF EXISTS(SELECT * FROM dbo.CustomerLog)
	 EXEC('INSERT INTO dbo.Tmp_CustomerLog (Id, CustomerId, RecordCode, BeforeDebit, Amount, AfterDebit, CreatedDate)
		SELECT Id, CustomerId, RecordCode, BeforeDebit, Amount, AfterDebit, CreatedDate FROM dbo.CustomerLog WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CustomerLog OFF
GO
DROP TABLE dbo.CustomerLog
GO
EXECUTE sp_rename N'dbo.Tmp_CustomerLog', N'CustomerLog', 'OBJECT' 
GO
ALTER TABLE dbo.CustomerLog ADD CONSTRAINT
	PK_CustomerLog PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CustomerLog ADD CONSTRAINT
	FK_CustomerLog_Customer FOREIGN KEY
	(
	CustomerId
	) REFERENCES dbo.Customer
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CustomerLog', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CustomerLog', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CustomerLog', 'Object', 'CONTROL') as Contr_Per 