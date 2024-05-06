CREATE PROCEDURE removePatientAndRelatedRecords
    @MaBenhNhan INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Bắt đầu một giao dịch
        BEGIN TRANSACTION;

        -- Xóa các hóa đơn của bệnh nhân
        DELETE FROM [dbo].[HOADON] WHERE [MaBenhNhan] = @MaBenhNhan;

        -- Xóa các thông tin bệnh án của bệnh nhân
        DELETE FROM [dbo].[THONGTINBENHAN] WHERE [MaBenhNhan] = @MaBenhNhan;

        -- Xóa bệnh nhân từ bảng BENHNHAN
        DELETE FROM [dbo].[BENHNHAN] WHERE [MaBenhNhan] = @MaBenhNhan;

        -- Commit giao dịch nếu mọi thứ thành công
        COMMIT TRANSACTION;
        
        PRINT 'Đã xóa bệnh nhân và các thông tin liên quan thành công.';
    END TRY
    BEGIN CATCH
        -- Nếu có lỗi, rollback giao dịch
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- In ra thông báo lỗi
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
