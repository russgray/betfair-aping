@echo off
cls

if not exist src\packages\FAKE\Tools\FAKE.exe (
    ".nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "src\packages" "-ExcludeVersion"
)

if not exist src\packages\FSharp.Data\lib\net40\FSharp.Data.dll (
    ".nuget\NuGet.exe" "Install" "FSharp.Data" "-OutputDirectory" "src\packages" "-ExcludeVersion"
)

"src\packages\FAKE\tools\Fake.exe" build.fsx
