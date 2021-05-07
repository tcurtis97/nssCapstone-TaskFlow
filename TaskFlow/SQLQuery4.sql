
  

SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile]
  ([Id], [FirebaseUserId], [Email], [CreateDateTime], [DisplayName], [FirstName], [LastName])
VALUES
  (1, 'E6YFEL7By5SmamI8i5tYmaewQx52', 'mradmin@admin.com', '2020-04-23', 'MrAdmin', 'Mr', 'Admin'),
  (2, 'RaA2XpHASVhJfgSds5nsWiAnPng2', 'admin@admin.com', '2020-03-18', 'Admin', 'Admin', 'Admin');
SET IDENTITY_INSERT [UserProfile] OFF

SET ANSI_WARNINGS  OFF;
SET IDENTITY_INSERT [Customer] ON
INSERT INTO [Customer]
  ([Id], [Name], [PhoneNumber])
VALUES
  (1, 'Mr.Lay', '931-456-4347'),
  (2, 'Mrs.Jones', '931-421-2133')
SET IDENTITY_INSERT [Customer] OFF
SET ANSI_WARNINGS  ON;



SET IDENTITY_INSERT [Address] ON
INSERT INTO [Address]
  ([Id],  [CustomerId], [Address])
VALUES
  (1, 1, '123 Green rd'),
  (2, 2, '456 Cedar ave')
SET IDENTITY_INSERT [Address] OFF





SET IDENTITY_INSERT [Job] ON
INSERT INTO [Job]
  ([Id],  [Description],  [CreateDate], [CustomerId], [AddressId])
VALUES
  (1, 'Light bulb change', '2020-01-12', 1, 1)
 
SET IDENTITY_INSERT [Job] OFF



SET IDENTITY_INSERT [Note] ON
INSERT INTO [Note]
  ([Id],  [UserProfileId], [JobId], [CreateDate],  [NoteText])
VALUES
  (1, 1, 1, '2020-07-19', 'Bing pvc glue on the next trip')
 
SET IDENTITY_INSERT [Note] OFF


SET IDENTITY_INSERT [WorkDay] ON
INSERT INTO [WorkDay]
  ([Id],  [UserProfileId], [JobId])
VALUES
  (1, 1, 1)
 
SET IDENTITY_INSERT [WorkDay] OFF


SET IDENTITY_INSERT [WorkRecord] ON
INSERT INTO [WorkRecord]
  ([Id],  [UserProfileId], [JobId], [CreateDate],  [NoteText], [TimeOnJob])
VALUES
  (1, 1, 1, '2020-05-30', 'Arrived on call and found a failed light bulb, I pust in an order for a new light bulb for the next trip out.', 3)
 
SET IDENTITY_INSERT [WorkRecord] OFF


