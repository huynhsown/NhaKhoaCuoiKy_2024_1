CREATE PROCEDURE [dbo].[addEmployee]
	@MaNhanVien  INT OUTPUT,
	@HoVaTen	 nVARCHAR(255),
	@GioiTinh    nVARCHAR (5),
	@NgaySinh    DATE,
	@TienLuong	 INT,
	@NgayBatDauLamViec  DATE,
	@SoNha       INT,
	@Phuong      nVARCHAR (255),
    @ThanhPho    nVARCHAR (255),
	@ViTriLamViec NVARCHAR(255),
    @Anh         IMAGE         ,
    @SoDienThoai nVARCHAR (50)
AS
BEGIN
	INSERT INTO NHANVIEN VALUES( 
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
		@SoDienThoai
		);
	SET @MaNhanVien = SCOPE_IDENTITY();
	RETURN 1;
END