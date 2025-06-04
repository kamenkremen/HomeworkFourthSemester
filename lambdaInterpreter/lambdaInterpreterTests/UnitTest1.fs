module lambdaInterptreterTests

open NUnit.Framework
open LambdaInterpreter
open FsUnit
let var x = Var x
let app a b = App(a, b)
let abs x e = Abs(x, e)

[<Test>]
let ``freeVars should return variable itself``() =
    let expr = var "x"
    Assert.AreEqual(Set.singleton "x", freeVars expr)

[<Test>]
let ``freeVars should remove bound variables``() =
    let expr = abs "x" (app (var "x") (var "y"))
    Assert.AreEqual(Set.singleton "y", freeVars expr)

[<Test>]
let ``freeVars should handle nested abstractions``() =
    let expr = abs "x" (abs "y" (app (var "x") (var "z")))
    Assert.AreEqual(Set.singleton "z", freeVars expr)


[<Test>]
let ``substitute should replace free variable``() =
    let expr = app (var "x") (var "y")
    let result = substitute expr "x" (var "z")
    Assert.AreEqual(app (var "z") (var "y"), result)

[<Test>]
let ``substitute should avoid variable capture with alpha conversion``() =
    let expr = abs "y" (app (var "x") (var "y"))
    let result = substitute expr "x" (var "y")
    
    match result with
    | Abs(newName, App(Var "y", Var v)) when newName <> "y" && v = newName -> 
        Assert.Pass()
    | _ -> 
        Assert.Fail()

[<Test>]
let ``substitute should not replace bound variable``() =
    let expr = abs "x" (var "x")
    let result = substitute expr "x" (var "y")
    Assert.AreEqual(expr, result)

[<Test>]
let ``normalStep should reduce beta-redex``() =
    let expr = app (abs "x" (var "x")) (var "y")
    let result = normalStep expr
    Assert.AreEqual(var "y", result)

[<Test>]
let ``normalStep should reduce outer-leftmost redex first``() =
    let expr = app (app (abs "x" (var "x")) (abs "y" (var "y"))) (var "z")
    let result = normalStep expr
    Assert.AreEqual(app (abs "y" (var "y")) (var "z"), result)

[<Test>]
let ``normalStep should reduce under abstraction``() =
    let expr = abs "x" (app (abs "y" (var "y")) (var "z"))
    let result = normalStep expr
    Assert.AreEqual(abs "x" (var "z"), result)

[<Test>]
let ``reduce should reach normal form``() =
    let expr = app (abs "x" (app (var "x") (var "x"))) (abs "y" (var "y"))
    let result = reduce expr
    Assert.AreEqual(abs "y" (var "y"), result)

[<Test>]
let ``reduce should handle alpha conversion in normal form``() =
    let expr = app (abs "x" (abs "y" (var "x"))) (var "y")
    let result = reduce expr
    
    match result with
    | Abs(name, Var "y") when name <> "y" -> 
        Assert.Pass()
    | _ -> 
        Assert.Fail()

[<Test>]
let ``reduce should compute church numerals correctly``() =
    let church0 = abs "f" (abs "x" (var "x"))
        
    let succ = 
        abs "n" (abs "f" (abs "x" (app (var "f") (app (app (var "n") (var "f")) (var "x")))))
    
    let expr = app succ church0
    let result = reduce expr
    
    match result with
    | Abs("f", Abs("x", App(Var "f", Var "x"))) -> 
        Assert.Pass()
    | Abs(_, Abs(_, App(Var f, Var x))) when f = x -> 
        Assert.Fail("Variable names conflict")
    | _ -> 
        Assert.Fail("Unexpected result structure")
