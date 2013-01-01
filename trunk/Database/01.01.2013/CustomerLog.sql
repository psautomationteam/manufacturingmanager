USE [BaoHienCompany]
GO
/****** Object:  Table [dbo].[CustomerLog]    Script Date: 01/01/2013 21:40:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[RecordCode] [varchar](25) NOT NULL,
	[BeforeDebit] [float] NOT NULL,
	[Amount] [float] NOT NULL,
	[AfterDebit] [float] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_CustomerLog_BeforeDebit]    Script Date: 01/01/2013 21:40:21 ******/
ALTER TABLE [dbo].[CustomerLog] ADD  CONSTRAINT [DF_CustomerLog_BeforeDebit]  DEFAULT ((0)) FOR [BeforeDebit]
GO
/****** Object:  Default [DF_CustomerLog_Amount]    Script Date: 01/01/2013 21:40:21 ******/
ALTER TABLE [dbo].[CustomerLog] ADD  CONSTRAINT [DF_CustomerLog_Amount]  DEFAULT ((0)) FOR [Amount]
GO
/****** Object:  Default [DF_CustomerLog_AfterDebit]    Script Date: 01/01/2013 21:40:21 ******/
ALTER TABLE [dbo].[CustomerLog] ADD  CONSTRAINT [DF_CustomerLog_AfterDebit]  DEFAULT ((0)) FOR [AfterDebit]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Customer]    Script Date: 01/01/2013 21:40:21 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_Customer]
GO
