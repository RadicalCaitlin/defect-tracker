CREATE TABLE [dbo].[DefectTypes]
(
	 [Id]				INT					NOT NULL
	,[Name]				NVARCHAR(100)		NOT NULL
	,[DefectModelId]	INT				NOT NULL Default 1
    CONSTRAINT [PK_DefectType] PRIMARY KEY ([Id])
)
