module stringCalculationsTests
open Workflows
open NUnit.Framework
open FsUnit

module WorkflowTests = 
    let calculate = StringCalculationsBuilder()
    let rounding = RoundingBuilder

    [<Test>]
    let ``Numbers should calculate correctly`` () =
        calculate {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        } |> should equal (Some 3)

    [<Test>]
    let ``Invalid symbols should not calculate`` () = 
        calculate {
            let! x = "1"
            let! y = "Ъ"
            let z = x + y
            return z
        } |> should equal None

    [<Test>]
    let ``Workflow with zero division should throw exception`` () = 
        try 
            calculate {
                let! x = "1"
                let! y = "0"
                let z = x / y
                return z
            } |> ignore
        with
        | :? System.DivideByZeroException -> Assert.Pass()
        | ex -> Assert.Fail()
    
    [<Test>]
    let ``Rounding workflow should work correctly`` () = 
        rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        |> should equal 0.048
