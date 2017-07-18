module State

open Types
open Api
open Fable.Import.Browser
open Elmish

// State
let init model =
  let model = {
    CurrentUser   = None
    ErrorMessages = []
  }
  model, Api().FetchCurrentUser

let update  msg model=

  match msg with
  | UserFetched p  ->  {model with CurrentUser = p}, Cmd.none
  | FetchFailed  e ->  {model with ErrorMessages = e.Message :: model.ErrorMessages}, Cmd.none