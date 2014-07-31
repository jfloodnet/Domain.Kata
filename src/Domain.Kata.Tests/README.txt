TEST TOOLS
-----------------------

All tests make use of AutoFixture and xUnits data theories.

Essentially, AutoData allows our tests to invert control of test data, mocks and sut creation to AutoFixture, which builds 
and injects them into the test via parameters to the test method. 

Read the following links for more information.

AutoFixture: https://github.com/AutoFixture/AutoFixture

"AutoFixture makes it easier for developers to do Test-Driven Development by automating non-relevant 
Test Fixture Setup, allowing the Test Developer to focus on the essentials of each test case."

AutoData Theories with AutoFixture: http://blog.ploeh.dk/2010/10/08/AutoDataTheoriesWithAutoFixture.aspx


Test Patterns
------------------------
A lot of the conventions within the tests follow some of the thoughts in this series on zero friction TDD.

http://blogs.msdn.com/b/ploeh/archive/2008/11/13/zero-friction-tdd.aspx

1. AutoFixture derives it's name from xunit patterns "Fixture"

2. Naming the System Under Test "sut".
