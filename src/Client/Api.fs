module Api
open Fable.PowerPack
open Fable.PowerPack.Fetch.Fetch_types
open Elmish

open DNN
open Types

//Api
let helloworld moduleId = 
      promise {    
        if moduleId ="DEBUG"
        then
          return "DEBUG"
        else
          let sf = ServiceFramework moduleId    
          let url = sf.getServiceRoot("fsharp/spike") + "service/" + "helloworld"
          let props = 
              [ RequestProperties.Method HttpMethod.GET
                Fetch.requestHeaders (moduleHeaders sf)
              ]
          return! Fable.PowerPack.Fetch.fetchAs<string> url props
    }

let helloworldCmd moduleId = Cmd.ofPromise helloworld moduleId HelloWorldFetched HelloWorldFailed 