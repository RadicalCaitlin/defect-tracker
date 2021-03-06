﻿CREATE TABLE [dbo].[Defects]
(
	 [Id]						INT					NOT NULL IDENTITY(1,1)
	,[DefectTypeId]				INT					NOT NULL
	,[DefectQualifierTypeId]	INT					NOT NULL
	,[OriginDateCreatedOffset]	DATETIMEOFFSET		NOT NULL DEFAULT GETUTCDATE()
	,[DateCreatedOffset]		DATETIMEOFFSET		NOT NULL DEFAULT GETUTCDATE()
	,[CreatedByUserId]			NVARCHAR(450)		NULL
	,[ProjectId]				INT					NOT NULL
	,[BugId]					INT					NOT NULL
	,[DefectModelTypeId]		INT					NOT NULL
    CONSTRAINT [PK_Defect] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Defect_DefectType] FOREIGN KEY ([DefectTypeId]) REFERENCES [DefectTypes]([Id]), 
    CONSTRAINT [FK_Defect_DefectQualifierType] FOREIGN KEY ([DefectQualifierTypeId]) REFERENCES [DefectQualifierTypes]([Id]),
    CONSTRAINT [FK_Defects_AspNetUsers] FOREIGN KEY ([CreatedByUserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_Defects_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [Projects]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_Defects_DefectModelTypes] FOREIGN KEY ([DefectModelTypeId]) REFERENCES [DefectModelTypes]([Id])
)
