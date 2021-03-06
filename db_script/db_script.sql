USE [master]
GO
/****** Object:  Database [Room_Managment]    Script Date: 20-12-2019 09:47:24 ******/
CREATE DATABASE [Room_Managment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Room_Managment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Room_Managment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Room_Managment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Room_Managment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Room_Managment] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Room_Managment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Room_Managment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Room_Managment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Room_Managment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Room_Managment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Room_Managment] SET ARITHABORT OFF 
GO
ALTER DATABASE [Room_Managment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Room_Managment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Room_Managment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Room_Managment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Room_Managment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Room_Managment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Room_Managment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Room_Managment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Room_Managment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Room_Managment] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Room_Managment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Room_Managment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Room_Managment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Room_Managment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Room_Managment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Room_Managment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Room_Managment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Room_Managment] SET RECOVERY FULL 
GO
ALTER DATABASE [Room_Managment] SET  MULTI_USER 
GO
ALTER DATABASE [Room_Managment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Room_Managment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Room_Managment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Room_Managment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Room_Managment] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Room_Managment', N'ON'
GO
ALTER DATABASE [Room_Managment] SET QUERY_STORE = OFF
GO
USE [Room_Managment]
GO
/****** Object:  Table [dbo].[Guest]    Script Date: 20-12-2019 09:47:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guest](
	[GuestId] [bigint] IDENTITY(1,1) NOT NULL,
	[GuestName] [nvarchar](50) NULL,
	[Sex] [nvarchar](20) NULL,
	[Age] [int] NULL,
	[RoomId] [bigint] NOT NULL,
	[CheckInDate] [datetime] NULL,
	[CheckOutDate] [datetime] NULL,
 CONSTRAINT [PK_Guest] PRIMARY KEY CLUSTERED 
(
	[GuestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 20-12-2019 09:47:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoomName] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[Location] [nvarchar](50) NULL,
	[Capacity] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 20-12-2019 09:47:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsAdmin] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Guest]  WITH CHECK ADD  CONSTRAINT [FK_Guest_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Guest] CHECK CONSTRAINT [FK_Guest_RoomId]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Room]
GO
USE [master]
GO
ALTER DATABASE [Room_Managment] SET  READ_WRITE 
GO
