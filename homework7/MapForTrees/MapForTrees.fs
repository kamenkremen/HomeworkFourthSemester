type Tree<'a> = 
    | Leaf of 'a
    | Node of 'a * Tree<'a> * Tree<'a>

/// Map for trees
let rec mapForTrees tree func = 
    match tree with
    | Leaf x -> Leaf (func x)
    | Node (value, left, right) -> Node (func value, mapForTrees left func, mapForTrees right func)
