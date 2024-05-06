CREATE PROCEDURE updateDoctor
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
    @SoDienThoai NVARCHAR(50),
    @TenDuong NVARCHAR(255),
    @HocVi NVARCHAR(255),
    @ChuyenMon NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Cập nhật thông tin trong bảng NHANVIEN
    UPDATE NHANVIEN
    SET 
        HoVaTen = @HoVaTen,
        GioiTinh = @GioiTinh,
        NgaySinh = @NgaySinh,
        TienLuong = @TienLuong,
        NgayBatDauLamViec = @NgayBatDauLamViec,
        SoNha = @SoNha,
        Phuong = @Phuong,
        ThanhPho = @ThanhPho,
        ViTriLamViec = @ViTriLamViec,
        SoDienThoai = @SoDienThoai,
        TenDuong = @TenDuong
    WHERE
        MaNhanVien = @MaNhanVien;

    -- Cập nhật thông tin trong bảng BACSI
    UPDATE BACSI
    SET
        HocVi = @HocVi,
        ChuyenMon = @ChuyenMon
    WHERE
        MaNhanVien = @MaNhanVien;
END;
