--CREATE DATABASE JOBFAIR;
--USE JOBFAIR;

CREATE TABLE COMPANY (
    companyId INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(30) NOT NULL UNIQUE,
    sector VARCHAR(30) NOT NULL,
    description VARCHAR(30),
    contactEmail VARCHAR(30) NOT NULL CHECK (contactEmail LIKE '_%@_%._%'),
    contactPhone VARCHAR(11) NOT NULL CHECK (contactPhone LIKE '[0-9]%')
);

CREATE TABLE USERS (
    userId INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(30) NOT NULL,
    email VARCHAR(30) UNIQUE NOT NULL,
    password VARCHAR(30) NOT NULL,
    phoneNumber VARCHAR(11) NOT NULL,
    role VARCHAR(30) NOT NULL CHECK (role IN ('Student', 'Recruiter', 'Admin', 'BoothCoordinator')),
    isApproved BIT NOT NULL,
    isActive BIT NOT NULL,
	--Constraint(s)
	CHECK ((role <> 'Student') OR (email LIKE 'i2_____%@isb.nu.edu.pk'))
);

CREATE TABLE STUDENT (
    studentId VARCHAR(8) PRIMARY KEY CHECK (studentId LIKE '2_I-____'),
    batchYear INT NOT NULL CHECK (batchYear >= 2000 AND batchYear <= YEAR(GETDATE())),
    status VARCHAR(20) NOT NULL CHECK (status IN ('Active', 'Graduated', 'Suspended', 'Dropped')),
	--Foregin key(s)
    userId INT NOT NULL UNIQUE,
	FOREIGN KEY (userId) references USERS(UserId)
);

CREATE TABLE SKILL (
    skillCertId INT PRIMARY KEY IDENTITY(1,1),
    title VARCHAR(50) NOT NULL,
    type VARCHAR(30) NOT NULL CHECK (type IN ('Technical', 'Soft', 'Language', 'Other')),
    issuedBy VARCHAR(30) NOT NULL,
    description VARCHAR(100),
    issuedDate DATE NOT NULL CHECK (issuedDate <= GETDATE()),
	--Foregin key(s)
    studentId VARCHAR(8) NOT NULL,
    FOREIGN KEY (studentId) REFERENCES STUDENT(studentId)
);

CREATE TABLE ACADEMIC_RECORD (
    recordId INT PRIMARY KEY IDENTITY(1,1),
    gpa FLOAT NOT NULL CHECK (gpa >= 0.0 AND gpa <= 4.0),
    currentSemester VARCHAR(10) NOT NULL CHECK (currentSemester IN ('Spring', 'Summer', 'Fall')),
    degreeProgram VARCHAR(30) NOT NULL CHECK (degreeProgram IN ('CS', 'SE', 'AI', 'DS')),
	--Foregin key(s)
    studentId VARCHAR(8) NOT NULL,
    FOREIGN KEY (studentId) REFERENCES STUDENT(studentId)
);

CREATE TABLE ADMIN (
    adminId INT PRIMARY KEY IDENTITY(1,1),
    status VARCHAR(20) NOT NULL CHECK (status IN ('Active', 'Inactive', 'Suspended')),
	--Foregin key(s)
    userId INT NOT NULL UNIQUE,
    FOREIGN KEY (userId) REFERENCES USERS(userId)
);

CREATE TABLE BOOTHCOORDINATOR (
    boothCoordinatorId INT PRIMARY KEY IDENTITY(1,1),
    shiftStartTime TIME NOT NULL,
    shiftEndTime TIME NOT NULL,
	--Foregin key(s)
    userId INT NOT NULL UNIQUE,
    FOREIGN KEY (userId) REFERENCES USERS(userId),
	--Constraint(s)
	CHECK (shiftEndTime > shiftStartTime),
);

CREATE TABLE BOOTH (
    boothId INT PRIMARY KEY IDENTITY(1,1),
    location VARCHAR(50) NOT NULL,
    studentVisited INT NOT NULL CHECK (studentVisited >= 0) DEFAULT 0,
	--Foregin key(s)
    adminId INT NOT NULL,
    companyId INT NOT NULL,
	boothCoordinatorId INT NOT NULL,
    FOREIGN KEY (adminId) REFERENCES ADMIN(adminId),
    FOREIGN KEY (companyId) REFERENCES COMPANY(companyId),
    FOREIGN KEY (boothCoordinatorId) REFERENCES BOOTHCOORDINATOR(boothCoordinatorId)
);


