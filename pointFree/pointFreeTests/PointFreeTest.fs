module pointFreeTests

open PointFree
open NUnit.Framework
open FsCheck
open FsUnit


[<Test>]
let ``Functions should be equal``() = 
    let equality1 x l = (func1 x l = func2 x l)
    let equality2 x l = (func2 x l = func3 x l)
    let equality3 x l = (func3 x l = func4 x l)
    Check.Quick equality1
    Check.Quick equality2
    Check.Quick equality3
    