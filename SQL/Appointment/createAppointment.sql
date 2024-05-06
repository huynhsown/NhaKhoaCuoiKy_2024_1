USE NHAKHOA
GO

CREATE PROCEDURE [dbo].[createAppointment]
	@MaLichHen INT OUT,
	@MaNhanVien INT,
	@TenKhachHang NVARCHAR(255),
	@SoDienThoaiKhachHang NVARCHAR(50),
	@DiaChi NVARCHAR(255),
	@BatDau DATETIME,
	@ThoiGian INT,
	@NoiDung NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO [dbo].[LICHHEN] VALUES(
									@MaNhanVien,
									@TenKhachHang,
									@SoDienThoaiKhachHang,
									@DiaChi,
									@BatDau,
									@ThoiGian,
									@NoiDung);
	SET @MaLichHen = SCOPE_IDENTITY();
	RETURN 1;
END