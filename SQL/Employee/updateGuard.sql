CREATE PROCEDURE updateGuard
    @MaNhanVien INT,
    @HoVaTen NVARCHAR(255),
    @GioiTinh NVARCHAR(5),
    @NgaySinh DATE,
    @TienLuong INT,
    @NgayBatDauLamViec DATE,
    @SoNha INT,
    @Phuong NVARCHAR(255),
    @ThanhPho NVARCHAR(255),
    @ViTriLamViec NVARCHAR(255),
    @Anh IMAGE,
    @SoDienThoai NVARCHAR(50),
    @TenDuong NVARCHAR(255)
AS
BEGIN
    UPDATE [dbo].[NHANVIEN]
    SET
        [HoVaTen] = @HoVaTen,
        [GioiTinh] = @GioiTinh,
        [NgaySinh] = @NgaySinh,
        [TienLuong] = @TienLuong,
        [NgayBatDauLamViec] = @NgayBatDauLamViec,
        [SoNha] = @SoNha,
        [Phuong] = @Phuong,
        [ThanhPho] = @ThanhPho,
        [ViTriLamViec] = @ViTriLamViec,
        [Anh] = @Anh,
        [SoDienThoai] = @SoDienThoai,
        [TenDuong] = @TenDuong
    WHERE
        [MaNhanVien] = @MaNhanVien;
END;
