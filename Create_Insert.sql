-- Country Table
CREATE TABLE Country (
    Id SERIAL PRIMARY KEY,
    CountryName VARCHAR(255) NOT NULL
);

-- City Table
CREATE TABLE City (
    Id SERIAL PRIMARY KEY,
    CountryId INT REFERENCES Country(Id),
    CityName VARCHAR(255) NOT NULL
);

-- MissionSkill Table
CREATE TABLE MissionSkill (
    Id SERIAL PRIMARY KEY,
    SkillName VARCHAR(255) NOT NULL,
    Status VARCHAR(50)
);

-- MissionTheme Table
CREATE TABLE MissionTheme (
    Id SERIAL PRIMARY KEY,
    ThemeName VARCHAR(255) NOT NULL,
    Status VARCHAR(50)
);

-- User Table
CREATE TABLE "User" (
    Id SERIAL PRIMARY KEY,
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    PhoneNumber VARCHAR(20),
    EmailAddress VARCHAR(255) UNIQUE,
    UserType VARCHAR(50),
    Password VARCHAR(255)
);

-- UserDetail Table
CREATE TABLE UserDetail (
    Id SERIAL PRIMARY KEY,
    UserId INT REFERENCES "User"(Id),
    Name VARCHAR(100),
    Surname VARCHAR(100),
    EmployeeId VARCHAR(100),
    Manager VARCHAR(100),
    Title VARCHAR(100),
    Department VARCHAR(100),
    MyProfile TEXT,
    WhyIVolunteer TEXT,
    CountryId INT REFERENCES Country(Id),
    CityId INT REFERENCES City(Id),
    Avilability VARCHAR(100),
    LinkdInUrl VARCHAR(255),
    MySkills TEXT,
    UserImage TEXT,
    Status BOOLEAN
);

-- UserSkills Table
CREATE TABLE UserSkills (
    Id SERIAL PRIMARY KEY,
    Skill VARCHAR(255),
    UserId INT REFERENCES "User"(Id)
);

-- Missions Table
CREATE TABLE Missions (
    Id SERIAL PRIMARY KEY,
    MissionTitle VARCHAR(255),
    MissionDescription TEXT,
    MissionOrganisationName VARCHAR(255),
    MissionOrganisationDetail TEXT,
    CountryId INT REFERENCES Country(Id),
    CityId INT REFERENCES City(Id),
    StartDate DATE,
    EndDate DATE,
    MissionType VARCHAR(50),
    TotalSheets INT,
    RegistrationDeadLine DATE,
    MissionThemeId VARCHAR(255),
    MissionSkillId VARCHAR(255),
    MissionImages TEXT,
    MissionDocuments TEXT,
    MissionAvilability VARCHAR(50),
    MissionVideoUrl TEXT
);

-- MissionApplication Table
CREATE TABLE MissionApplication (
    Id SERIAL PRIMARY KEY,
    MissionId INT REFERENCES Missions(Id),
    UserId INT REFERENCES "User"(Id),
    AppliedDate TIMESTAMP,
    Status BOOLEAN,
    Sheet INT
);

INSERT INTO Country (CountryName) VALUES
('India'),
('United States'),
('Germany');
INSERT INTO City (CountryId, CityName) VALUES
(1, 'Mumbai'),
(1, 'Delhi'),
(2, 'New York'),
(3, 'Berlin');
INSERT INTO MissionSkill (SkillName, Status) VALUES
('Teaching', 'Active'),
('Coding', 'Active'),
('Photography', 'Inactive');
INSERT INTO MissionTheme (ThemeName, Status) VALUES
('Education', 'Active'),
('Healthcare', 'Active'),
('Environment', 'Inactive');
INSERT INTO "User" (FirstName, LastName, PhoneNumber, EmailAddress, UserType, Password) VALUES
('John', 'Doe', '1234567890', 'john@example.com', 'Volunteer', 'pass123'),
('Jane', 'Smith', '0987654321', 'jane@example.com', 'Admin', 'pass456');
INSERT INTO UserDetail (UserId, Name, Surname, EmployeeId, Manager, Title, Department, MyProfile, WhyIVolunteer, CountryId, CityId, Avilability, LinkdInUrl, MySkills, UserImage, Status) VALUES
(1, 'John', 'Doe', 'EMP001', 'Manager A', 'Developer', 'IT', 'Profile text...', 'I love to help!', 1, 1, 'Weekends', 'https://linkedin.com/in/john', 'Teaching,Coding', 'john.jpg', true),
(2, 'Jane', 'Smith', 'EMP002', 'Manager B', 'Lead', 'HR', 'Profile text...', 'Making a difference matters.', 2, 3, 'Weekdays', 'https://linkedin.com/in/jane', 'Photography', 'jane.jpg', true);
INSERT INTO UserSkills (Skill, UserId) VALUES
('Teaching', 1),
('Coding', 1),
('Photography', 2);
INSERT INTO Missions (
    MissionTitle, MissionDescription, MissionOrganisationName, MissionOrganisationDetail,
    CountryId, CityId, StartDate, EndDate, MissionType, TotalSheets, RegistrationDeadLine,
    MissionThemeId, MissionSkillId, MissionImages, MissionDocuments, MissionAvilability, MissionVideoUrl
) VALUES
('Teach Kids', 'Helping students learn basic English.', 'TeachOrg', 'Teaching org description.', 1, 1, '2025-06-01', '2025-07-01', 'Onsite', 100, '2025-05-31', '1', '1', 'image1.jpg', 'doc1.pdf', 'Weekends', 'https://youtu.be/sample1'),
('Code for Good', 'Developing websites for NGOs.', 'CodeOrg', 'Coding for social causes.', 2, 3, '2025-08-01', '2025-09-01', 'Remote', 50, '2025-07-15', '2', '2', 'image2.jpg', 'doc2.pdf', 'Weekdays', 'https://youtu.be/sample2');
INSERT INTO MissionApplication (MissionId, UserId, AppliedDate, Status, Sheet) VALUES
(1, 1, CURRENT_TIMESTAMP, true, 1),
(2, 1, CURRENT_TIMESTAMP, false, 0),
(2, 2, CURRENT_TIMESTAMP, true, 1);