# Norwegian organization numbers (organisasjonsnummer orgnummer)
This project contains a library for computations on norwegian organization numbers in many programming languages. The library works exactly the same in all implementations:

 * Java
 * C
 * C++
 * JavaScript
 * C#
 * PHP
 * Python
 * Visual Basic

Norwegian organization numbers are called "organiasjonsnummer" or "orgnummer" in norwegian.

The library has been made with [progsbase](https://www.progsbase.com), a tool for creating rot-resistant programs that are translatable to many programming languages.

The library can also be browsed and tested on the [progsbase repository's orgnummer page](https://repo.progsbase.com/repoviewer/no.inductive.libraries/orgnummer/0.1.0).

![progsbase logo](/docs/images/progsbase-logo.png)

For more information about progsbase, check out: [progsbase.com](https://www.progsbase.com).

## Usage

The library has the same interface in all programming languages.

### Validate
`boolean erGyldigOrgNummer(char [] orgnummer)`

The function takes a string as input, containing a norwegian organization number.

The output is a boolean, true if the number is valid and false if not.
