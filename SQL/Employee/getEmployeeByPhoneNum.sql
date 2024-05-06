CREATE PROCEDURE [dbo].[getEmployeeByPhoneNum]
	@SoDienThoai VARCHAR(50)
AS
BEGIN
	SELECT * FROM NHANVIEN WHERE SoDienThoai = @SoDienThoai;
	RETURN 1;
END