module PrimesTests

open Primes
open NUnit.Framework
open FsUnit


[<Test>]
let ``primes should produce first 25 prime numbers correctly`` () =
    let expected = [2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 67; 71; 73; 79; 83; 89; 97]
    primes() |> Seq.take 25 |> Seq.toList |> should equal expected
