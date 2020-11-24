module Config

open Shared

type ClientConfig =
    { Url: string }
    static member Load() =
        let scheme = Env.getEnv "CLIENT_SCHEME" "http"
        let host = Env.getEnv "CLIENT_HOST" "localhost"
        let port = Env.getEnv "CLIENT_PORT" "3000"
        let portComponent = Url.getPortComponent port
        { Url = sprintf "%s://%s%s" scheme host portComponent }

type ServerConfig =
    { Host: string
      Port: int }
    static member Load() =
        { Host = Env.getEnv "HOST" "0.0.0.0"
          Port = Env.getEnv "PORT" "5000" |> int }
