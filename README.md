# Ef6Uml

### WIP

Outputs the script required to generate a class diagram with [yUML](http://yuml.me/) of all the entities and their relationships from an Entity Framework 6 DbContext.

### Design Notes

To allow for future customisation of the input (initially Ef6) or output (initially yUML) the application will be designed to work in 2 stages.

1) Build a UML model from the input.

2) Output the UML model in the desired format.

### License

[Apache License, Version 2.0](https://raw.githubusercontent.com/matthewrwilton/Ef6Uml/master/LICENSE)
