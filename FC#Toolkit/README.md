# Toolkit for extending C# with functional capabilites
- **HOT TO NAVIGATE:**
	- *DataTypes namespace:*
		- Consists of basic functional datatypes: Either, Option
		- Also provides extension methods for processing the datatypes
	- *Library root namespace:*
		- Extension class - Provides these extension methods: Pipe, PipeO, PipeE, Try, UseContext 
		- Context struct: Helper object for function execution in a context
- For usage examples see the "Tests" project

# Immutablility
- For objects use structs
- For collections use System.Collections.Immutable