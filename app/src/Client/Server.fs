module Server

open Fable.Remoting.Client
open Shared

let serverConfig = Config.ServerConfig.Load()

printfn "serverConfig: %A" serverConfig

let counterApi =
    Remoting.createApi()
    |> Remoting.withBaseUrl serverConfig.Url
    |> Remoting.buildProxy<ICounterApi>
