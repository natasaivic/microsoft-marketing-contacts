CREATE TABLE [dbo].[MasterList]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyName] NCHAR(50) NOT NULL, 
    [VendorCode] NCHAR(6) NOT NULL UNIQUE
)
