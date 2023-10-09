CREATE TABLE [dbo].[Events]
(
	EventId INT NOT NULL PRIMARY KEY,
    PropertyManagerId INT NOT NULL,
    PropertyOwnerId INT NOT NULL,
    EventDescription NVARCHAR(MAX) NOT NULL,
    ApartmentId INT NULL,
    Timestamp DATETIME,
    EventTypeId INT FOREIGN KEY REFERENCES EventTypes(EventTypeId) NOT NULL
)
