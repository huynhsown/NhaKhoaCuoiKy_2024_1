CREATE PROCEDURE addMedicineInformation
    @MaThuoc NVARCHAR(50),
    @SoLuong INT,
    @NgaySuDung DATETIME,
    @MaHoaDon INT
AS
BEGIN

    INSERT INTO THONGTINSUDUNGTHUOC (MaThuoc, SoLuong, NgaySuDung, MaHoaDon)
    VALUES (@MaThuoc, @SoLuong, @NgaySuDung, @MaHoaDon);
END;
