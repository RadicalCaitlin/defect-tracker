CREATE TABLE [dbo].[Tasks]
(
	 [Id]					INT				NOT NULL IDENTITY(1,1)
	,[Name]					NVARCHAR(200)	NOT NULL
	,[ActivityId]			INT				NOT NULL
	,[ProjectId]			INT				NOT NULL
	,[DateCreatedOffset]	DATETIMEOFFSET	NOT NULL
    CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Tasks_Activites] FOREIGN KEY ([ActivityId]) REFERENCES [Activities]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [Projects]([Id])
)
