CREATE PROCEDURE addInvoice
    @MaBenhNhan INT,
    @NgayThamKham DATE,
	@MaHoaDon INT OUTPUT
AS
BEGIN

    INSERT INTO HOADON (MaBenhNhan, NgayThamKham)
    VALUES (@MaBenhNhan, @NgayThamKham);

    SET @MaHoaDon = SCOPE_IDENTITY();

	SELECT @MaHoaDon AS MaHoaDon;
END;