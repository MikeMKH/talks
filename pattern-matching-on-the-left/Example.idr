import Data.List.Views

listAreEquivalent : [1, 2, 3] = 1 :: 2 :: 3 :: []
listAreEquivalent = Refl

take' : (n : Nat) -> (xs : List a) -> List a
take' Z _ = []
take' (S k) [] = []
take' (S k) (x :: xs) = x :: take' k xs

headIsFirstElem : 1 = head [1, 2, 3]
headIsFirstElem = Refl

take1IsSimilarToHead : take 1 [1, 2, 3] = head [1, 2, 3] :: []
take1IsSimilarToHead = Refl

takeLast' : (n : Nat) -> (xs : List a) -> List a
takeLast' n xs = reverse $ take n $ reverse xs

lastIsLastElem : 3 = last [1, 2, 3]
lastIsLastElem = Refl

takeLast1IsSimilarToLast : takeLast' 1 [1, 2, 3] = last [1, 2, 3] :: []
takeLast1IsSimilarToLast = Refl

takeLast : (n : Nat) -> List a -> List a
takeLast n xs with (snocList xs)
  takeLast Z [] | Empty = []
  takeLast Z (ys ++ [x]) | (Snoc rec) = []
  takeLast (S k) [] | Empty = []
  takeLast (S k) (ys ++ [x]) | (Snoc rec) = takeLast k ys ++ [x] | rec

-- takeLastsAreEquivalent : takeLast' 2 [1, 2, 3] = takeLast 2 [1, 2, 3]
-- takeLastsAreEquivalent = Refl
