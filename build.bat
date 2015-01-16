@echo off
cls

if not exist buildpackages\FAKE\Tools\FAKE.exe (
    ".nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "buildpackages" "-ExcludeVersion"
)

if not exist buildpackages\FSharp.Data\lib\net40\FSharp.Data.dll (
    ".nuget\NuGet.exe" "Install" "FSharp.Data" "-OutputDirectory" "buildpackages" "-ExcludeVersion"
)

if not exist buildpackages\GitVersion.CommandLine\Tools\GitVersion.exe (
    ".nuget\NuGet.exe" "Install" "GitVersion.CommandLine" "-OutputDirectory" "buildpackages" "-ExcludeVersion"
)

if not exist buildpackages\Machine.Specifications.Runner.Console\tools\mspec-clr4.exe (
    ".nuget\NuGet.exe" "Install" "Machine.Specifications.Runner.Console" "-OutputDirectory" "buildpackages" "-ExcludeVersion"
)

"buildpackages\FAKE\tools\Fake.exe" build.fsx
