@echo off

setlocal enableextensions enabledelayedexpansion

rem Parse info.xml

rem set "taskName="
rem for /f "tokens=3 delims=<>" %%a in (
rem     'find /i "<taskName>" ^< "info.xml"'
rem ) do set "taskName=%%a"

@echo off
for /f %%i in ('xmlstarlet\XML.EXE sel -t -v "//taskName" Info.xml') do (
	set taskName=%%i
	)
rem echo repoDir is %var%


rem if exist my_test_task do(
rem 	SCHTASKS /Create /XML NishantKarkiFile.xml /TN %taskName% /F
rem ) else do (
	SCHTASKS /Create /XML NishantKarkiFile.xml /TN git_automate /F
rem )
