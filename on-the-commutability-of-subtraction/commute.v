Require Import Coq.Arith.Arith.
Require Import Omega.

Example test_subtraction_commutes :
0 - 0 = 0 - 0.
Proof.
  simpl.
  reflexivity.
Qed.

Example test_subtraction_commutes' :
1 - 0 = 0 - 1.
Proof.
  simpl.
Abort.

(* from http://stackoverflow.com/a/44039996/2370606 *)
Lemma subtraction_does_not_commute :
forall a b : nat, a <> b -> a - b <> b - a.
Proof.
induction a. intros b.
- now rewrite Nat.sub_0_r.
- destruct b.
  + trivial.
  + repeat rewrite Nat.sub_succ; auto.
Qed.

Lemma subtraction_does_not_commute' :
forall a b : nat, a <> b -> a - b <> b - a.
Proof.
intros; omega.
Qed.