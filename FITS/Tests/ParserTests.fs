namespace Tests.FITS

open Xunit 
open Swensen.Unquote
open FITSParser

module Parser = 
    
    let location = @"..\..\..\"
    let bad = location + "Bad.fits"
    let good = location + "superBasic.fits"

    [<Fact>]
    let ``If file does not exist, returns NoSuchFile(string)``() =
        test <@ Parse "noSuchFile.fits" = NoSuchFile("noSuchFile.fits") @>

    [<Fact>]
    let ``If invalid header found, returns FailedToParse(InvalidHeader(string))``() =
        test <@ Parse bad = FailedToParse(InvalidHeader("foo")) @>

    [<Fact>]
    let ``A FITS file with one valid header returns key and value``() =
        test <@ Parse good = File(Header([KeyValue(Key("SIMPLE"), Value("T"))])) @>
    







