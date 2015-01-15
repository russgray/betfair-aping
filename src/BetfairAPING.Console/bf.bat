@echo off
set dir=%~dp0%
cmd /c "%dir%BetfairAPING.Console.exe %*"
exit /b %errorlevel%

