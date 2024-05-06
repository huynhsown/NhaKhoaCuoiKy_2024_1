USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[getAllDoctorsByID]
	@MaBacSi INT
AS
BEGIN
	SELECT * FROM [dbo].[NHANVIEN] join [dbo].[BACSI] on [dbo].[NHANVIEN].MaNhanVien = [dbo].[BACSI].MaNhanVien WHERE [dbo].[BACSI].MaNhanVien = @MaBacSi;
	RETURN 1;
END