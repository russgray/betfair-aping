@echo off
cls

if not exist packages\FAKE\Tools\FAKE.exe (
    ".nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "packages" "-ExcludeVersion"
)

if not exist packages\FSharp.Data\lib\net40\FSharp.Data.dll (
    ".nuget\NuGet.exe" "Install" "FSharp.Data" "-OutputDirectory" "packages" "-ExcludeVersion"
)

"packages\FAKE\tools\Fake.exe" build.fsx
