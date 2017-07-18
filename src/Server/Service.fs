namespace Spike.Service

open DotNetNuke.Web.Api
open System.Net.Http
open System.Web.Http
open System.Net

type Defaults = {id:string} 

type RouteMapper ()=
    interface IServiceRouteMapper with
      member __.RegisterRoutes (rtm:IMapRoute) = 
        let namespaces = [|"Spike.Service"|]
        let defaults = {id=""}
        rtm.MapHttpRoute  ("fsharp/spike", "withid",  "{controller}/{action}/{id}" ,defaults, namespaces)|>ignore                                
        rtm.MapHttpRoute  ("fsharp/spike", "default", "{controller}/{action}", namespaces)|>ignore                                
           
open SharedTypes
open Newtonsoft.Json
open System.Net.Http.Formatting
open DotNetNuke.Security

type ServiceController  ()  =
    inherit DnnApiController ()
    
    let formatter = JsonMediaTypeFormatter ( 
                      SerializerSettings = JsonSerializerSettings (Converters = [|Fable.JsonConverter()|]))

    [<HttpGet>]
    [<ValidateAntiForgeryToken>] 
    [<DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)>] 
    member __.CurrentUser () =
        match  __.User.Identity.IsAuthenticated  with
                  | true  ->  let user =  { firstName = Some __.UserInfo.FirstName
                                            lastName  = Some __.UserInfo.LastName  }
                              __.Request.CreateResponse (HttpStatusCode.OK, Some user, formatter)
                  | false ->  __.Request.CreateResponse (HttpStatusCode.OK, None)
        

    [<HttpGet>]
    [<AllowAnonymous>]
    member __.HelloWorld () : HttpResponseMessage =
            __.Request.CreateResponse (HttpStatusCode.OK, "Hello World!")

    [<HttpGet>]
    member __.Hello (id) : HttpResponseMessage =
            __.Request.CreateResponse (HttpStatusCode.OK, sprintf "Hello %s!" id)

    [<HttpGet>]
    member __.Say (message, id) : HttpResponseMessage =
            __.Request.CreateResponse (HttpStatusCode.OK, sprintf "%s %s!" message id)
