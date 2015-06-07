// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "Library1.fs"
open FITSParser
open System
open System.IO

let location = @"C:\Users\brodyberg\Documents\GitHub\FSharp-FITS\FITS\"
let bad = location + "Bad.fits"
let good = location + "superBasic.fits"

Parse bad
Parse good

//let x = System.IO.File.ReadAllText(location + @"superBasic.fits")




