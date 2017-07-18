module Types


type Person = SharedTypes.Person

//Types
type Model ={
  CurrentUser: Person option
  ErrorMessages : string list
}

type Msgs = 
  | UserFetched of Person option
  | FetchFailed of exn

let containerId = "elmish-container"