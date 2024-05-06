CREATE PROCEDURE addMedicineInformation
    @MaThuoc NVARCHAR(50),
    @SoLuong INT,
    @NgaySuDung DATETIME,
    @MaHoaDon INT
AS
BEGIN
    BEGIN TRANSACTION;
    DECLARE @SoLuongHienTai INT;

    -- Lấy số lượng hiện tại của thuốc
    SELECT @SoLuongHienTai = SoLuong FROM THUOC WHERE MaThuoc = @MaThuoc;

    -- Kiểm tra xem có đủ thuốc để giảm số lượng không
    IF @SoLuongHienTai >= @SoLuong
    BEGIN
        -- Giảm số lượng của thuốc
        UPDATE THUOC SET SoLuong = SoLuong - @SoLuong WHERE MaThuoc = @MaThuoc;

        -- Thêm thông tin sử dụng thuốc
        INSERT INTO THONGTINSUDUNGTHUOC (MaThuoc, SoLuong, NgaySuDung, MaHoaDon)
        VALUES (@MaThuoc, @SoLuong, @NgaySuDung, @MaHoaDon);

        COMMIT;
    END
    ELSE
    BEGIN
        -- Không đủ số lượng để giảm
        ROLLBACK;
        THROW 51000, 'Số lượng thuốc không đủ', 1;
    END
END;
