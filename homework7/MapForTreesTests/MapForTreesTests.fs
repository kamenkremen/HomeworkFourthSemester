module MapForTreesTests

open NUnit.Framework
open MapForTrees
open FsCheck.NUnit
open FsUnit

let testCases () =
    [ Node(Leaf(1), Leaf(2)), (fun(x: int) -> x + 1), Node(Leaf(2), Leaf(3)) 
      Leaf(2), (fun(x: int) -> x * 2), Leaf(4)
      Node(Node(Leaf(1), Node(Leaf(2), Leaf(3))), Node(Leaf(4), Leaf(5))), (fun(x: int) -> x * 5), Node(Node(Leaf(5), Node(Leaf(10), Leaf(15))), Node(Leaf(20), Leaf(25)))
    ]
    |> List.map (fun (tree, mapFunction, expected) -> TestCaseData(tree, mapFunction, expected))

[<TestCaseSource(nameof(testCases))>]
let ``mapForTrees should work correctly`` (tree: Tree<int>) (mapFunction: int -> int) (expected: Tree<int>) =
    mapForTrees tree mapFunction |> should equal expected
