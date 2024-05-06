CREATE PROCEDURE [dbo].[addMedicine]
	@MaThuoc NVARCHAR(50),
	@TenThuoc NVARCHAR(255),
	@HuongDanSD NVARCHAR(MAX),
	@ThanhPhan NVARCHAR(MAX),
	@GiaNhap INT, 
	@GiaBan INT,
	@SoLuong INT,
	@CongTy NVARCHAR(50)
AS
BEGIN
	INSERT INTO [dbo].[THUOC] VALUES(
			@MaThuoc,
			@TenThuoc,
			@HuongDanSD,
			@ThanhPhan,
			@GiaNhap, 
			@GiaBan,
			@SoLuong,
			@CongTy);
	RETURN 1;
END;