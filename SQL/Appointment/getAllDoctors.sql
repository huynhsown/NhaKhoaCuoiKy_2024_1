USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[getAllDoctors]
AS
BEGIN
	SELECT * FROM [dbo].[NHANVIEN] join [dbo].[BACSI] on [dbo].[NHANVIEN].MaNhanVien = [dbo].[BACSI].MaNhanVien;
	RETURN 1;
END