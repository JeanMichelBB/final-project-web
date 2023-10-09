CREATE TABLE [dbo].[Buildings]
(
	BuildingId INT NOT NULL PRIMARY KEY,
    AddressId INT NOT NULL FOREIGN KEY REFERENCES Addresses(AddressId),
    NumberOfFloors INT,
    ConstructionYear INT,
    Amenities NVARCHAR(MAX)
)
