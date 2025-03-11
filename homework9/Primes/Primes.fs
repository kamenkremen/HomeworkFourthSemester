/// sequence of prime numbers
let primes() = 
    let rec sieve numbers = 
        seq {
            let x = Seq.head numbers
            yield x
            yield! sieve (numbers |> Seq.skip 1 |> Seq.filter (fun n -> n % x <> 0))
        }
    sieve (Seq.initInfinite (fun i -> i + 2))
