@echo off

setlocal enableextensions

Parse info.xml and 
set "taskName="
for /f "tokens=3 delims=<>" %%a in (
    'find /i "<taskName>" ^< "xmls\info.xml"'
) do set "taskName=%%a"

SCHTASKS /Create /XML xmls\info.xml /TN taskName /F
pause