USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[addPatient]
	@MaBenhNhan INT OUTPUT,
	@HoVaTen nVARCHAR(255),
	@GioiTinh    nVARCHAR (5),
	@NgaySinh    DATE,
	@SoNha       INT,
	@Phuong    nVARCHAR (255),
    @ThanhPho    nVARCHAR (255),
    @Anh         IMAGE         ,
    @SoDienThoai nVARCHAR (50),
    @TenDuong    nVARCHAR (255)
AS
BEGIN
	INSERT INTO BENHNHAN VALUES( 
		@HoVaTen, 
		@GioiTinh, 
		@NgaySinh, 
		@SoNha, 
		@Phuong, 
		@ThanhPho,
		@TenDuong, 
		@Anh, 
		@SoDienThoai
		);
	SET @MaBenhNhan = SCOPE_IDENTITY();
	RETURN 1;
END