USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[getAllDoctorsByName]
	@Ten NVARCHAR(MAX)
AS
BEGIN
	SELECT * FROM [dbo].[NHANVIEN] join [dbo].[BACSI] on [dbo].[NHANVIEN].MaNhanVien = [dbo].[BACSI].MaNhanVien WHERE [dbo].[NHANVIEN].HoVaTen LIKE '%' + @Ten + '%';
	RETURN 1;
END