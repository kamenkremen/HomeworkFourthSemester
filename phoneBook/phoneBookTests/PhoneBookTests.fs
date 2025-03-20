module PhoneBookTests

open NUnit.Framework
open FsUnit
open PhoneBook

[<SetUp>]
let Setup () =
    let emptyBook: PhoneBook = Map.empty<string, string>
    ()

[<Test>]
let ``addEntry should work correctly`` () =
    let emptyBook: PhoneBook = Map.empty<string, string>
    let newBook = addEntry "sample" "123" emptyBook
    newBook |> Map.find "sample" |> should equal "123"

[<Test>]
let ``findPhoneByName should work correctly`` () =
    let sampleBook: PhoneBook = Map ["Alice", "123456"; "Bob", "789012"; "Charlie", "345678"]
    findPhoneByName "Alice" sampleBook |> should equal (Some "123456")
    findPhoneByName "Bob" sampleBook |> should equal (Some "789012")
    findPhoneByName "Charlie" sampleBook |> should equal (Some "345678")
    findPhoneByName "amogus" sampleBook |> should equal None

[<Test>]
let ``findNameByPhone should work correctly`` () =
    let sampleBook: PhoneBook = Map ["Alice", "123456"; "Bob", "789012"; "Charlie", "345678"]
    findNameByPhone "123456" sampleBook |> should equal (Some "Alice")
    findNameByPhone "789012" sampleBook|> should equal (Some "Bob")
    findNameByPhone "345678" sampleBook|> should equal (Some "Charlie")
    findNameByPhone "amogus" sampleBook|> should equal None

[<Test>]
let ``listPhoneBook should work correctly`` () =
    let sampleBook: PhoneBook = Map ["Alice", "123456"; "Bob", "789012"; "Charlie", "345678"]
    let expected = ["Alice", "123456"; "Bob", "789012"; "Charlie", "345678"]
    listPhoneBook sampleBook |> should equal expected

[<Test>]
let ``Writing and reading phoneBook should work correctly``() =
    let sampleBook: PhoneBook = Map ["Alice", "123456"; "Bob", "789012"; "Charlie", "345678"]

    writePhoneBook "test.txt" sampleBook
    let loaded = readPhoneBook "test.txt"
    loaded |> should equal sampleBook
