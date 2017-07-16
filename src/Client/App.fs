module App.View

open Elmish
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.PowerPack
open Fable.PowerPack.Fetch.Fetch_types

open Types
open State

//view
let root model dispatch =
 div [ ] [str<|sprintf "module-id %A" model]

open Elmish.React
open Elmish.Debug

// App
Program.mkProgram init update root
|> Program.withReact containerId
|> Program.withConsoleTrace
(* //-:cnd
#if DEBUG
|> Program.withDebugger
#endif
//+:cnd *)
|> Program.run