/// A function that reverses a list.
let reverseList list =
    let rec Helper accumulator = function
        | [] -> accumulator
        | head :: tail -> Helper (head :: accumulator) tail
    Helper [] list
