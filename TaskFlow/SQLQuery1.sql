USE [master]

IF db_id('TaskFlow') IS NULl
  CREATE DATABASE [TaskFlow]
GO

USE [TaskFlow]
GO


DROP TABLE IF EXISTS [Customer];
DROP TABLE IF EXISTS [Address];
DROP TABLE IF EXISTS [Job];
DROP TABLE IF EXISTS [WorkDay];
DROP TABLE IF EXISTS [Note];
DROP TABLE IF EXISTS [WorkRecord];
DROP TABLE IF EXISTS [UserProfile];
GO




CREATE TABLE [UserProfile] (
  [Id] integer PRIMARY KEY IDENTITY,
  [FirebaseUserId] NVARCHAR(28) NOT NULL,
  [FirstName] nvarchar(50) NOT NULL,
  [LastName] nvarchar(50) NOT NULL,
  [DisplayName] nvarchar(50) NOT NULL,
  [Email] nvarchar(555) NOT NULL,
  [CreateDateTime] datetime NOT NULL,

  CONSTRAINT UQ_FirebaseUserId UNIQUE(FirebaseUserId)
)

CREATE TABLE [Customer] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Name] nvarchar NOT NULL,
  [PhoneNumber] nvarchar NOT NULL,
)

CREATE TABLE [Address] (
  [Id] integer PRIMARY KEY IDENTITY,
  [CustomerId] integer NOT NULL,
  [Address] nvarchar NOT NULL, 

  CONSTRAINT [FK_Address_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]),
)

CREATE TABLE [Job] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Decsription] text NOT NULL,
  [ImageUrl] nvarchar,
  [CompletionDate] datetime,
  [CreateDate] datetime NOT NULL,
  [CustomerId] integer NOT NULL,
  

  CONSTRAINT [FK_Job_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id])
)

CREATE TABLE [WorkDay] (
  [Id] integer PRIMARY KEY IDENTITY,
  [UserProfileId] integer NOT NULL,
  [JobId] integer NOT NULL,

  CONSTRAINT [FK_WorkDay_Job] FOREIGN KEY ([JobId]) REFERENCES [Job] ([Id]),
  CONSTRAINT [FK_WorkDay_UserProfile] FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
)

CREATE TABLE [Note] (
  [Id] integer PRIMARY KEY IDENTITY,
   [UserProfileId] integer NOT NULL,
    [JobId] integer NOT NULL,
    [CreateDate] datetime NOT NULL,
  [NoteText] text NOT NULL,

  CONSTRAINT [FK_Note_Job] FOREIGN KEY ([JobId]) REFERENCES [Job] ([Id]),
  CONSTRAINT [FK_Note_UserProfile] FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
)

CREATE TABLE [WorkRecord] (
  [id] integer PRIMARY KEY IDENTITY,
 [UserProfileId] integer NOT NULL,
    [JobId] integer NOT NULL,
      [CreateDate] datetime NOT NULL,
       [NoteText] text NOT NULL,
       TimeOnJob decimal NOT NULL,
  
  CONSTRAINT [FK_WorkRecord_Job] FOREIGN KEY ([JobId]) REFERENCES [Job] ([Id]),
  CONSTRAINT [FK_WorkRecord_UserProfile] FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
)


GO