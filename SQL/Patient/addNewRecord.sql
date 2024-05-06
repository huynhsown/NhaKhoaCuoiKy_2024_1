CREATE PROCEDURE AddNewRecord
	@RecordID INT,
    @PatientID INT,
    @StaffID INT,
    @DentalDisease NVARCHAR(MAX),
    @OtherDisease NVARCHAR(MAX),
    @Symptoms NVARCHAR(MAX),
    @Result NVARCHAR(MAX),
    @Diagnosis NVARCHAR(MAX),
    @TreatmentMethod NVARCHAR(MAX),
    @NextAppointment NVARCHAR(MAX),
    @RecordDate DATETIME
AS
BEGIN
    -- Thêm bản ghi mới và lấy giá trị của MaBenhAn vừa được sinh ra
    INSERT INTO [dbo].[THONGTINBENHAN] (
        [MaBenhNhan],
        [MaNhanVien],
        [BenhLyNhaKhoa],
        [BenhLyKhac],
        [TrieuChung],
        [KetQua],
        [ChanDoan],
        [PhuongPhapDieuTri],
        [LichTrinhTiepTheo],
        [NgayLapBenhAn]
    )
    VALUES (
        @PatientID,
        @StaffID,
        @DentalDisease,
        @OtherDisease,
        @Symptoms,
        @Result,
        @Diagnosis,
        @TreatmentMethod,
        @NextAppointment,
        @RecordDate
    );
    SET @RecordID = SCOPE_IDENTITY();
END;
