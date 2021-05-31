# MoveSearchingManager
Project to assess the technical level

﻿Backend Development Assignment - Nyiko Anetta

I've started working on the project on 27th of May, but I'm completely new int the MongoDB, so I've started with learning.
I've checked some MongoDB tutorials at 27th and 28th of May, after my normal working hours.
I started the actual development on Saturday morning.  In total at the weekend I've worked approximative 12 hours.
At Monday I created the Git repository, and I worked a little bit in the ReportPerDay function. (approx. 4 hours) I didn't follow the branching strategy, cause I worked at my local machine, and I put the code to the repo when it was mostly done.

Comments:
	- Firstly I've thought that in the Movie.cs class I'll split the different properties in arrays (ie. Genre, Writer, Actors), but I kept the given version.
	- The same problem with the Movie.cs class Date property (Released), I'm not sure if every value are unified in the database, so I kept in string.
	- I've made the simple mongoDB query functions Generic for reusability reason.
	- I made two controllers based on security.
	- I wrote some unit tests, but just for the MovieSearchingController. I'm still have a lot to learn about the TDD, especially about the data mocking, but I tried to create my application unit testable for the future.
	- An update suggestion is to separate the generic functions for mongoDB operations in a class, and we can use this class as a parent class, and derive the specific classes from it. 
