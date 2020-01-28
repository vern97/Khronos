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