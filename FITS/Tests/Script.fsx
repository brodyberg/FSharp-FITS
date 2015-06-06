// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

//#load "Library1.fs"
//open Tests

// Define your library scripting code here

#r @"../packages/Unquote.3.0.0/lib/net45/Unquote.dll"

open Swensen.Unquote

test <@ (1+2)/3 = 1 @>

test <@ (1 + 2) = 2 @> 











