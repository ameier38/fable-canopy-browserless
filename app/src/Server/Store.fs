module Store

type IStore =
    abstract member IncrementBy: int -> Async<unit>
    abstract member DecrementBy: int -> Async<unit>
    abstract member GetCount: unit -> Async<int>

type MemoryStore() =
    let mutable count = 0

    interface IStore with
        member _.IncrementBy(increment:int) =
            async { 
                do! Async.Sleep 1000
                count <- count + increment
            }
        member _.DecrementBy(decrement:int) =
            async {
                do! Async.Sleep 1000
                count <- count - decrement
            }
        member _.GetCount() =
            async {
                do! Async.Sleep 1000
                return count
            }
