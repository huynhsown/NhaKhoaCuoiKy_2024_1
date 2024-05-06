CREATE PROCEDURE [dbo].[addNurse]
	@MaNhanVien INT OUT,
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
	INSERT INTO [dbo].[NHANVIEN] VALUES(
			@HoVaTen,
			@GioiTinh,
			@NgaySinh,
			@TienLuong, 
			@NgayBatDauLamViec,
			@SoNha,
			@Phuong,
			@ThanhPho,
			@TenDuong,
			@ViTriLamViec,
			@Anh,
			@SoDienThoai);
	SET @MaNhanVien = SCOPE_IDENTITY();
	INSERT INTO [dbo].[YTA] VALUES ( @HocVi, @ChuyenMon, @MaNhanVien);
	RETURN 1;
END