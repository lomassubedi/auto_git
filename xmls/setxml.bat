@echo off
rem setlocal EnableDelayedExpansion

rem set anotherVariable=/c/program files/my\ test_files/

rem (for /F "delims=" %%a in (info.xml) do (
rem    set "line=%%a"
rem    set "newLine=!line:repoDirUnix>=!"
rem    if "!newLine!" neq "!line!" (
rem       set "newLine=<repoDirUnix>%anotherVariable%</repoDirUnix>"
rem    )
rem    echo !newLine!
rem )) > info.xml

setlocal enableDelayedExpansion
set "newValue=ThisIsmyNewValue"
type "info.xml"|repl "(<repoDirUnix>).*(</repoDirUnix>)" "$1!newValue!$2" >info.xml.new
move /y "info.xml.new" "info.xml"