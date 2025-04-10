module testTests
open Test
open NUnit.Framework
open FsUnit

module SignSequenceTests =
    [<Test>]
    let ``Test infinite sequence should produce first 10 numbers correctly `` () =
        let expected = [1; -2; 3; -4; 5; -6; 7; -8; 9; -10]
        signSequence() |> Seq.take 10 |> Seq.toList |> should equal expected

module FilterTreeTests =
    let sampleTree = 
        Node(4, Node(2, Leaf 1, Leaf 3), Node(6, Leaf 5, Leaf 7))
    

    [<Test>]
    let ``Filter for tree should work correctly with parity predicate`` () =
        filterForTrees sampleTree (fun x -> x % 2 = 0)
        |> should equal [2; 4; 6]


module PriorityQueueTests =
    [<Test>]
    let ``Test dequeue returns values in order of their priority`` () =
        let pq = PriorityQueue<int>()
        pq.Enqueue(10, 3)
        pq.Enqueue(20, 1)
        pq.Enqueue(30, 2)
        pq.Dequeue() |> should equal 10
        pq.Dequeue() |> should equal 30
        pq.Dequeue() |> should equal 20

    [<Test>]
    let ``Test dequeue throws exception from empty queue`` () =
        let pq = PriorityQueue<string>()
        try
            pq.Dequeue() |> ignore
        with
        | :? System.InvalidOperationException -> Assert.Pass()
        | ex -> Assert.Fail()

    [<Test>]
    let ``Test enqueue keeps order with equal priorities`` () =
        let pq = PriorityQueue<string>()
        pq.Enqueue("A", 2)
        pq.Enqueue("B", 2)
        pq.Enqueue("C", 1)
        pq.Dequeue() |> should equal "A"
        pq.Dequeue() |> should equal "B"
        pq.Dequeue() |> should equal "C"
