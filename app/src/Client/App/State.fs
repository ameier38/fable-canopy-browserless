module App.State

open Elmish
open Domain
open Shared
open Server

let getCount () =
    async {
        return! counterApi.getCount()
    }

let incrementBy (amount:int) =
    async {
        do! counterApi.incrementBy amount
        return! getCount()
    }

let decrementBy (amount:int) =
    async {
        do! counterApi.decrementBy amount
        return! getCount()
    }

let init () =
    let initialState =
        { IsLoading = true
          Error = None
          Count = 0 }

    let cmd =
        Cmd.OfAsync.either getCount () CountReceived ErrorReceived

    initialState, cmd

let update (msg: Msg) (state: State) =
    match msg with
    | IncrementBy amount ->
        { state with IsLoading = true }, Cmd.OfAsync.either incrementBy amount CountReceived ErrorReceived
    | DecrementBy amount ->
        { state with IsLoading = true }, Cmd.OfAsync.either decrementBy amount CountReceived ErrorReceived
    | CountReceived count ->
        { state with
              IsLoading = false
              Error = None
              Count = count },
        Cmd.none
    | ErrorReceived e ->
        { state with
              IsLoading = false
              Error = Some(sprintf "Error: %A" e) },
        Cmd.none
    | ClearError -> { state with Error = None }, Cmd.none
