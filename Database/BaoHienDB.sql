/****** Object:  ForeignKey [FK_Customer_Employee]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Customer_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_Employee]
GO
/****** Object:  ForeignKey [FK_MaterialInStock_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MaterialInStock_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[MaterialInStock]'))
ALTER TABLE [dbo].[MaterialInStock] DROP CONSTRAINT [FK_MaterialInStock_Product]
GO
/****** Object:  ForeignKey [FK_Order_Customer]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Order_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Order]'))
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Customer]
GO
/****** Object:  ForeignKey [FK_Order_SystemUser]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Order_SystemUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Order]'))
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_SystemUser]
GO
/****** Object:  ForeignKey [FK_OrderDetail_Order]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrderDetail_Order]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderDetail]'))
ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetail_Order]
GO
/****** Object:  ForeignKey [FK_OrderDetail_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrderDetail_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderDetail]'))
ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetail_Product]
GO
/****** Object:  ForeignKey [FK_Price_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Price_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Price]'))
ALTER TABLE [dbo].[Price] DROP CONSTRAINT [FK_Price_Product]
GO
/****** Object:  ForeignKey [FK_Product_MeasurementUnit]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_MeasurementUnit]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_MeasurementUnit]
GO
/****** Object:  ForeignKey [FK_Product_ProductType]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_ProductType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_ProductType]
GO
/****** Object:  ForeignKey [FK_ProductAttribute_Attribute]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttribute_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttribute]'))
ALTER TABLE [dbo].[ProductAttribute] DROP CONSTRAINT [FK_ProductAttribute_Attribute]
GO
/****** Object:  ForeignKey [FK_ProductAttribute_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttribute_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttribute]'))
ALTER TABLE [dbo].[ProductAttribute] DROP CONSTRAINT [FK_ProductAttribute_Product]
GO
/****** Object:  ForeignKey [FK_ProductInStock_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductInStock_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductInStock]'))
ALTER TABLE [dbo].[ProductInStock] DROP CONSTRAINT [FK_ProductInStock_Product]
GO
/****** Object:  ForeignKey [FK_ProductionRequestDetail_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionRequestDetail_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionRequestDetail]'))
ALTER TABLE [dbo].[ProductionRequestDetail] DROP CONSTRAINT [FK_ProductionRequestDetail_Product]
GO
/****** Object:  ForeignKey [FK_ProductionRequestDetail_ProductionRequest]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionRequestDetail_ProductionRequest]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionRequestDetail]'))
ALTER TABLE [dbo].[ProductionRequestDetail] DROP CONSTRAINT [FK_ProductionRequestDetail_ProductionRequest]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDetail]') AND type in (N'U'))
DROP TABLE [dbo].[OrderDetail]
GO
/****** Object:  Table [dbo].[Price]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Price]') AND type in (N'U'))
DROP TABLE [dbo].[Price]
GO
/****** Object:  Table [dbo].[MaterialInStock]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MaterialInStock]') AND type in (N'U'))
DROP TABLE [dbo].[MaterialInStock]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order]') AND type in (N'U'))
DROP TABLE [dbo].[Order]
GO
/****** Object:  Table [dbo].[ProductAttribute]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductAttribute]') AND type in (N'U'))
DROP TABLE [dbo].[ProductAttribute]
GO
/****** Object:  Table [dbo].[ProductInStock]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductInStock]') AND type in (N'U'))
DROP TABLE [dbo].[ProductInStock]
GO
/****** Object:  Table [dbo].[ProductionRequestDetail]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionRequestDetail]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionRequestDetail]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO
/****** Object:  Table [dbo].[MeasurementUnit]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasurementUnit]') AND type in (N'U'))
DROP TABLE [dbo].[MeasurementUnit]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductType]') AND type in (N'U'))
DROP TABLE [dbo].[ProductType]
GO
/****** Object:  Table [dbo].[SystemUser]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemUser]') AND type in (N'U'))
DROP TABLE [dbo].[SystemUser]
GO
/****** Object:  Table [dbo].[ProductionRequest]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionRequest]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionRequest]
GO
/****** Object:  Table [dbo].[Attribute]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Attribute]') AND type in (N'U'))
DROP TABLE [dbo].[Attribute]
GO
/****** Object:  Default [DF_Customer_Status]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_Status]
END


End
GO
/****** Object:  Default [DF_Order_VAT]    Script Date: 07/10/2012 00:53:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Order_VAT]') AND parent_object_id = OBJECT_ID(N'[dbo].[Order]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Order_VAT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_VAT]
END


End
GO
/****** Object:  Table [dbo].[Attribute]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Attribute]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Attribute](
	[AttributeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AttributeCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Attribute] PRIMARY KEY CLUSTERED 
(
	[AttributeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ProductionRequest]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionRequest]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionRequest](
	[ReqId] [int] IDENTITY(1,1) NOT NULL,
	[ReqCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RequestedDate] [int] NULL,
	[RequestedBy] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ProductionRequest] PRIMARY KEY CLUSTERED 
(
	[ReqId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[SystemUser]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SystemUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[password] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Status] [smallint] NULL,
	[Type] [smallint] NOT NULL,
	[Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductType](
	[ProductTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TypeCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[ProductTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MeasurementUnit]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasurementUnit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MeasurementUnit](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UnitCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MeasurementUnit] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FirstName] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MiddleName] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NickName] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobilePhone] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Type] [smallint] NOT NULL,
	[Description] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Product]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BaseUnit] [int] NULL,
	[ProductType] [int] NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Product', N'COLUMN',N'BaseUnit'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Don vi tinh co ban' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'BaseUnit'
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
	[CustId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CustCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Phone] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BankAcc] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BankName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactPerson] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactPersonPhone] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactPersonEmail] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [smallint] NULL,
	[SalerId] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ProductionRequestDetail]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionRequestDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionRequestDetail](
	[ReqId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[NumberUnit] [int] NULL,
	[Origin] [smallint] NULL,
 CONSTRAINT [PK_ProductionRequestDetail] PRIMARY KEY CLUSTERED 
(
	[ReqId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ProductInStock]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductInStock]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductInStock](
	[ProductId] [int] NOT NULL,
	[NumberOfItem] [int] NOT NULL,
	[LatestUpdate] [datetime] NOT NULL,
	[Note] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ProductInStock] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[LatestUpdate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ProductAttribute]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductAttribute]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductAttribute](
	[ProductId] [int] NOT NULL,
	[AttributeId] [int] NOT NULL,
	[Value] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ProductAttribute] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[AttributeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Order]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreateBy] [int] NOT NULL,
	[CustId] [int] NOT NULL,
	[Total] [float] NOT NULL,
	[Note] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VAT] [float] NOT NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MaterialInStock]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MaterialInStock]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MaterialInStock](
	[ProductId] [int] NOT NULL,
	[NumberOfItem] [int] NOT NULL,
	[LatestUpdate] [datetime] NOT NULL,
	[Note] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MaterialInStock] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[LatestUpdate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Price]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Price]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Price](
	[ProductId] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[UpdatedDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 07/10/2012 00:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[NumberUnit] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Cost] [float] NOT NULL,
	[Tax] [float] NOT NULL,
	[Note] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_BillDetail] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Default [DF_Customer_Status]    Script Date: 07/10/2012 00:53:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_Status]  DEFAULT ((1)) FOR [Status]
END


End
GO
/****** Object:  Default [DF_Order_VAT]    Script Date: 07/10/2012 00:53:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Order_VAT]') AND parent_object_id = OBJECT_ID(N'[dbo].[Order]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Order_VAT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_VAT]  DEFAULT ((0)) FOR [VAT]
END


End
GO
/****** Object:  ForeignKey [FK_Customer_Employee]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Customer_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Employee] FOREIGN KEY([SalerId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Customer_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Employee]
GO
/****** Object:  ForeignKey [FK_MaterialInStock_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MaterialInStock_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[MaterialInStock]'))
ALTER TABLE [dbo].[MaterialInStock]  WITH CHECK ADD  CONSTRAINT [FK_MaterialInStock_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MaterialInStock_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[MaterialInStock]'))
ALTER TABLE [dbo].[MaterialInStock] CHECK CONSTRAINT [FK_MaterialInStock_Product]
GO
/****** Object:  ForeignKey [FK_Order_Customer]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Order_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Order]'))
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustId])
REFERENCES [dbo].[Customer] ([CustId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Order_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Order]'))
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
/****** Object:  ForeignKey [FK_Order_SystemUser]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Order_SystemUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Order]'))
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_SystemUser] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[SystemUser] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Order_SystemUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Order]'))
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_SystemUser]
GO
/****** Object:  ForeignKey [FK_OrderDetail_Order]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrderDetail_Order]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderDetail]'))
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrderDetail_Order]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderDetail]'))
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
/****** Object:  ForeignKey [FK_OrderDetail_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrderDetail_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderDetail]'))
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrderDetail_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderDetail]'))
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
/****** Object:  ForeignKey [FK_Price_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Price_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Price]'))
ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Price_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Price]'))
ALTER TABLE [dbo].[Price] CHECK CONSTRAINT [FK_Price_Product]
GO
/****** Object:  ForeignKey [FK_Product_MeasurementUnit]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_MeasurementUnit]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_MeasurementUnit] FOREIGN KEY([BaseUnit])
REFERENCES [dbo].[MeasurementUnit] ([UnitId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_MeasurementUnit]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_MeasurementUnit]
GO
/****** Object:  ForeignKey [FK_Product_ProductType]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_ProductType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductType] FOREIGN KEY([ProductType])
REFERENCES [dbo].[ProductType] ([ProductTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_ProductType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductType]
GO
/****** Object:  ForeignKey [FK_ProductAttribute_Attribute]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttribute_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttribute]'))
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_Attribute] FOREIGN KEY([AttributeId])
REFERENCES [dbo].[Attribute] ([AttributeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttribute_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttribute]'))
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_Attribute]
GO
/****** Object:  ForeignKey [FK_ProductAttribute_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttribute_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttribute]'))
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttribute_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttribute]'))
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_Product]
GO
/****** Object:  ForeignKey [FK_ProductInStock_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductInStock_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductInStock]'))
ALTER TABLE [dbo].[ProductInStock]  WITH CHECK ADD  CONSTRAINT [FK_ProductInStock_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductInStock_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductInStock]'))
ALTER TABLE [dbo].[ProductInStock] CHECK CONSTRAINT [FK_ProductInStock_Product]
GO
/****** Object:  ForeignKey [FK_ProductionRequestDetail_Product]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionRequestDetail_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionRequestDetail]'))
ALTER TABLE [dbo].[ProductionRequestDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductionRequestDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionRequestDetail_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionRequestDetail]'))
ALTER TABLE [dbo].[ProductionRequestDetail] CHECK CONSTRAINT [FK_ProductionRequestDetail_Product]
GO
/****** Object:  ForeignKey [FK_ProductionRequestDetail_ProductionRequest]    Script Date: 07/10/2012 00:53:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionRequestDetail_ProductionRequest]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionRequestDetail]'))
ALTER TABLE [dbo].[ProductionRequestDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductionRequestDetail_ProductionRequest] FOREIGN KEY([ReqId])
REFERENCES [dbo].[ProductionRequest] ([ReqId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionRequestDetail_ProductionRequest]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionRequestDetail]'))
ALTER TABLE [dbo].[ProductionRequestDetail] CHECK CONSTRAINT [FK_ProductionRequestDetail_ProductionRequest]
GO
