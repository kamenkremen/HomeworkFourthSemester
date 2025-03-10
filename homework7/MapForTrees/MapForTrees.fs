type Tree<'a> = 
    | Leaf of 'a
    | Node of Tree<'a> * Tree<'a>

/// Map for trees
let rec mapForTrees tree func = 
    match tree with
    | Leaf x -> Leaf (func x)
    | Node (left, right) -> Node (mapForTrees left func, mapForTrees right func)
