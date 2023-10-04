```
Author:    [Sangeun An]
Partner:    None
Date:       [10/4/2023]
Course:     CS 3500,  UAC,ECE
GitHub ID:  [sangeunaan]
Repo:       https://github.com/Fall-2023-CS3500-Class/spreadsheet-[sangeunaan]
Commit #:   1b10211fa6b7e5ed43c8f81463475c7ab39bded9
Project:    Dependency Graph
Copyright:  CS 3500 and [Your Name(s)] - This work may not be copied for use in Academic Coursework.
```

# Comments to Dependency Graph:

Dependency Graph deals with the dependency relationship between cells.
Dependee cells should be already evaluated to evaluate a dependent cell.
This project implements the dependency using two dictionary data structures, dependents and dependees, which have hash tables as the value.
The methods will help adding and removing the relationships of the dependency graph.

# Assignment Specific Topics:

There was some confusion on the function of methods.
For example, it had to check "Dependees" library to check the dependency graph has dependents,
and check "Dependents" library to check the dependency graph has dependees.

I was not sure whether I should delete the key if the hashset value of the key is empty.

# Consulted Peers:

No Peer

# References:

    1. HashSet<T> class - https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.hashset-1?view=net-7.0
   