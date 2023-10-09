CREATE TABLE [dbo].[Logins]
(
	LoginId INT PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    Email NVARCHAR(100),
    Password NVARCHAR(100)
)
