module Test
// First task
let signSequence() = 
    let ones = Seq.initInfinite (fun index -> 1 - 2 * (index % 2))
    Seq.map2 (*) (Seq.initInfinite ((+) 1)) ones

// Second task
type Tree<'a> = 
    | Leaf of 'a
    | Node of 'a * Tree<'a> * Tree<'a>

let rec filterForTrees tree predicate =
    match tree with
    | Leaf x -> 
        match (predicate x) with
        | true -> [x]
        | false -> []
    | Node (value, left, right) -> 
        let leftValues = filterForTrees left predicate
        let current = 
            match predicate value with
            | true -> [value]
            | false -> []
        let rightValues = filterForTrees right predicate
        leftValues @ current @ rightValues

// Third task
/// Priority queue
type PriorityQueue<'a>() =
    /// Tuple with elements and priorities
    let mutable elements: ('a * int) list = []

    /// Enqueue priority queue
    member this.Enqueue(item: 'a, priority: int) =
        let rec insert list =
            match list with
            | [] -> [ (item, priority) ]
            | (headValue, headPriority) :: tail ->
                if priority > headPriority then
                    (item, priority) :: list
                else
                    (headValue, headPriority) :: insert tail
        elements <- insert elements

    /// Dequeue priority queue. Throws System.InvalidOperationException if empty 
    member this.Dequeue(): 'a =
        match elements with
        | [] -> raise (System.InvalidOperationException("Queue is empty"))
        | (head, _) :: tail ->
            elements <- tail
            head

    /// Returns whether priority queue is empty or not
    member this.IsEmpty = List.isEmpty elements

