/****** Object:  StoredProcedure [dbo].[uspInsertRequestLog]    Script Date: 1/16/2016 11:09:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspInsertRequestLog]
	@Id UNIQUEIDENTIFIER,
	@UserName VARCHAR(50),
	@UserIpAddress VARCHAR(50),
	@HttpAction VARCHAR(10),
	@RequestUrl VARCHAR(8000),
	@RequestHeader VARCHAR(MAX),
	@RequestBody VARCHAR(MAX),
	@UserAgent VARCHAR(8000),
	@DeviceInfo VARCHAR(8000),
	@BrowserInfo VARCHAR(8000),
	@IsAnonymous bit,
	@IsAuthenticated bit,
	@IsGuest bit,
	@IsSystem bit,
	@RequestTimeStamp DATETIME
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.HttpRequestLog
            ( Id ,
              UserName ,
              UserIpAddress ,
              HttpAction ,
              RequestUrl ,
			  RequestHeader,
              RequestBody ,
              UserAgent ,
              DeviceInfo ,
              BrowserInfo ,
              IsAnonymous ,
              IsAuthenticated ,
              IsGuest ,
              IsSystem ,
              RequestTimeStamp
            )
    VALUES  ( 
			  @Id, -- Id - uniqueidentifier
              @UserName, -- UserName - varchar(50)
              @UserIpAddress, -- UserIpAddress - varchar(50)
              @HttpAction, -- HttpAction - varchar(10)
              @RequestUrl, -- RequestUrl - varchar(8000)
			  @RequestHeader, -- RequestHeader - varchar(max)
              @RequestBody, -- RequestBody - varchar(max)
              @UserAgent, -- UserAgent - varchar(8000)
              @DeviceInfo, -- DeviceInfo - varchar(8000)
              @BrowserInfo, -- BrowserInfo - varchar(8000)
              @IsAnonymous, -- IsAnonymous - bit
              @IsAuthenticated, -- IsAuthenticated - bit
              @IsGuest, -- IsGuest - bit
              @IsSystem, -- IsSystem - bit
              @RequestTimeStamp -- RequestTimeStamp - datetime
            )
END

GO

