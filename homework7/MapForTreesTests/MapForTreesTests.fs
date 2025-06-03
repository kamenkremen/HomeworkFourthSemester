module MapForTreesTests

open NUnit.Framework
open MapForTrees
open FsCheck.NUnit
open FsUnit

let testCases () =
    [ Node(2, Leaf(1), Leaf(3)), (fun(x: int) -> x + 1), Node(3, Leaf(2), Leaf(4)) 
      Leaf(2), (fun(x: int) -> x * 2), Leaf(4)
      Node(6, Node(2, Leaf(1), Node(3, Leaf(4), Leaf(5))), Node(8, Leaf(7), Leaf(9))), (fun(x: int) -> x * 5), Node(30, Node(10, Leaf(5), Node(15, Leaf(20), Leaf(25))), Node(40, Leaf(35), Leaf(45)))
    ]
    |> List.map (fun (tree, mapFunction, expected) -> TestCaseData(tree, mapFunction, expected))

[<TestCaseSource(nameof(testCases))>]
let ``mapForTrees should work correctly`` (tree: Tree<int>) (mapFunction: int -> int) (expected: Tree<int>) =
    mapForTrees tree mapFunction |> should equal expected
