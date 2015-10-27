USE [ArtPostings]
GO

/****** Object:  Table [dbo].[ArtPostingItems]    Script Date: 26/10/2015 11:15:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArtPostingItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Order] [int] NULL,
	[Filename] [nvarchar](100) NULL,
	[Title] [nvarchar](50) NULL,
	[Shortname] [nvarchar](50) NULL,
	[Header] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Size] [nvarchar](50) NULL,
	[Price] [nvarchar](50) NULL,
	[Archive_flag] [bit] NULL,
 CONSTRAINT [PK_ArtPostingItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


