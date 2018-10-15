;WITH [DefectTypes] AS (
	SELECT Id = 1, [Name] = 'Function'
	UNION
	SELECT Id = 2, [Name] = 'Assignment'
	UNION
	SELECT Id = 3, [Name] = 'Checking'
	UNION
	SELECT Id = 4, [Name] = 'Interface/Contract'
	UNION
	SELECT Id = 5, [Name] = 'Timing/Serialization/Concurrency'
	UNION
	SELECT Id = 6, [Name] = 'Build/Package/Merge'
	UNION
	SELECT Id = 7, [Name] = 'Documentation'
	UNION
	SELECT Id = 8, [Name] = 'Algorithm'
) 
MERGE INTO dbo.DefectTypes AS TARGET 
USING [DefectTypes] AS SOURCE ON TARGET.Id = SOURCE.Id
WHEN NOT MATCHED BY TARGET THEN INSERT
(
	 [Id]
	,[Name]
)
VALUES
(
	 SOURCE.[Id]
	,SOURCE.[Name]
)
WHEN MATCHED THEN UPDATE
	SET TARGET.Name = SOURCE.Name
WHEN NOT MATCHED BY SOURCE THEN DELETE;
