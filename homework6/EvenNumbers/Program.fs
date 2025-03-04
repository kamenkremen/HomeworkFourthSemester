module EvenNumbers

/// Function that counts even numbers in the given list and returns their amount, based on List.filter
let CountEvenNumbersFilter list = 
    list
    |> List.filter (fun x -> x % 2 = 0)
    |> List.length

/// Function that counts even numbers in the given list and returns their amount, based on List.map
let CountEvenNumbersMap list = 
    list
    |> List.map (fun x -> if x % 2 = 0 then 1 else 0)
    |> List.sum

/// Function that counts even numbers in the given list and returns their amount, based on List.fold
let CountEvenNumbersFold list = 
    list
    |> List.fold (fun accumulator x -> if x % 2 = 0 then accumulator + 1 else accumulator) 0
