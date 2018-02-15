import Data.List.Views

listAreEquivalent : [1, 2, 3] = 1 :: 2 :: 3 :: []
listAreEquivalent = Refl

take' : (n : Nat) -> (xs : List a) -> List a
take' Z _ = []
take' (S k) [] = []
take' (S k) (x :: xs) = x :: take' k xs

headIsFirstElem : 1 = head [1, 2, 3]
headIsFirstElem = Refl

headIsAlwaysFirstElem : (ty : Type) -> (x : ty) -> (xs : List ty) ->
  x = head (x :: xs)
headIsAlwaysFirstElem ty x xs = Refl

take1IsSimilarToHead : take 1 [1, 2, 3] = head [1, 2, 3] :: []
take1IsSimilarToHead = Refl

take1IsAlwaysHead : (ty : Type) -> (x : ty) -> (xs : List ty) ->
  take 1 (x :: xs) = head (x :: xs) :: []
take1IsAlwaysHead ty x xs = Refl

takeLast' : (n : Nat) -> (xs : List a) -> List a
takeLast' n xs = reverse $ take n $ reverse xs

lastIsLastElem : 3 = last [1, 2, 3]
lastIsLastElem = Refl

takeLast1IsSimilarToLast : takeLast' 1 [1, 2, 3] = last [1, 2, 3] :: []
takeLast1IsSimilarToLast = Refl

takeLast1IsAlwaysLastElem : (ty : Type) -> (x : ty) -> (xs : List ty) ->
  takeLast' 1 (xs ++ [x]) = [x]
takeLast1IsAlwaysLastElem ty x xs with (snocList xs)
  takeLast1IsAlwaysLastElem ty x [] | Empty = Refl
  takeLast1IsAlwaysLastElem ty x ([] ++ [y]) | (Snoc rec) = Refl

takeLast : (n : Nat) -> List a -> List a
takeLast n xs with (snocList xs)
  takeLast Z [] | Empty = []
  takeLast Z (ys ++ [x]) | (Snoc rec) = []
  takeLast (S k) [] | Empty = []
  takeLast (S k) (ys ++ [x]) | (Snoc rec) = takeLast k ys ++ [x] | rec

-- takeLastsAreEquivalent : takeLast' 2 [1, 2, 3] = takeLast 2 [1, 2, 3]
-- takeLastsAreEquivalent = Refl
