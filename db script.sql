USE [master]
GO
/****** Object:  Database [addressDatabase]    Script Date: 2/24/2024 6:56:35 PM ******/
CREATE DATABASE [addressDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'addressDatabase', FILENAME = N'D:\Programs\MSSQL16.SQLEXPRESS\MSSQL\DATA\addressDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'addressDatabase_log', FILENAME = N'D:\Programs\MSSQL16.SQLEXPRESS\MSSQL\DATA\addressDatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [addressDatabase] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [addressDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [addressDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [addressDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [addressDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [addressDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [addressDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [addressDatabase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [addressDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [addressDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [addressDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [addressDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [addressDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [addressDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [addressDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [addressDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [addressDatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [addressDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [addressDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [addressDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [addressDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [addressDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [addressDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [addressDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [addressDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [addressDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [addressDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [addressDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [addressDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [addressDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [addressDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [addressDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [addressDatabase] SET QUERY_STORE = ON
GO
ALTER DATABASE [addressDatabase] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [addressDatabase]
GO
/****** Object:  Table [dbo].[adresai]    Script Date: 2/24/2024 6:56:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adresai](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pilnas_vardas] [varchar](250) NULL,
	[telefono_numeris] [varchar](12) NOT NULL,
	[gimimo_data] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddContact]    Script Date: 2/24/2024 6:56:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddContact]
@name varchar(250),
@number varchar(12),
@birthDate DATE
AS
BEGIN
	INSERT INTO adresai (pilnas_vardas, telefono_numeris, gimimo_data)
	VALUES (@name, @number, @birthDate)
END

/*
USE [addressDatabase]
EXECUTE GetAddressList
*/
GO
/****** Object:  StoredProcedure [dbo].[DeleteContact]    Script Date: 2/24/2024 6:56:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteContact]
@id int
AS
BEGIN
	DELETE FROM adresai WHERE id = @id
END

GO
/****** Object:  StoredProcedure [dbo].[GetAddressList]    Script Date: 2/24/2024 6:56:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAddressList]
AS
BEGIN
	SELECT * FROM adresai
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateContact]    Script Date: 2/24/2024 6:56:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateContact]
@id int,
@name varchar(250),
@number varchar(12),
@birthDate DATE
AS
BEGIN
	UPDATE adresai
	SET pilnas_vardas = @name,
		telefono_numeris = @number,
		gimimo_data = @birthDate
	WHERE id = @id
END
GO
USE [master]
GO
ALTER DATABASE [addressDatabase] SET  READ_WRITE 
GO
