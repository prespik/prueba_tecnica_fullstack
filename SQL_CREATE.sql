CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE Tasks (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500),

    -- Status como INT (enum)
    Status INT NOT NULL,

    UserId INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    AdditionalData NVARCHAR(MAX),

    CONSTRAINT FK_Tasks_Users FOREIGN KEY (UserId)
        REFERENCES Users(Id),

    -- Validación del enum
    CONSTRAINT CK_Tasks_Status CHECK (Status IN (0, 1, 2)),

    CONSTRAINT CK_Tasks_AdditionalData_JSON
        CHECK (AdditionalData IS NULL OR ISJSON(AdditionalData) = 1)
);

CREATE INDEX IX_Tasks_UserId_Status_CreatedAt
ON Tasks (UserId, Status, CreatedAt);
