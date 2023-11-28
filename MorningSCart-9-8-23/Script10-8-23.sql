USE [master]
GO
/****** Object:  Database [DbMorSCart]    Script Date: 8/10/2023 11:18:11 PM ******/
CREATE DATABASE [DbMorSCart]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DbMorSCart', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\DbMorSCart.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DbMorSCart_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\DbMorSCart_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DbMorSCart] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DbMorSCart].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DbMorSCart] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DbMorSCart] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DbMorSCart] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DbMorSCart] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DbMorSCart] SET ARITHABORT OFF 
GO
ALTER DATABASE [DbMorSCart] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DbMorSCart] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DbMorSCart] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DbMorSCart] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DbMorSCart] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DbMorSCart] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DbMorSCart] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DbMorSCart] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DbMorSCart] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DbMorSCart] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DbMorSCart] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DbMorSCart] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DbMorSCart] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DbMorSCart] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DbMorSCart] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DbMorSCart] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [DbMorSCart] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DbMorSCart] SET RECOVERY FULL 
GO
ALTER DATABASE [DbMorSCart] SET  MULTI_USER 
GO
ALTER DATABASE [DbMorSCart] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DbMorSCart] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DbMorSCart] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DbMorSCart] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DbMorSCart] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DbMorSCart', N'ON'
GO
ALTER DATABASE [DbMorSCart] SET QUERY_STORE = OFF
GO
USE [DbMorSCart]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [DbMorSCart]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/10/2023 11:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 8/10/2023 11:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CName] [nvarchar](45) NOT NULL,
	[slug] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/10/2023 11:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Order_Id] [int] IDENTITY(1,1) NOT NULL,
	[UsersId] [int] NOT NULL,
	[TotalAmount] [nvarchar](max) NOT NULL,
	[Cardholdername] [nvarchar](max) NOT NULL,
	[CreditCard] [bigint] NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[DateTime] [datetime2](7) NOT NULL,
	[PlantsId] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Order_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 8/10/2023 11:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[PlantsId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[SDescription] [nvarchar](150) NOT NULL,
	[LDescription] [nvarchar](350) NOT NULL,
	[Brand] [nvarchar](50) NOT NULL,
	[PImage] [nvarchar](50) NOT NULL,
	[Price] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[slug] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[PlantsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 8/10/2023 11:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[UsersId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CPassword] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PhoneNo] [nvarchar](20) NOT NULL,
	[HAddress] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[UsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230411044718_Initial', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230429051130_AddLogin', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230504043305_addslug', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230809135437_addOrders', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230809150030_addOrders', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230810093716_addOrder', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230810105206_addCC', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230810105534_addCC', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230810121854_addCC', N'7.0.4')
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [CName], [slug]) VALUES (1, N'Electronics', N'ELC')
INSERT [dbo].[Categories] ([CategoryId], [CName], [slug]) VALUES (2, N'Garments', N'GMT')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Order_Id], [UsersId], [TotalAmount], [Cardholdername], [CreditCard], [Address], [DateTime], [PlantsId]) VALUES (8, 1, N'50000', N'Jawaid', 0, N'Karachi', CAST(N'2023-08-10T09:53:32.3534513' AS DateTime2), 2)
INSERT [dbo].[Orders] ([Order_Id], [UsersId], [TotalAmount], [Cardholdername], [CreditCard], [Address], [DateTime], [PlantsId]) VALUES (9, 1, N'50000', N'Jawaid', 45545, N'', CAST(N'2023-08-10T12:14:29.0558820' AS DateTime2), 2)
INSERT [dbo].[Orders] ([Order_Id], [UsersId], [TotalAmount], [Cardholdername], [CreditCard], [Address], [DateTime], [PlantsId]) VALUES (10, 1, N'25000', N'Jawaid', 0, N'Karachi', CAST(N'2023-08-10T12:16:55.9610164' AS DateTime2), 2)
INSERT [dbo].[Orders] ([Order_Id], [UsersId], [TotalAmount], [Cardholdername], [CreditCard], [Address], [DateTime], [PlantsId]) VALUES (11, 1, N'50000', N'Jawaid', 12345678911, N'Karachi', CAST(N'2023-08-10T12:20:21.9292785' AS DateTime2), 2)
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([PlantsId], [ProductName], [SDescription], [LDescription], [Brand], [PImage], [Price], [CategoryId], [slug]) VALUES (1, N'Philips LCD 32 Inch', N'asdf asdfdsafdsafdsafdsa asdfsdafas', N'sadf dsaf sadfsadfdsafsafasdfsa fadsfasdfsad dsff  asdfsadfsdafdsafasd sadf  dfasdfas', N'PH-215', N'\Image\img7.jpg', 45000, 1, N'ELC')
INSERT [dbo].[Products] ([PlantsId], [ProductName], [SDescription], [LDescription], [Brand], [PImage], [Price], [CategoryId], [slug]) VALUES (2, N'SonyLCD 32 Inch', N'fassf sdf dasf dsfa afsadfdsa fdsa sdafasdf', N'sadf dsaf sadfsadfdsafsafasdfsa fadsfasdfsad dsff  asdfsadfsdafdsafasd sadf  dfasdfas', N'SN-215', N'\Image\img8.jpg', 25000, 1, N'ELC')
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[UserLogins] ON 

INSERT [dbo].[UserLogins] ([UsersId], [UserName], [Password], [CPassword], [Email], [PhoneNo], [HAddress]) VALUES (1, N'Jawaid', N'123456', N'123456', N'iqbaljawaid@gmail.com', N'034525555558', N'Karachi Pakistan')
SET IDENTITY_INSERT [dbo].[UserLogins] OFF
/****** Object:  Index [IX_Orders_PlantsId]    Script Date: 8/10/2023 11:18:11 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_PlantsId] ON [dbo].[Orders]
(
	[PlantsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_UsersId]    Script Date: 8/10/2023 11:18:11 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UsersId] ON [dbo].[Orders]
(
	[UsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 8/10/2023 11:18:11 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [PlantsId]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (N'') FOR [slug]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Products_PlantsId] FOREIGN KEY([PlantsId])
REFERENCES [dbo].[Products] ([PlantsId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Products_PlantsId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_UserLogins_UsersId] FOREIGN KEY([UsersId])
REFERENCES [dbo].[UserLogins] ([UsersId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_UserLogins_UsersId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
USE [master]
GO
ALTER DATABASE [DbMorSCart] SET  READ_WRITE 
GO
