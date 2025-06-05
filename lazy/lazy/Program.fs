module Lazy

type ILazy<'a> =
    abstract member Get: unit -> 'a

type SingleThreadedLazy<'a>(supplier: unit -> 'a) =
    let mutable result = None
    interface ILazy<'a> with
        member this.Get() =
            match result with
            | Some value -> value
            | None ->
                let value = supplier()
                result <- Some value
                value

type MultiThreadedLazy<'a>(supplier: unit -> 'a) =
    let mutable result = None
    let lockObj = obj()
    interface ILazy<'a> with
        member this.Get() =
            match result with
            | Some value -> value
            | None ->
                lock lockObj (fun () ->
                    match result with
                    | Some value -> value
                    | None ->
                        let value = supplier()
                        result <- Some value
                        value)

type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable result = None
    interface ILazy<'a> with
        member this.Get() =
            match result with
            | Some value -> value
            | None ->
                let value = supplier()
                System.Threading.Interlocked.CompareExchange(&result, Some value, None) |> ignore
                result.Value
