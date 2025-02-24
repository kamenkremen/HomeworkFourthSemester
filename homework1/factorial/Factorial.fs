let rec factorial n =
    match n with
    | n when n < 0 -> None
    | 0 | 1 -> Some 1
    | _ -> 
    match factorial (n - 1) with
        | None -> None
        | Some number -> Some (number * n)
