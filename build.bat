@echo off
cls

if not exist packages\FAKE\Tools\FAKE.exe (
    ".nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "packages" "-ExcludeVersion"
)

"packages\FAKE\tools\Fake.exe" build.fsx
pause
