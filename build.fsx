// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

RestorePackages()

// Properties
let buildDir = "./build/"
let versionInformational = StringHelper.ReadLine "./VERSION"

// This is normally used to trim trailing .0, but also works for prerelease tag such as -dev
let version = StringHelper.NormalizeVersion versionInformational

// Targets
Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "GenerateAssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./src/BetfairAPING/Properties/AssemblyInfo.cs"
        [Attribute.Title "BetfairAPING"
         Attribute.Description "Client library for Betfair API-NG"
         Attribute.Guid "2a120bd3-a369-47ed-a67d-26823d332807"
         Attribute.Product "BetfairAPING"
         Attribute.Version version
         Attribute.FileVersion version
         Attribute.InformationalVersion versionInformational]

    CreateCSharpAssemblyInfo "./src/BetfairAPING.Console/Properties/AssemblyInfo.cs"
        [Attribute.Title "BetfairAPING.Console"
         Attribute.Description "Command-line interface for Betfair API-NG"
         Attribute.Guid "2a120bd3-a369-47ed-a67d-26823d332807"
         Attribute.Product "BetfairAPING.Console"
         Attribute.Version version
         Attribute.FileVersion version
         Attribute.InformationalVersion versionInformational]
)

Target "BuildApp" (fun _ ->
    !! "src/**/*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

// Dependencies
"Clean"
  ==> "GenerateAssemblyInfo"
  ==> "BuildApp"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
