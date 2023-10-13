-- Check if the database exists, and if so, drop it
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'PropertyRentalDB')
    BEGIN
        ALTER DATABASE PropertyRentalDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        DROP DATABASE PropertyRentalDB;
    END

-- Create a new database
CREATE DATABASE PropertyRentalDB;

-- Use the newly created database
USE PropertyRentalDB;

-- Create the Roles Table
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    RoleName NVARCHAR(50)
);

-- Create the Addresses Table
CREATE TABLE Addresses (
    AddressID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    StreetName NVARCHAR(100),
    StreetNumber NVARCHAR(20),
    City NVARCHAR(50),
    PostalCode NVARCHAR(10),
    Country NVARCHAR(50),
    Province NVARCHAR(50)
);

-- Create the MessageStatus Table
CREATE TABLE MessageStatus (
    MessageStatusID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Status NVARCHAR(50)
);

-- Create the Statuses Table
CREATE TABLE Statuses (
    StatusID INT NOT NULL PRIMARY KEY,
    StatusName NVARCHAR(50)
);

-- Create the EventTypes Table
CREATE TABLE EventTypes (
    EventTypeID INT PRIMARY KEY IDENTITY(1,1),
    EventTypeName NVARCHAR(50)
);

-- Create the Users Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    RoleID INT NOT NULL,
    Phone NVARCHAR(20),
    AddressID INT NOT NULL,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
    FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID)
);

-- Create the Login Table
CREATE TABLE Login (
    LoginID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    UserID INT NOT NULL,
    Email NVARCHAR(100),
    Password NVARCHAR(100),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create the Apartments Table
CREATE TABLE Apartments (
    ApartmentID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    PropertyManagerID INT NOT NULL,
    AddressID INT NOT NULL,
    StatusID INT NOT NULL,
    BuildingID INT NOT NULL,
    NumberOfRooms INT,
    Amenities NVARCHAR(MAX),
    Price DECIMAL(10, 2),
    Floor INT,
    ConstructionYear INT,
    Area FLOAT
);

-- Create the ApartmentImages Table
CREATE TABLE ApartmentImages (
    ImageID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ApartmentID INT NOT NULL,
    ImageURL NVARCHAR(MAX),
    FOREIGN KEY (ApartmentID) REFERENCES Apartments(ApartmentID)
);

-- Create the Buildings Table
CREATE TABLE Buildings (
    BuildingID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AddressID INT NOT NULL,
    NumberOfFloors INT,
    ConstructionYear INT,
    Amenities NVARCHAR(MAX)
);

-- Create the Appointments Table
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    PropertyManagerID INT NOT NULL,
    TenantID INT NOT NULL,
    Timestamp DATETIME,
    AddressID INT NOT NULL,
    FOREIGN KEY (PropertyManagerID) REFERENCES Users(UserID),
    FOREIGN KEY (TenantID) REFERENCES Users(UserID),
    FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID)
);

-- Create the Messages Table
CREATE TABLE Messages (
    MessageID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    SenderID INT NOT NULL,
    ReceiverID INT NOT NULL,
    Subject NVARCHAR(100),
    MessageBody NVARCHAR(MAX),
    Timestamp DATETIME,
    MessageStatusID INT NOT NULL,
    FOREIGN KEY (SenderID) REFERENCES Users(UserID),
    FOREIGN KEY (ReceiverID) REFERENCES Users(UserID),
    FOREIGN KEY (MessageStatusID) REFERENCES MessageStatus(MessageStatusID)
);

-- Create the Events Table
CREATE TABLE Events (
    EventID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    PropertyManagerID INT,
    PropertyOwnerID INT,
    EventDescription NVARCHAR(MAX),
    ApartmentID INT,
    Timestamp DATETIME,
    EventTypeID INT,
    FOREIGN KEY (PropertyManagerID) REFERENCES Users(UserID),
    FOREIGN KEY (PropertyOwnerID) REFERENCES Users(UserID),
    FOREIGN KEY (ApartmentID) REFERENCES Apartments(ApartmentID),
    FOREIGN KEY (EventTypeID) REFERENCES EventTypes(EventTypeID)
);

