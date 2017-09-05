Predicates Class notes.
========================
- 5/9 class notes for predicates in discrete math.
------------------------------
## Assignment
- Assignment is to create a program that handles predicates and such in a program to programming with predicates.
[http://www.swi-prolog.org/](http://www.swi-prolog.org/) - to download.

assignments description has not yet been formulated, but it is about classes and students and the relations between one another. In the swi prolog program.

## Termonology
Statements is a sentence which will result in truth or false.

Predicates are statements with variables which will then change to either true or false when giving the statements concret variables.

- R = all real numbers
- R+ = all positiv numbers (0 not included)
- R- = all negativ numbers (0 not included)
- R(nonneg) = all positiv numbers (0 included)
- Z = all integers
- N or Z = all natural numbers (0 not included as it's not a natural number
- N0 or Z(noneng) = all natural numbers (0 included)
- Q = all rational numbers(redigeret)
- ∀ = for all (For this to be true, then "ALL" needs to hold true", alot of and operations)
- ∃ = one of them, one exists within (For this to be true, just one needs to hold true, alot of or operations)
- ∈ = set ((part of) as in x is part of R) meaning x is all real numbers (most used to discripe elements which will be apart of a domain)

## Notes
--------------------------------
'''
A = {x ∈ N | x^2 < 10}
'''
a now becomes a domain with elements. in this example the elements in the domain will look like this {1,2,3} because nothing else can be powered by 2 and still be under 10, and still be a natural number.

as A = {1,2,3,....,10}.

so with the above set logic X = {1,2,3} as they become 1,4,9 respectively which is less than 10 and since X is a natural number it can't be 0 or below.

Example: { x ∈ D | P(x) }.

Means: X is an element of D. D is a domain, and P(x) is the predicate.

P(x) is the predicate.
'''
"x^2 > x", x E R
'''
Truth set is: x = {R} , {...,-2, -1, 2, ...} or {x ∈ R | x < 0 A 1 < x}
-------------------------------------------------
All men are mortal.

Socrates is a man.

∴ Socrates is mortal.

∀ human beings X, x is mortal.

∀x ∈ H, is mortal.

∃ = there exists(redigeret).

There is a student in Discrete Math.

∃ a person p such that p is a Discrete Math student.

∃p ∈ P, such that p is a Discrete Math student.

E = {5,6,7,8} and N is all positive ints.

"∃m ∈ N, m^2 = m" is true.

"∃m ∈ E, m^2 = m" is false.

---------------------------
