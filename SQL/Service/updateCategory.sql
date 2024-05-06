CREATE PROCEDURE UpdateCategory
    @CategoryID INT,
    @CategoryName NVARCHAR(MAX)
AS
BEGIN
    UPDATE [dbo].[LOAIDICHVU]
    SET
        [TenLoaiDichVu] = @CategoryName
    WHERE
        [MaLoaiDichVu] = @CategoryID;
END;
