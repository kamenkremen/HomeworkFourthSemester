module EvenNumbersTests

open NUnit.Framework
open EvenNumbers
open FsCheck.NUnit
open FsUnit

let testCases () =
    [ [ 1; 5; 2; 342; 123; 5 ], 2 
      [ 2; 4; 6; 8 ], 4
      [ ], 0
      [ 1 ], 0
      [ -2 ], 1
      [ -1 ], 0 ]
    |> List.map (fun (list, expected) -> TestCaseData(list, expected))

[<Property>]
let ``All functions return the same result`` (list: int list) =
    let result1 = CountEvenNumbersMap list
    let result2 = CountEvenNumbersFold list
    let result3 = CountEvenNumbersFilter list
    result1 = result2 && result2 = result3

[<TestCaseSource(nameof(testCases))>]
let ``CountEvenNumbersFilter should return correct amount of even numbers`` list expected =
    list |> CountEvenNumbersFilter |> should equal expected
