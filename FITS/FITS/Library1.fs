﻿module FITSParser

open System
open System.IO
open System.Text.RegularExpressions

type HeaderValue = 
| Value of string

type HeaderKey = 
| Key of string

type HeaderComment = 
| Comment of string option

type HeaderKeyValue = 
| KeyValue of HeaderKey * HeaderValue 
| KeyValueComment of HeaderKey * HeaderValue * HeaderComment

type HDU = 
| Header of HeaderKeyValue list

type SpecificHeaderProblem = 
| NoEND
| Invalid of string

type BadFile = 
| InvalidHeader of SpecificHeaderProblem
| Other

type FITS = 
| FailedToParse of BadFile
| NoSuchFile of string
| File of HDU

// need an active pattern that just searches for ^END

let (|Regex|_|) pattern input =
    printfn "regex: pattern: %A input: %A" pattern input
    let m = Regex.Match(input, pattern)
    printfn "m: %A" m
    if m.Success 
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

//let phone = "(555) 555-5555"
//match phone with
//| Regex @"\(([0-9]{3})\)[-. ]?([0-9]{3})[-. ]?([0-9]{4})" [ area; prefix; suffix ] ->
//    printfn "Area: %s, Prefix: %s, Suffix: %s" area prefix suffix
//| _ -> printfn "Not a phone number"

let (|Header|_|) (str:string) = 
    match str.LastIndexOf("END") with
    | -1 -> None
    | _ -> Some(str)

let (|KeyValue|_|) (str:string) = 
    printfn "KeyValue: %s" str
    match str with
    | Regex @"(^[a-zA-Z]*)\s*=\s*([a-zA-Z])" [key; value] -> Some(key, value)    
    | _ -> None

//    let parts1 = str.Split([| '\n' |])
//    printfn "parts1: %A" parts1

//    str
//    |> Seq.iter (fun item -> printfn "item: [%c]" item)
//
//    let parts = str.Split([|' '|])
//
//    printfn "parts: %A" parts

//    None

let (|KeyValueComment|_|) (str:string) = None

let Parse (path:string) = 

    if not (File.Exists path) 
    then NoSuchFile(path)
    else 
        let fileText = File.ReadAllText path

        match fileText with 
        | Header(headerText) -> 
            match (fileText.Length >= 80) with
            | true -> 
//                let first80 = fileText.Substring(0, 80)
  
                let parts = fileText.Split([| '\n' |])
                let first80 = parts.[0]
    
                match first80 with
                | KeyValue(key,value) -> 
                    File(Header([KeyValue(Key(key), Value(value))]))
                | KeyValueComment(key,value,comment) -> 
                    File(Header([KeyValueComment(Key(key), Value(value), Comment(comment))]))
                | _ -> FailedToParse(InvalidHeader(Invalid(first80)))
            | false -> FailedToParse(InvalidHeader(Invalid("Too short")))
        | _ -> FailedToParse(InvalidHeader(NoEND))

