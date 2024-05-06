CREATE PROCEDURE getUser
    @Username VARCHAR(255),
    @Password VARCHAR(255)
AS
BEGIN
    SELECT *
    FROM TAIKHOAN
    WHERE TenDangNhap = @Username AND MatKhau = @Password;
END;
