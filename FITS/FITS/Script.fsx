// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "Library1.fs"
open FITSParser
open System
open System.IO

let location = @"C:\Users\brodyberg\Documents\GitHub\FSharp-FITS\FITS\"
let bad = location + "Bad.fits"
let good = location + "superBasic.fits"

let fileText = File.ReadAllText good

let headerText = fileText

let splitWhitespace (str:string) = str.Split()

headerText
|> stringToNewlineSeq
|> Seq.toList
|> List.map 
    (fun line -> 
        line
        |> splitWhitespace
        |> Array.filter (fun item -> not (String.IsNullOrEmpty(item))))



Parse bad
Parse good




