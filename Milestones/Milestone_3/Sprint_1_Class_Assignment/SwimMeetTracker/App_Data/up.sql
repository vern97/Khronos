CREATE TABLE [dbo].[Coaches]
(
	[CID]			INT IDENTITY (1,1) 	NOT NULL,
	[FirstName]			[NVARCHAR](50)		NOT NULL,
	[LastName]			[NVARCHAR](50)		NOT NULL,
	CONSTRAINT [PK_dbo.Coaches] PRIMARY KEY CLUSTERED ([CID] ASC)
);

CREATE TABLE [dbo].[Teams]
(
    [TID]				INT IDENTITY (1,1)  NOT NULL,
	[CoachID]			INT					NOT NULL,
    [Name]				NVARCHAR(50)		NOT NULL,
    CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([TID] ASC),
	CONSTRAINT [FK_dbo.Teams_dbo.Coachs_CoachID] FOREIGN KEY ([CoachID]) 
		REFERENCES [dbo].[Coaches] ([CID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Athletes]
(
	[AID]			INT IDENTITY (1,1) 	NOT NULL,
	[FirstName]		[NVARCHAR](50)		NOT NULL,
	[LastName]		[NVARCHAR](50)		NOT NULL,
	[DateOfBirth]	[DATETIME]			NOT NULL
	CONSTRAINT [PK_dbo.Athletes] PRIMARY KEY CLUSTERED ([AID] ASC)
);

CREATE TABLE [dbo].[AthleteTeams]
(
	[ATID]		INT IDENTITY (1,1)	NOT NULL,
    [AthleteID]	INT                 NOT NULL,
    [TeamID]   	INT                 NOT NULL,
    CONSTRAINT [PK_dbo.AthleteTeams] PRIMARY KEY CLUSTERED ([ATID] ASC),
	CONSTRAINT [FK_dbo.AthleteTeams_dbo.Teams_TID] FOREIGN KEY ([TeamID]) 
		REFERENCES [dbo].[Teams] ([TID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.AthleteTeams_dbo.Athletes_AID] FOREIGN KEY ([AthleteID]) 
		REFERENCES [dbo].[Athletes] ([AID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Meetings]
(
    [EID]		INT IDENTITY (1,1)  NOT NULL,
    [Name]		NVARCHAR(50)		NOT NULL,
	[MeetingDate]	DATETIME			NOT NULL,
    CONSTRAINT [PK_dbo.Meetings] PRIMARY KEY CLUSTERED ([EID] ASC)
);

CREATE TABLE [dbo].[Races]
(
	[RID]			INT IDENTITY (1,1)	NOT NULL,
    [AthleteID]		INT                 NOT NULL,
    [MeetingID]   	INT                 NOT NULL,
	[FinishTime]	REAL				NOT NULL,
	[TypeOfMeeting]	NVARCHAR(50)		NOT NULL,
    CONSTRAINT [PK_dbo.Races] PRIMARY KEY CLUSTERED ([RID] ASC),
	CONSTRAINT [FK_dbo.Races_dbo.Meetings_EID] FOREIGN KEY ([MeetingID]) 
		REFERENCES [dbo].[Meetings] ([EID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Races_dbo.Athletes_AID] FOREIGN KEY ([AthleteID]) 
		REFERENCES [dbo].[Athletes] ([AID]) ON DELETE CASCADE
);