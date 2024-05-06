CREATE TABLE [dbo].[THONGTINBENHAN] (
    [MaBenhAn]   INT            IDENTITY (1, 1) NOT NULL,
    [MaBenhNhan] INT            NULL,
    [MaNhanVien] INT            NULL,
	BenhLyNhaKhoa NVARCHAR(MAX),
	BenhLyKhac NVARCHAR(MAX),
	TrieuChung NVARCHAR(MAX),
	KetQua NVARCHAR(MAX),
    ChanDoan NVARCHAR (MAX),
    PhuongPhapDieuTri NVARCHAR(MAX),
	LichTrinhTiepTheo NVARCHAR(MAX),
    PRIMARY KEY CLUSTERED ([MaBenhAn] ASC),
    FOREIGN KEY ([MaBenhNhan]) REFERENCES [dbo].[BENHNHAN] ([MaBenhNhan]),
    FOREIGN KEY ([MaNhanVien]) REFERENCES [dbo].[NHANVIEN] ([MaNhanVien])
);

