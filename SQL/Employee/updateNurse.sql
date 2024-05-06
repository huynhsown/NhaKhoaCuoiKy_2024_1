CREATE PROCEDURE updateNurse
    @MaNhanVien INT,
	@HoVaTen NVARCHAR(255),
	@HocVi NVARCHAR(255),
	@ChuyenMon NVARCHAR(255),
	@GioiTinh NVARCHAR(5),
	@NgaySinh DATE,
	@TienLuong INT, 
	@NgayBatDauLamViec DATE,
	@SoNha INT,
	@Phuong NVARCHAR(255),
	@TenDuong NVARCHAR(255),
	@ThanhPho NVARCHAR(255),
	@ViTriLamViec NVARCHAR(255),
	@Anh IMAGE,
	@SoDienThoai NVARCHAR(50)
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
        [TenDuong] = @TenDuong,
        [ThanhPho] = @ThanhPho,
        [ViTriLamViec] = @ViTriLamViec,
        [Anh] = @Anh,
        [SoDienThoai] = @SoDienThoai
    WHERE
        [MaNhanVien] = @MaNhanVien;
    UPDATE [dbo].[YTA]
    SET
        [HocVi] = @HocVi,
        [ChuyenMon] = @ChuyenMon
    WHERE 
        [MaNhanVien] = @MaNhanVien;


END;