#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.JavaScript
open BlackFox.Fake

let clean = BuildTask.create "Clean" [] {
    !! "src/**/bin"
    ++ "src/**/obj"
    |> Shell.cleanDirs 
}

BuildTask.create "Restore" [clean.IfNeeded] {
    !! "src/**/*.*proj"
    |> Seq.iter (DotNet.restore id)
}

let testUnits = BuildTask.create "TestUnits" [] {
    let result = DotNet.exec id "run" "--project src/UnitTests/UnitTests.fsproj"
    if not result.OK then failwithf "Error! %A" result.Errors
}

BuildTask.create "StartServer" [] {
    let result = DotNet.exec id "run" "--project src/Server/Server.fsproj"
    if not result.OK then failwithf "Error! %A" result.Errors
}

BuildTask.create "StartClient" [] {
    Npm.run "start" id
}

let publish (proj:string) =
    Trace.tracef "Publishing %s..." proj
    // ref: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
    let runtime =
        if Environment.isLinux then "linux-x64"
        elif Environment.isWindows then "win-x64"
        elif Environment.isMacOS then "osx-x64"
        else failwithf "environment not supported"
    DotNet.publish (fun args ->
        { args with
            OutputPath = Some (sprintf "src/%s/out" proj)
            Runtime = Some runtime })
        (sprintf "src/%s/%s.fsproj" proj proj)

BuildTask.create "PublishServer" [testUnits] {
    publish "Server"
}

BuildTask.create "BuildClient" [] {
    Npm.run "build" id
}

BuildTask.create "TestIntegrations" [] {
    let result = DotNet.exec id "run" "--project src/IntegrationTests/IntegrationTests.fsproj"
    if not result.OK then failwithf "Error! %A" result.Errors
}

BuildTask.create "PublishIntegrationTests" [] {
    publish "IntegrationTests"
}

let _default = BuildTask.createEmpty "Default" []

BuildTask.runOrDefault _default
