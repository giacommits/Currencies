USE [CurrenciesDb]
GO
/****** Object:  Table [dbo].[Currencies]    Script Date: 12/05/2020 19:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currencies](
	[CurrencyId] [int] IDENTITY(1,1) NOT NULL,
	[ISO_Code] [varchar](3) NULL,
	[Name] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CurrenciesExchangeRate]    Script Date: 12/05/2020 19:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrenciesExchangeRate](
	[CurrenciesExchangeRateId] [int] IDENTITY(1,1) NOT NULL,
	[BaseCurrencyId] [int] NOT NULL,
	[QuoteCurrencyId] [int] NOT NULL,
	[RateDate] [date] NOT NULL,
	[ExchangeRate] [decimal](20, 10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CurrenciesExchangeRateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Currencies] ON 

INSERT [dbo].[Currencies] ([CurrencyId], [ISO_Code], [Name]) VALUES (1, N'CAD', N'Canadian Dollar')
INSERT [dbo].[Currencies] ([CurrencyId], [ISO_Code], [Name]) VALUES (2, N'HKD', N'Hong Kong Dollar')
INSERT [dbo].[Currencies] ([CurrencyId], [ISO_Code], [Name]) VALUES (3, N'EUR', N'Euro')
INSERT [dbo].[Currencies] ([CurrencyId], [ISO_Code], [Name]) VALUES (4, N'USD', N'United States Dollar')
INSERT [dbo].[Currencies] ([CurrencyId], [ISO_Code], [Name]) VALUES (5, N'MXN', N'Mexican Peso')
SET IDENTITY_INSERT [dbo].[Currencies] OFF
SET IDENTITY_INSERT [dbo].[CurrenciesExchangeRate] ON 

INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (2, 1, 2, CAST(N'2020-05-09' AS Date), CAST(5.5597000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (4, 1, 2, CAST(N'2020-05-08' AS Date), CAST(5.6785000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (5, 1, 3, CAST(N'2020-05-08' AS Date), CAST(0.6614000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (6, 1, 4, CAST(N'2020-05-08' AS Date), CAST(0.7172000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (7, 1, 5, CAST(N'2020-05-08' AS Date), CAST(17.1334000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (8, 2, 1, CAST(N'2020-05-08' AS Date), CAST(0.1798000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (9, 2, 3, CAST(N'2020-05-08' AS Date), CAST(0.1189000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (10, 2, 4, CAST(N'2020-05-08' AS Date), CAST(0.1290000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (11, 2, 5, CAST(N'2020-05-08' AS Date), CAST(3.0816000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (12, 3, 1, CAST(N'2020-05-08' AS Date), CAST(1.5118000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (13, 3, 2, CAST(N'2020-05-08' AS Date), CAST(8.4052000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (14, 3, 4, CAST(N'2020-05-08' AS Date), CAST(1.0843000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (15, 3, 5, CAST(N'2020-05-08' AS Date), CAST(25.9023000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (16, 4, 1, CAST(N'2020-05-08' AS Date), CAST(1.3942000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (17, 4, 2, CAST(N'2020-05-08' AS Date), CAST(7.7517000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (18, 4, 3, CAST(N'2020-05-08' AS Date), CAST(0.9222000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (19, 4, 5, CAST(N'2020-05-08' AS Date), CAST(23.8884000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (20, 5, 1, CAST(N'2020-05-08' AS Date), CAST(0.0583000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (21, 5, 2, CAST(N'2020-05-08' AS Date), CAST(0.3244000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (22, 5, 3, CAST(N'2020-05-08' AS Date), CAST(0.0338600000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (23, 5, 4, CAST(N'2020-05-08' AS Date), CAST(0.0418000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (24, 1, 3, CAST(N'2020-05-09' AS Date), CAST(0.6532000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (25, 1, 4, CAST(N'2020-05-09' AS Date), CAST(0.7341000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (26, 1, 5, CAST(N'2020-05-09' AS Date), CAST(17.5436000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (27, 2, 1, CAST(N'2020-05-09' AS Date), CAST(0.1856000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (28, 2, 3, CAST(N'2020-05-09' AS Date), CAST(0.1039000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (29, 2, 4, CAST(N'2020-05-09' AS Date), CAST(0.1103000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (30, 2, 5, CAST(N'2020-05-09' AS Date), CAST(3.1396000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (31, 3, 1, CAST(N'2020-05-09' AS Date), CAST(1.4312000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (32, 3, 2, CAST(N'2020-05-09' AS Date), CAST(8.5413000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (33, 3, 4, CAST(N'2020-05-09' AS Date), CAST(1.1236000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (34, 3, 5, CAST(N'2020-05-09' AS Date), CAST(26.7819000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (35, 4, 1, CAST(N'2020-05-09' AS Date), CAST(0.0631000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (36, 4, 2, CAST(N'2020-05-09' AS Date), CAST(0.3010000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (37, 4, 3, CAST(N'2020-05-09' AS Date), CAST(0.8513000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (38, 4, 5, CAST(N'2020-05-09' AS Date), CAST(23.9564000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (39, 5, 1, CAST(N'2020-05-09' AS Date), CAST(0.0631000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (40, 5, 2, CAST(N'2020-05-09' AS Date), CAST(0.3010000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (41, 5, 3, CAST(N'2020-05-09' AS Date), CAST(0.4567000000 AS Decimal(20, 10)))
INSERT [dbo].[CurrenciesExchangeRate] ([CurrenciesExchangeRateId], [BaseCurrencyId], [QuoteCurrencyId], [RateDate], [ExchangeRate]) VALUES (42, 5, 4, CAST(N'2020-05-09' AS Date), CAST(0.0589000000 AS Decimal(20, 10)))
SET IDENTITY_INSERT [dbo].[CurrenciesExchangeRate] OFF
/****** Object:  Index [unq_currencyexchangerate_3]    Script Date: 12/05/2020 19:41:51 ******/
ALTER TABLE [dbo].[CurrenciesExchangeRate] ADD  CONSTRAINT [unq_currencyexchangerate_3] UNIQUE NONCLUSTERED 
(
	[BaseCurrencyId] ASC,
	[QuoteCurrencyId] ASC,
	[RateDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CurrenciesExchangeRate]  WITH CHECK ADD FOREIGN KEY([BaseCurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
GO
ALTER TABLE [dbo].[CurrenciesExchangeRate]  WITH CHECK ADD FOREIGN KEY([QuoteCurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
GO
