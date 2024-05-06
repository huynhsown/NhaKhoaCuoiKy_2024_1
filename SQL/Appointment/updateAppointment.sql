USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[updateAppointment]
	@MaLichHen INT OUT,
	@TenKhachHang NVARCHAR(255),
	@SoDienThoaiKhachHang NVARCHAR(50),
	@DiaChi NVARCHAR(255),
	@BatDau DATETIME,
	@ThoiGian INT,
	@NoiDung NVARCHAR(MAX)
AS
BEGIN
	UPDATE [dbo].[LICHHEN]
	SET 
		TenKhachHang = @TenKhachHang,
		SoDienThoaiKhachHang = @SoDienThoaiKhachHang,
		DiaChi = @DiaChi,
		BatDau = @BatDau,
		ThoiGian = @ThoiGian,
		NoiDung = @NoiDung
	WHERE MaLichHen = @MaLichHen
	RETURN 1;
END