-- This file contains SQL statements that will be executed after the build script.
TRUNCATE TABLE [dbo].[WeatherForecasts];

INSERT INTO [dbo].[WeatherForecasts]
SELECT [value], DATEADD(day, [value], SYSDATETIME()), -20 + RAND(CONVERT(VARBINARY, NEWID())) * 75 FROM GENERATE_SERIES(1, 50);
