;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname talk) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(define-struct title [main sub])
(define-struct author [name twitter])
(define-struct presentation [title author])

(check-expect
 (author-name (presentation-author talk))
 "Mike Harris")

(check-expect
 (title-main (presentation-title talk))
 "Recursion")

(define talk
  (make-presentation
   (make-title
    "Recursion" "Lather. Rinse. Repeat.")
   (make-author
    "Mike Harris" "@MikeMKH")))

(define-struct agenda [list-of-topics])

(check-expect
 (first (agenda-list-of-topics topics))
 "Recursive Data Type")

(define topics
 (make-agenda
  '("Recursive Data Type"
    "Structural Recursion"
    "Generative Recursion")))

(define (plus-5 n)
  (+ n 5))

(check-expect
 (plus-5 42)
 47)

; list - constructors
; - (cons value list)
; - '()

(check-expect
 '()
 empty)

(check-expect
 (cons 1
       (cons 2
             (cons 3 '())))
 '(1 2 3))

; list - selectors
; - first : list -> value
; - rest  : list -> list

(check-expect
 (first (cons 1 '()))
 1)

(check-expect
 (first '(1))
 1)

(check-expect
 (rest (cons 1 '()))
 '())

(check-expect
 (rest '(1))
 '())

; list - predicates
; - empty? : list -> boolean

(check-satisfied
 '()
 empty?)

; nat - constructors
; - 0
; - (add1 nat)

(check-expect
 (zero? 0)
 #true)

(check-expect
 (add1
  (add1
   (add1 0)))
 3)

; nat - selectors
; - sub1 : nat -> nat

(check-expect
 (sub1
  (add1
   (add1 0)))
 (add1 0))

(check-expect
 (sub1 2)
 1)

; nat - predicates
; - zero? : nat -> boolean

(check-satisfied
 0
 zero?)