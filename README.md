8-puzzle solver created during AI course in university, following S. Russell and P. Norvig book "Artificial Intelligence - A Modern Approach".

C#, .NET 4.5

Includes unit tests and GUI application (WPF) for some vizualization.

![gif](http://i.imgur.com/vQtCeZf.gif)

Implemented algorithms:

- Iterative Deepening Search
- A*
- Recursive Best-First Search

A* and RBFS can be used with Manhattan Distance heuristic function or without any heuristic function.

In GUI it is possible to choose board size, from 2x2 to 5x5 (can be not square). But starting from 4x4 it may take more time (such as 2-10 minutes) to find the solution and it finishes in reasonable time not for all inputs, using RBFS (A* works too but may take too much memory and crash).

# How to build

Open the .sln file in Visual Studio 2015 or later, build solution. 

*(some C#6 features are used, so it will not work in earlier VS versions, and because of .NET 4.5 it runs only on Win 7+. There is also win-xp branch in this repository to target .NET 4.0.)*

Run EightPuzzleSolverApp (should be active by default in VS) for GUI. 

Tests (xUnit.net) can be run using Visual Studio Test Explorer or ReSharper (with xUnit extension), or `packages\xunit.runner.console.2.1.0\tools\xunit.console EightPuzzleSolver.Tests\bin\Release\EightPuzzleSolver.Tests.dll` command.
