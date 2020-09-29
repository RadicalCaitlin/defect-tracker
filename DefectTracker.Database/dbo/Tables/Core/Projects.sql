CREATE TABLE [dbo].[Projects]
(
	 [Id]					INT				NOT NULL IDENTITY(1,1)
	,[Name]					NVARCHAR (100)	NOT NULL
	,[DateCreatedOffset]	DATETIMEOFFSET	NOT NULL
	,[OriginDateOffset]		DATETIMEOFFSET	NOT NULL
	--,[CreatedByUserId]		NVARCHAR (450)	NOT NULL
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]), 
    --CONSTRAINT [FK_Projects_ToTable] FOREIGN KEY ([CreatedByUserId]) REFERENCES [AspNetUsers]([Id])
)
