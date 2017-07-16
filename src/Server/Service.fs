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
           
type ServiceController () =
    inherit DnnApiController ()

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
