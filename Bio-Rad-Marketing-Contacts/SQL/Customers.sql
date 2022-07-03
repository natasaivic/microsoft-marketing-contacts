CREATE TABLE Customers
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(50) NOT NULL, 
    [Company] NCHAR(50) NOT NULL, 
    [PhoneNumber] NCHAR(12) NOT NULL, 
    [Address] NCHAR(100) NOT NULL, 
    [Note] NTEXT NULL, 
    [CreatedOn] DATETIME NOT NULL
)
