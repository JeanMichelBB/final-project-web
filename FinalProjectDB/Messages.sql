CREATE TABLE [dbo].[Messages]
(
	MessageId INT NOT NULL PRIMARY KEY,
    SenderId INT FOREIGN KEY REFERENCES Users(UserId) NOT NULL,
    ReceiverId INT FOREIGN KEY REFERENCES Users(UserId) NOT NULL,
    Subject NVARCHAR(100) NOT NULL,
    MessageBody NVARCHAR(MAX) NOT NULL,
    Timestamp DATETIME NOT NULL,
    MessageStatusId INT FOREIGN KEY REFERENCES MessageStatuses(MessageStatusId) NOT NULL
)
