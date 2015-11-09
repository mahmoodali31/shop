USE [master]
GO
/****** Object:  Database [ShoppingCardDB]    Script Date: 9/4/2015 4:24:15 PM ******/
CREATE DATABASE [ShoppingCardDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShoppingCardDB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ShoppingCardDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ShoppingCardDB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ShoppingCardDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ShoppingCardDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShoppingCardDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShoppingCardDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ShoppingCardDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShoppingCardDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShoppingCardDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShoppingCardDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShoppingCardDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ShoppingCardDB] SET  MULTI_USER 
GO
ALTER DATABASE [ShoppingCardDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShoppingCardDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShoppingCardDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShoppingCardDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [ShoppingCardDB]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 9/4/2015 4:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[PKCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[PKCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 9/4/2015 4:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[PKOrderId] [int] IDENTITY(1,1) NOT NULL,
	[FKUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[PKOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 9/4/2015 4:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[PKOrderDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[FKProductId] [int] NOT NULL,
	[FKOrderId] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Cost] [money] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[PKOrderDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 9/4/2015 4:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[PKProductId] [int] IDENTITY(1,1) NOT NULL,
	[FKCategoryId] [int] NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[ImagePath] [varchar](50) NULL,
	[LargeImagePath] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[Price] [money] NULL,
	[DateCreated] [date] NOT NULL,
	[DateUpdated] [date] NULL,
	[IsActive] [bit] NOT NULL,
	[FKProductId] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[PKProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 9/4/2015 4:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[PKUserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailId] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[PKUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAddress]    Script Date: 9/4/2015 4:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAddress](
	[PKShippingAddressId] [int] IDENTITY(1,1) NOT NULL,
	[FKOrderId] [int] NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[ZipCode] [int] NOT NULL,
	[AddressType] [int] NULL,
	[MobileNumber] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ShippingAddress] PRIMARY KEY CLUSTERED 
(
	[PKShippingAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([FKUserId])
REFERENCES [dbo].[User] ([PKUserId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Order] FOREIGN KEY([FKOrderId])
REFERENCES [dbo].[Order] ([PKOrderId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Order]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Product] FOREIGN KEY([FKProductId])
REFERENCES [dbo].[Product] ([PKProductId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([FKCategoryId])
REFERENCES [dbo].[Category] ([PKCategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Product] FOREIGN KEY([FKProductId])
REFERENCES [dbo].[Product] ([PKProductId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Product]
GO
ALTER TABLE [dbo].[UserAddress]  WITH CHECK ADD  CONSTRAINT [FK_ShippingAddress_Order1] FOREIGN KEY([FKOrderId])
REFERENCES [dbo].[Order] ([PKOrderId])
GO
ALTER TABLE [dbo].[UserAddress] CHECK CONSTRAINT [FK_ShippingAddress_Order1]
GO
USE [master]
GO
ALTER DATABASE [ShoppingCardDB] SET  READ_WRITE 
GO
