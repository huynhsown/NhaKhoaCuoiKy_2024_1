CREATE PROCEDURE [dbo].[getGuardByPhoneNum]
	@SoDienThoai VARCHAR(50)
AS
BEGIN
	SELECT * FROM NHANVIEN
	INNER JOIN BAOVE ON NHANVIEN.MaNhanVien = BAOVE.MaNhanVien
	WHERE SoDienThoai = @SoDienThoai;
	RETURN 1;
END