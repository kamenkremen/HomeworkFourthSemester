module bracketBalance

let isBalanced (str: string) =
    let openToClosed = 
        Map [ ('(', ')'); ('[', ']'); ('{', '}') ]
    
    let closed =
        Set [ ')'; ']'; '}' ]

    let rec check string stack =
        match string with
        | [] -> List.isEmpty stack
        | head::tail ->
            if Map.containsKey head openToClosed then
                check tail (openToClosed.[head] :: stack)
            elif Set.contains head closed then
                match stack with
                | expected::remaining when head = expected -> check tail remaining
                | _ -> false
            else
                check tail stack
    
    check (Seq.toList str) []
