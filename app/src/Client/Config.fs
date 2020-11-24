module Config

open Shared

type ServerConfig =
    { Url: string }
    static member Load() =
        let scheme = Env.getEnv "SERVER_SCHEME" "http"
        let host = Env.getEnv "SERVER_HOST" "localhost"
        let port = Env.getEnv "SERVER_PORT" "5000"
        let portComponent = Url.getPortComponent port
        let url = sprintf "%s://%s%s" scheme host portComponent
        printfn "url: %s" url
        { Url = url }
