USE [master]
GO
/****** Object:  Database [DoAnWeb]    Script Date: 09/05/2023 16:30:02 ******/
CREATE DATABASE [DoAnWeb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DoAnWeb3', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DoAnWeb3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DoAnWeb3_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DoAnWeb3_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DoAnWeb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DoAnWeb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DoAnWeb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DoAnWeb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DoAnWeb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DoAnWeb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DoAnWeb] SET ARITHABORT OFF 
GO
ALTER DATABASE [DoAnWeb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DoAnWeb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DoAnWeb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DoAnWeb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DoAnWeb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DoAnWeb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DoAnWeb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DoAnWeb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DoAnWeb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DoAnWeb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DoAnWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DoAnWeb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DoAnWeb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DoAnWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DoAnWeb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DoAnWeb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DoAnWeb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DoAnWeb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DoAnWeb] SET  MULTI_USER 
GO
ALTER DATABASE [DoAnWeb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DoAnWeb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DoAnWeb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DoAnWeb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DoAnWeb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DoAnWeb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DoAnWeb] SET QUERY_STORE = OFF
GO
USE [DoAnWeb]
GO
/****** Object:  Table [dbo].[AnhSP]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnhSP](
	[MaSP] [bigint] NOT NULL,
	[MaMau] [bigint] NOT NULL,
	[Anh] [nvarchar](max) NULL,
 CONSTRAINT [PK_AnhSP] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC,
	[MaMau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatLieu]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatLieu](
	[MaCL] [bigint] IDENTITY(1,1) NOT NULL,
	[TenChatLieu] [nvarchar](max) NULL,
 CONSTRAINT [PK_ChatLieu] PRIMARY KEY CLUSTERED 
(
	[MaCL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[MaCV] [int] IDENTITY(1,1) NOT NULL,
	[TenCV] [nvarchar](max) NULL,
 CONSTRAINT [PK_ChucVu] PRIMARY KEY CLUSTERED 
(
	[MaCV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[MaCMT] [bigint] IDENTITY(1,1) NOT NULL,
	[MaTK] [bigint] NULL,
	[MaSP] [bigint] NOT NULL,
	[DanhGia] [int] NULL,
	[CMT] [nvarchar](256) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Comment_1] PRIMARY KEY CLUSTERED 
(
	[MaCMT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CT_HoaDon]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CT_HoaDon](
	[MaHD] [bigint] NOT NULL,
	[MaSP] [bigint] NOT NULL,
	[MaCL] [bigint] NULL,
	[MaSize] [int] NULL,
	[MaMau] [bigint] NULL,
	[SoLuong] [bigint] NULL,
	[ThanhTien] [bigint] NULL,
 CONSTRAINT [PK_CT_HoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GioHang]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioHang](
	[MaTK] [bigint] NOT NULL,
	[MaSP] [bigint] NOT NULL,
	[MaCL] [bigint] NULL,
	[MaSize] [int] NOT NULL,
	[MaMau] [bigint] NOT NULL,
	[SoLuong] [bigint] NULL,
	[ThanhTien] [bigint] NULL,
 CONSTRAINT [PK_GioHang] PRIMARY KEY CLUSTERED 
(
	[MaTK] ASC,
	[MaSP] ASC,
	[MaSize] ASC,
	[MaMau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GioiTinh]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioiTinh](
	[MaGT] [int] IDENTITY(1,1) NOT NULL,
	[GioiTinh] [nvarchar](50) NULL,
 CONSTRAINT [PK_GioiTinh] PRIMARY KEY CLUSTERED 
(
	[MaGT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHD] [bigint] IDENTITY(1,1) NOT NULL,
	[MaTK] [bigint] NOT NULL,
	[NgayMua] [datetime] NULL,
	[MaThe] [bigint] NULL,
	[DiaChi] [nvarchar](max) NULL,
	[TongTien] [bigint] NULL,
	[TrangThai] [int] NULL,
	[SDTKH] [varchar](10) NULL,
 CONSTRAINT [PK_HoaDon_1] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiSP]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSP](
	[MaLoaiSP] [bigint] IDENTITY(1,1) NOT NULL,
	[TenLoaiSP] [nvarchar](max) NULL,
	[Anh] [nvarchar](max) NULL,
 CONSTRAINT [PK_LoaiSP] PRIMARY KEY CLUSTERED 
(
	[MaLoaiSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mau]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mau](
	[MaMau] [bigint] IDENTITY(1,1) NOT NULL,
	[TenMau] [nvarchar](50) NULL,
	[Anh] [nvarchar](max) NULL,
 CONSTRAINT [PK_Mau] PRIMARY KEY CLUSTERED 
(
	[MaMau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NCC]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NCC](
	[MaNCC] [bigint] IDENTITY(1,1) NOT NULL,
	[TenNCC] [nvarchar](max) NULL,
	[DiaChi] [nvarchar](max) NULL,
	[SDT] [nvarchar](12) NULL,
	[Email] [nvarchar](max) NULL,
 CONSTRAINT [PK_NCC] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RepCMT]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RepCMT](
	[MaRep] [bigint] IDENTITY(1,1) NOT NULL,
	[MaCMT] [bigint] NOT NULL,
	[MaTK] [bigint] NOT NULL,
	[NoiDung] [nvarchar](256) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_RepCMT] PRIMARY KEY CLUSTERED 
(
	[MaRep] ASC,
	[MaCMT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [bigint] IDENTITY(1,1) NOT NULL,
	[TenSP] [nvarchar](max) NULL,
	[DonGia] [bigint] NULL,
	[MaLoaiSP] [bigint] NULL,
	[MaCL] [bigint] NULL,
	[MaNCC] [bigint] NULL,
	[MaTH] [bigint] NULL,
	[MaGT] [int] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[MaSize] [int] IDENTITY(1,1) NOT NULL,
	[TenSize] [nvarchar](10) NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[MaSize] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SoLuong]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SoLuong](
	[MaSP] [bigint] NOT NULL,
	[MaMau] [bigint] NOT NULL,
	[MaSize] [int] NOT NULL,
	[SoLuong] [bigint] NULL,
 CONSTRAINT [PK_SoLuong] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC,
	[MaMau] ASC,
	[MaSize] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[MaTK] [bigint] IDENTITY(1,1) NOT NULL,
	[TenDN] [varchar](20) NULL,
	[MatKhau] [varchar](20) NULL,
	[MaCV] [int] NULL,
	[TenNguoiDung] [nvarchar](max) NULL,
	[SDT] [nvarchar](12) NULL,
	[DiaChi] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Anh] [nvarchar](max) NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[MaTK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThanhToanOnline]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThanhToanOnline](
	[MaThe] [bigint] IDENTITY(1,1) NOT NULL,
	[TenThe] [nvarchar](max) NULL,
	[Anh] [nvarchar](max) NULL,
 CONSTRAINT [PK_ThanhToanOnline] PRIMARY KEY CLUSTERED 
(
	[MaThe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThuongHieu]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThuongHieu](
	[MaTH] [bigint] IDENTITY(1,1) NOT NULL,
	[TenTH] [varchar](max) NULL,
 CONSTRAINT [PK_ThuongHieu] PRIMARY KEY CLUSTERED 
(
	[MaTH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangThai]    Script Date: 09/05/2023 16:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangThai](
	[MaTrangThai] [int] IDENTITY(1,1) NOT NULL,
	[TenTrangThai] [nvarchar](50) NULL,
 CONSTRAINT [PK_TrangThai] PRIMARY KEY CLUSTERED 
(
	[MaTrangThai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (18, 2, N'a1.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (19, 1, N'a3.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (19, 2, N'a2.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (20, 2, N'a5.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (20, 10, N'a4.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (21, 3, N'a8.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (21, 4, N'a7.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (21, 10, N'a6.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (22, 2, N'a12.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (22, 4, N'a11.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (22, 11, N'a9.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (23, 1, N'aa.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (23, 2, N'q.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (23, 4, N'aaa.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (24, 2, N'a33.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (25, 2, N'qq.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (26, 2, N'w.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (27, 8, N'ee.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (28, 1, N'qqq.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (29, 2, N'aaaa.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (30, 3, N'qs.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (31, 11, N'ss.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (32, 4, N'sss.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (33, 4, N'vv.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (34, 2, N'vvv.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (35, 4, N'as.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (36, 2, N'v1.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (37, 2, N'v3.png')
INSERT [dbo].[AnhSP] ([MaSP], [MaMau], [Anh]) VALUES (38, 2, N'v4.png')
GO
SET IDENTITY_INSERT [dbo].[ChatLieu] ON 

INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (1, N'Cotton')
INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (2, N'Phông')
INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (4, N'Lụa')
INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (5, N'Thun')
INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (6, N'Len')
INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (7, N'Vải')
INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (8, N'Lót lông')
INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (9, N'Bò')
INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (10, N'Da')
INSERT [dbo].[ChatLieu] ([MaCL], [TenChatLieu]) VALUES (11, N'Nhung')
SET IDENTITY_INSERT [dbo].[ChatLieu] OFF
GO
SET IDENTITY_INSERT [dbo].[ChucVu] ON 

INSERT [dbo].[ChucVu] ([MaCV], [TenCV]) VALUES (1, N'Admin')
INSERT [dbo].[ChucVu] ([MaCV], [TenCV]) VALUES (2, N'Thường')
SET IDENTITY_INSERT [dbo].[ChucVu] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([MaCMT], [MaTK], [MaSP], [DanhGia], [CMT], [Date]) VALUES (1, 1, 18, 0, N'San pham dung tot', CAST(N'2023-05-04T01:13:47.447' AS DateTime))
INSERT [dbo].[Comment] ([MaCMT], [MaTK], [MaSP], [DanhGia], [CMT], [Date]) VALUES (2, 1, 18, 0, N'Good', CAST(N'2023-05-04T02:12:04.333' AS DateTime))
INSERT [dbo].[Comment] ([MaCMT], [MaTK], [MaSP], [DanhGia], [CMT], [Date]) VALUES (3, 1, 18, 0, N'Butt', CAST(N'2023-05-04T02:22:51.497' AS DateTime))
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
INSERT [dbo].[CT_HoaDon] ([MaHD], [MaSP], [MaCL], [MaSize], [MaMau], [SoLuong], [ThanhTien]) VALUES (49, 22, 1, 1, 4, 1, 159000)
INSERT [dbo].[CT_HoaDon] ([MaHD], [MaSP], [MaCL], [MaSize], [MaMau], [SoLuong], [ThanhTien]) VALUES (50, 18, 10, 3, 2, 1, 390000)
INSERT [dbo].[CT_HoaDon] ([MaHD], [MaSP], [MaCL], [MaSize], [MaMau], [SoLuong], [ThanhTien]) VALUES (50, 19, 10, 2, 1, 1, 490000)
INSERT [dbo].[CT_HoaDon] ([MaHD], [MaSP], [MaCL], [MaSize], [MaMau], [SoLuong], [ThanhTien]) VALUES (50, 21, 1, 2, 4, 1, 259000)
INSERT [dbo].[CT_HoaDon] ([MaHD], [MaSP], [MaCL], [MaSize], [MaMau], [SoLuong], [ThanhTien]) VALUES (50, 23, 1, 1, 1, 1, 99000)
INSERT [dbo].[CT_HoaDon] ([MaHD], [MaSP], [MaCL], [MaSize], [MaMau], [SoLuong], [ThanhTien]) VALUES (51, 18, 10, 2, 2, 1, 390000)
GO
SET IDENTITY_INSERT [dbo].[GioiTinh] ON 

INSERT [dbo].[GioiTinh] ([MaGT], [GioiTinh]) VALUES (1, N'Nam')
INSERT [dbo].[GioiTinh] ([MaGT], [GioiTinh]) VALUES (2, N'Nữ')
INSERT [dbo].[GioiTinh] ([MaGT], [GioiTinh]) VALUES (3, N'Unisex')
SET IDENTITY_INSERT [dbo].[GioiTinh] OFF
GO
SET IDENTITY_INSERT [dbo].[HoaDon] ON 

INSERT [dbo].[HoaDon] ([MaHD], [MaTK], [NgayMua], [MaThe], [DiaChi], [TongTien], [TrangThai], [SDTKH]) VALUES (49, 1, CAST(N'2023-01-13T00:00:00.000' AS DateTime), 7, N'Hà Nam', 318000, 2, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaTK], [NgayMua], [MaThe], [DiaChi], [TongTien], [TrangThai], [SDTKH]) VALUES (50, 1, CAST(N'2023-01-14T00:00:00.000' AS DateTime), 7, N'Hà Nam', 2476000, 1, N'098373233')
INSERT [dbo].[HoaDon] ([MaHD], [MaTK], [NgayMua], [MaThe], [DiaChi], [TongTien], [TrangThai], [SDTKH]) VALUES (51, 1, CAST(N'2023-05-04T00:00:00.000' AS DateTime), 7, N'Hà Nam', 780000, 1, N'098373233')
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiSP] ON 

INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP], [Anh]) VALUES (1, N'Áo khoác', NULL)
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP], [Anh]) VALUES (2, N'Áo thun', NULL)
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP], [Anh]) VALUES (3, N'Áo nỉ & Hoodie', NULL)
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP], [Anh]) VALUES (4, N'Áo sơ mi', NULL)
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP], [Anh]) VALUES (5, N'Quần jean', NULL)
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP], [Anh]) VALUES (6, N'Quần âu', NULL)
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP], [Anh]) VALUES (7, N'Quần Short', NULL)
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP], [Anh]) VALUES (8, N'Quần dài', NULL)
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP], [Anh]) VALUES (9, N'Váy | Đầm', NULL)
SET IDENTITY_INSERT [dbo].[LoaiSP] OFF
GO
SET IDENTITY_INSERT [dbo].[Mau] ON 

INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (1, N'Trắng', N'trắng.png')
INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (2, N'Đen', N'đen.jpg')
INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (3, N'Đỏ', N'dỏ.jpg')
INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (4, N'Xanh', N'tím.jpg')
INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (5, N'Nâu', N'nâu.png')
INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (8, N'Hồng', NULL)
INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (9, N'Tím', NULL)
INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (10, N'Be', NULL)
INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (11, N'Xám', NULL)
INSERT [dbo].[Mau] ([MaMau], [TenMau], [Anh]) VALUES (12, N'Bạc', NULL)
SET IDENTITY_INSERT [dbo].[Mau] OFF
GO
SET IDENTITY_INSERT [dbo].[NCC] ON 

INSERT [dbo].[NCC] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (1, N'Hoang Linh', N'Mỹ', N'0387487139', N'LosAngeles@gmail.com')
INSERT [dbo].[NCC] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (2, N'SSM', N'Hà Nội', N'0972838663', N'ssm@gmail.com')
INSERT [dbo].[NCC] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (3, N'GMN', N'Hà Nội', N'0972838888', N'gmn@gmail.com')
INSERT [dbo].[NCC] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (4, N'Hoang Nam', N'TPHCM', N'0986862762', N'hoangnamstore@gmail.com')
INSERT [dbo].[NCC] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (5, N'USA', N'Mỹ', N'126272222', N'funtion@gmail.com')
INSERT [dbo].[NCC] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (6, N'Life', N'Mỹ', N'11987987', N'life@gmai.com')
SET IDENTITY_INSERT [dbo].[NCC] OFF
GO
SET IDENTITY_INSERT [dbo].[RepCMT] ON 

INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (1, 1, 1, N'Nen dung!', CAST(N'2023-05-04T01:33:25.110' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (2, 1, 1, N'get away', CAST(N'2023-05-04T01:42:18.973' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (3, 1, 1, N'No', CAST(N'2023-05-04T01:53:42.617' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (4, 1, 1, N'q1', CAST(N'2023-05-04T02:00:03.707' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (5, 1, 1, N'God', CAST(N'2023-05-04T02:01:56.243' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (6, 1, 1, N'damn', CAST(N'2023-05-04T02:04:49.907' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (7, 1, 1, N'how', CAST(N'2023-05-04T02:07:53.873' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (8, 1, 1, N'can', CAST(N'2023-05-04T02:08:43.497' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (9, 1, 1, N'we', CAST(N'2023-05-04T02:09:23.263' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (10, 1, 1, N'do', CAST(N'2023-05-04T02:11:39.943' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (11, 2, 1, N'really?', CAST(N'2023-05-04T02:12:18.840' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (12, 2, 1, N'what>', CAST(N'2023-05-04T02:12:33.750' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (13, 2, 1, N'tra loi', CAST(N'2023-05-04T02:17:46.113' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (14, 2, 1, N'gg', CAST(N'2023-05-04T02:23:11.500' AS DateTime))
INSERT [dbo].[RepCMT] ([MaRep], [MaCMT], [MaTK], [NoiDung], [Date]) VALUES (15, 3, 1, N'ggnore', CAST(N'2023-05-04T02:31:39.073' AS DateTime))
SET IDENTITY_INSERT [dbo].[RepCMT] OFF
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (18, N'Áo khoác da cho nam cao cấp J09', 390000, 1, 10, 2, 4, 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (19, N'Áo khoác nam, Áo khoác demi mùa xuân phiên bản hàn quốc. Phong cách body trẻ trung', 490000, 1, 10, 3, 4, 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (20, N'Áo khoác kaki nam giả vest', 210000, 1, 1, 3, 3, 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (21, N'Áo Khoác KaKi Nam 2 Lớp 3 màu Sport Mã AK02 Cao Cấp', 259000, 1, 1, 2, 3, 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (22, N'Áo Thun Nam SITAKI Có Cổ Thêu Logo ATN02', 159000, 2, 1, 3, 8, 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (23, N'Áo thun nam cổ tim ngắn tay đẹp nhiều màu đủ size', 99000, 2, 1, 3, 8, 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (24, N'Áo Sweater Nữ Form Rộng Phối Layer Susu Vải Nỉ Bông Dày Dặn', 110000, 3, 1, 5, 8, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (25, N'Áo khoác nỉ hoodie Unisex phong cách Hàn SOMEDAY', 89000, 3, 1, 3, 4, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (26, N'Áo Hoodie Sweater chất NỈ under Hà Nội', 99000, 3, 1, 2, 4, 3)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (27, N'Áo hoodie lót nỉ in chữ dễ thương freesize cho nữ', 79000, 3, 1, 3, 4, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (28, N'Áo hoodie nam, nữ phong cách trẻ trung', 89000, 3, 1, 2, 11, 3)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (29, N'Áo Khoác Bomber Nỉ 199 Chất Nỉ Bông Mịn Đẹp Form rộng', 189000, 1, 1, 3, 11, 3)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (30, N'Áo sơ mi nữ nhung tăm màu đỏ nắp túi hộp from rộng tay bồng', 129000, 4, 1, 2, 10, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (31, N'Áo Sơ Mi Nhung TĂM Unisex Form Rộng Ulzzang Hàn Quốc', 129000, 4, 2, 3, 4, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (32, N'Áo Sơ Mi Trơn Dài Tay Thun kiểu form rộng bánh bèo', 119000, 4, 1, 2, 4, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (33, N'Đầm suông dạo phối hở vai, thời trang trẻ, phong cách Hàn Quốc', 159000, 9, 1, 2, 3, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (34, N'Đầm tiểu thư phối đen trắng LUXY V30310, kiểu dáng thanh lịch', 229000, 9, 11, 2, 3, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (35, N'Đầm nữ thiết kế LUXY V30318, cổ sơ mi phối yếm trước ngực duyên dáng', 259000, 9, 7, 2, 3, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (36, N'Váy nữ LUXY V30301 dáng suông, nhún ly cách điệu, tiểu thư sang chảnh', 299000, 9, 11, 2, 3, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (37, N'Đầm suông big size, đầm cho người mập', 650000, 9, 11, 2, 3, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [MaCL], [MaNCC], [MaTH], [MaGT]) VALUES (38, N'Đầm nữ thiết kế LUXY V27 cổ tròn', 450000, 9, 11, 2, 3, 2)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
GO
SET IDENTITY_INSERT [dbo].[Size] ON 

INSERT [dbo].[Size] ([MaSize], [TenSize]) VALUES (1, N'S')
INSERT [dbo].[Size] ([MaSize], [TenSize]) VALUES (2, N'M')
INSERT [dbo].[Size] ([MaSize], [TenSize]) VALUES (3, N'L')
INSERT [dbo].[Size] ([MaSize], [TenSize]) VALUES (4, N'XL')
INSERT [dbo].[Size] ([MaSize], [TenSize]) VALUES (5, N'XXL')
SET IDENTITY_INSERT [dbo].[Size] OFF
GO
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (18, 2, 1, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (18, 2, 2, 211)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (18, 2, 3, 1)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (18, 2, 4, 12)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (19, 1, 1, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (19, 1, 2, 32)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (19, 1, 3, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (19, 1, 4, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (19, 2, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (19, 2, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (19, 2, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (19, 2, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (20, 2, 1, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (20, 2, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (20, 2, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (20, 2, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (20, 10, 1, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (20, 10, 2, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (20, 10, 3, 21)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (20, 10, 4, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 3, 1, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 3, 2, 21)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 3, 3, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 3, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 4, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 4, 2, 21)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 4, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 4, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 10, 1, 32)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 10, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 10, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (21, 10, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 2, 1, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 2, 2, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 2, 3, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 2, 4, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 4, 1, 21)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 4, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 4, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 4, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 11, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 11, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 11, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (22, 11, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 1, 1, 32)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 1, 2, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 1, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 1, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 1, 5, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 2, 1, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 2, 2, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 2, 3, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 2, 4, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 2, 5, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 4, 1, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 4, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 4, 3, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 4, 4, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (23, 4, 5, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (24, 2, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (24, 2, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (24, 2, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (24, 2, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (25, 2, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (25, 2, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (25, 2, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (25, 2, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (26, 2, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (26, 2, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (26, 2, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (26, 2, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (27, 8, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (27, 8, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (27, 8, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (27, 8, 4, 2)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (28, 1, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (28, 1, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (28, 1, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (28, 1, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (29, 2, 2, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (29, 2, 3, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (29, 2, 4, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (29, 2, 5, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (30, 3, 1, 12)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (30, 3, 2, 12)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (30, 3, 3, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (30, 3, 4, 11)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (31, 11, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (31, 11, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (31, 11, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (31, 11, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (32, 4, 1, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (32, 4, 2, 33)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (32, 4, 3, 3)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (32, 4, 4, 3)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (33, 4, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (33, 4, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (33, 4, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (33, 4, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (34, 2, 1, 22)
GO
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (34, 2, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (34, 2, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (34, 2, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (35, 4, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (35, 4, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (35, 4, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (35, 4, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (36, 2, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (36, 2, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (36, 2, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (36, 2, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (37, 2, 1, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (37, 2, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (37, 2, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (37, 2, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (38, 2, 2, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (38, 2, 3, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (38, 2, 4, 22)
INSERT [dbo].[SoLuong] ([MaSP], [MaMau], [MaSize], [SoLuong]) VALUES (38, 2, 5, 22)
GO
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([MaTK], [TenDN], [MatKhau], [MaCV], [TenNguoiDung], [SDT], [DiaChi], [Email], [Anh]) VALUES (1, N'Admin', N'Admin', 1, N'Admin', N'098373233', N'Hà Nam', N'admin@donotatme.com', N'user1.jpg')
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDN], [MatKhau], [MaCV], [TenNguoiDung], [SDT], [DiaChi], [Email], [Anh]) VALUES (2, N'norm', N'norm', 2, N'Lê Tuấn Tú', N'0912345678', NULL, NULL, N'user1.jpg')
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDN], [MatKhau], [MaCV], [TenNguoiDung], [SDT], [DiaChi], [Email], [Anh]) VALUES (3, N'Dongoccho', N'123', 2, N'Vũ Thế Vinh', N'0123456789', NULL, NULL, N'user1.jpg')
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDN], [MatKhau], [MaCV], [TenNguoiDung], [SDT], [DiaChi], [Email], [Anh]) VALUES (4, N'rise', N'123456', 2, N'Yu', N'0912345678', N'Japan?', N'dontatme@gmail.com', N'user1.jpg')
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
GO
SET IDENTITY_INSERT [dbo].[ThanhToanOnline] ON 

INSERT [dbo].[ThanhToanOnline] ([MaThe], [TenThe], [Anh]) VALUES (7, N'Thanh toán offlne', NULL)
INSERT [dbo].[ThanhToanOnline] ([MaThe], [TenThe], [Anh]) VALUES (9, N'Momo', N'logo-momo.jpg')
SET IDENTITY_INSERT [dbo].[ThanhToanOnline] OFF
GO
SET IDENTITY_INSERT [dbo].[ThuongHieu] ON 

INSERT [dbo].[ThuongHieu] ([MaTH], [TenTH]) VALUES (3, N'LADOS')
INSERT [dbo].[ThuongHieu] ([MaTH], [TenTH]) VALUES (4, N'ZERO')
INSERT [dbo].[ThuongHieu] ([MaTH], [TenTH]) VALUES (7, N'The 1992')
INSERT [dbo].[ThuongHieu] ([MaTH], [TenTH]) VALUES (8, N'FASVIN')
INSERT [dbo].[ThuongHieu] ([MaTH], [TenTH]) VALUES (9, N'2s Clothing')
INSERT [dbo].[ThuongHieu] ([MaTH], [TenTH]) VALUES (10, N'LEVENTS')
INSERT [dbo].[ThuongHieu] ([MaTH], [TenTH]) VALUES (11, N'ATINO')
INSERT [dbo].[ThuongHieu] ([MaTH], [TenTH]) VALUES (12, N'SADBOIZ')
SET IDENTITY_INSERT [dbo].[ThuongHieu] OFF
GO
SET IDENTITY_INSERT [dbo].[TrangThai] ON 

INSERT [dbo].[TrangThai] ([MaTrangThai], [TenTrangThai]) VALUES (1, N'Chưa xác nhận')
INSERT [dbo].[TrangThai] ([MaTrangThai], [TenTrangThai]) VALUES (2, N'Đã xác nhận')
INSERT [dbo].[TrangThai] ([MaTrangThai], [TenTrangThai]) VALUES (3, N'Đang lấy hàng')
INSERT [dbo].[TrangThai] ([MaTrangThai], [TenTrangThai]) VALUES (4, N'Đã lấy hàng')
INSERT [dbo].[TrangThai] ([MaTrangThai], [TenTrangThai]) VALUES (5, N'Đang vận chuyển')
INSERT [dbo].[TrangThai] ([MaTrangThai], [TenTrangThai]) VALUES (6, N'Đã giao hàng')
INSERT [dbo].[TrangThai] ([MaTrangThai], [TenTrangThai]) VALUES (7, N'Đã nhận hàng')
SET IDENTITY_INSERT [dbo].[TrangThai] OFF
GO
ALTER TABLE [dbo].[AnhSP]  WITH CHECK ADD  CONSTRAINT [FK_AnhSP_Mau] FOREIGN KEY([MaMau])
REFERENCES [dbo].[Mau] ([MaMau])
GO
ALTER TABLE [dbo].[AnhSP] CHECK CONSTRAINT [FK_AnhSP_Mau]
GO
ALTER TABLE [dbo].[AnhSP]  WITH CHECK ADD  CONSTRAINT [FK_AnhSP_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[AnhSP] CHECK CONSTRAINT [FK_AnhSP_SanPham]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_SanPham]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_TaiKhoan] FOREIGN KEY([MaTK])
REFERENCES [dbo].[TaiKhoan] ([MaTK])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_TaiKhoan]
GO
ALTER TABLE [dbo].[CT_HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_CT_HoaDon_ChatLieu] FOREIGN KEY([MaCL])
REFERENCES [dbo].[ChatLieu] ([MaCL])
GO
ALTER TABLE [dbo].[CT_HoaDon] CHECK CONSTRAINT [FK_CT_HoaDon_ChatLieu]
GO
ALTER TABLE [dbo].[CT_HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_CT_HoaDon_HoaDon] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HoaDon] ([MaHD])
GO
ALTER TABLE [dbo].[CT_HoaDon] CHECK CONSTRAINT [FK_CT_HoaDon_HoaDon]
GO
ALTER TABLE [dbo].[CT_HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_CT_HoaDon_Mau] FOREIGN KEY([MaMau])
REFERENCES [dbo].[Mau] ([MaMau])
GO
ALTER TABLE [dbo].[CT_HoaDon] CHECK CONSTRAINT [FK_CT_HoaDon_Mau]
GO
ALTER TABLE [dbo].[CT_HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_CT_HoaDon_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[CT_HoaDon] CHECK CONSTRAINT [FK_CT_HoaDon_SanPham]
GO
ALTER TABLE [dbo].[CT_HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_CT_HoaDon_Size] FOREIGN KEY([MaSize])
REFERENCES [dbo].[Size] ([MaSize])
GO
ALTER TABLE [dbo].[CT_HoaDon] CHECK CONSTRAINT [FK_CT_HoaDon_Size]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_ChatLieu] FOREIGN KEY([MaCL])
REFERENCES [dbo].[ChatLieu] ([MaCL])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_ChatLieu]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_Mau] FOREIGN KEY([MaMau])
REFERENCES [dbo].[Mau] ([MaMau])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_Mau]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_SanPham]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_Size] FOREIGN KEY([MaSize])
REFERENCES [dbo].[Size] ([MaSize])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_Size]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_TaiKhoan] FOREIGN KEY([MaTK])
REFERENCES [dbo].[TaiKhoan] ([MaTK])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_TaiKhoan]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_TaiKhoan] FOREIGN KEY([MaTK])
REFERENCES [dbo].[TaiKhoan] ([MaTK])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_TaiKhoan]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_ThanhToanOnline] FOREIGN KEY([MaThe])
REFERENCES [dbo].[ThanhToanOnline] ([MaThe])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_ThanhToanOnline]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_TrangThai] FOREIGN KEY([TrangThai])
REFERENCES [dbo].[TrangThai] ([MaTrangThai])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_TrangThai]
GO
ALTER TABLE [dbo].[RepCMT]  WITH CHECK ADD  CONSTRAINT [FK_RepCMT_Comment] FOREIGN KEY([MaCMT])
REFERENCES [dbo].[Comment] ([MaCMT])
GO
ALTER TABLE [dbo].[RepCMT] CHECK CONSTRAINT [FK_RepCMT_Comment]
GO
ALTER TABLE [dbo].[RepCMT]  WITH CHECK ADD  CONSTRAINT [FK_RepCMT_TaiKhoan] FOREIGN KEY([MaTK])
REFERENCES [dbo].[TaiKhoan] ([MaTK])
GO
ALTER TABLE [dbo].[RepCMT] CHECK CONSTRAINT [FK_RepCMT_TaiKhoan]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_ChatLieu] FOREIGN KEY([MaCL])
REFERENCES [dbo].[ChatLieu] ([MaCL])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_ChatLieu]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_GioiTinh] FOREIGN KEY([MaGT])
REFERENCES [dbo].[GioiTinh] ([MaGT])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_GioiTinh]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_LoaiSP] FOREIGN KEY([MaLoaiSP])
REFERENCES [dbo].[LoaiSP] ([MaLoaiSP])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_LoaiSP]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_NCC] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NCC] ([MaNCC])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_NCC]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_ThuongHieu] FOREIGN KEY([MaTH])
REFERENCES [dbo].[ThuongHieu] ([MaTH])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_ThuongHieu]
GO
ALTER TABLE [dbo].[SoLuong]  WITH CHECK ADD  CONSTRAINT [FK_SoLuong_Mau] FOREIGN KEY([MaMau])
REFERENCES [dbo].[Mau] ([MaMau])
GO
ALTER TABLE [dbo].[SoLuong] CHECK CONSTRAINT [FK_SoLuong_Mau]
GO
ALTER TABLE [dbo].[SoLuong]  WITH CHECK ADD  CONSTRAINT [FK_SoLuong_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[SoLuong] CHECK CONSTRAINT [FK_SoLuong_SanPham]
GO
ALTER TABLE [dbo].[SoLuong]  WITH CHECK ADD  CONSTRAINT [FK_SoLuong_Size] FOREIGN KEY([MaSize])
REFERENCES [dbo].[Size] ([MaSize])
GO
ALTER TABLE [dbo].[SoLuong] CHECK CONSTRAINT [FK_SoLuong_Size]
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD  CONSTRAINT [FK_TaiKhoan_ChucVu] FOREIGN KEY([MaCV])
REFERENCES [dbo].[ChucVu] ([MaCV])
GO
ALTER TABLE [dbo].[TaiKhoan] CHECK CONSTRAINT [FK_TaiKhoan_ChucVu]
GO
USE [master]
GO
ALTER DATABASE [DoAnWeb] SET  READ_WRITE 
GO
Use [DoAnWeb]
Go
Create trigger [dbo].[Update_SoLuong] on [dbo].[CT_HoaDon] for insert as
begin
	update SoLuong
	set SoLuong.SoLuong = (select SoLuong.SoLuong from SoLuong where SoLuong.MaSP = (select MaSP from inserted) and SoLuong.MaSize = (select MaSize from inserted) and SoLuong.MaMau = (Select MaMau from inserted)) - (select SoLuong from inserted) where SoLuong.MaSP = (select MaSP from inserted) and SoLuong.MaSize = (select MaSize from inserted) and SoLuong.MaMau = (Select MaMau from inserted)
end
Go
Create trigger [dbo].[update_tongtien] on [dbo].[CT_HoaDon] for insert as
begin
	update HoaDon
	set tongtien=(select tongtien from HoaDon where MaHD = (select MaHD from inserted)) + (select ThanhTien from inserted) where MaHD =(select MaHD from inserted)
end
Go

Create trigger [dbo].[CapNhatGioHang] on [dbo].[GioHang] for insert as
begin
	update GioHang
	set ThanhTien = (select DonGia from SanPham where MaSP = (Select MaSP from inserted)) * (select SoLuong from inserted) where MaTK = (select MaTK from inserted) and MaSP = (select MaSP from inserted) and MaMau = (select MaMau from inserted) and MaSize = (select MaSize from inserted)
end
Go