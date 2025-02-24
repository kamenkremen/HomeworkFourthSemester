///A function to calculate the n-th Fibonacci number.
let fibonacci n =
    let rec fibonacciHelper = function
        | n when n < 0 -> Error "value can`t be negative"
        | 0 -> Ok 0
        | 1 -> Ok 1
        | n -> Ok(Result.defaultValue 0 (fibonacciHelper (n - 1)) + 
            Result.defaultValue 0 (fibonacciHelper (n - 2)))
    fibonacciHelper n
