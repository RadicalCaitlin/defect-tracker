CREATE TABLE [dbo].[Projects]
(
	 [Id]					INT				NOT NULL IDENTITY(1,1)
	,[Name]					NVARCHAR (100)	NOT NULL
	,[DateCreatedUtc]		DATETIMEOFFSET	NOT NULL DEFAULT GETUTCDATE()
	,[OriginDateUtc]		DATETIMEOFFSET	NOT NULL DEFAULT GETUTCDATE()
	,[DateLastUpdatedUtc]	DATETIMEOFFSET	NOT NULL DEFAULT GETUTCDATE()
	,[VersionNumber]		NVARCHAR(25)	NULL
	,[ClientId]				INT				NULL
	,[CreatedByUserId]		NVARCHAR (450)	NULL
	,[ProjectStatusId]		INT				NOT NULL DEFAULT 1
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Projects_AspNetUsers] FOREIGN KEY ([CreatedByUserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_Projects_Client] FOREIGN KEY ([ClientId]) REFERENCES [Clients]([Id]), 
    CONSTRAINT [FK_Projects_ProjectStatus] FOREIGN KEY ([ProjectStatusId]) REFERENCES [ProjectStatuses]([Id])
)
