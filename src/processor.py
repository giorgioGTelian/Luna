from parser import parse
from utils import generate_assembly

def process_file(file_path):
    """Reads the source file, tokenizes it, parses it, and generates assembly."""
    with open(file_path, 'r') as file:
        source_code = file.read()

    # Step 1: Parse the source code to generate AST
    ast = parse(source_code)

    # Step 2: Generate assembly code from the AST
    assembly_code = generate_assembly(ast)

    # Step 3: Write the assembly code to an output file
    with open("output.asm", 'w') as out_file:
        out_file.write(assembly_code)

    print("Assembly code successfully generated in output.asm")
