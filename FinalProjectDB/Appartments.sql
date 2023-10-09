CREATE TABLE [dbo].[Appartments]
(
	AppartmentId INT NOT NULL PRIMARY KEY,
    PropertyManagerId INT FOREIGN KEY REFERENCES Users(UserId),
    AddressId INT FOREIGN KEY REFERENCES Addresses(AddressId) ON DELETE NO ACTION NOT NULL,
    StatusId INT FOREIGN KEY REFERENCES AppartmentStatuses(StatusId),
    BuildingId INT FOREIGN KEY REFERENCES Buildings (BuildingId) ON DELETE CASCADE,
    NumberOfRooms INT NOT NULL,
    Amenities NVARCHAR(MAX),
    Price DECIMAL(10, 2) NOT NULL,
    Floor INT,
    ConstructionYear INT,
    Area FLOAT
)
