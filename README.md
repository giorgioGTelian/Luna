# Luna Programming Language 🌙
Welcome to Luna, the programming language of the future! Luna aims to bring simplicity, elegance, and performance to developers across the globe. Whether you're building a simple script or a complex system, Luna is here to light your path.
<p align="center">
<img align="center" alt="img" src="luna.png"/>
 </p>

## Features
* <strong>Elegant Syntax:</strong> Write clean and understandable code with Luna's intuitive syntax. it will resample the italian language <br>
* <strong>Performance:</strong>  Luna is designed with performance in mind, allowing you to write efficient applications.<br>
* <strong>Cross-Platform:</strong>  Write once, run anywhere. Luna is compatible with major platforms.<br>
* <strong>Strongly Typed:</strong>  Catch errors before they become bugs with Luna's robust type system.<br>
* <strong>Extensible:</strong>  Easily integrate Luna with other systems and languages.<br>
* <strong>Imperative Paradigm:</strong>Luna follows an imperative programming style, making it intuitive for developers familiar with languages like C, Java, and Python.<br>

## Quick Start
```stampa("Hello, Luna!")```
### To run a Luna program:

bash  <br>
```$ luna run my_program.luna```

## Installation
TBD: Installation instructions go here.

## Documentation
Dive deep into Luna's features, syntax, and standard library by visiting our official documentation.

## Contributing
We welcome contributions from the community! Check out our contribution guidelines to get started.

## example
```
struttura Person {
    argomento name
    argomento experience
    argomento is_developer }
fine entita;

inserisci your_name;
inserisci your_experience_in_years;
inserisci do_you_like_programming;

person = nuova Person [your_name your_experience_in_years do_you_like_programming == "si"];
stampa person;

se ( person :: is_developer) {

    person_name = person :: name;
    stampa "hey " + person_name + "!";

    experience = person :: experience

   } se (experience > 0)  {
        started_in = 2022 - experience;
        stampa "you had started your career in " + started_in;
}
 
```
## Roadmap updated to 02/09/2023
 + Implement basic I/O functions <br>
 + Design and implement the standard library <br>
 + Optimize the compiler for better performance <br>
 + Extend platform support <br>
 +  Write the Luna compiler using Java and integrate testing with Maven. <br>
## License
Luna is open source and licensed under the MIT License.

