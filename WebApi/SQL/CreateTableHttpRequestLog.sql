/****** Object:  Table [dbo].[HttpRequestLog]    Script Date: 1/16/2016 11:06:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HttpRequestLog](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserIpAddress] [varchar](50) NOT NULL,
	[HttpAction] [varchar](10) NOT NULL,
	[RequestUrl] [varchar](8000) NOT NULL,
	[RequestHeader] [varchar](max) NULL,
	[RequestBody] [varchar](max) NULL,
	[Response] [varchar](8000) NULL,
	[ResponseBody] [varchar](max) NULL,
	[UserAgent] [varchar](8000) NULL,
	[DeviceInfo] [varchar](8000) NULL,
	[BrowserInfo] [varchar](8000) NULL,
	[IsAnonymous] [bit] NOT NULL CONSTRAINT [DF_HttpRequestLog_IsAnonymous]  DEFAULT ((0)),
	[IsAuthenticated] [bit] NOT NULL CONSTRAINT [DF_HttpRequestLog_IsAuthenticated]  DEFAULT ((0)),
	[IsGuest] [bit] NOT NULL CONSTRAINT [DF_HttpRequestLog_IsGuest]  DEFAULT ((0)),
	[IsSystem] [bit] NOT NULL CONSTRAINT [DF_HttpRequestLog_IsSystem]  DEFAULT ((0)),
	[RequestTimeStamp] [datetime] NOT NULL,
	[ResponseTimeStamp] [datetime] NULL,
	[RequestTotalTime] [time](7) NULL,
 CONSTRAINT [PK_HttpRequestLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

