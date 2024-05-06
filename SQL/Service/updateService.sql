CREATE PROCEDURE updateService
    @ServiceID INT,
    @CategoryID INT,
    @Title NVARCHAR(255),
    @Price INT,
    @Discount INT,
    @Warranty INT,
    @Unit INT,
    @Time INT,
    @Detail NVARCHAR(MAX)
AS
BEGIN
    UPDATE [dbo].[DICHVU]
    SET
        [MaLoaiDichVu] = @CategoryID,
        [TenDichVu] = @Title,
        [GiaDichVu] = @Price,
        [GiamGia] = @Discount,
        [DonVi] = @Unit,
        [BaoHanh] = @Warranty,
        [ThoiGianThucHien] = @Time,
        [ChiTiet] = @Detail
    WHERE
        [MaDichVu] = @ServiceID;
END;
