# final-project-web

`Developed by @anyruizd and @JeanMichelBB`

### Database Design

#### 1. Users Table: 

   - UserID (INT, Primary Key, Not Null) 
   - FirstName (NVARCHAR(50)) 
   - LastName (NVARCHAR(50)) 
   - RoleId (INT, Foreign Key to Roles table, Not Null) 
   - Phone (NVARCHAR(20)) 
   - AddressId (INT, Foreign Key to Addresses table, Not Null) 

#### 2. Roles Table: 

   - RoleId (INT, Primary Key, Not Null) 
   - RoleName (NVARCHAR(50)) 

#### 3. Addresses Table: 

   - AddressId (INT, Primary Key, Not Null) 
   - StreetName (NVARCHAR(100)) 
   - StreetNumber (NVARCHAR(20)) 
   - City (NVARCHAR(50)) 
   - PostalCode (NVARCHAR(10)) 
   - Country (NVARCHAR(50)) 
   - Province (NVARCHAR(50)) 

#### 4. Login Table: 

   - LoginId (INT, Primary Key, Not Null) 
   - UserID (INT, Foreign Key to Users table, Not Null) 
   - Email (NVARCHAR(100)) 
   - Password (NVARCHAR(100)) 

#### 5. Apartments Table: 

   - ApartmentID (INT, Primary Key, Not Null) 
   - PropertyManagerID (INT, Foreign Key to Users table, Not Null) 
   - AddressId (INT, Foreign Key to Addresses table, Not Null) 
   - StatusId (INT, Foreign Key to Statuses table, Not Null) 
   - BuildingId (INT, Foreign Key to Buildings table, Not Null) 
   - NumberOfRooms (INT) 
   - Amenities (NVARCHAR(MAX)) 
   - Price (DECIMAL(10, 2)) 
   - Floor (INT) 
   - ConstructionYear (INT) 
   - Area (FLOAT)

#### 6. Statuses Table: 

   - StatusId (INT, Not Null, Primary Key) 
   - StatusName (NVARCHAR(50)) 

#### 7. Apartment Images Table: 

   - ImageId (INT, Primary Key, Not Null) 
   - ApartmentID (INT, Foreign Key to Apartments table, Not Null) 
   - ImageURL (NVARCHAR(MAX)) 

#### 8. Buildings Table: 

   - BuildingId (INT, Primary Key, Not Null) 
   - AddressId (INT, Foreign Key to Addresses table, Not Null) 
   - NumberOfFloors (INT) 
   - ConstructionYear (INT) 
   - Amenities (NVARCHAR(MAX)) 

#### 9. Appointments Table: 

   - AppointmentId (INT, Primary Key, Not Null) 
   - PropertyManagerID (INT, Foreign Key to Users table, Not Null) 
   - TenantID (INT, Foreign Key to Users table, Not Null) 
   - Timestamp (DATETIME) 
   - AddressId (INT, Foreign Key to Addresses table, Not Null) 

#### 10. Messages Table: 

   - MessageId (INT, Primary Key, Not Null) 
   - SenderID (INT, Foreign Key to Users table, Not Null) 
   - ReceiverID (INT, Foreign Key to Users table, Not Null) 
   - Subject (NVARCHAR(100)) 
   - MessageBody (NVARCHAR(MAX)) 
   - Timestamp (DATETIME) 
   - MessageStatusId (INT, Foreign Key to Message Statuses table, Not Null) 

#### 11. Message Status Table: 

   - MessageStatusId (INT, Primary Key, Not Null) 
   - Status (INT) 

#### 12. Events Table: 

   - EventId (INT, Primary Key, Not Null) 
   - PropertyManagerID (INT) 
   - PropertyOwnerID (INT) 
   - EventDescription (NVARCHAR(MAX)) 
   - ApartmentID (INT) 
   - Timestamp (DATETIME) 
   - EventTypeID (INT, Foreign Key to Event Types table) 

#### 13. Event Types Table: 

   - EventTypeId (INT, Primary Key) 
   - EventTypeName (NVARCHAR(50)) 

### Code Design

#### 1. Models
#### 2. Views

- Login
  - Form login for all users
- Registration
  - Form Create account for Tenant
  - Form Create account for Property Manager (Admin and Property Owner only)
- Home -> Apartments
  - Button to create an apartment
  - List of apartments
  - Search for an apartment
- Apartment/Id
   - Apartment Details
   - Button to update an apartment -> Redirects to the apartment form (Admin and Property Owner only)
   - Button to delete an apartment -> Redirects to the apartment form (Admin and Property Owner only)
   - Button to make an appointment with the property manager -> Redirects to the appointment form
   - Button to send messages to the property manager -> Redirects to the message form
- Apartment/Create (Admin and Property Owner only)
  - Form to create an apartment
- Apartment/Update (Admin and Property Owner only)
  - Form to update an apartment 
- Apartment/Delete (Admin and Property Owner only)
  - Apartment Details
  - Button to delete an apartment
- Create Appointment
  - Form to make an appointment with the property manager
  - Date picker
- Send Message
  - Form to send necessary messages to the property manager
- Buildings (Admin and Property Owner only)
  - Button to create a building -> Redirects to the building form
  - List of buildings
- Building/Id (Admin and Property Owner only)
  - Building Details
  - Button to update a building -> Redirects to the building form
  - Button to delete a building -> Redirects to the building form
- Building/Create (Admin and Property Owner only)
  - Form to create a building
- Building/Update (Admin and Property Owner only)
  - Form to update a building
- Building/Delete (Admin and Property Owner only)
  - Building Details
  - Button to delete a building
