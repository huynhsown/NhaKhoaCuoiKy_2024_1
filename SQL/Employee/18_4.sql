CREATE PROCEDURE [dbo].[addDoctor]
	@MaNhanVien INT OUT,
	@HoVaTen NVARCHAR(255),
	@HocVi NVARCHAR(255),
	@ChuyenMon NVARCHAR(255),
	@GioiTinh NVARCHAR(5),
	@NgaySinh DATE,
	@TienLuong INT, 
	@NgayBatDauLamViec DATE,
	@SoNha INT,
	@Phuong NVARCHAR(255),
	@ThanhPho NVARCHAR(255),
	@ViTriLamViec NVARCHAR(255),
	@Anh IMAGE,
	@SoDienThoai NVARCHAR(255)
AS
BEGIN
	INSERT INTO [dbo].[NHANVIEN] VALUES(
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
			@SoDienThoai);
	SET @MaNhanVien = SCOPE_IDENTITY();
	INSERT INTO [dbo].[BACSI] VALUES ( @HocVi, @ChuyenMon, @MaNhanVien);
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getAllDoctor]
AS
BEGIN
	SELECT NHANVIEN.*, BACSI.HocVi, BACSI.ChuyenMon FROM NHANVIEN
	INNER JOIN BACSI ON NHANVIEN.MaNhanVien = BACSI.MaNhanVien;
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getDoctorByID]
	@MaNhanVien INT OUTPUT
AS
BEGIN
	SELECT * FROM NHANVIEN
	INNER JOIN BACSI ON NHANVIEN.MaNhanVien = BACSI.MaNhanVien
	WHERE NHANVIEN.MaNhanVien = @MaNhanVien
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getDoctorByName]
	@HoVaTen VARCHAR(255)
AS
BEGIN
	SELECT * FROM NHANVIEN
	INNER JOIN BACSI ON NHANVIEN.MaNhanVien = BACSI.MaNhanVien
	WHERE HoVaTen LIKE '%' + @HoVaTen + '%';
	RETURN 1;
END;

CREATE PROCEDURE [dbo].[getDoctorByPhoneNum]
	@SoDienThoai VARCHAR(50)
AS
BEGIN
	SELECT * FROM NHANVIEN
	INNER JOIN BACSI ON NHANVIEN.MaNhanVien = BACSI.MaNhanVien
	WHERE SoDienThoai = @SoDienThoai;
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[removeDoctor]
	@MaNhanVien INT
AS
BEGIN
	DELETE FROM NHANVIEN WHERE MaNhanVien = @MaNhanVien;
	DELETE FROM BACSI WHERE MaNhanVien = @MaNhanVien;
        RETURN 1;
END;


CREATE PROCEDURE [dbo].[addNurse]
	@MaNhanVien INT OUT,
	@HoVaTen NVARCHAR(255),
	@HocVi NVARCHAR(255),
	@ChuyenMon NVARCHAR(255),
	@GioiTinh NVARCHAR(5),
	@NgaySinh DATE,
	@TienLuong INT, 
	@NgayBatDauLamViec DATE,
	@SoNha INT,
	@Phuong NVARCHAR(255),
	@ThanhPho NVARCHAR(255),
	@ViTriLamViec NVARCHAR(255),
	@Anh IMAGE,
	@SoDienThoai NVARCHAR(255)
AS
BEGIN
	INSERT INTO [dbo].[NHANVIEN] VALUES(
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
			@SoDienThoai);
	SET @MaNhanVien = SCOPE_IDENTITY();
	INSERT INTO [dbo].[YTA] VALUES ( @HocVi, @ChuyenMon, @MaNhanVien);
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getAllNurse]
AS
BEGIN
	SELECT NHANVIEN.*, YTA.HocVi, YTA.ChuyenMon FROM NHANVIEN
	INNER JOIN YTA ON NHANVIEN.MaNhanVien = YTA.MaNhanVien;
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getNurseByID]
	@MaNhanVien INT OUTPUT
