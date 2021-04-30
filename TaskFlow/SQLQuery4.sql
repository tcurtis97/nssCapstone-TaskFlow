SET IDENTITY_INSERT [UserType] ON
INSERT INTO [UserType]
  ([Id], [Name])
VALUES 
  (1, 'admin'), 
  (2, 'user');
SET IDENTITY_INSERT [UserType] OFF
  

SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile]
  ([Id], [FirebaseUserId], [Email], [CreateDateTime], [DisplayName], [FirstName], [LastName])
VALUES
  (1, 'E6YFEL7By5SmamI8i5tYmaewQx52', 'mradmin@admin.com', '2020-04-23', 'MrAdmin', 'Mr', 'Admin'),
  (2, 'RaA2XpHASVhJfgSds5nsWiAnPng2', 'admin@admin.com', '2020-03-18', 'Admin', 'Admin', 'Admin');
SET IDENTITY_INSERT [UserProfile] OFF


SET IDENTITY_INSERT [Customer] ON
INSERT INTO [Customer]
  ([Id], [Name], [PhoneNumber])
VALUES
  (1, 'Mr.Lay', '931-456-4347'),
  (2, 'Mrs.Jones', '931-421-2133')
SET IDENTITY_INSERT [Customer] OFF



SET IDENTITY_INSERT [Address] ON
INSERT INTO [Address]
  ([Id],  [CustomerId], [Address])
VALUES
  (1, 1, '123 Green rd'),
  (2, 2, '456 Cedar ave')
SET IDENTITY_INSERT [Address] OFF


CREATE TABLE [Job] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Decsription] text NOT NULL,
  [ImageUrl] nvarchar,
  [CompletionDate] datetime,
  [CreateDate] datetime NOT NULL,
  [CustomerId] integer NOT NULL,
  

  CONSTRAINT [FK_Job_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id])
)


SET IDENTITY_INSERT [Job] ON
INSERT INTO [Job]
  ([Id],  [Decsription], [ImageUrl], [CompletionDate],  [CreateDate], [CustomerId])
VALUES
  (1, 'Light bulb change', '2020-01-12', 1)
 
SET IDENTITY_INSERT [Job] OFF