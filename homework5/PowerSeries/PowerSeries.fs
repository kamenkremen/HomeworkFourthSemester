/// Function that generates a series of powers of two from 2^n to 2^(n+m)
let powerSeries n m =
    let rec helper currentValue count accumulator =
        if count = 0 then
            List.rev accumulator
        else
            helper (currentValue * 2) (count - 1) (currentValue :: accumulator)
    helper (pown 2 n) m []
