module Workflows
open System

type StringCalculationsBuilder() =
    member _.Bind(str: string, f) =
        match Int32.TryParse(str) with
        | (true, num) -> f num
        | _ -> None
    member _.Return(x: int) = Some x

type RoundingBuilder(accuracy: int) = 
    member _.Bind(number: float, f ) = 
        f (Math.Round(number, accuracy))
    member _.Return(number: float) =
        Math.Round(number, accuracy)
