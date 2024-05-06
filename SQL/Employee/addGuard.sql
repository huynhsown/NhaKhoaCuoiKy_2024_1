USE NHAKHOA
GO
CREATE PROCEDURE addGuard
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
    INSERT INTO [dbo].[NHANVIEN] (
        [HoVaTen],
        [GioiTinh],
        [NgaySinh],
        [TienLuong],
        [NgayBatDauLamViec],
        [SoNha],
        [Phuong],
        [ThanhPho],
        [ViTriLamViec],
        [Anh],
        [SoDienThoai],
        [TenDuong]
    )
    VALUES (
        @HoVaTen,
        @GioiTinh,
        @NgaySinh,
        @TienLuong,
        @NgayBatDauLamViec,
        @SoNha,
        @Phuong,
        @ThanhPho,
        @ViTriLamViec,
        @Anh,
        @SoDienThoai,
        @TenDuong
    );
    SET @MaNhanVien = SCOPE_IDENTITY();
    INSERT INTO BAOVE VALUES(@MaNhanVien);
END;