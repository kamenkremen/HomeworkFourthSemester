module testTests
open Test
open NUnit.Framework
open FsUnit

[<SetUp>]
let Setup () =
    ()

[<Test>]
let ``Test infinite sequence should produce first 10 numbers correctly `` () =
    let expected = [1; -2; 3; -4; 5; -6; 7; -8; 9; -10]
    numbersSequence() |> Seq.take 10 |> Seq.toList |> should equal expected
