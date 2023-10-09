CREATE TABLE [dbo].[Addresses]
(
	AddressId INT NOT NULL PRIMARY KEY,
    StreetName NVARCHAR(100) NOT NULL,
    StreetNumber NVARCHAR(20) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    PostalCode NVARCHAR(10) NOT NULL,
    Country NVARCHAR(50),
    Province NVARCHAR(50)
)
