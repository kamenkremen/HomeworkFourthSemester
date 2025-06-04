module LambdaInterpreter

type Expr =
    | Var of string
    | App of Expr * Expr
    | Abs of string * Expr

/// Return free vars from expression
let rec freeVars expr =
    match expr with
    | Var v -> Set.singleton v
    | App (e1, e2) -> Set.union (freeVars e1) (freeVars e2)
    | Abs (x, body) -> Set.remove x (freeVars body)

/// Generate fresh unused name
let freshName used =
    let rec loop i =
        let name = $"x_{i}"
        if Set.contains name used then loop (i+1) else name
    loop 0

/// Substitute e` into e on the place of the x
let rec substitute e x e' =
    match e with
    | Var y when y = x -> e'
    | Var y -> Var y
    | App (e1, e2) -> 
        App (substitute e1 x e', substitute e2 x e')
    | Abs (y, body) when y = x -> 
        Abs(y, body)
    | Abs (y, body) ->
        let fv_e' = freeVars e'
        if Set.contains y fv_e' then
            let z = freshName (Set.union (freeVars body) fv_e')
            let newBody = substitute body y (Var z)
            Abs(z, substitute newBody x e')
        else
            Abs(y, substitute body x e')

/// One step of normal reduction
let rec normalStep expr =
    match expr with
    | App (Abs (x, body), arg) ->
        substitute body x arg
    | App (e1, e2) ->
        match normalStep e1 with
        | e1' when e1' <> e1 -> App (e1', e2)
        | _ -> App (e1, normalStep e2)
    | Abs (x, body) ->
        let body' = normalStep body
        if body' <> body then Abs (x, body') else expr
    | _ -> expr

/// Full normal reduction
let rec reduce expr =
    let expr' = normalStep expr
    if expr' = expr then expr
    else reduce expr'
