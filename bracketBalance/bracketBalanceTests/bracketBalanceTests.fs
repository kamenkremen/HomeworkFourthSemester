module bracketBalanceTests

open NUnit.Framework
open bracketBalance
open FsCheck.NUnit
open FsUnit

let testCases () =
    [
        "", true
        "()", true
        "{}[]()", true
        "{}}", false
        "(()", false
        "{)", false
        "((((((()))))))", true
        "s(u)s", true
    ]
    |> List.map (fun (string, expected) -> TestCaseData(string, expected))

[<TestCaseSource(nameof(testCases))>]
let ``isBalanced should work correctly`` string expected =
    isBalanced string |> should equal expected