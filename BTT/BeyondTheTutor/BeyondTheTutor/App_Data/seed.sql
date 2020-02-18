
INSERT INTO [dbo].[BTTUsers](FirstName, LastName, ASPNetIdentityID)
	VALUES	('Victoria', 'Rhine', 'e2810071-8b0b-4465-aa61-0c07950231b1');

INSERT INTO [dbo].[Tutors](ClassOf, VNumber, AdminApproved)
	VALUES
	('2020', 'V00000123', 1);

INSERT INTO [dbo].[TutorSchedule](Description, StartTime, EndTime, TutorID)
	VALUES
	('Victoria Tutoring', '2/18/2020 8:00AM', '2/18/2020 11:00AM', 1),
	('Victoria Tutoring', '2/19/2020 9:00AM', '2/19/2020 2:00PM', 1),
	('Victoria Info Booth', '2/20/2020 11:00AM', '2/20/2020 4:00PM', 1);

