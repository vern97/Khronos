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

CREATE TABLE [dbo].[Classes]
(
	[ID]				INT IDENTITY (1,1)	NOT NULL,
	[Name]				[NVARCHAR](50)		NOT NULL,
	CONSTRAINT [PK_dbo.Classes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[TutoringAppts]
(
    [ID]				INT IDENTITY (1,1)	NOT NULL,
	[StartTime]			[DATETIME]			NOT NULL,
	[EndTime]			[DATETIME] 			NOT NULL,
	[TypeOfMeeting]		[NVARCHAR](50)		NOT NULL,
	[ClassID]			INT					NOT NULL,
	[Length]			[NVARCHAR](50)		NOT NULL,
	[Status]			[NVARCHAR](50)		NOT NULL,
	[Note]				TEXT				NULL,
	[StudentID]			INT					NOT NULL,
	[TutorID]			INT					NULL
	CONSTRAINT [PK_dbo.TutoringAppts] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK_dbo.TutoringAppts_dbo.Classes_ID] FOREIGN KEY ([ClassID]) 
		REFERENCES [dbo].[Classes] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.TutoringAppts_dbo.Students_ID] FOREIGN KEY ([StudentID]) 
		REFERENCES [dbo].[Students] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.TutoringAppts_dbo.Tutors_ID] FOREIGN KEY ([TutorID]) 
		REFERENCES [dbo].[Tutors] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[StudentResources]
(
    [ID]				INT IDENTITY (1,1)	NOT NULL,
	[Topic]				NVARCHAR(50)		NOT NULL,
	[URL]				NVARCHAR(50)		NOT NULL,	
	[DisplayText]		NVARCHAR(50)		NOT NULL,
	[UserID]			INT					NULL
	CONSTRAINT [PK_dbo.StudentResources] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK_dbo.StudentResources_dbo.BTTUsers_ID] FOREIGN KEY ([UserID]) 
		REFERENCES [BTTUsers] ([ID]) ON DELETE CASCADE
);

INSERT INTO [dbo].[Classes](Name)
	VALUES
	('CS 122'),
	('CS 133'),
	('CS 160'),
	('CS 161'),
	('CS 162'),
	('CS 260'),
	('CS 271'),
	('CS 340'),
	('CS 360'),
	('CS 361'),
	('CS 363'),
	('CS 364'),
	('CS 365'),
	('CS 434'),
	('CS 465'),
	('IS 240'),
	('IS 278'),
	('IS 340'),
	('IS 345'),
	('IS 350'),
	('IS 355'),
	('IS 485');

INSERT INTO [dbo].[StudentResources](Topic, URL, DisplayText)
	VALUES
	('JavaScript', 'https://www.w3schools.com/js/default.asp', 'JavaScript Tutorial'),
	('CSS', 'https://www.w3schools.com/css/default.asp', 'CSS Tutorial'),
	('HTML', 'https://www.w3schools.com/html/default.asp', 'HTML Tutorial'),
	('Python', 'https://www.w3schools.com/python/default.asp', 'Python Tutorial'),
	('PHP', 'https://www.w3schools.com/php/default.asp', 'PHP Tutorial'),
	('Bootstrap', 'https://www.w3schools.com/bootstrap4/default.asp', 'Bootstrap 4 Tutorial'),
	('JQuery', 'https://www.w3schools.com/jquery/default.asp', 'JQuery Tutorial'),
	('SQL', 'https://www.w3schools.com/sql/default.asp', 'SQL Tutorial'),
	('C#', 'https://www.w3schools.com/cs/default.asp', 'C# Tutorial'),
	('Java', 'https://www.w3schools.com/java/default.asp', 'Java Tutorial');

