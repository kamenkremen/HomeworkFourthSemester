module pointFreeTests

open PointFree
open FsCheck.NUnit

[<Property>]
let ``All functions return same result`` (x: int, l: int list) =
    func1 x l = func2 x l 
    && func2 x l = func3 x l 
    && func3 x l = func4 x l

