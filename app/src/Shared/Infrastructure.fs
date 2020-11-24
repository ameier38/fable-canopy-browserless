namespace Shared

open Fable.Core
open System

[<RequireQualifiedAccess>]
module Url =
    let getPortComponent (port:string) =
        match port with
        | "" | "80" | "443" -> ""
        | port -> ":" + port

[<RequireQualifiedAccess>]
module Env =
    #if FABLE_COMPILER
    [<Emit("process.env[$0] ? process.env[$0] : $1")>]
    let getEnv (key:string) (defaultValue:string): string = jsNative
    #else
    let getEnv (key:string) (defaultValue:string) =
        match Environment.GetEnvironmentVariable(key) with
        | value when String.IsNullOrEmpty(value) -> defaultValue
        | value -> value
    #endif
