CREATE TABLE [dbo].[AppartmentImages]
(
	ImageId INT PRIMARY KEY,
    ApartmentId INT FOREIGN KEY REFERENCES Appartments(AppartmentId) ON DELETE CASCADE NOT NULL,
    ImageURL NVARCHAR(MAX) NOT NULL
)
