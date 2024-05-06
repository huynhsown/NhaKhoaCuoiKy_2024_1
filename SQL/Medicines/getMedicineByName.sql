CREATE PROCEDURE [dbo].[getMedicineByName]
	@TenThuoc VARCHAR(255)
AS
BEGIN
	SELECT * FROM THUOC
	WHERE TenThuoc LIKE '%' + @TenThuoc + '%';
	RETURN 1;
END