-- Seed the Roles Table
INSERT INTO Roles (RoleName)
VALUES
    ('Admin'),
    ('Property Owner'),
    ('Property Manager'),
    ('Potential Tenant');

-- Seed the Addresses Table
INSERT INTO Addresses (StreetName, StreetNumber, City, PostalCode, Country, Province)
VALUES
    ('123 Main Street', 'Apt 101', 'Cityville', '12345', 'Countryland', 'Stateville'),
    ('456 Elm Street', 'Apt 202', 'Townsville', '54321', 'Countryland', 'Stateville');

-- Seed the MessageStatus Table
INSERT INTO MessageStatus (Status)
VALUES
    ('Unread'),
    ('Read'),
    ('Replied');

-- Seed the Statuses Table
INSERT INTO Statuses (StatusID, StatusName)
VALUES
    (1, 'Available'),
    (2, 'Rented'),
    (3, 'Under Maintenance');

-- Seed the EventTypes Table
INSERT INTO EventTypes (EventTypeName)
VALUES
    ('Financial'),
    ('Occupancy'),
    ('Maintenance');

-- Seed the Users Table
INSERT INTO Users (FirstName, LastName, RoleID, Phone, AddressID)
VALUES
    ('John', 'Doe', 1, '123-456-7890', 1), -- Admin
    ('Alice', 'Smith', 2, '987-654-3210', 2), -- Property Owner
    ('Bob', 'Johnson', 3, '555-123-4567', 1), -- Property Manager
    ('Eve', 'Brown', 4, '777-888-9999', 2); -- Potential Tenant

-- Seed the Login Table (for user with ID 1, the admin user)
INSERT INTO Login (UserID, Email, Password)
VALUES
    (1, 'admin@example.com', 'adminpassword');

-- Seed the Apartments Table
INSERT INTO Apartments (PropertyManagerID, AddressID, StatusID, BuildingID, NumberOfRooms, Amenities, Price, Floor, ConstructionYear, Area)
VALUES
    (3, 1, 1, 1, 2, 'Balcony, Parking', 1200.00, 2, 2010, 800),
    (3, 2, 2, 2, 3, 'Swimming Pool, Gym', 1500.00, 4, 2015, 1100);

-- Seed the ApartmentImages Table
INSERT INTO ApartmentImages (ApartmentID, ImageURL)
VALUES
    (1, 'image1.jpg'),
    (1, 'image2.jpg'),
    (2, 'image3.jpg'),
    (2, 'image4.jpg');

-- Seed the Buildings Table
INSERT INTO Buildings (AddressID, NumberOfFloors, ConstructionYear, Amenities)
VALUES
    (1, 5, 2010, 'Parking Garage'),
    (2, 10, 2015, 'Swimming Pool');

-- Seed the Appointments Table
INSERT INTO Appointments (PropertyManagerID, TenantID, Timestamp, AddressID)
VALUES
    (3, 4, '2023-10-15 10:00:00', 2),
    (3, 4, '2023-10-17 14:00:00', 2);

-- Seed the Messages Table
INSERT INTO Messages (SenderID, ReceiverID, Subject, MessageBody, Timestamp, MessageStatusID)
VALUES
    (1, 2, 'Regarding Your Property', 'Let''s discuss the maintenance schedule.', '2023-10-13 15:30:00', 1),
    (4, 3, 'Rental Inquiry', 'I''m interested in renting an apartment.', '2023-10-14 09:15:00', 2);

-- Seed the Events Table (Event Type with ID 1, "Maintenance")
INSERT INTO Events (PropertyManagerID, PropertyOwnerID, EventDescription, ApartmentID, Timestamp, EventTypeID)
VALUES
    (3, 2, 'Scheduled Maintenance', 1, '2023-10-20 09:00:00', 1),
    (3, 2, 'Inspection', 2, '2023-10-22 11:30:00', 2);
