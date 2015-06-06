module public Bar

open Xunit 
open Swensen.Unquote

[<Fact>]
let ``When we add we get three`` () = 
    test <@ (1+2)/3 = 1 @>

[<Fact>]
let ``When we add we get two`` () = 
    test <@ (1+2)/3 = 2 @>










