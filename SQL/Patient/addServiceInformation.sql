CREATE PROCEDURE addServiceInformation
    @MaNhanVien INT,
    @NgaySuDung DATETIME,
    @SoLuong INT,
    @ThoiGianThucHien INT,
    @MaDichVu INT,
    @MaHoaDon INT
AS
BEGIN
    INSERT INTO THONGTINDICHVU (MaNhanVien, NgaySuDung, SoLuong, ThoiGianThucHien, MaDichVu, MaHoaDon)
    VALUES (@MaNhanVien, @NgaySuDung, @SoLuong, @ThoiGianThucHien, @MaDichVu, @MaHoaDon);
END;
