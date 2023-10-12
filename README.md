# final-project-web

# User Interfaces

## Login/Authentication Interface
- Universal login for all users.
### Form login
1. TextBox and EditText
   - Login
   - Password
2. Buttons
   - Login
   - Signup
3. Attributes
   - UserId
   - RoleId
   - LoginId

### Form Create account for Tenant
1. TextBox and EditText
   - FirstName
   - LastName
   - Email
   - Password
   - Confirm Password
   - Address
     - StreetName
     - StreetNumber
     - City
     - PostalCode
     - Country
     - Province
     - Phone
2. Buttons
   - Signup
   - Back
3. Attributes
   - UserID
   - RoleId
   - LoginId

## Potential Tenant Interface
- For property browsing and rental inquiries.
### Dropdown List of Apartment and Building (only available)
1. ApartmentID
2. BuildingId
### TextBox
1. NumberOfRooms
2. Amenities
3. Price
4. Floor
5. ConstructionYear
6. Area
7. StatusName
8. ImageURL
9. Address
   - StreetName
   - StreetNumber
   - City
   - PostalCode
   - Country
   - Province
10. PropertyManager
    - FirstName
    - LastName
### Attributes
1. ApartmentID
2. PropertyManagerID
3. AddressId
4. StatusId
5. BuildingId
6. AddressId

### DropDown of Timestamp Make an appointment with the property manager
1. DropDown
   - Timestamp
2. Button
   - Make appointment

### Form Send necessary messages to the property manager
1. Button
   - Create a message (redirect to message box)

### Form messages Box
1. Dropdown
   - MessageId
2. TextBox and EditText
   - PropertyManagerID
     - FirstName
     - LastName
   - Subject
   - MessageBody
   - Timestamp
   - MessageStatusId
     - Status
3. Button
   - Answer

## Property Manager Interface
- For property-related tasks.
### Form Perform CRUD operations related to buildings
1. Dropdown List of Building
   - BuildingID
2. TextBox and EditText
   - BuildingId
   - Address
     - StreetName
     - StreetNumber
     - City
     - PostalCode
     - Country
     - Province
3. NumberOfFloors
4. ConstructionYear
5. Amenities
### Buttons
   - Create
   - Update
   - Delete

### Form Perform CRUD operations related to apartments
1. Dropdown List of Apartment
   - ApartmentID
2. TextBox and EditBox
   - AppointmentId
   - Address
     - StreetName
     - StreetNumber
     - City
     - PostalCode
     - Country
     - Province
   - StatusId
   - BuildingId
   - NumberOfRooms
   - Amenities
   - Price
   - Floor
   - ConstructionYear
   - Area
3. DropDown
   - PropertyManager
     - FirstName
     - LastName
### Buttons
   - Create
   - Update
   - Delete

### GridView
- Keep track of apartments' status
1. Dropdown
   - ApartmentID
   - StatusId
     - StatusName
2. DropDown
   - StatusId
     - StatusName
3. Button
   - Change Status

### GridView
- Schedule potential tenant’s appointments
1. AppointmentId
2. TenantID
3. Timestamp
4. AddressId
   - StreetName
   - StreetNumber
   - City
   - PostalCode
   - Country
   - Province

### Respond to potential tenants' messages
- Form messages Box
1. Dropdown
   - MessageId
2. TextBox and EditText
   - TenantID
     - FirstName
     - LastName
   - Subject
   - MessageBody
   - Timestamp
   - MessageStatusId
     - Status
3. Button
   - Answer

### Report any events to the property owner when necessary
- Form messages Box
1. Dropdown
   - EventId
2. TextBox and EditText
   - TenantID
     - FirstName
     - LastName
   - Subject
   - MessageBody
   - Timestamp
   - MessageStatusId
     - Status
3. Button
   - Answer

## Admin Interface
- For system management and overall control.
### Form Create/Update/Delete/Search/List any property manager account
1. Dropdown
   - PropertyManagerID
2. TextBox and EditBox
   - UserID
   - FirstName
   - LastName
   - RoleId
     - RoleName
   - Phone
   - AddressId
     - StreetName
     - StreetNumber
     - City
     - PostalCode
     - Country
     - Province
3. Buttons
   - Create
   - Update
   - Delete

### Form Update/Delete/Search/List any potential tenant account
1. Dropdown
   - TenantID
2. TextBox and EditBox
   - UserID
   - FirstName
   - LastName
   - RoleId
     - RoleName
   - Phone
   - AddressId
     - StreetName
     - StreetNumber
     - City
     - PostalCode
     - Country
     - Province
3. Buttons
   - Update
   - Delete

### Full control of the Web Site
- Hyperlink to everywhere?
