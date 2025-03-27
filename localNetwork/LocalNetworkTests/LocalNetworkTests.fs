module LocalNetworkTests

open LocalNetwork
open NUnit.Framework
open FsUnit
open System.Collections.Generic
open Moq

[<Test>]
let ``When always infecting should act as BFS`` () =
    let AlwaysInfectOS = Mock<IOperatingSystem>()
    AlwaysInfectOS.Setup(fun os -> os.InfectionChance).Returns 1.0 |> ignore
    let a = Computer(0, AlwaysInfectOS.Object)
    let b = Computer(1, AlwaysInfectOS.Object)
    let c = Computer(2, AlwaysInfectOS.Object)
    a.IsInfected <- true

    let adjacencyList = Dictionary<Computer, Computer list>()
    adjacencyList.Add(a, [b])
    adjacencyList.Add(b, [a; c])
    adjacencyList.Add(c, [b])

    let network = LocalNetwork(adjacencyList)

    network.Step() |> should be True
    b.IsInfected |> should be True
    c.IsInfected |> should be False

    network.Step() |> should be True
    c.IsInfected |> should be True

    network.Step() |> should be False

[<Test>]
let ``When never infecting should do nothing`` () =
    let NeverInfectOs = Mock<IOperatingSystem>()
    NeverInfectOs.Setup(fun os -> os.InfectionChance).Returns 0.0 |> ignore
    let a = Computer(0, NeverInfectOs.Object)
    let b = Computer(1, NeverInfectOs.Object)
    let c = Computer(2, NeverInfectOs.Object)
    a.IsInfected <- true

    let adjacencyList = Dictionary<Computer, Computer list>()
    adjacencyList.Add(a, [b])
    adjacencyList.Add(b, [a; c])
    adjacencyList.Add(c, [b])

    let network = LocalNetwork(adjacencyList)

    network.Step() |> should be False
    b.IsInfected |> should be False
    c.IsInfected |> should be False
