;WITH [ProjectStatuses] AS (
	SELECT Id = 1, [Name] = 'Active'
	UNION
	SELECT Id = 2, [Name] = 'Cancelled'
	UNION
	SELECT Id = 2, [Name] = 'Complete'
	UNION
	SELECT Id = 2, [Name] = 'On Hold'
) 
MERGE INTO dbo.ProjectStatuses AS TARGET 
USING [ProjectStatuses] AS SOURCE ON TARGET.Id = SOURCE.Id
WHEN NOT MATCHED BY TARGET THEN INSERT
(
	[Name]
)
VALUES
(
	SOURCE.[Name]
)
WHEN MATCHED THEN UPDATE
	SET TARGET.Name = SOURCE.Name
WHEN NOT MATCHED BY SOURCE THEN DELETE;
