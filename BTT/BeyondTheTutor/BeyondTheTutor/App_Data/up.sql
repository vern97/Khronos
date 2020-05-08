CREATE TABLE [dbo].[BTTUsers]
(
	[ID]				INT IDENTITY (1,1) 	NOT NULL,
	[FirstName]			[NVARCHAR](50)		NOT NULL,
	[LastName]			[NVARCHAR](50)		NOT NULL,
	[ASPNetIdentityID]	NVARCHAR (128)		NOT NULL	
	CONSTRAINT [PK_dbo.BTTUsers] PRIMARY KEY CLUSTERED ([ID] ASC)

);


CREATE TABLE [dbo].[Students]
(
    [ID]                INT					NOT NULL,
    [GraduatingYear]    SMALLINT            NOT NULL,
    [ClassStanding]     [NVARCHAR](10)      NOT NULL 
	CONSTRAINT [PK_dbo.Students] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK.dbo.Students_dbo.BTTUsers_ID] FOREIGN KEY ([ID]) REFERENCES [BTTUsers] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[Tutors]
(
    [ID]                INT					NOT NULL,
    [ClassOf]           SMALLINT			NOT NULL,
    [VNumber]           [NVARCHAR](9)		NOT NULL,
	[AdminApproved]		BIT					NOT NULL
	CONSTRAINT [PK_dbo.Tutors] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK.dbo.Tutors_dbo.BTTUsers_ID] FOREIGN KEY ([ID]) REFERENCES [BTTUsers] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[Professors]
(
    [ID]                INT					NOT NULL,
	[AdminApproved]		BIT					NOT NULL
	CONSTRAINT [PK_dbo.Professors] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK.dbo.Professors_dbo.BTTUsers_ID] FOREIGN KEY ([ID]) REFERENCES [BTTUsers] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE

);

CREATE TABLE [dbo].[Admins]
(
	[ID]	INT		NOT NULL
	CONSTRAINT [PK_dbo.Admins] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK.dbo.Admins_dbo.BTTUsers_ID] FOREIGN KEY ([ID]) REFERENCES [BTTUsers] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
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
	CONSTRAINT [FK_dbo.TutorSchedule_dbo.Tutors_ID] FOREIGN KEY ([TutorID]) REFERENCES [dbo].[Tutors] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
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
		REFERENCES [dbo].[Tutors] ([ID])
);

CREATE TABLE [dbo].[StudentResources]
(
    [ID]				INT IDENTITY (1,1)	NOT NULL,
	[Topic]				NVARCHAR(50)		NOT NULL,
	[URL]				NVARCHAR(100)		NOT NULL,	
	[DisplayText]		NVARCHAR(50)		NOT NULL,
	[UserID]			INT					NULL
	CONSTRAINT [PK_dbo.StudentResources] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK_dbo.StudentResources_dbo.BTTUsers_ID] FOREIGN KEY ([UserID]) 
		REFERENCES [BTTUsers] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[TutoringServiceAlerts]
(
	[ID]		INT IDENTITY (1,1)		NOT NULL,
	[Status]	NVARCHAR(50)			NOT NULL,
	[EndTime]	DATETIME				NOT NULL,
	[TutorID]	INT						NOT NULL
	CONSTRAINT [PK_dbo.TutoringServiceAlerts] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK_dbo.TutoringServiceAlerts_dbo.Tutors_ID] FOREIGN KEY ([TutorID]) 
		REFERENCES [dbo].[Tutors] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[Surveys]
(
	[ID]				INT IDENTITY (1,1)	NOT NULL,
	[Name]				NVARCHAR(20)		NOT NULL,
	[Description]		TEXT,
	[ClassID]			INT					NOT NULL,
	CONSTRAINT [PK_dbo.Surveys] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Surveys_dbo.Classes_ClassID] FOREIGN KEY ([ClassID]) 
		REFERENCES [dbo].[Classes] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[Questions]
(
	[ID]				INT IDENTITY (1,1)	NOT NULL,
	[AskingQuestion]	TEXT				NOT NULL,
	[SurveyID]			INT					NOT NULL
	CONSTRAINT [PK_dbo.Questions] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK_dbo.Qusetions_dbo.SurveyID_SurveyID] FOREIGN KEY ([SurveyID]) 
		REFERENCES [dbo].[Surveys] ([ID])
	ON DELETE CASCADE ON UPDATE CASCADE

);

