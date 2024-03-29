USE [CryptoDB]
GO
/****** Object:  Table [dbo].[Chances]    Script Date: 3/8/2018 8:33:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chances](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ExchangeToSell] [varchar](50) NULL,
	[ExchangeToBuy] [varchar](50) NULL,
	[Difference] [float] NULL,
	[SellPrice] [float] NULL,
	[BuyPrice] [float] NULL,
	[Market] [varchar](50) NULL,
 CONSTRAINT [PK_Chances] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CryptoMarkets]    Script Date: 3/8/2018 8:33:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CryptoMarkets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[market] [varchar](50) NULL,
	[price] [varchar](50) NULL,
	[buyPrice] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExchangeDb]    Script Date: 3/8/2018 8:33:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeDb](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[exch_id] [varchar](50) NULL,
	[exch_name] [varchar](50) NULL,
	[exch_code] [varchar](50) NULL,
	[exch_fee] [varchar](50) NULL,
	[exch_trade_enabled] [varchar](50) NULL,
	[exch_balance_enabled] [varchar](50) NULL,
	[exch_url] [varchar](250) NULL,
 CONSTRAINT [PK_ExchangeNew] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarketPairsDistinct]    Script Date: 3/8/2018 8:33:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketPairsDistinct](
	[market] [varchar](50) NULL,
	[count] [int] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_MarketPairsDistinct] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[markets]    Script Date: 3/8/2018 8:33:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[markets](
	[id] [int] NOT NULL,
	[market] [varchar](50) NULL,
 CONSTRAINT [PK_markets] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarketsDb]    Script Date: 3/8/2018 8:33:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketsDb](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[exch_id] [varchar](250) NULL,
	[exch_code] [varchar](50) NULL,
	[mkt_id] [varchar](50) NULL,
	[mkt_name] [varchar](50) NULL,
	[exchmkt_id] [varchar](50) NULL,
 CONSTRAINT [PK_MarketsDb] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceDb]    Script Date: 3/8/2018 8:33:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceDb](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[exchange] [varchar](50) NULL,
	[market] [varchar](50) NULL,
	[last_trade] [varchar](250) NULL,
	[high_trade] [varchar](250) NULL,
	[low_trade] [varchar](250) NULL,
	[current_volume] [varchar](250) NULL,
	[timestamp] [varchar](250) NULL,
	[ask] [varchar](250) NULL,
	[bid] [varchar](250) NULL,
 CONSTRAINT [PK_PriceDb] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
