// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open System
open Fake
open Fake.AssemblyInfoFile
open FSharp.Data
open FSharp.Data.JsonExtensions

let authors = ["Russell Gray <russgray@gmail.com>"]

// Project name and description
let projectName = "BetfairAPING"
let projectDescription = "Client library for Betfair API-NG"
let projectSummary = projectDescription // TODO: write a summary


RestorePackages()

// Properties
let gitVersion = environVarOrDefault "GitVersion" "GitVersion"
let buildDir = "./build/"
let packagingRoot = "./packaging/"
let packagingDir = packagingRoot @@ "betfair-aping"
let buildMode = getBuildParamOrDefault "buildMode" "Debug"

let getVersionInfo =
    // Execute GitVersion
    let (result, messages) =
        ExecProcessRedirected
            (fun info -> info.FileName <- gitVersion)
            TimeSpan.MaxValue

    // Check response
    if not result then failwithf "Error executing GitVersion: %A" messages
    // Concat the messages together (one per line of output)
    String.Concat (query { for m in messages do select m.Message })

let versionJson = getVersionInfo |> JsonValue.Parse

// Targets
Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "GenerateAssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./src/BetfairAPING/Properties/AssemblyInfo.cs"
        [Attribute.Title projectName
         Attribute.Description projectDescription
         Attribute.Guid "2a120bd3-a369-47ed-a67d-26823d332807"
         Attribute.Product projectName
         Attribute.Version (versionJson?ClassicVersion.AsString())
         Attribute.FileVersion (versionJson?AssemblySemVer.AsString())
         Attribute.InformationalVersion (versionJson?InformationalVersion.AsString())]

    CreateCSharpAssemblyInfo "./src/BetfairAPING.Console/Properties/AssemblyInfo.cs"
        [Attribute.Title "BetfairAPING.Console"
         Attribute.Description "Command-line interface for Betfair API-NG"
         Attribute.Guid "2a120bd3-a369-47ed-a67d-26823d332807"
         Attribute.Product "BetfairAPING.Console"
         Attribute.Version (versionJson?ClassicVersion.AsString())
         Attribute.FileVersion (versionJson?AssemblySemVer.AsString())
         Attribute.InformationalVersion (versionJson?InformationalVersion.AsString())]
)

Target "BuildApp" (fun _ ->
    MSBuild buildDir "Build" ["Configuration", buildMode] ["./src/BetfairAPING.sln"]
      |> Log "AppBuild-Output: "
)

Target "CreatePackage" (fun _ ->
    let net45Dir = packagingDir @@ "lib/net45/"
    CleanDirs [net45Dir]

    CopyFile net45Dir (buildDir @@ "BetfairAPING.dll")
    CopyFile net45Dir (buildDir @@ "BetfairAPING.pdb")
    CopyFiles packagingDir ["LICENSE"; "README.md"]

    NuGet (fun p ->
        {p with
            Authors = authors
            Project = projectName
            Description = projectDescription
            OutputPath = packagingRoot
            Summary = projectSummary
            Tags = "betfair, tagwager"
            Dependencies = [ "RestSharp", GetPackageVersion "./src/packages" "RestSharp"]
            WorkingDir = packagingDir
            Version = (versionJson?NuGetVersion.AsString())
            SymbolPackage = NugetSymbolPackage.Nuspec
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" })
            "./src/BetfairAPING/BetfairAPING.nuspectemplate"
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

// Dependencies
"Clean"
  ==> "GenerateAssemblyInfo"
  ==> "BuildApp"
  ==> "CreatePackage"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
