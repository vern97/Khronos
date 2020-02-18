CREATE TABLE [dbo].[BTTUsers]
(
	[ID]				INT IDENTITY (1,1) 	NOT NULL,
	[FirstName]			[NVARCHAR](50)		NOT NULL,
	[LastName]			[NVARCHAR](50)		NOT NULL,
	[ASPNetIdentityID]	NVARCHAR (128)		NOT NULL	
	CONSTRAINT [PK_dbo.BTTBTTUsers] PRIMARY KEY CLUSTERED ([ID] ASC)
);


CREATE TABLE [dbo].[Students]
(
    [ID]                INT					NOT NULL,
    [GraduatingYear]    SMALLINT            NOT NULL,
    [ClassStanding]     [NVARCHAR](10)      NOT NULL 
	CONSTRAINT [PK_dbo.Students] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK.dbo.Students_dbo.BTTUsers_ID] FOREIGN KEY ([ID]) REFERENCES [BTTUsers] ([ID])
);


CREATE TABLE [dbo].[Tutors]
(
    [ID]                INT					NOT NULL,
    [ClassOf]           SMALLINT			NOT NULL,
    [VNumber]           [NVARCHAR](9)		NOT NULL,
	[AdminApproved]		BIT					NOT NULL
	CONSTRAINT [PK_dbo.Tutors] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK.dbo.Tutors_dbo.BTTUsers_ID] FOREIGN KEY ([ID]) REFERENCES [BTTUsers] ([ID])
);


CREATE TABLE [dbo].[Professors]
(
    [ID]                INT					NOT NULL,
	[AdminApproved]		BIT					NOT NULL
	CONSTRAINT [PK_dbo.Professors] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK.dbo.Professors_dbo.BTTUsers_ID] FOREIGN KEY ([ID]) REFERENCES [BTTUsers] ([ID])

);

CREATE TABLE [dbo].[Admins]
(
	[ID]	INT		NOT NULL
	CONSTRAINT [PK_dbo.Admins] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK.dbo.Admins_dbo.BTTUsers_ID] FOREIGN KEY ([ID]) REFERENCES [BTTUsers] ([ID])
);

CREATE TABLE [dbo].[TutorSchedule]
(
	[ID]				INT IDENTITY (1,1)	NOT NULL,
	[Description]		[NVARCHAR](50)		NOT NULL,
	[StartTime]			[DATETIME]			NOT NULL,
	[EndTime]			[DATETIME] 			NOT NULL,
	[ThemeColor]		[NVARCHAR](50)		NULL,
	[IsFullDay]			[BIT]				NULL,
	[TutorID]			INT					NOT NULL,
	CONSTRAINT [PK_dbo.TutorSchedule] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.TutorSchedule_dbo.Tutors_ID] FOREIGN KEY ([TutorID]) REFERENCES [dbo].[Tutors] ([ID]) ON DELETE CASCADE
);
