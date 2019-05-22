#introduction

I used to read codinghorror since it is an interesting blog for programmers. On it, he mentioned the fizzbuzz problem and the (lack of) skill of senior programmers.

I decided to do a take on this, "the proper way".

> write a program that prints the numbers from 1 to 100. But for multiples of three print "Fizz" instead of the number and for the multiples of five print "Buzz". For numbers which are multiples of both three and five print "FizzBuzz".

https://blog.codinghorror.com/why-cant-programmers-program/
https://en.wikipedia.org/wiki/Fizz_buzz

I decided to create different versions on it to show my thought-process.

#versions

- v1: basic version, basic implementation with a simple for-loop
- v2: separate logic from for-loop
- v3: place logic inside a domain-class and unit test this version

#remarks
I made one solution for this, due to the triviality of this version, "set as startup project" whenever I want to change a version.

FizzBuzz_v1
FizzBuzz_v2
FizzBuzz_v3

Are the startable projects.

v3 consists of multiple projects
- FizzBuzz_v3:			main program
- FizzBuzz_v3.Domain: 	logic portion
- FizzBuzz_v3.UnitTest:	unit test

Note: normally one first starts with unit tests. In this case they are added afterwards, which should _not_ happen.