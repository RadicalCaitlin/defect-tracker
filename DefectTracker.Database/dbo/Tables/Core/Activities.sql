CREATE TABLE [dbo].[Activities]
(
	 [Id]					INT					NOT NULL IDENTITY(1,1)
	,[Name]					NVARCHAR(200)		NOT NULL
	,[AreaId]				INT					NOT NULL
	,[ParentAreaId]			INT					NULL
	,[ProjectUserId]		INT					NOT NULL
	,[ProjectId]			INT					NOT NULL
	,[DateCreatedOffset]	DATETIMEOFFSET	NOT NULL
    CONSTRAINT [PK_Activities] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Activities_ProjectAreas] FOREIGN KEY ([AreaId]) REFERENCES [ProjectAreas]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_Activities_Activities] FOREIGN KEY ([ParentAreaId]) REFERENCES [Activities]([Id]), 
    CONSTRAINT [FK_Activities_ProjectUsers] FOREIGN KEY ([ProjectUserId]) REFERENCES [ProjectUsers]([Id]), 
    CONSTRAINT [FK_Activities_Proejcts] FOREIGN KEY ([ProjectId]) REFERENCES [Projects]([Id])
)
