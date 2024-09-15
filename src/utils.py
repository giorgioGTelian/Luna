def generate_assembly(ast):
    """Generate x86 assembly code from the given AST."""
    assembly_lines = []
    
    # Simple example for generating assembly from a structure
    for node in ast:
        if node['type'] == 'struct':
            assembly_lines.append(f"; Structure: {node['name']}")
            for field in node['fields']:
                assembly_lines.append(f"    {field}: db 0")
        elif node['type'] == 'print':
            assembly_lines.append(f"    ; Print {node['message']}")
            assembly_lines.append(f"    mov eax, {node['message']}")
            assembly_lines.append(f"    call print")

    # Add some minimal assembly header/footer
    assembly_code = "\n".join(assembly_lines)
    return assembly_code
