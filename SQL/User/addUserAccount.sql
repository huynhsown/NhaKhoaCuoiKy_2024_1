CREATE PROCEDURE addUserAccount
    @TenDangNhap VARCHAR(255),
    @MatKhau VARCHAR(255),
    @Quyen INT,
    @MaNhanVien INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra xem tài khoản có tồn tại hay không
    IF EXISTS (SELECT 1 FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        RAISERROR ('Tài khoản đã tồn tại.', 16, 1);
        RETURN;
    END

    -- Thêm tài khoản mới vào bảng TAIKHOAN
    INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, Quyen, MaNhanVien)
    VALUES (@TenDangNhap, @MatKhau, @Quyen, @MaNhanVien);

    -- Trả về thành công
    SELECT 'Tài khoản đã được thêm thành công.' AS [Message];
END;
