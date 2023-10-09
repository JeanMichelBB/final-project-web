CREATE TABLE [dbo].[Appointments]
(
	AppointmentId INT PRIMARY KEY,
    PropertyManagerId INT FOREIGN KEY REFERENCES Users(UserId) NOT NULL,
    TenantId INT FOREIGN KEY REFERENCES Users(UserId) NOT NULL,
    Timestamp DATETIME NOT NULL,
    AddressId INT FOREIGN KEY REFERENCES Addresses(AddressId) NULL
)
