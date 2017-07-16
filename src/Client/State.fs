module State

open Types
open Api
open Fable.Import.Browser
open Elmish


// State
let init model =
  let model = {
    ModuleId =  (document.getElementById containerId).dataset.["moduleid"]
    Ergebnis = None
  }
  model , helloworldCmd model.ModuleId

let update  msg model=
  match msg with
  | HelloWorldFetched s -> {model with Ergebnis = Some s}, Cmd.none
  | HelloWorldFailed  e -> {model with Ergebnis = Some e.Message}, Cmd.none