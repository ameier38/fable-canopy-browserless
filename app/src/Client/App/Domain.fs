module App.Domain

open System

type State =
    { IsLoading: bool
      Error: string option
      Count: int }

type Msg =
    | IncrementBy of int
    | DecrementBy of int
    | CountReceived of int
    | ErrorReceived of Exception
    | ClearError
