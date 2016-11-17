@echo off
    setlocal enableextensions

    set "repoDir="
    for /f "tokens=3 delims=<>" %%a in (
        'find /i "<repoDir>" ^< "tracker.xml"'
    ) do set "repoDir=%%a"
    echo %repoDir%

    set "taskName="
    for /f "tokens=3 delims=<>" %%a in (
        'find /i "<taskName>" ^< "tracker.xml"'
    ) do set "taskName=%%a"
    echo %taskName%
    
    