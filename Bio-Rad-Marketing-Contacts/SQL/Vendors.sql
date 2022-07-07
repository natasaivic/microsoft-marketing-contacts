CREATE TABLE [dbo].[Vendors] (
    [Id]          INT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Company]     NVARCHAR (50)  NOT NULL,
    [PhoneNumber] NVARCHAR (12)  NOT NULL,
    [Address]     NVARCHAR (100) NOT NULL,
    [CreatedOn]   DATETIME    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

