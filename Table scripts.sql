CREATE TABLE [dbo].[Person] (
    [PersonID] UNIQUEIDENTIFIER NOT NULL,
    [Name]     VARCHAR (100)    NOT NULL,
    [DOB]      DATETIME         NOT NULL,
    [Gender]   VARCHAR (10)     NOT NULL,
    [Suburb]   VARCHAR (50)     NOT NULL,
    [postcode] VARCHAR (5)      NOT NULL,
    [State]    VARCHAR (15)     NOT NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([PersonID] ASC)
);



CREATE TABLE [dbo].[Events] (
    [EventID] UNIQUEIDENTIFIER NOT NULL,
    [PersonName]     VARCHAR (100)    NOT NULL,
    [PersonDOB]      DATETIME         NOT NULL,
    [Description]   VARCHAR (500)     NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([EventID] ASC)
);


-- Database level
ALTER DATABASE PersonEventsDemoDB SET ENABLE_BROKER;