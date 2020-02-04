INSERT INTO [dbo].[Athletes] (FirstName, LastName, DateOfBirth) VALUES
	('Brandon', 'Linton', '1/24/97'),
	('Julia', 'Webster', '8/15/98')
GO

INSERT INTO [dbo].[Coaches] (FirstName, LastName) VALUES
	('Max', 'Stoyanov'),
	('Victoria', 'Rhine')
Go

INSERT INTO [dbo].[Teams] (CoachID, Name) VALUES
	(1, 'Wolves'),
	(2, 'Huskies')
GO

INSERT INTO [dbo].[AthleteTeams] (AthleteID, TeamID) VALUES
	(1, 1),
	(2, 2)
GO

INSERT INTO [dbo].[Meetings] (Name, MeetingDate) VALUES
	('Wolfland High 2020', '1/23/2020'),
	('Husk High 2019 Championship', '12/21/2019'),
	('Entry Level Meet', '4/6/2019')
GO


INSERT INTO [dbo].[Races] (AthleteID, MeetingID, FinishTime, TypeOfMeeting) VALUES
	('1', '1', 23.2, '50 meter freestyle'),
	('1', '1', 42.9, '100 meter freestyle'),
	('1', '1', 112, '200 meter freestyle'),
	('1', '1', 258, '400 meter freestyle'),
	('1', '2', 652, '1000 meter freestyle'),
	('1', '2', 26.5, '100 meter backstroke'),
	('1', '2', 45.7, '200 meter backstroke'),
	('1', '1', 30.2, '100 meter breaststroke'),
	('1', '3', 49.9, '200 meter breaststroke'),
	('1', '3', 28.2, '100 meter butterfly'),
	('1', '3', 53.7, '200 meter butterfly'),
	('1', '3', 42, '200 meter individual medley'),
	('1', '3', 51, '400 meter individual medley'),
	--athlete two--
	('2', '1', 24.9, '50 meter freestyle'),
	('2', '1', 39.7, '100 meter freestyle'),
	('2', '1', 111, '200 meter freestyle'),
	('2', '1', 260, '400 meter freestyle'),
	('2', '2', 638.9, '1000 meter freestyle'),
	('2', '2', 24.8, '100 meter backstroke'),
	('2', '2', 45.9, '200 meter backstroke'),
	('2', '1', 32.8, '100 meter breaststroke'),
	('2', '3', 44, '200 meter breaststroke'),
	('2', '3', 28, '100 meter butterfly'),
	('2', '3', 59, '200 meter butterfly'),
	('2', '3', 61, '200 meter individual medley'),
	('2', '3', 46, '400 meter individual medley')
Go

