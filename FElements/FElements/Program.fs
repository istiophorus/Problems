// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System.Net
open System.IO
open FSharp.Data

let splitTextToWords (text: string) =
    text.Split ' ' 
    |> Array.toList

let mapR (input: int list) =
    input |> List.filter (fun x -> x % 3 = 0) |> List.map (fun x -> x * 7)

let printValues (input: int list) =
    input |> List.map (fun x -> printfn "%d\n" x)

let countDistinctWords text = 
    let words = splitTextToWords text
    let numWords = words.Length
    let distinctWords = List.distinct words
    let numDups = numWords - distinctWords.Length
    (numWords, numDups)

let getHttp (url: string) =
    let req = WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let result = reader.ReadToEnd()
    resp.Close()
    result

type Species = HtmlProvider<"https://en.wikipedia.org/wiki/The_world's_100_most_threatened_species">

let species = 
    [ for x in Species.GetSample().Tables.``Species list``.Rows -> x.Type, x.``Common name`` ]

let speciesSorted = 
    species
        |> List.countBy fst
        |> List.sortByDescending snd

let printSpecies (category: string, count: int) =
    printfn "%s\t%d" category count
    ()

let printDangeredSpecies =
    speciesSorted |> List.map printSpecies

let sign x =
    if x > 0 then 1
    elif x < 0 then -1
    else 0

let length li =
    let rec lengthImpl li initial =
        match li with
        | [] -> initial
        | h :: t -> lengthImpl t (1 + initial)
    lengthImpl li 0

let factorial (n: int): int64 =
    let rec factorialImpl (n: int) (initial: int64): int64 =
        if 1 = n then initial
        else factorialImpl (n - 1) (initial * (int64 n))
    factorialImpl n (int64 1)

let fibo (x: int): int =
    let rec fiboImpl (v: int): int =
        printfn "[Inner] Fibo %d" v
        match v with
        | 0 -> 1
        | 1 -> 1
        | _ -> (fiboImpl (v - 1)) + (fiboImpl (v - 2))
    let r = fiboImpl x
    printfn "[In] Fibo %d" r
    r

[<EntryPoint>]
let main argv = 
    let c, dc = countDistinctWords "ala ma kota ale kot nie ma ali"
    printfn " %c %c", c, dc
    seq {0..100} |> Seq.toList |> mapR |> printValues
    getHttp "http://www.krab.agh.edu.pl/forum"
    printDangeredSpecies
    seq {-10..10} |> Seq.toList |> List.map sign |> List.map (printfn "Sign %d")
    seq {-10..10} |> Seq.toList |> length |> printfn "List length %d"
    seq {1..5} |> Seq.toList |> List.map factorial |> List.map (printfn "Factorial %d")
    printfn "[A] Fibo %d" (fibo 10)
    seq {1..5} |> Seq.toList |> List.map fibo |> List.map (printfn "[B] Fibo %d")
    printfn "[C] Fibo %d" (fibo 1)
    0 // return an integer exit code
