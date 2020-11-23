USE [DIMSCore]
GO
SET IDENTITY_INSERT [dbo].[TaskState] ON 

INSERT [dbo].[TaskState] ([StateId], [StateName]) VALUES (1, N'Active')
INSERT [dbo].[TaskState] ([StateId], [StateName]) VALUES (2, N'Pause')
INSERT [dbo].[TaskState] ([StateId], [StateName]) VALUES (3, N'Success')
INSERT [dbo].[TaskState] ([StateId], [StateName]) VALUES (4, N'Fail')
SET IDENTITY_INSERT [dbo].[TaskState] OFF
GO
