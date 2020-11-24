open Expecto
open Store

[<Tests>]
let testStore =
    testAsync "test store" {
        let store = MemoryStore() :> IStore
        let! count = store.GetCount()
        Expect.equal count 0 "count should be 0"
        do! store.IncrementBy 10
        let! count = store.GetCount()
        Expect.equal count 10 "count should be 10"
        do! store.DecrementBy 5
        let! count = store.GetCount()
        Expect.equal count 5 "count should be 5"
    }

[<EntryPoint>]
let main argv = runTestsInAssembly defaultConfig argv
