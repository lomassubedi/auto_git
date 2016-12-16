@echo off

setlocal enableextensions

rem Parse info.xml and
set "taskName="
for /f "tokens=3 delims=<>" %%a in (
    'find /i "<taskName>" ^< "info.xml"'
) do set "taskName=%%a"

SCHTASKS /Create /XML NishantKarkiFile.xml /TN %taskName% /F
pause
