﻿CREATE TABLE Customers
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [PhoneNumber] NVARCHAR(12) NOT NULL, 
    [Address] NVARCHAR(100) NOT NULL, 
    [Note] NTEXT NULL, 
    [CreatedOn] DATETIME NOT NULL
)