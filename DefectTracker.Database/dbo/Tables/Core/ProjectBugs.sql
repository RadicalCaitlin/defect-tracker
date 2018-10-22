CREATE TABLE [dbo].[ProjectBugs]
(
	 [Id]					INT					NOT NULL IDENTITY(1,1)
	,[Title]				NVARCHAR(100)		NOT NULL
	,[WorkItemId]			INT					NOT NULL
	,[ProjectId]			INT					NOT NULL
	,[OriginDateOffset]		DATETIMEOFFSET		NOT NULL
	,[DateCreatedOffset]	DATETIMEOFFSET		NOT NULL
    CONSTRAINT [PK_ProjectBugs] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_ProjectBugs_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [Projects]([Id])
)
