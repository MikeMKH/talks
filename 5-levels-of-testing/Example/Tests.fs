module Tests

open System
open Xunit
open FsCheck.Xunit

// not tail call optimize
let rec append xs ys =
  match xs, ys with
  | [], ys -> ys
  | x :: xs', ys -> x :: append xs' ys 

[<Fact>]
let ``Given empty list with list it must return list`` () =
    Assert.Equal<Collections.Generic.IEnumerable<int>>(
        [1], append [] [1])

[<Fact>]
let ``Given list with empty list it must return list`` () =
    Assert.Equal<Collections.Generic.IEnumerable<int>>(
        [1], append [1] [])

[<Fact>]
let ``Given two list it must concat them`` () =
    Assert.Equal<Collections.Generic.IEnumerable<int>>(
        [1; 2; 3; 4], append [1; 2] [3; 4])
        
[<Property>]
let ``Append with empty returns list``
  (xs : int list) =
  xs = append [] xs
  && xs = append xs []
        
[<Property>]
let ``Append will be length of both``
  (xs : int list, ys : int list) =
  List.length xs + List.length ys =
    (append xs ys |> List.length)
        
[<Property>]
let ``Append will contain members``
  (x : int, ys : int list) =
  append [x] ys |> List.contains x
  
[<Property>]
let ``Use built in``
  (xs : int list, ys : int list) =
  xs @ ys = append xs ys