- Property Managers (Admin and Property Owner only)
  - List of property managers
  - Search for a property manager account 
  - Button to see a property manager account -> Redirects to the property manager details
- Property Manager/Id (Admin and Property Owner only)
  - Property Manager Details
  - Button to update a property manager account -> Redirects to the property manager form
  - Button to delete a property manager account -> Redirects to the property manager form
- Property Manager/Create (Admin and Property Owner only)
  - Form to create a property manager account
- Property Manager/Update (Admin and Property Owner only)
  - Form to update a property manager account
- Property Manager/Delete (Admin and Property Owner only)
  - Property Manager Details
  - Button to delete a property manager account
- Potential Tenants (Admin and Property Owner only)
  - List of potential tenants
  - Search for a potential tenant account 
  - Button to see a potential tenant account -> Redirects to the potential tenant details
- Potential Tenant/Id (Admin and Property Owner only)
  - Potential Tenant Details
  - Button to update a potential tenant account -> Redirects to the potential tenant form
  - Button to delete a potential tenant account -> Redirects to the potential tenant form
- Potential Tenant/Create (Admin and Property Owner only)
  - Form to create a potential tenant account
- Potential Tenant/Update (Admin and Property Owner only)
  - Form to update a potential tenant account
- Potential Tenant/Delete (Admin and Property Owner only)
  - Potential Tenant Details
  - Button to delete a potential tenant account
- Messages 
  - List of messages sent
  - List of messages received
  - Search for a message 
  - Button to see a message -> Redirects to the message details
  - Button to create a message -> Redirects to the message form
- Message/Id
  - Message Details
  - Button to answer a message -> Redirects to the create message form
- Message/Create
  - Form to create a message
- Message/Update
  - Form to update a message
- Message/Delete
  - Message Details
  - Button to delete a message
- Events
  - List of events
  - Search for an event 
  - Button to see an event -> Redirects to the event details
  - Button to create an event -> Redirects to the event form
- Event/Id
  - Event Details
  - Button to update an event -> Redirects to the event form
  - Button to delete an event -> Redirects to the event form
- Event/Create
  - Form to create an event
- Event/Update
  - Form to update an event
- Event/Delete
  - Event Details
  - Button to delete an event

#### 3. Controllers

### Functional Requirements

#### Users And Roles

- [ ] The system will have four types of users: admin, Property Owner, Property Manager, and Potential Tenant.
- [ ] The system will have one admin account.
- [ ] The system can have several property owner account.
- [ ] The system can have several property manager account.
- [ ] The system can have several potential tenant account.

### Admin / Property Owner
- Accounts Management
   - [ ] The property owner / admin can create a property manager account.
   - [ ] The property owner / admin can update a property manager account.
   - [ ] The property owner / admin can delete a property manager account.
   - [ ] The property owner / admin can search for a property manager account.
   - [ ] The property owner / admin can list all property manager accounts.
   - [ ] The property owner / admin can update a potential tenant account.
   - [ ] The property owner / admin can delete a potential tenant account.
   - [ ] The property owner / admin can search for a potential tenant account.
   - [ ] The property owner / admin can list all potential tenant accounts.
- Building Management
   - [ ] The property owner can create a building.
   - [ ] The property owner can update a building.
   - [ ] The property owner can delete a building.
   - [ ] The property owner can search for a building.
   - [ ] The property owner can list all buildings.
- Apartment Management
   - [ ] The property owner can create an apartment.
   - [ ] The property owner can update an apartment.
   - [ ] The property owner can delete an apartment.
   - [ ] The property owner can search for an apartment.
   - [ ] The property owner can list all apartments.

#### Property Manager
- Building Management
- [ ] The property manager can create a building.
- [ ] The property manager can update a building.
- [ ] The property manager can delete a building.
- [ ] The property manager can search for a building.
- [ ] The property manager can list all buildings.
- Apartment Management
- [ ] The property manager can create an apartment.
- [ ] The property manager can update an apartment.
- [ ] The property manager can delete an apartment.
- [ ] The property manager can search for an apartment.
- [ ] The property manager can list all apartments.
- Apartment Status
- [ ] The property manager can change the status of an apartment.
- Appointment Scheduling
- [ ] The property manager can schedule potential tenant’s appointments.
- [ ] The property manager can create, update, and delete appointments.
- Message Handling
- [ ] The property manager can respond to potential tenants' messages.
- [ ] The property manager can create, update, and delete messages.
- Reporting
- [ ] The property manager can report any events to the property owner when necessary.
- [ ] The property manager can create, update, and delete events.

#### Potential Tenant
- [ ] The potential tenant can create an account.
- [ ] The potential tenant can search and see the list of apartments.
- [ ] The potential tenant can create appointments with property managers.
- [ ] The potential tenant can send messages to property managers.

### Non-Functional Requirements
- Technologies
  - [ ] The system will be developed using ASP.NET Core MVC.
  - [ ] The system will be developed using C#.
  - [ ] The system will be developed using Visual Studio 2019.
  - [ ] The system will be developed using SQL Server.
  - [ ] The system will be developed using Entity Framework Core.
  - [ ] The system will be developed using Bootstrap.
  - [ ] The system will be developed using HTML5.
  - [ ] The system will be developed using CSS3.
  - [ ] The system will be developed using JavaScript.
  - [ ] The system will be developed using jQuery.

- Usability
  - [ ] The system should be accessible and usable on various web browsers and devices.
  - [ ] Security measures should be implemented to protect user data and sensitive information.
  - [ ] The system should be user-friendly and easy to use.
