type ParseTree = 
  | Number of int
  | Add of ParseTree * ParseTree
  | Subtract of ParseTree * ParseTree
  | Multiply of ParseTree * ParseTree
  | Divide of ParseTree * ParseTree

/// the function that evaluates the value of a parse tree
let rec evaluate tree = 
    match tree with
      | Number x -> x
      | Add (left, right) -> evaluate left + evaluate right
      | Subtract (left, right) -> evaluate left - evaluate right
      | Multiply (left, right) -> evaluate left * evaluate right
      | Divide (left, right) -> 
        match evaluate right with 
          | 0 -> failwith "Division by zero"
          | _ -> evaluate left / evaluate right
