echo off
cls
echo -- BACKUP DATABASE --
set DATABASENAME=BaoHienCompany

:: filename format Name-Date (eg MyDatabase-2009.5.19.bak)
set DATESTAMP=%DATE:~-4%.%DATE:~7,2%.%DATE:~4,2%
set BACKUPFILENAME=%CD%\Temp\%DATABASENAME%.bak
set SERVERNAME=localhost\sqlexpress
echo.

sqlcmd -E -S %SERVERNAME% -d master -Q "BACKUP DATABASE [%DATABASENAME%] TO DISK = N'%BACKUPFILENAME%' WITH INIT , NOUNLOAD , NAME = N'%DATABASENAME% backup', NOSKIP , STATS = 10, NOFORMAT"
echo