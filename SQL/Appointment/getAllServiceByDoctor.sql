USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[getAllServiceByDoctor]
	@MaBacSi INT
AS
BEGIN
	SELECT [dbo].[THONGTINDICHVU].* FROM [dbo].[THONGTINDICHVU] JOIN [dbo].[BACSI] ON
	[dbo].[THONGTINDICHVU].MaNhanVien = [dbo].[BACSI].MaNhanVien WHERE [dbo].[BACSI].MaNhanVien = @MaBacSi;
END