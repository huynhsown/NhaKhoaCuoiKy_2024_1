USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[addService]
	@MaDichVu INT OUTPUT,
	@MaLoaiDichVu INT,
	@TenDichVu NVARCHAR(255),
	@GiaDichVu INT,
	@GiamGia INT,
	@DonVi INT,
	@BaoHanh INT,
	@ThoiGianThucHien INT,
	@ChiTiet NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO DICHVU VALUES(
							@MaLoaiDichVu,
							@TenDichVu,
							@GiaDichVu,
							@GiamGia,
							@DonVi,
							@BaoHanh,
							@ThoiGianThucHien,
							@ChiTiet
							);
	SET @MaDichVu = SCOPE_IDENTITY();
	RETURN 1;
END