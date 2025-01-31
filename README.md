# Toolkit for extending C# with functional capabilities
- **HOW TO NAVIGATE:**
	- *DataTypes namespace:*
		- Consists of basic functional datatypes: Either, Option
		- Also provides extension methods for processing the datatypes
	- *Library root namespace:*
		- Extension class - Provides these extension methods: Pipe, PipeO, PipeE, Try, UseContext 
		- Context struct: Helper object for function execution in a context 

- For usage examples see the "Tests" project

# Context struct
- Enables to set data for function chain execution within a context
- Use InitContext extension on a data that are desired to be used as context data
- Use Next or Final to execute a function within the context 
- Next: will enable to chain another function within a context
- Final: This method will result in returning the final output (the context is exited by this call)

# Immutability
- For objects use structs
- For collections use System.Collections.Immutable