AS
BEGIN
	SELECT * FROM NHANVIEN 
	INNER JOIN YTA ON NHANVIEN.MaNhanVien = YTA.MaNhanVien
	WHERE NHANVIEN.MaNhanVien = @MaNhanVien
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getNurseByName]
	@HoVaTen VARCHAR(255)
AS
BEGIN
	SELECT * FROM NHANVIEN
	INNER JOIN YTA ON NHANVIEN.MaNhanVien = YTA.MaNhanVien
	WHERE HoVaTen LIKE '%' + @HoVaTen + '%';
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getNurseByPhoneNum]
	@SoDienThoai VARCHAR(50)
AS
BEGIN
	SELECT * FROM NHANVIEN
	INNER JOIN YTA ON NHANVIEN.MaNhanVien = YTA.MaNhanVien
	WHERE SoDienThoai = @SoDienThoai;
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[removeNurse]
	@MaNhanVien INT
AS
BEGIN
	DELETE FROM NHANVIEN WHERE MaNhanVien = @MaNhanVien;
	DELETE FROM YTA WHERE MaNhanVien = @MaNhanVien;
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getAllGuard]
AS
BEGIN
	SELECT * FROM NHANVIEN
	INNER JOIN BAOVE ON NHANVIEN.MaNhanVien = BAOVE.MaNhanVien;
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getGuardByID]
	@MaNhanVien INT OUTPUT
AS
BEGIN
	SELECT * FROM NHANVIEN 
	INNER JOIN BAOVE ON NHANVIEN.MaNhanVien = BAOVE.MaNhanVien
	WHERE NHANVIEN.MaNhanVien = @MaNhanVien
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getGuardByName]
	@HoVaTen VARCHAR(255)
AS
BEGIN
	SELECT * FROM NHANVIEN
	INNER JOIN BAOVE ON NHANVIEN.MaNhanVien = BAOVE.MaNhanVien
	WHERE HoVaTen LIKE '%' + @HoVaTen + '%';
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getGuardByPhoneNum]
	@SoDienThoai VARCHAR(50)
AS
BEGIN
	SELECT * FROM NHANVIEN
	INNER JOIN BAOVE ON NHANVIEN.MaNhanVien = BAOVE.MaNhanVien
	WHERE SoDienThoai = @SoDienThoai;
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[removeGuard]
	@MaNhanVien INT
AS
BEGIN
	DELETE FROM NHANVIEN WHERE MaNhanVien = @MaNhanVien;
	DELETE FROM BAOVE WHERE MaNhanVien = @MaNhanVien;
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[addMedicine]
	@MaThuoc NVARCHAR(255) OUT,
	@TenThuoc NVARCHAR(255),
	@HuongDanSD NVARCHAR(255),
	@ThanhPhan NVARCHAR(255),
	@GiaNhap INT, 
	@GiaBan INT,
	@SoLuong INT,
	@CongTy NVARCHAR(255)
	
AS
BEGIN
	INSERT INTO [dbo].[THUOC] VALUES(
			@TenThuoc,
			@HuongDanSD,
			@ThanhPhan,
			@GiaNhap, 
			@GiaBan,
			@SoLuong,
			@CongTy);
	SET @MaThuoc = SCOPE_IDENTITY();
	RETURN 1;
END;

CREATE PROCEDURE [dbo].[getMedicineByID]
	@MaThuoc NVARCHAR(50) OUTPUT
AS
BEGIN
	SELECT * FROM THUOC 
	WHERE MaThuoc = @MaThuoc
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getMedicineByName]
	@TenThuoc VARCHAR(255)
AS
BEGIN
	SELECT * FROM THUOC
	WHERE TenThuoc LIKE '%' + @TenThuoc + '%';
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[removeMedicine]
	@MaThuoc INT
AS
BEGIN
	DELETE FROM THUOC WHERE MaThuoc = @MaThuoc;
	RETURN 1;
END;


CREATE PROCEDURE [dbo].[getAllMedicine]
AS
BEGIN
	SELECT * FROM THUOC
	RETURN 1;
END;

