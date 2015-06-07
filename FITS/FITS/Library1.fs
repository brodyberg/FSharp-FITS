module FITSParser

open System
open System.IO

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

type BadFile = 
| InvalidHeader of string

type FITS = 
| FailedToParse of BadFile
| NoSuchFile of string
| File of HDU

let (|KeyValue|_|) (str:string) = None
let (|KeyValueComment|_|) (str:string) = None

let Parse (path:string) = 
 
    if not (File.Exists path) 
    then NoSuchFile(path)
    else 
        let fileText = File.ReadAllText path

        let first80 = fileText.Substring(0, 80)
    
        match first80 with
        | KeyValue(key,value) -> File(Header([KeyValue(Key(key), Value(value))]))
        | KeyValueComment(key,value,comment) -> File(Header([KeyValueComment(Key(key), Value(value), Comment(comment))]))




