
module MapForTreesTests

open NUnit.Framework
open ParseTree
open FsCheck.NUnit
open FsUnit

let testCases () =
    [ (Add(Number(5), Number(3)), 8)
      (Subtract(Number(10), Number(4)), 6)
      (Multiply(Number(7), Number(2)), 14)
      (Divide(Number(15), Number(3)), 5)
      (Add(Multiply(Number(2), Number(5)), Subtract(Number(8), Number(3))), 15)
      (Divide(Add(Number(10), Number(20)), Subtract(Number(8), Number(2))), 5)
      (Multiply(Add(Subtract(Number(20), Number(5)), Divide(Number(15), Number(3))), Number(2)), 40)
      (Divide(Multiply(Add(Number(3), Number(7)), Subtract(Number(5), Number(2))), Number(4)), 7) ]  
    |> List.map (fun (tree, expected) -> TestCaseData(tree, expected))

[<TestCaseSource(nameof(testCases))>]
let ``evaluate should work correctly`` (tree: ParseTree) expected =
    evaluate tree |> should equal expected

[<Test>]
let ``evaluate should throw exception when dividing by zero`` () =
    (fun () -> evaluate (Divide(Number(10), Number(0))) |> ignore)
    |> should throw typeof<System.Exception>
