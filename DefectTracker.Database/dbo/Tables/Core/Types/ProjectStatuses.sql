CREATE TABLE [dbo].[ProjectStatuses]
(
	 [Id]			INT				NOT NULL IDENTITY(1,1)
	,[Name]			NVARCHAR(100)	NOT NULL, 
    CONSTRAINT [PK_ProjectStatuses] PRIMARY KEY ([Id]), 
)
