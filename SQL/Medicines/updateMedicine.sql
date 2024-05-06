CREATE PROCEDURE updateMedicine
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
    UPDATE [dbo].[THUOC]
    SET
        [TenThuoc] = @TenThuoc,
        [HuongDanSD] = @HuongDanSD,
        [ThanhPhan] = @ThanhPhan,
        [GiaNhap] = @GiaNhap,
        [GiaBan] = @GiaBan,
        [SoLuong] = @SoLuong,
        [CongTy] = @CongTy
        
    WHERE
        [MaThuoc] = @MaThuoc;
    


END;