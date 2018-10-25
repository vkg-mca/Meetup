CREATE TABLE [dbo].[MeetupDetail]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Date] DATETIME2,
	[Topic] VARCHAR(500),
	[ParticipantsCount] INT
)
