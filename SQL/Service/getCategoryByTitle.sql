USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[getCategoryByTitle]
	@LoaiDichVu NVARCHAR(MAX)
AS
BEGIN
	SELECT * FROM [dbo].[LOAIDICHVU] 
		WHERE TenLoaiDichVu LIKE '%' + @LoaiDichVu + '%';
	RETURN 1;
END