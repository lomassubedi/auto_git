@echo off

setlocal enableextensions

Parse info.xml and 
set "taskName="
for /f "tokens=3 delims=<>" %%a in (
    'find /i "<taskName>" ^< "tracker.xml"'
) do set "taskName=%%a"

SCHTASKS /Create /XML tracker.xml /TN taskName /F
pause