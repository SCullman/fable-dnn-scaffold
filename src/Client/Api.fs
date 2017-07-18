module Api
open Fable.PowerPack
open Fable.PowerPack.Fetch.Fetch_types
open Elmish
open Fable.Import.Browser

open DNN
open Types

let moduleName = "fsharp/spike"
let serviceController = "service"

//Api

type Api () =
  let moduleId = (document.getElementById Types.containerId).dataset.["moduleid"]
  let sf = ServiceFramework moduleId    
  let serviceroot = sf.getServiceRoot(moduleName) + serviceController +  "/"
  let moduleHeaders = moduleHeaders sf

  let fetchCurrentUser  () = 
    promise {    
      if moduleId = "DEBUG"
      then
        return  Some { Person.firstName = Some "John"; Person.lastName = Some "Doe"}
      else  
          let url = serviceroot + "currentUser"
          let props = 
              [ RequestProperties.Method HttpMethod.GET
                Fetch.requestHeaders (moduleHeaders)
                Credentials RequestCredentials.Sameorigin
              ]
          return! Fable.PowerPack.Fetch.fetchAs<Person option> url props
    }

  member self.FetchCurrentUser = Cmd.ofPromise fetchCurrentUser () UserFetched FetchFailed 