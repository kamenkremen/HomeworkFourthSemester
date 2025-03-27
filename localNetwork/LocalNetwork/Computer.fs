namespace LocalNetwork

open System

type Computer(id: int, os: IOperatingSystem) = 
    member val ID = id with get
    member val OS = os with get
    member val IsInfected = false with set, get
    member this.TryInfect(random: Random) = 
        if not this.IsInfected && random.NextDouble() < this.OS.InfectionChance then
            this.IsInfected <- true
     