module Tests

// cd test/; dotnet watch test
open Xunit

type Order = {
    Zip : int;
    Price : double;
    Quantity : int;
}

[<Fact>]
let ``My test`` () =
  let orders = [
    { Zip = 53202; Price = 1.89; Quantity = 3 };
    { Zip = 60191; Price = 1.99; Quantity = 2 };
    { Zip = 60060; Price = 0.99; Quantity = 7 };
    { Zip = 53202; Price = 1.29; Quantity = 8 };
    { Zip = 60191; Price = 1.89; Quantity = 2 };
    { Zip = 53202; Price = 0.99; Quantity = 3 }
  ]
  let result =
    orders
      |> List.filter (fun x -> x.Zip = 53202)
      |> List.map (fun x -> x.Price * (double x.Quantity))
      |> List.sum
  Assert.Equal(18.96, result)
