CREATE TABLE [dbo].[WeatherForecasts]
(
  [Id] INT NOT NULL PRIMARY KEY,
  [Date] DATE NOT NULL,
  [DegreesCelsius] INT NOT NULL
)
GO

INSERT INTO [dbo].[WeatherForecasts]
SELECT [value], DATEADD(day, [value], SYSDATETIME()), -20 + RAND(CONVERT(VARBINARY, NEWID())) * 75 FROM GENERATE_SERIES(1, 50);
GO
