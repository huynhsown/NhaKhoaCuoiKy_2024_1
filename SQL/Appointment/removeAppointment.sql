USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[removeApointment]
	@MaLichHen INT
AS
BEGIN
	DELETE FROM [dbo].[LICHHEN] WHERE MaLichHen = @MaLichHen;
END;