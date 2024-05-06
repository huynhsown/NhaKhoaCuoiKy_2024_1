USE NHAKHOA
GO
CREATE PROCEDURE addNurse
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
    @TenDuong NVARCHAR(255),
    @HocVi NVARCHAR(255),
    @ChuyenMon NVARCHAR(255)
AS
BEGIN
    DECLARE @MaNhanVien INT;

    -- Thêm vào bảng NHANVIEN
    INSERT INTO NHANVIEN (HoVaTen, GioiTinh, NgaySinh, TienLuong, NgayBatDauLamViec, SoNha, Phuong, ThanhPho, ViTriLamViec, Anh, SoDienThoai, TenDuong)
    VALUES (@HoVaTen, @GioiTinh, @NgaySinh, @TienLuong, @NgayBatDauLamViec, @SoNha, @Phuong, @ThanhPho, @ViTriLamViec, @Anh, @SoDienThoai, @TenDuong);

    -- Lấy MaNhanVien vừa thêm vào
    SET @MaNhanVien = SCOPE_IDENTITY();

    -- Thêm vào bảng YTA
    INSERT INTO YTA (HocVi, ChuyenMon, MaNhanVien)
    VALUES (@HocVi, @ChuyenMon, @MaNhanVien);
END;
