module lazyTests
open Lazy
open NUnit.Framework
open FsUnit
open System.Threading.Tasks

[<Test>]
let ``SingleThreadedLazy computes once``() =
    let mutable counter = 0
    let lazyInt = SingleThreadedLazy(fun () -> counter <- counter + 1; 42) :> ILazy<int>
    
    Assert.AreEqual(42, lazyInt.Get())
    Assert.AreEqual(42, lazyInt.Get())
    Assert.AreEqual(1, counter)

[<Test>]
let ``MultiThreadedLazy computes once under contention``() =
    let mutable counter = 0
    let lazyInt = MultiThreadedLazy(fun () -> counter <- counter + 1; 42) :> ILazy<int>
    
    let results = Array.zeroCreate 100
    Parallel.For(0, 100, fun i ->
        results[i] <- lazyInt.Get()
    ) |> ignore
    
    Assert.AreEqual(100, results |> Array.filter ((=) 42) |> Array.length)
    Assert.AreEqual(1, counter)

[<Test>]
let ``LockFreeLazy may compute multiple times but returns consistent value``() =
    let mutable counter = 0
    let lazyInt = (LockFreeLazy(fun () -> 
        System.Threading.Interlocked.Increment(&counter) |> ignore
        42) :> ILazy<int>)
    
    let results = Array.zeroCreate 100
    Parallel.For(0, 100, fun i ->
        results[i] <- lazyInt.Get()
    ) |> ignore
    
    Assert.AreEqual(100, results |> Array.filter ((=) 42) |> Array.length)
    counter |> should greaterThanOrEqualTo 1
    counter |> should lessThanOrEqualTo 100
