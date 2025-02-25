/// A function that finds a number in the list.
let find number list = 
    let rec findHelper index = function
    | [] -> None
    | head :: tail -> 
        if head = number then
            Some index
        else
            findHelper (index + 1) tail
    findHelper 0 list
