USE [master]
GO
/****** Object:  Database [EstructuraDB]    Script Date: 5/5/2023 2:58:23 PM ******/
CREATE DATABASE [EstructuraDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EstructuraDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\EstructuraDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EstructuraDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\EstructuraDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [EstructuraDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EstructuraDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EstructuraDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EstructuraDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EstructuraDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EstructuraDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EstructuraDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EstructuraDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EstructuraDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EstructuraDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EstructuraDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EstructuraDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EstructuraDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EstructuraDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EstructuraDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EstructuraDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EstructuraDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EstructuraDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EstructuraDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EstructuraDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EstructuraDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EstructuraDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EstructuraDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EstructuraDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EstructuraDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EstructuraDB] SET  MULTI_USER 
GO
ALTER DATABASE [EstructuraDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EstructuraDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EstructuraDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EstructuraDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EstructuraDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EstructuraDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [EstructuraDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [EstructuraDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EstructuraDB]
GO
/****** Object:  User [estructura2]    Script Date: 5/5/2023 2:58:23 PM ******/
CREATE USER [estructura2] FOR LOGIN [estructura2] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [estructura]    Script Date: 5/5/2023 2:58:23 PM ******/
CREATE USER [estructura] FOR LOGIN [estructura] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] NOT NULL,
	[Name] [varchar](59) NOT NULL,
	[Lastname] [varchar](12) NOT NULL,
	[Phone] [varchar](27) NOT NULL,
	[Email] [varchar](12) NOT NULL,
	[Password] [varchar](40) NOT NULL,
	[CreatedDate] [varchar](19) NOT NULL,
	[LastLoginDate] [varchar](19) NOT NULL,
	[Role] [int] NOT NULL,
	[IsActive] [varchar](5) NOT NULL,
	[CompanyInformationId] [int] NOT NULL,
	[UnderAdminUserId] [varchar](30) NULL,
	[StateId] [varchar](30) NULL,
	[CityId] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdditionalIncoming]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdditionalIncoming](
	[Id] [int] NOT NULL,
	[StudyEconomicSituationId] [int] NOT NULL,
	[Activity] [varchar](20) NOT NULL,
	[TimeFrame] [varchar](11) NOT NULL,
	[Amount] [numeric](22, 20) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidate](
	[Id] [int] NOT NULL,
	[Name] [varchar](17) NOT NULL,
	[Lastname] [varchar](66) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Email] [varchar](66) NOT NULL,
	[CURP] [varchar](45) NOT NULL,
	[NSS] [varchar](19) NOT NULL,
	[Address] [varchar](17) NOT NULL,
	[Position] [varchar](24) NOT NULL,
	[MediaId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[CityId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[CandidateStatusId] [int] NOT NULL,
	[UnderAdminUserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CandidateNote]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CandidateNote](
	[Id] [int] NOT NULL,
	[Description] [varchar](18) NOT NULL,
	[CandidateId] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] NOT NULL,
	[Name] [varchar](42) NOT NULL,
	[StateId] [int] NOT NULL,
	[CityNumber] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyInformation]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyInformation](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CompanyName] [varchar](49) NOT NULL,
	[CompanyPhone] [varchar](35) NOT NULL,
	[RazonSocial] [varchar](39) NOT NULL,
	[RFC] [varchar](36) NOT NULL,
	[DireccionFiscal] [varchar](11) NOT NULL,
	[RegimenFiscal] [varchar](20) NOT NULL,
	[PaymentId] [int] NOT NULL,
	[TotalStudies] [int] NOT NULL,
	[CompletedStudies] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Compatibility]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compatibility](
	[AppVersion] [int] NOT NULL,
	[ApiVersion] [varchar](19) NOT NULL,
	[KillSwitch] [varchar](5) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AppVersion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credit]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credit](
	[Id] [int] NOT NULL,
	[StudyEconomicSituationId] [int] NOT NULL,
	[Bank] [varchar](23) NOT NULL,
	[AccountNumber] [varchar](40) NOT NULL,
	[CreditLimit] [numeric](22, 19) NOT NULL,
	[Debt] [numeric](21, 19) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doccument]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doccument](
	[Id] [int] NOT NULL,
	[DoccumentName] [varchar](23) NOT NULL,
	[DoccumentRoute] [varchar](19) NOT NULL,
	[DoccumentURL] [varchar](19) NOT NULL,
	[StoreMediaType] [int] NOT NULL,
	[StoreFileType] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[Base64Doccument] [varchar](55) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estate]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estate](
	[Id] [int] NOT NULL,
	[StudyEconomicSituationId] [int] NOT NULL,
	[Concept] [varchar](26) NOT NULL,
	[AcquisitionMethod] [varchar](11) NOT NULL,
	[AcquisitionDate] [varchar](19) NOT NULL,
	[Owner] [varchar](66) NOT NULL,
	[PurchaseValue] [numeric](20, 18) NOT NULL,
	[CurrentValue] [numeric](21, 19) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExtracurricularActivities]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtracurricularActivities](
	[Id] [int] NOT NULL,
	[StudySchoolarityId] [int] NOT NULL,
	[Name] [varchar](42) NOT NULL,
	[Instituution] [varchar](15) NOT NULL,
	[KnowledgeLevel] [int] NOT NULL,
	[Period] [varchar](36) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldsToFill]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldsToFill](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[Resume] [varchar](5) NOT NULL,
	[GeneralInformation] [varchar](5) NOT NULL,
	[RecommendationLetter] [varchar](5) NOT NULL,
	[IdentificationCardPics] [varchar](5) NOT NULL,
	[EducationalLevel] [varchar](5) NOT NULL,
	[Extracurricular] [varchar](5) NOT NULL,
	[ScholarVerification] [varchar](5) NOT NULL,
	[Family] [varchar](5) NOT NULL,
	[EconomicSituation] [varchar](5) NOT NULL,
	[Social] [varchar](5) NOT NULL,
	[WorkHistory] [varchar](5) NOT NULL,
	[IMSSValidation] [varchar](5) NOT NULL,
	[PersonalReferences] [varchar](5) NOT NULL,
	[Pictures] [varchar](5) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IMSSValidation]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMSSValidation](
	[Id] [int] NOT NULL,
	[StudyIMSSValidationId] [int] NOT NULL,
	[CompanyBusinessName] [varchar](23) NOT NULL,
	[StartDate] [varchar](19) NOT NULL,
	[EndDate] [varchar](19) NOT NULL,
	[Result] [varchar](36) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incoming]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incoming](
	[Id] [int] NOT NULL,
	[StudyEconomicSituationId] [int] NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[Relationship] [varchar](35) NOT NULL,
	[Amount] [numeric](22, 20) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JwtExpirationResponse]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JwtExpirationResponse](
	[Token] [varchar](13) NOT NULL,
	[SlidingExpiration] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LivingFamily]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LivingFamily](
	[Id] [int] NOT NULL,
	[StudyFamilyId] [int] NOT NULL,
	[Name] [varchar](14) NOT NULL,
	[Relationship] [varchar](21) NOT NULL,
	[Age] [varchar](14) NOT NULL,
	[MaritalStatusId] [int] NOT NULL,
	[Schoolarity] [varchar](42) NOT NULL,
	[Address] [varchar](18) NOT NULL,
	[Phone] [varchar](10) NOT NULL,
	[Occupation] [varchar](37) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Media]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Media](
	[Id] [int] NOT NULL,
	[ImageName] [varchar](19) NOT NULL,
	[ImageRoute] [varchar](49) NOT NULL,
	[MediaURL] [varchar](37) NOT NULL,
	[StoreMediaTypeId] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[Base64Image] [varchar](40) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoLivingFamily]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoLivingFamily](
	[Id] [int] NOT NULL,
	[StudyFamilyId] [int] NOT NULL,
	[Name] [varchar](12) NOT NULL,
	[Relationship] [varchar](14) NOT NULL,
	[Age] [varchar](33) NOT NULL,
	[MaritalStatusId] [int] NOT NULL,
	[Schoolarity] [varchar](39) NOT NULL,
	[Address] [varchar](43) NOT NULL,
	[Phone] [varchar](16) NOT NULL,
	[Occupation] [varchar](21) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethod]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethod](
	[Id] [int] NOT NULL,
	[Description] [varchar](11) NOT NULL,
	[Method] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecommendationCard]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecommendationCard](
	[Id] [int] NOT NULL,
	[StudyGeneralInformationId] [int] NOT NULL,
	[IssueCompany] [varchar](20) NOT NULL,
	[WorkingFrom] [varchar](19) NOT NULL,
	[WorkingTo] [varchar](19) NOT NULL,
	[TimeWorking] [varchar](10) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[Token] [varchar](31) NOT NULL,
	[ChallengeHash] [varchar](16) NOT NULL,
	[UserID] [int] NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[IssuedServerDate] [varchar](19) NOT NULL,
	[DaysToLive] [int] NOT NULL,
	[IsValid] [varchar](5) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] NOT NULL,
	[RoleDescription] [varchar](30) NOT NULL,
	[ParentRole] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Scholarity]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scholarity](
	[Id] [int] NOT NULL,
	[StudySchoolarityId] [int] NOT NULL,
	[SchoolarLevel] [int] NOT NULL,
	[Career] [varchar](39) NOT NULL,
	[Period] [varchar](37) NOT NULL,
	[TimeOnCareer] [varchar](37) NOT NULL,
	[Institution] [varchar](44) NOT NULL,
	[DoccumentId] [int] NOT NULL,
	[Place] [varchar](40) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType](
	[Id] [int] NOT NULL,
	[Description] [varchar](37) NOT NULL,
	[Service] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SilentData]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SilentData](
	[Id] [int] NOT NULL,
	[Email] [varchar](51) NOT NULL,
	[date] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocialGoals]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocialGoals](
	[Id] [int] NOT NULL,
	[StudySocialId] [int] NOT NULL,
	[CoreValue] [varchar](13) NOT NULL,
	[LifeGoal] [varchar](12) NOT NULL,
	[NextGoal] [varchar](12) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocioeconomicStudy]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocioeconomicStudy](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[IdentificationCard] [varchar](5) NOT NULL,
	[AddressProof] [varchar](5) NOT NULL,
	[BirthCertificate] [varchar](5) NOT NULL,
	[CURP] [varchar](5) NOT NULL,
	[StudiesProof] [varchar](5) NOT NULL,
	[SocialSecurityNumber] [varchar](5) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] NOT NULL,
	[Name] [varchar](40) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Study]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Study](
	[Id] [int] NOT NULL,
	[CandidateId] [int] NOT NULL,
	[ServiceTypeId] [int] NOT NULL,
	[StudyStatusId] [int] NOT NULL,
	[StudyProgressStatusId] [int] NOT NULL,
	[WorkStudyId] [int] NOT NULL,
	[SocioeconomicStudyId] [int] NOT NULL,
	[FieldsToFillId] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
	[UnderAdminUserId] [int] NOT NULL,
	[StudyEconomicSituationId] [int] NOT NULL,
	[StudyFamilyId] [int] NOT NULL,
	[StudyFinalResultId] [int] NOT NULL,
	[StudyGeneralInformationId] [int] NOT NULL,
	[StudySchoolarityId] [int] NOT NULL,
	[StudySocialId] [int] NOT NULL,
	[StudyIMSSValidationId] [int] NOT NULL,
	[StudyPicturesId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyEconomicSituation]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyEconomicSituation](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[Electricity] [numeric](22, 19) NOT NULL,
	[Rent] [numeric](21, 19) NOT NULL,
	[Gas] [numeric](21, 19) NOT NULL,
	[Infonavit] [numeric](21, 19) NOT NULL,
	[Water] [numeric](21, 19) NOT NULL,
	[Credits] [numeric](22, 19) NOT NULL,
	[PropertyTax] [numeric](21, 19) NOT NULL,
	[Maintenance] [numeric](22, 19) NOT NULL,
	[Internet] [numeric](21, 18) NOT NULL,
	[Cable] [numeric](21, 19) NOT NULL,
	[Food] [numeric](22, 19) NOT NULL,
	[Cellphone] [numeric](22, 19) NOT NULL,
	[Gasoline] [numeric](23, 20) NOT NULL,
	[Entertainment] [numeric](21, 19) NOT NULL,
	[Clothing] [numeric](21, 19) NOT NULL,
	[Miscellaneous] [numeric](21, 19) NOT NULL,
	[Schoolar] [numeric](22, 19) NOT NULL,
	[EconomicSituationSummary] [varchar](13) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyFamily]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyFamily](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[Notes] [varchar](42) NOT NULL,
	[FamiliarArea] [varchar](33) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyFinalResult]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyFinalResult](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[PositionSummary] [varchar](39) NOT NULL,
	[AttitudeSummary] [varchar](17) NOT NULL,
	[WorkHistorySummary] [varchar](41) NOT NULL,
	[ArbitrationAndConciliationSummary] [varchar](30) NOT NULL,
	[FinalResultsBy] [varchar](19) NOT NULL,
	[FinalResultsPositionBy] [varchar](49) NOT NULL,
	[VisitDate] [varchar](19) NOT NULL,
	[ApplicationDate] [varchar](19) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyGeneralInformation]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyGeneralInformation](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[Name] [varchar](28) NOT NULL,
	[Email] [varchar](21) NOT NULL,
	[TimeOnComany] [varchar](35) NOT NULL,
	[EmployeeNumber] [varchar](22) NOT NULL,
	[BornCityId] [int] NOT NULL,
	[BornStateId] [int] NOT NULL,
	[CountryName] [varchar](18) NOT NULL,
	[BornDate] [varchar](19) NOT NULL,
	[Age] [varchar](14) NOT NULL,
	[MaritalStatusId] [int] NOT NULL,
	[TaxRegime] [varchar](29) NOT NULL,
	[Address] [varchar](19) NOT NULL,
	[PostalCode] [varchar](25) NOT NULL,
	[Suburb] [varchar](53) NOT NULL,
	[HomePhone] [varchar](17) NOT NULL,
	[CityId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[MobilPhone] [varchar](39) NOT NULL,
	[IDCardOriginal] [varchar](5) NOT NULL,
	[IDCardCopy] [varchar](5) NOT NULL,
	[IDCardRecord] [varchar](25) NOT NULL,
	[IDCardExpeditionPlace] [varchar](14) NOT NULL,
	[IDCardObservations] [varchar](27) NOT NULL,
	[AddressProofOriginal] [varchar](5) NOT NULL,
	[AddressProofCopy] [varchar](5) NOT NULL,
	[AddressProofRecord] [varchar](66) NOT NULL,
	[AddressProofExpeditionPlace] [varchar](24) NOT NULL,
	[AddressProofObservations] [varchar](26) NOT NULL,
	[BirthCertificateOriginal] [varchar](5) NOT NULL,
	[BirthCertificateCopy] [varchar](5) NOT NULL,
	[BirthCertificateRecord] [varchar](14) NOT NULL,
	[BirthCertificateExpeditionPlace] [varchar](67) NOT NULL,
	[BirthCertificateObservations] [varchar](42) NOT NULL,
	[CURPOriginal] [varchar](5) NOT NULL,
	[CURPCopy] [varchar](5) NOT NULL,
	[CURPRecord] [varchar](21) NOT NULL,
	[CURPExpeditionPlace] [varchar](45) NOT NULL,
	[CURPObservations] [varchar](69) NOT NULL,
	[StudyProofOriginal] [varchar](5) NOT NULL,
	[StudyProofCopy] [varchar](5) NOT NULL,
	[StudyProofRecord] [varchar](37) NOT NULL,
	[StudyProofExpeditionPlace] [varchar](26) NOT NULL,
	[StudyProofObservations] [varchar](47) NOT NULL,
	[SocialSecurityProofOriginal] [varchar](5) NOT NULL,
	[SocialSecurityProofCopy] [varchar](5) NOT NULL,
	[SocialSecurityProofRecord] [varchar](48) NOT NULL,
	[SocialSecurityProofExpeditionPlace] [varchar](79) NOT NULL,
	[SocialSecurityProofObservations] [varchar](52) NOT NULL,
	[MilitaryLetterOriginal] [varchar](5) NOT NULL,
	[MilitaryLetterCopy] [varchar](5) NOT NULL,
	[MilitaryLetterRecord] [varchar](47) NOT NULL,
	[MilitaryLetterExpeditionPlace] [varchar](19) NOT NULL,
	[MilitaryLetterObservations] [varchar](19) NOT NULL,
	[RFCOriginal] [varchar](5) NOT NULL,
	[RFCCopy] [varchar](5) NOT NULL,
	[RFCRecord] [varchar](33) NOT NULL,
	[RFCExpeditionPlace] [varchar](25) NOT NULL,
	[RFCObservations] [varchar](30) NOT NULL,
	[CriminalRecordLetterOriginal] [varchar](5) NOT NULL,
	[CriminalRecordLetterCopy] [varchar](5) NOT NULL,
	[CriminalRecordLetterRecord] [varchar](22) NOT NULL,
	[CriminalRecordLetterExpeditionPlace] [varchar](34) NOT NULL,
	[CriminalRecordLetterObservations] [varchar](20) NOT NULL,
	[INEFrontMediaId] [int] NOT NULL,
	[INEBackMediaId] [int] NOT NULL,
	[AddressProofMediaId] [int] NOT NULL,
	[BornCertificateMediaId] [int] NOT NULL,
	[CURPMediaId] [int] NOT NULL,
	[StudiesProofMediaId] [int] NOT NULL,
	[SocialSecurityProofMediaId] [int] NOT NULL,
	[MilitaryLetterMediaId] [int] NOT NULL,
	[RFCMediaId] [int] NOT NULL,
	[CriminalRecordMediaId] [int] NOT NULL,
	[PresentedIdentificationMediaId] [int] NOT NULL,
	[VerificationMediaId] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyIMSSValidation]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyIMSSValidation](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[CreditNumber] [varchar](19) NOT NULL,
	[CreditStatus] [varchar](41) NOT NULL,
	[GrantDate] [varchar](19) NOT NULL,
	[ConciliationClaimsSummary] [varchar](30) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyLaboralTrayectory]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyLaboralTrayectory](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[TrayectoryName] [varchar](29) NOT NULL,
	[CompanyName] [varchar](33) NOT NULL,
	[CandidateBusinessName] [varchar](35) NOT NULL,
	[CompanyBusinessName] [varchar](23) NOT NULL,
	[CandidateRole] [varchar](36) NOT NULL,
	[CompanyRole] [varchar](16) NOT NULL,
	[CandidatePhone] [varchar](20) NOT NULL,
	[CompanyPhone] [varchar](10) NOT NULL,
	[CandidateAddress] [varchar](19) NOT NULL,
	[CompanyAddress] [varchar](42) NOT NULL,
	[CandidateStartDate] [varchar](19) NOT NULL,
	[CompanyStartDate] [varchar](19) NOT NULL,
	[CandidateEndDate] [varchar](19) NOT NULL,
	[CompanyEndDate] [varchar](19) NOT NULL,
	[CandidateInitialRole] [varchar](56) NOT NULL,
	[CompanyInitialRole] [varchar](24) NOT NULL,
	[CandidateFinalRole] [varchar](14) NOT NULL,
	[CompanyFinalRole] [varchar](26) NOT NULL,
	[CandidateStartSalary] [numeric](22, 19) NOT NULL,
	[CompanyStartSalary] [numeric](22, 20) NOT NULL,
	[CandidateEndSalary] [numeric](23, 20) NOT NULL,
	[CompanyEndSalary] [numeric](23, 20) NOT NULL,
	[CandidateBenefits] [varchar](68) NOT NULL,
	[CompanyBenefits] [varchar](26) NOT NULL,
	[CandidateResignationReason] [varchar](52) NOT NULL,
	[CompanyResignationReason] [varchar](14) NOT NULL,
	[CandidateDirectBoss] [varchar](14) NOT NULL,
	[CompanyDirectBoss] [varchar](39) NOT NULL,
	[CandidateLaborUnion] [varchar](26) NOT NULL,
	[CompanyLaborUnion] [varchar](25) NOT NULL,
	[Recommended] [varchar](55) NOT NULL,
	[RecommendedReasonWhy] [varchar](17) NOT NULL,
	[Rehire] [varchar](32) NOT NULL,
	[RehireReason] [varchar](22) NOT NULL,
	[Observations] [varchar](54) NOT NULL,
	[Notes] [varchar](13) NOT NULL,
	[Media1Id] [int] NOT NULL,
	[Media2Id] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyNote]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyNote](
	[Id] [int] NOT NULL,
	[Description] [varchar](28) NOT NULL,
	[StudyId] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyPersonalReference]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyPersonalReference](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[ReferenceTitle] [varchar](28) NOT NULL,
	[Name] [varchar](26) NOT NULL,
	[CurrentJob] [varchar](41) NOT NULL,
	[Address] [varchar](68) NOT NULL,
	[Phone] [varchar](53) NOT NULL,
	[YearsKnowingEachOther] [varchar](21) NOT NULL,
	[KnowAddress] [varchar](35) NOT NULL,
	[YearsOnCurrentResidence] [varchar](34) NOT NULL,
	[KnowledgeAboutPreviousJobs] [varchar](24) NOT NULL,
	[OpinionAboutTheCandidate] [varchar](10) NOT NULL,
	[PoliticalActivity] [varchar](13) NOT NULL,
	[WouldYouRecommendIt] [varchar](14) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyPictures]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyPictures](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[Media1Id] [int] NOT NULL,
	[Media2Id] [int] NOT NULL,
	[Media3Id] [int] NOT NULL,
	[Media4Id] [int] NOT NULL,
	[Media5Id] [int] NOT NULL,
	[Media6Id] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudySchoolarity]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudySchoolarity](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[ScholarVerificationSummary] [varchar](53) NOT NULL,
	[ScholarVerificationMediaId] [int] NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudySocial]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudySocial](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[SocialArea] [varchar](66) NOT NULL,
	[Hobbies] [varchar](12) NOT NULL,
	[HealthInformation] [varchar](24) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserIdentitySearchPermission]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserIdentitySearchPermission](
	[AdminId] [int] NOT NULL,
	[FullSearch] [varchar](5) NOT NULL,
	[QueryFilter] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[Id] [int] NOT NULL,
	[StudyEconomicSituationId] [int] NOT NULL,
	[Plates] [varchar](20) NOT NULL,
	[SerialNumber] [varchar](28) NOT NULL,
	[BrandAndModel] [varchar](17) NOT NULL,
	[Owner] [varchar](59) NOT NULL,
	[PurchaseValue] [numeric](22, 20) NOT NULL,
	[CurrentValue] [numeric](23, 20) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Visit]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visit](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[VisitDate] [varchar](19) NOT NULL,
	[ConfirmAssistance] [varchar](5) NOT NULL,
	[VisitStatusId] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[Address] [varchar](39) NOT NULL,
	[Observations] [varchar](30) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
	[UnderAdminUserId] [varchar](30) NULL,
	[NotationColor] [varchar](14) NOT NULL,
	[EvidenceId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkStudy]    Script Date: 5/5/2023 2:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkStudy](
	[Id] [int] NOT NULL,
	[StudyId] [int] NOT NULL,
	[IdentificationCard] [varchar](5) NOT NULL,
	[AddressProof] [varchar](5) NOT NULL,
	[BirthCertificate] [varchar](5) NOT NULL,
	[CURP] [varchar](5) NOT NULL,
	[StudiesProof] [varchar](5) NOT NULL,
	[SocialSecurityNumber] [varchar](5) NOT NULL,
	[RFC] [varchar](5) NOT NULL,
	[MilitaryLetter] [varchar](5) NOT NULL,
	[CriminalRecordLetter] [varchar](5) NOT NULL,
	[CreatedAt] [varchar](19) NOT NULL,
	[UpdatedAt] [varchar](19) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [EstructuraDB] SET  READ_WRITE 
GO
