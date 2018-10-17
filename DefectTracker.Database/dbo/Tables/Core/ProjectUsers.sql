CREATE TABLE [dbo].[ProjectUsers]
(
	 [Id]					INT				NOT NULL IDENTITY(1,1)
	,[Name]					NVARCHAR(200)	NOT NULL
	,[ProjectId]			INT				NOT NULL
	,[DateCreatedOffset]	DATETIMEOFFSET	NOT NULL
    CONSTRAINT [PK_ProjectUsers] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_ProjectUsers_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [Projects]([Id]) ON DELETE CASCADE
)
