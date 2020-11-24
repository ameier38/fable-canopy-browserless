namespace Shared

type ICounterApi =
    { incrementBy: int -> Async<unit>
      decrementBy: int -> Async<unit>
      getCount: unit -> Async<int> }
