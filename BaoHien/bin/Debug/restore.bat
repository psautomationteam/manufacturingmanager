cho off
cls
echo -- RESTORE DATABASE --
set DATABASENAME=BaoHienCompany
set BACKUPFILENAME=Temp\%DATABASENAME%.bak
set SERVERNAME=localhost\sqlexpress
sqlcmd -E -S %SERVERNAME% -d master -Q "ALTER DATABASE [%DATABASENAME%] SET SINGLE_USER WITH ROLLBACK IMMEDIATE"

:: WARNING - delete the database, suits me
:: sqlcmd -E -S %SERVERNAME% -d master -Q "IF EXISTS (SELECT * FROM sysdatabases WHERE name=N'%DATABASENAME%' ) DROP DATABASE [%DATABASENAME%]"
:: sqlcmd -E -S %SERVERNAME% -d master -Q "CREATE DATABASE [%DATABASENAME%]"

:: restore
sqlcmd -E -S %SERVERNAME% -d master -Q "RESTORE DATABASE [%DATABASENAME%] FROM DISK = N'%CD%\%BACKUPFILENAME%' WITH REPLACE"

:: remap user/login (http://msdn.microsoft.com/en-us/library/ms174378.aspx)
sqlcmd -E -S %SERVERNAME% -d %DATABASENAME% -Q "sp_change_users_login 'Update_One', 'sa', 'baohien123'"
sqlcmd -E -S %SERVERNAME% -d master -Q "ALTER DATABASE [%DATABASENAME%] SET MULTI_USER"
echo.