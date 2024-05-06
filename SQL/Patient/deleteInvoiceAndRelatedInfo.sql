CREATE PROCEDURE deleteInvoiceAndRelatedInfo
    @MaHoaDon INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Xóa thông tin dịch vụ
        DELETE FROM THONGTINDICHVU WHERE MaHoaDon = @MaHoaDon;

        -- Xóa thông tin bệnh án
        DELETE FROM THONGTINBENHAN WHERE MaHoaDon = @MaHoaDon;

        -- Xóa hóa đơn
        DELETE FROM HOADON WHERE MaHoaDon = @MaHoaDon;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END;
