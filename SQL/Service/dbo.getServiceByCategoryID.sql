
CREATE PROCEDURE [dbo].[getServiceByCategoryID]
	@MaLoaiDichVu INT
AS
BEGIN
	SELECT * FROM DICHVU WHERE MaLoaiDichVu = @MaLoaiDichVu;
END;