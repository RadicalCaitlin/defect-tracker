CREATE TABLE [dbo].[DefectModelTypes]
(
	 [Id]			INT				NOT NULL IDENTITY(1,1)
	,[Name]			NVARCHAR(100)	NOT NULL, 
    CONSTRAINT [PK_DefectModelTypes] PRIMARY KEY ([Id])
)
