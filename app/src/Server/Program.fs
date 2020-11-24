open Fable.Remoting.Server
open Fable.Remoting.Suave
open Shared
open Store
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.Writers

let counterApi
    (store:IStore)
    : ICounterApi =
    { incrementBy = store.IncrementBy
      decrementBy = store.DecrementBy
      getCount = store.GetCount }

let setCORSHeaders (clientUrl:string) =
    setHeader  "Access-Control-Allow-Origin" clientUrl
    >=> setHeader  "Access-Control-Allow-Credentials" "true"
    >=> setHeader  "Access-Control-Allow-Methods" "GET, POST, OPTIONS"
    >=> setHeader "Access-Control-Allow-Headers" "content-type,x-remoting-proxy"

[<EntryPoint>]
let main _ =
    let serverConfig = Config.ServerConfig.Load()
    let clientConfig = Config.ClientConfig.Load()
    let store = MemoryStore()
    let bindings = [ HttpBinding.createSimple HTTP serverConfig.Host serverConfig.Port ]
    let api : WebPart =
        Remoting.createApi()
        |> Remoting.fromValue (counterApi store)
        |> Remoting.buildWebPart
    let app = choose [
        path "/healthz" >=> GET >=> OK "Healthy!"
        OPTIONS >=> setCORSHeaders clientConfig.Url  >=> OK "CORS allowed"
        setCORSHeaders clientConfig.Url >=> api
    ]
    startWebServer { defaultConfig with bindings = bindings } app
    0 // return an integer exit code