CREATE TABLE [dbo].[Answers]
(
	[UserID]			INT,
	[SurveyID]			INT,
	[QuestionID]		INT,
	[UserAnswer]		SMALLINT,
	CONSTRAINT [PK_dbo.Answers] PRIMARY KEY ([UserID], [SurveyID], [QuestionID]),

	CONSTRAINT [FK_dbo.Answers_dbo.BTTUsers_UserID] FOREIGN KEY ([UserID]) 
		REFERENCES [dbo].[BTTUsers] ([ID])
			ON DELETE CASCADE ON UPDATE CASCADE,


	CONSTRAINT [FK_dbo.Answers_dbo.Surveys_SurveyID] FOREIGN KEY ([SurveyID]) 
		REFERENCES [dbo].[Surveys] ([ID]),


	CONSTRAINT [FK_dbo.Answers_dbo.Questions_QuestionID] FOREIGN KEY ([QuestionID]) 
		REFERENCES [dbo].[Questions] ([ID])
			ON DELETE CASCADE ON UPDATE CASCADE

);

CREATE TABLE [dbo].[WeightedGrades]
(
    [ID]				INT IDENTITY (1,1)	NOT NULL,
	[RecordedDate]		[DATETIME]			NOT NULL,
	[ClassName]			NVARCHAR(50)		NOT NULL,	
	[Grade]				FLOAT				NOT NULL,
	[UserID]			INT					NOT NULL
	CONSTRAINT [PK_dbo.WeightedGrades] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK_dbo.WeightedGrades_dbo.BTTUsers_ID] FOREIGN KEY ([UserID]) 
		REFERENCES [BTTUsers] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[FinalGrades]
(
    [ID]				INT IDENTITY (1,1)	NOT NULL,
	[RecordedDate]		[DATETIME]			NOT NULL,
	[ClassName]			NVARCHAR(50)		NOT NULL,	
	[Grade]				FLOAT				NOT NULL,
	[UserID]			INT					NOT NULL
	CONSTRAINT [PK_dbo.FinalGrades] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK_dbo.FinalGrades_dbo.BTTUsers_ID] FOREIGN KEY ([UserID]) 
		REFERENCES [BTTUsers] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[CumulativeGPAs]
(
    [ID]				INT IDENTITY (1,1)	NOT NULL,
	[RecordedDate]		[DATETIME]			NOT NULL,
	[CumulativeGPA]		FLOAT				NOT NULL,
	[UserID]			INT					NOT NULL
	CONSTRAINT [PK_dbo.CumulativeGPAs] PRIMARY KEY CLUSTERED ([ID] ASC),

	CONSTRAINT [FK_dbo.CumulativeGPAs_dbo.BTTUsers_ID] FOREIGN KEY ([UserID]) 
		REFERENCES [BTTUsers] ([ID])
		ON DELETE CASCADE 
		ON UPDATE CASCADE
);


CREATE TABLE [TimeSheets] 
(
    [ID] 			INT IDENTITY (1, 1) 	NOT NULL,
    [Month] 		TINYINT					NOT NULL,
    [Year] 			SMALLINT 				NOT NULL,
	[TutorID]		INT						NOT NULL,
	CONSTRAINT [PK_TimeSheets] PRIMARY KEY CLUSTERED ([ID] ASC),

	CONSTRAINT [FK.dbo.TimeSheets_dbo.Tutors_ID] FOREIGN KEY ([TutorID]) 
		REFERENCES [dbo].[Tutors] ([ID])
		ON DELETE CASCADE 
		ON UPDATE CASCADE,
);


CREATE TABLE [Days] 
(
	[ID]			INT IDENTITY(1,1)	NOT NULL,
    [On] 			TINYINT 			NOT NULL,
    [RegularHrs] 	DECIMAL(4,2),
    [TimeSheetID] 	INT 				NOT NULL,
    CONSTRAINT [PK_Days] PRIMARY KEY ([ID]), 
		
	CONSTRAINT [FK.dbo.TutorTimeSheets_dbo.TimeSheets_ID] FOREIGN KEY ([TimeSheetID]) 
		REFERENCES [dbo].[TimeSheets] ([ID])
		ON DELETE CASCADE 
		ON UPDATE CASCADE
);


CREATE TABLE [WorkHours] 
(
	[ID] 				INT IDENTITY (1, 1) NOT NULL,
    [ClockedIn] 		DATETIME 			NOT NULL,
    [ClockedOut] 		DATETIME 			NOT NULL,
    [DayID] 			INT					NOT NULL,
    CONSTRAINT [PK_WorkHours] PRIMARY KEY ([ID]),
	
	CONSTRAINT [FK.dbo.WorkHours_dbo.Days_ID] FOREIGN KEY ([DayID]) 
	REFERENCES [dbo].[Days] ([ID])
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE [dbo].[ProfilePictures]
(
	[ID]			INT IDENTITY (1,1)	NOT NULL,
	[ImagePath]		VARBINARY (MAX)		NULL,
	[UserID]		INT					NOT NULL
	CONSTRAINT [PK_dbo.ProfilePictures] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK_dbo.ProfilePictures_dbo.BTTUsers_ID] FOREIGN KEY ([UserID]) 
		REFERENCES [BTTUsers] ([ID])
		ON DELETE CASCADE ON UPDATE CASCADE
);

