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
               