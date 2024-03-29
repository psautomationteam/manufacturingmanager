/*
   Thursday, May 16, 201311:03:59 PM
   User: sa
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
ALTER TABLE dbo.Bill
	DROP CONSTRAINT FK_Bill_Customer
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Customer', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Customer', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Customer', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Bill
	DROP CONSTRAINT FK_Bill_SystemUser
GO
COMMIT
select Has_Perms_By_Name(N'dbo.SystemUser', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.SystemUser', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.SystemUser', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Bill
	(
	Id int NOT NULL IDENTITY (1, 1),
	BillCode varchar(50) NOT NULL,
	CustId int NOT NULL,
	UserId int NOT NULL,
	Amount float(53) NOT NULL,
	Note nvarchar(250) NULL,
	Status tinyint NULL,
	CreatedDate datetime NOT NULL
	)  ON [PRIMARY]
GO
SET IDENTITY_INSERT dbo.Tmp_Bill ON
GO
IF EXISTS(SELECT * FROM dbo.Bill)
	 EXEC('INSERT INTO dbo.Tmp_Bill (Id, BillCode, CustId, UserId, Amount, Note, CreatedDate)
		SELECT Id, BillCode, CustId, UserId, Amount, Note, CreatedDate FROM dbo.Bill WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Bill OFF
GO
DROP TABLE dbo.Bill
GO
EXECUTE sp_rename N'dbo.Tmp_Bill', N'Bill', 'OBJECT' 
GO
ALTER TABLE dbo.Bill ADD CONSTRAINT
	PK_Bill_1 PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Bill ADD CONSTRAINT
	FK_Bill_SystemUser FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.SystemUser
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Bill ADD CONSTRAINT
	FK_Bill_Customer FOREIGN KEY
	(
	CustId
	) REFERENCES dbo.Customer
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Bill', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Bill', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Bill', 'Object', 'CONTROL') as Contr_Per 