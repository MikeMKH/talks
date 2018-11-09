;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-beginner-abbr-reader.ss" "lang")((modname talk) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(define talk-goals
  '("design with recursion"
    "understanding FP examples"
    "teach to learn"))

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
    "Recursion"
    "Lather. Rinse. Repeat.")
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
    "General Recursion")))

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

(define design-steps
  '("problem analysis"
    "function signature"
    "examples"
    "function definition"
    "tests"))

(define recusive-process
  '("identify principal"
    "test basis"
    "reduced recursion"
    "combine results"))

; plus : nat -> nat -> nat
; (plus 0 0) ; 0
; (plus 0 b) ; b
; (plus a 0) ; a
; (plus a b) ; a + b
; principal: a
; basis:     zero?
; reducer:   sub1
; combine:   add1
(define (plus a b)
  (cond
    [(zero? a) b]
    [else
     (add1
      (plus (sub1 a)
            b))]))

(check-satisfied
 (plus 0 0)
 zero?)

(check-expect
 (plus
  0
  (add1 (add1 0)))
 (add1 (add1 0)))

(check-expect
 (plus 0 2)
 2)

(check-expect
 (plus
  (add1 0)
  0)
 (add1 0))

(check-expect
 (plus 1 0)
 1)

(check-expect
 (plus
  (add1 0)
  (add1 (add1 0)))
 (add1 (add1 (add1 0))))

(check-expect
 (plus 1 2)
 3)

(check-expect
 (plus
  (add1 0)
  (add1 (add1 0)))
 (plus
  0
  (add1 (add1 (add1 0)))))

; take : nat -> list -> list
; (take 0 l) ; '()
; (take 0 '()) ; '()
; (take n '()) ; '()
; (take n l of size > n) ; l of size n
; (take n l of size < n) ; l
; principal: n     / l
; basis:     zero? / empty?
; reducer:   sub1  / rest
; combine:   cons
(define (take n l)
  (cond
    [(zero? n) '()]
    [(empty? l) '()]
    [else
     (cons (first l)
           (take (sub1 n)
                 (rest l)))]))

(check-satisfied
 (take 0 '(1 2 3))
 empty?)

(check-satisfied
 (take 0 '())
 empty?)

(check-satisfied
 (take 2 '())
 empty?)

(check-expect
 (take 2 '(1 2 3))
 '(1 2))

(check-expect
 (take
  (add1 (add1 0))
  (cons 1 (cons 2 (cons 3 '()))))
 (cons 1 (cons 2 '())))

(check-expect
 (take
  (add1 (add1 0))
  '(1 2 3))
 (cons (first '(1 2 3))
       (cons (first '(2 3))
             '())))

(check-expect
 (take 5 '(1 2 3))
 '(1 2 3))

(check-expect
 (take
  (add1 (add1 (add1 (add1 (add1 0)))))
  (cons 1 (cons 2 (cons 3 '()))))
 (cons 1 (cons 2 (cons 3 '()))))

(check-expect
 (take 5 '(1 2 3))
 (cons (first '(1 2 3))
       (cons (first '(2 3))
             (cons (first '(3))
                   '()))))

(check-expect
 (take
  (add1 (add1 (add1 (add1 (add1 0)))))
  (cons 1 (cons 2 (cons 3 '()))))
 (take
  (add1 (add1 (add1 0)))
  (cons 1 (cons 2 (cons 3 '())))))

; drop : nat -> list -> list
; (drop 0 l) ; l
; (drop 0 '()) ; '()
; (drop n '()) ; '()
; (drop n l of size n + m) ; l of size m
; (drop n l of size < n) ; '()
; principal: n     / l
; basis:     zero? / empty?
; reducer:   sub1  / rest
; combine:   none
(define (drop n l)
  (cond
    [(zero? n) l]
    [(empty? l) '()]
    [else
     (drop (sub1 n)
           (rest l))]))

(check-expect
 (drop
  0
  (cons 1 (cons 2 (cons 3 '()))))
 (cons 1 (cons 2 (cons 3 '()))))

(check-expect
 (drop 0 '(1 2 3))
 '(1 2 3))

(check-satisfied
 (drop 0 '())
 empty?)

(check-satisfied
 (drop 2 '())
 empty?)

(check-expect
 (drop
  (add1 (add1 0))
  (cons 1 (cons 2 (cons 3 '()))))
 (cons 3 '()))

(check-expect
 (drop 2 '(1 2 3))
 '(3))

(check-expect
 (drop
  (add1 (add1 0))
  (cons 1 (cons 2 (cons 3 '()))))
 (drop
  0
  (cons 3 '())))

(check-satisfied
 (drop 5 '(1 2 3))
 empty?)

(check-expect
 (drop
  (add1 (add1 (add1 (add1 (add1 0)))))
  (cons 1 (cons 2 (cons 3 '()))))
 (drop
  (add1 (add1 0))
  '()))

; bundle : nat -> list -> list
; (bundle 0 l) ; '()
; (bundle 0 '()) ; '()
; (bundle n '()) ; '()
; (bundle n l of size n * m) ; '(l of size m, ...)
; (bundle n l of size n * m + p) ; '(l of size m, ..., l of size p)
; (bundle n l of size < n) ; '(l)
; principal: l
; basis:     zero? / empty?
; reducer:   take n
; combine:   cons
(define (bundle n l)
  (cond
    [(zero? n) '()]
    [(empty? l) '()]
    [else
     (cons (take n l)
      (bundle n (drop n l)))]))

(check-satisfied
 (bundle 0 '(1 2 3))
 empty?)

(check-satisfied
 (bundle 0 '())
 empty?)

(check-satisfied
 (bundle 2 '())
 empty?)

(check-expect
 (bundle 2 '(1 2 3 4))
 '((1 2) (3 4)))

(check-expect
 (bundle 3 '(1 2 3 4 5 6 7))
 '((1 2 3) (4 5 6) (7)))

(check-expect
 (bundle 3 '(1 2))
 '((1 2)))

; in closing

(define-struct joke [statement punch-line])

(define programmer-recursive-joke
  (make-joke
   "Why did the programmer run out of shampoo?"
   "The instructions said: lather, rinse, repeat."))

(check-expect
 (joke-statement programmer-recursive-joke)
 "Why did the programmer run out of shampoo?")
