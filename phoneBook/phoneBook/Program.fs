module PhoneBook

open System.IO

type PhoneBook = Map<string, string>

/// Add entry to the PhoneBook
let addEntry name phone phoneBook = 
    Map.add name phone phoneBook

/// Finds phone by name in the PhoneBook. Returns Some(phone) if found and None otherwise.
let findPhoneByName name phoneBook = 
    Map.tryFind name phoneBook

/// Finds name by phone in the PhoneBook. Returns Some(name) if found and None otherwise.
let findNameByPhone phone phoneBook= 
    Map.tryFindKey (fun _ value -> value = phone) phoneBook

/// Returns all PhoneBook entries in list.
let listPhoneBook phoneBook = 
    phoneBook
    |> Map.toList

/// Writes PhoneBook to the file.
let writePhoneBook path phoneBook = 
    phoneBook
    |> Map.toSeq
    |> Seq.map (fun (name, phone) -> $"{name}|{phone}")
    |> Seq.toArray
    |> fun lines -> File.WriteAllLines(path, lines)

/// Reads PhoneBook from the file.
let readPhoneBook path : PhoneBook =
    File.ReadAllLines(path)
    |> Array.choose (fun line ->
        match line.Split('|', 2) with
        | [|name; phone|] -> Some(name, phone)
        | _ -> None)
    |> Map.ofArray
