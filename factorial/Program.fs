let rec factorial n =
    match n with
    | n when n < 0 -> None
    | 0 | 1 -> Some 1
    | _ -> 
    match factorial (n - 1) with
        | None -> None
        | Some number -> Some (number * n)

printf "Enter number:"

let input = System.Console.ReadLine()
match System.Int32.TryParse(input) with 
| true, number -> 
    match factorial number with 
    | Some result -> printfn "%d! = %d" number result
    | None -> printfn "Number can`t be negative"
| false, _ -> printfn "Incorrect input"