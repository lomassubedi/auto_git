@echo off
setlocal EnableDelayedExpansion

rem Local String variables
set "sh_exe=bin\sh.exe"
set "script_file_name=\git_bash_scripts.sh"

rem Read the Git for Windows installation path from the Registry.
for %%k in (HKCU HKLM) do (
    for %%w in (\ \Wow6432Node\) do (
        for /f "skip=2 delims=: tokens=1*" %%a in ('reg query "%%k\SOFTWARE%%wMicrosoft\Windows\CurrentVersion\Uninstall\Git_is1" /v InstallLocation 2^> nul') do (
            for /f "tokens=3" %%z in ("%%a") do (
                set GIT=%%z:%%b
                rem echo Found Git at "!GIT!".                
            )
        )
    )
)

set sh_exe_path=%GIT%
set sh_exe_path=%sh_exe_path%%sh_exe%
set sh_exe_path=^"%sh_exe_path%^"
rem echo %sh_exe_path%


REM get the path
set "repoDir="
for /f "tokens=3 delims=<>" %%a in (
    'find /i "<repoDir>" ^< "info.xml"'
) do set "repoDir=%%a"
rem echo %repoDir%

rem append the file name
set final_path=%repoDir%%script_file_name%
echo %final_path%
%final_path%
pause


rem rem Copy path in DOS style 
rem set unix_path=%final_path% 

rem rem replace '\' with '/' for unix path conversion
rem set unix_path=%unix_path:\=/%

rem rem Omit ':'
rem set unix_path=%unix_path::=%

rem rem Omit the white space present
rem call :Trim unix_path %unix_path%

rem rem Add quotation marks at the end
rem set unix_path=^"^/%unix_path%^" &rem append '"'

rem rem Omit the white space present in the ends again
rem call :Trim unix_path %unix_path%

rem rem discard white space at the end and manage to place "\ " instead of 
rem rem solve for white space present directory
rem set unix_path=%unix_path: =\ %
rem rem echo %unix_path%

rem rem for repo dir
rem set dirOnly=%repoDir%

rem rem replace '\' with '/' for unix path conversion
rem set dirOnly=%dirOnly:\=/%

rem rem Omit ':'
rem set dirOnly=%dirOnly::=%

rem rem Add '/' in the begining
rem set dirOnly=^/%dirOnly%^/
rem rem rem Omit the white space present
rem rem call :Trim unix_path %unix_path%

rem rem set dirOnly=%unix_path:"=%
rem call :Trim dirOnly %dirOnly%

rem rem echo %dirOnly%
rem echo %dirOnly% >unix_path.dat &rem Write into the repo path to a text file

rem timeout 3

rem %sh_exe_path% --login -i -c %unix_path%
rem pause


rem :Trim
rem 	SetLocal EnableDelayedExpansion
rem 	set Params=%*
rem 	for /f "tokens=1*" %%a in ("!Params!") do EndLocal & set %1=%%b
rem 	exit /b