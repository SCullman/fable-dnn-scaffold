// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open System

// Directories
let buildDir  = "./build/"
let deployDir = "./deploy/"
let publicDir ="./public/" 

let dnnDir = "./../../../../" //Assuming src is located ~/desktopmodules/vendor/module/

let mutable dotnetExePath = "dotnet"
let clientPath = "./Client" |> FullName

// Filesets
let appReferences  =
    !! "/**/*.csproj"
    ++ "/**/*.fsproj"
    -- "**/Client/**/*.fsproj"
    -- "**/packages/**/*.fsproj"

let runDotnet workingDir args =
    let result =
        ExecProcess (fun info ->
            info.FileName <- dotnetExePath
            info.WorkingDirectory <- workingDir
            info.Arguments <- args) TimeSpan.MaxValue
    if result <> 0 then failwithf "dotnet %s failed" args

// version info
let version = "0.1"  // or retrieve from CI server

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir]
)

Target "Build" (fun _ ->
    // compile all projects below src/app/
    MSBuildDebug buildDir "Build" appReferences
    |> Log "AppBuild-Output: "
)

Target "Build-Client" (fun _ ->
    //runDotnet clientPath "restore"
    runDotnet clientPath "fable yarn-build"
)

Target "DNN" (fun _ ->
  !! (buildDir + "fsharp.spike.*")
  |> CopyFiles ( dnnDir + "/bin")
(*   (publicDir + "/**/*.*")

  |> Seq.iter (fun f ->CopyFileWithSubfolder (dnnDir + "/desktopmodules/fsharp/spike")) *)
)

Target "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
    -- "*.zip"
    |> Zip buildDir (deployDir + "ApplicationName." + version + ".zip")
)

// Build order
"Clean"
 //
  ==> "Build-Client"
  ==> "Build"
  ==> "DNN"
  ==> "Deploy"

// start build
RunTargetOrDefault "DNN"
