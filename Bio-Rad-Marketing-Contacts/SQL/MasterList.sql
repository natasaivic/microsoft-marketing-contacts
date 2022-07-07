CREATE TABLE [dbo].[MasterList] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [CompanyName] NVARCHAR (50) NOT NULL,
    [VendorCode]  NVARCHAR (4)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([VendorCode] ASC)
);

