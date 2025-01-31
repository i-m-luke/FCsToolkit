# Toolkit for extending C# with functional capabilities

- **HOW TO NAVIGATE:**
  - _DataTypes namespace:_
    - Consists of basic functional datatypes: Either, Option
    - Also provides extension methods for processing the datatypes
  - _Library root namespace:_
    - Extension class - Provides these extension methods: Pipe, PipeO, PipeE, Try, UseContext, Compose
    - Context struct: Helper object for function execution in a context
- For usage examples see the "Tests" project

# Basic extension methods

- **Pipe:** Basic pipe operation
- **PipeO:** Pipe operation resulting in Option
- **PipeE:** Pipe operation resulting in Either
- **Try:** Wraps a function in a Try/Catch block
- **UseContext:** Initializes a context for function execution
- **Compose:** Compose a function with other functions

# Context struct

- Enables to set data for function chain execution within a context
- Use InitContext extension on a data that are desired to be used as context data
- Use Next or Final to execute a function within the context
- Next: will enable to chain another function within a context
- Final: This method will result in returning the final output (the context is exited by this call)

# Immutability

- For objects use readonly structs
- For collections use System.Collections.Immutable
