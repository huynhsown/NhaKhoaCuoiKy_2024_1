USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[getAllAppointmentByDoctor]
	@MaBacSi INT,
	@Ngay DATE
AS
BEGIN
	SELECT [dbo].[LICHHEN].* FROM [dbo].[LICHHEN] JOIN [dbo].[BACSI] ON
	[dbo].[LICHHEN].MaNhanVien = [dbo].[BACSI].MaNhanVien 
	WHERE [dbo].[BACSI].MaNhanVien = @MaBacSi AND CAST([dbo].[LICHHEN].BatDau AS DATE) = @Ngay;
END