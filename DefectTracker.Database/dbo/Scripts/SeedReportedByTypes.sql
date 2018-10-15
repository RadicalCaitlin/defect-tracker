;WITH [ReportedByTypes] AS (
	SELECT Id = 1, [Name] = 'Internal - Engineer'
	UNION
	SELECT Id = 2, [Name] = 'Internal - QA'
	UNION
	SELECT Id = 3, [Name] = 'Internal - Other'
	UNION
	SELECT Id = 4, [Name] = 'External - Owner'
	UNION
	SELECT Id = 5, [Name] = 'External - User'
	UNION
	SELECT Id = 6, [Name] = 'External - Other'
) 
MERGE INTO dbo.DefectReportedByTypes AS TARGET 
USING [ReportedByTypes] AS SOURCE ON TARGET.Id = SOURCE.Id
WHEN NOT MATCHED BY TARGET THEN INSERT
(
	[Id],
	[Name]
)
VALUES
(
	SOURCE.[Id],
	SOURCE.[Name]
)
WHEN MATCHED THEN UPDATE
	SET TARGET.Name = SOURCE.Name
WHEN NOT MATCHED BY SOURCE THEN DELETE;