CREATE TABLE JOBFAIREVENT (
    jobFairEventId INT PRIMARY KEY IDENTITY(1,1),
    eventName VARCHAR(20) NOT NULL,
    description VARCHAR(100),
    status VARCHAR(20) NOT NULL CHECK (status IN ('Scheduled', 'Ongoing', 'Completed', 'Cancelled')),
    eventDate DATE NOT NULL CHECK (eventDate >= GETDATE()),
    eventTime TIME NOT NULL,
    location VARCHAR(50) NOT NULL,
	--Foregin key(s)
    adminId INT NOT NULL,
    FOREIGN KEY (adminId) REFERENCES ADMIN(adminId)
);

CREATE TABLE RECRUITER (
    recruiterId INT PRIMARY KEY IDENTITY(1,1),
    designation VARCHAR(30) NOT NULL,
	--Foregin key(s)
    userId INT NOT NULL UNIQUE,
    companyId INT,
    FOREIGN KEY (userId) REFERENCES USERS(userId),
    FOREIGN KEY (companyId) REFERENCES COMPANY(companyId)
);

CREATE TABLE JOBPOSTING (
    jobPostingId INT PRIMARY KEY IDENTITY(1,1),
    title VARCHAR(50) NOT NULL,
    jobType VARCHAR(20) NOT NULL CHECK (jobType IN ('Full-Time', 'Internship')),
    startingSalary DECIMAL(18, 2) NOT NULL CHECK (startingSalary >= 0),
    endingSalary DECIMAL(18, 2) NOT NULL,
    description VARCHAR(100) NOT NULL,
    status VARCHAR(20) NOT NULL CHECK (status IN ('Open', 'Closed', 'In Progress')),
	--Foregin key(s)
    recruiterId INT NOT NULL,
    FOREIGN KEY (recruiterId) REFERENCES RECRUITER(recruiterId),
	--Constraint(s)
	CHECK (endingSalary >= startingSalary),
);

CREATE TABLE REQUIREDSKILLS (
    skillId INT PRIMARY KEY IDENTITY(1,1),
    description VARCHAR(100) NOT NULL,
	--Foregin key(s)
    jobPostingId INT NOT NULL,
    FOREIGN KEY (jobPostingId) REFERENCES JOBPOSTING(jobPostingId)
);

CREATE TABLE APPLICATION (
    applicationId INT PRIMARY KEY IDENTITY(1,1),
    applicationDate DATE NOT NULL CHECK (applicationDate <= GETDATE()),
    status VARCHAR(20) NOT NULL CHECK (status IN ('Pending', 'Accepted', 'Rejected')),
	--Foregin key(s)
	jobPostingId INT NOT NULL,
    studentId VARCHAR(8) NOT NULL,
	recruiterId INT,
    FOREIGN KEY (jobPostingId) REFERENCES JOBPOSTING(jobPostingId),
    FOREIGN KEY (studentId) REFERENCES STUDENT(studentId),
	FOREIGN KEY (recruiterId) REFERENCES RECRUITER(recruiterId),
	--constraint(s)
	UNIQUE(studentId, jobPostingId)
);

CREATE TABLE INTERVIEW (
    interviewId INT PRIMARY KEY IDENTITY(1,1),
    interviewDate DATE NOT NULL CHECK (interviewDate >= GETDATE()),
    interviewTime TIME NOT NULL,
    interviewStatus VARCHAR(10) NOT NULL CHECK (interviewStatus IN ('Scheduled', 'Completed', 'Cancelled')),
	--Foregin key(s)
    boothId INT NOT NULL,
	applicationId INT,
    recruiterId INT NOT NULL,
    FOREIGN KEY (boothId) REFERENCES BOOTH(boothId),
    FOREIGN KEY (applicationId) REFERENCES APPLICATION(applicationId),
    FOREIGN KEY (recruiterId) REFERENCES RECRUITER(recruiterId)
);

CREATE TABLE REVIEW (
    reviewId INT PRIMARY KEY IDENTITY(1,1),
    comments VARCHAR(50),
    rating DECIMAL(3, 2) NOT NULL CHECK (rating >= 0 AND rating <= 5),
	--Foregin key(s)
    studentId VARCHAR(8) NOT NULL,
    interviewId INT NOT NULL UNIQUE,
    FOREIGN KEY (studentId) REFERENCES STUDENT(studentId),
    FOREIGN KEY (interviewId) REFERENCES INTERVIEW(interviewId)
);

CREATE TABLE HIRING_OUTCOME (
    outcomeId INT PRIMARY KEY IDENTITY(1,1),
    finalStatus VARCHAR(10) NOT NULL CHECK (finalStatus IN ('Hired', 'Rejected')),
    decisionDate DATE NOT NULL,
    packageOffered INT CHECK (packageOffered >= 0),
    remarks VARCHAR(30),
	--Foregin key(s)
    interviewId INT NOT NULL UNIQUE,
    FOREIGN KEY (interviewId) REFERENCES INTERVIEW(interviewId)
);