/****** Object:  StoredProcedure [dbo].[uspFinalizeRequestLog]    Script Date: 1/16/2016 11:11:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspFinalizeRequestLog]
	@Id UNIQUEIDENTIFIER,
	@Response VARCHAR(8000),
	@ResponseBody VARCHAR(MAX), 
	@ResponseTimeStamp DateTime,
	@RequestTotalTime Time
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE dbo.HttpRequestLog
	SET
		Response = @Response,
		ResponseBody = @ResponseBody,
		ResponseTimeStamp = @ResponseTimeStamp,
		RequestTotalTime = @RequestTotalTime
	WHERE
		Id = @Id
END

GO

