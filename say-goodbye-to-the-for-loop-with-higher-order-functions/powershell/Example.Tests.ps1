# powershell
# invoke-pester

Describe "Fusion property of iterators" {
  Context "Given collection of data" {
    $orders = @(
      @{ Zip = 53202; Price = 1.89; Quantity = 3},
      @{ Zip = 60191; Price = 1.99; Quantity = 2},
      @{ Zip = 60060; Price = 0.99; Quantity = 7},
      @{ Zip = 53202; Price = 1.29; Quantity = 8},
      @{ Zip = 60191; Price = 1.89; Quantity = 2},
      @{ Zip = 53202; Price = 0.99; Quantity = 3}
    )
    It "Must produce the value expected" {
      ($orders |
        Where-Object { $_.Zip -eq 53202 } |
        Select-Object @{
          Name ="Amount";
          Expression = {$_.Price * $_.Quantity} } |
        Measure-Object Amount -Sum
      ).Sum | Should Be 18.96
    }
  } 
}