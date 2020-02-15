CREATE TABLE [dbo].[Students]
(
    [ID]                INT IDENTITY (1,1)  NOT NULL,
    [FirstName]         [NVARCHAR](50)      NOT NULL,
    [LastName]			[NVARCHAR](50)      NOT NULL,
    [GraduatingYear]	[DATETIME]          NOT NULL,
	[ClassStanding]		[NVARCHAR](10)		NOT NULL,
	[ASPNetIdentityID]	[NVARCHAR] (128)	NOT NULL	
    CONSTRAINT [PK_dbo.Students] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Tutors]
(
    [ID]                INT IDENTITY (1,1)	NOT NULL,
    [FirstName]         [NVARCHAR](50)		NOT NULL,
    [LastName]          [NVARCHAR](50)		NOT NULL,
	[ClassOf]			[DATETIME]			NOT NULL,
    [VNumber]           [NVARCHAR](9)		NOT NULL,
	[ASPNetIdentityID]	[NVARCHAR] (128)	NOT NULL
    CONSTRAINT [PK_dbo.Tutors] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Professors]
(
	[ID]				INT IDENTITY (1,1)	NOT NULL,
	[FirstName]			[NVARCHAR](50)		NOT NULL,
	[LastName]			[NVARCHAR](50)		NOT NULL,
	[ASPNetIdentityID]	[NVARCHAR] (128)	NOT NULL	
	CONSTRAINT [PK_dbo.Professors] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Admins]
(
	[ID]				INT IDENTITY (1,1)	NOT NULL,
	[FirstName]			[NVARCHAR](50)		NOT NULL,
	[LastName]			[NVARCHAR](50)		NOT NULL,
	[ASPNetIdentityID]	[NVARCHAR] (128)	NOT NULL	
	CONSTRAINT [PK_dbo.Admins] PRIMARY KEY CLUSTERED ([ID] ASC)
);

--select * from Students;