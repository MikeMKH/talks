;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname background) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(require 2htdp/image)

(define SMALL 8)
(define small-triangle (triangle SMALL "outline" "red"))

(check-expect (sierpinski SMALL)
              small-triangle)
(check-expect (sierpinski (* 2 SMALL))
              (above small-triangle
                     (beside small-triangle small-triangle)))

(define (sierpinski side)
  (cond
    [(<= side SMALL) small-triangle]
    [else
     (local ((define half (sierpinski (/ side 2))))
       (above half
              (beside half half)))]))

(save-image (sierpinski (* 50 SMALL)) "sierpinski.jpg")