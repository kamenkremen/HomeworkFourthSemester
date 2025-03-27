namespace LocalNetwork

open System
open System.Collections.Generic

type LocalNetwork(adjacencyList: Dictionary<Computer, Computer list>) = 
    member val Computers = adjacencyList.Keys |> Seq.toList with get
    member val AdjacencyList = adjacencyList with get

    member private this.GetNextInfectedComputers = 
        this.Computers
        |> List.filter(fun computer -> not computer.IsInfected && this.AdjacencyList[computer] |> List.exists (fun c -> c.IsInfected))

    member this.Step() = 
        match this.GetNextInfectedComputers with 
        | [] -> false
        | computers -> 
            match computers |> List.filter (fun computer -> computer.OS.InfectionChance > 0.0) with
            | [] -> false
            | computers ->
                computers
                |> List.iter (fun computer -> computer.TryInfect Random.Shared)
                true
    
    member this.Start() = 
        while this.Step() do
            this.Computers |> List.iter(fun computer -> printfn "Computer %d, infection status: %b" computer.ID computer.IsInfected)
    
    member this.GetStatus()  = 
        this.Computers |> List.map(fun computer -> computer.IsInfected)

