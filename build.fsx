// include Fake lib
#r "src/packages/FAKE/tools/FakeLib.dll"
#r "src/packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open System
open Fake
open Fake.AssemblyInfoFile
open FSharp.Data
open FSharp.Data.JsonExtensions


// Project meta
let authors = ["Russell Gray <russgray@gmail.com>"]
let projectName = "BetfairAPING"
let projectDescription = "Client library for Betfair API-NG"
let projectSummary = projectDescription // TODO: write a summary
let copyright = "Copyright 2015 Russell Gray <russgray@gmail.com>"


// Properties
let gitVersion = environVarOrDefault "GitVersion" "GitVersion"
let buildMode = getBuildParamOrDefault "buildMode" "Debug"


// Directories
let buildDir = "./build"
let packagingRoot = "./packaging"
let sourcePackagesDir = "./src/packages"
let packagingDir = packagingRoot @@ "betfair-aping"


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
    CleanDir packagingRoot
)

Target "RestoreSrcPackages" (fun _ ->
    let restore =
        RestorePackage (fun p ->
            { p with
                OutputPath = sourcePackagesDir
            })

    !! "./src/**/packages.config"
    |> Seq.iter (restore)
)

Target "GenerateAssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./src/BetfairAPING/Properties/AssemblyInfo.cs"
        [Attribute.Title projectName
         Attribute.Description projectDescription
         Attribute.Guid "2a120bd3-a369-47ed-a67d-26823d332807"
         Attribute.Product projectName
         Attribute.Copyright copyright
         Attribute.Version (versionJson?ClassicVersion.AsString())
         Attribute.FileVersion (versionJson?ClassicVersion.AsString())
         Attribute.InformationalVersion (versionJson?InformationalVersion.AsString())]

    CreateCSharpAssemblyInfo "./src/BetfairAPING.Console/Properties/AssemblyInfo.cs"
        [Attribute.Title "BetfairAPING.Console"
         Attribute.Description "Command-line interface for Betfair API-NG"
         Attribute.Guid "2a120bd3-a369-47ed-a67d-26823d332807"
         Attribute.Product "BetfairAPING.Console"
         Attribute.Copyright copyright
         Attribute.Version (versionJson?ClassicVersion.AsString())
         Attribute.FileVersion (versionJson?ClassicVersion.AsString())
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
            Dependencies = [ 
                ("RestSharp", GetPackageVersion "./src/packages" "RestSharp")
                ("Newtonsoft.Json", GetPackageVersion "./src/packages" "Newtonsoft.Json")
            ]
            Files = [
                (@"LICENSE", None, None)
                (@"README.md", None, None)
                (@"lib\net45\*.dll", Some @"lib\net45", None)
                (@"lib\net45\*.pdb", Some @"lib\net45", None)
                (@"..\..\src\**\*.cs", Some "src", Some "..\..\src\**\TemporaryGeneratedFile*.cs")
            ]
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
  ==> "RestoreSrcPackages"
  ==> "GenerateAssemblyInfo"
  ==> "BuildApp"
  ==> "CreatePackage"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
