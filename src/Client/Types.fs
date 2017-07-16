module Types

//Types
type Model ={
  ModuleId : string
  Ergebnis : string option
}
type Msgs = 
  | HelloWorldFetched of string
  | HelloWorldFailed of exn

let containerId = "elmish-container"