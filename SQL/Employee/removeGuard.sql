CREATE PROCEDURE [dbo].[removeGuard]
	@MaNhanVien INT
AS
BEGIN
	DELETE FROM KHUONMAT WHERE MaNhanVien = @MaNhanVien;
	DELETE FROM TAIKHOAN WHERE MaNhanVien = @MaNhanVien;
	DELETE FROM BAOVE WHERE MaNhanVien = @MaNhanVien;
	DELETE FROM NHANVIEN WHERE MaNhanVien = @MaNhanVien;
	RETURN 1;
END