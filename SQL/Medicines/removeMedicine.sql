CREATE PROCEDURE [dbo].[removeMedicine]
	@MaThuoc NVARCHAR(50)
AS
BEGIN
	DELETE FROM THUOC WHERE MaThuoc = @MaThuoc;
	RETURN 1;
END