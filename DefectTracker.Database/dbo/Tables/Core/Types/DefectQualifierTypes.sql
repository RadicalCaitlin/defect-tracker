CREATE TABLE [dbo].[DefectQualifierTypes]
(
	 [Id]		INT				NOT NULL IDENTITY(1,1)
	,[Name]		NVARCHAR(100)	NOT NULL, 
    CONSTRAINT [PK_DefectQualifierType] PRIMARY KEY ([Id])
)
