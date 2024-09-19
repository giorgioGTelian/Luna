import re

# Token patterns for the custom language
TOKENS = [
    (r'struttura', 'STRUCT'),
    (r'argomento', 'ARG'),
    (r'inserisci', 'INPUT'),
    (r'nuova', 'NEW'),
    (r'stampa', 'PRINT'),
    (r'se', 'IF'),
    (r'fine entita;', 'END_STRUCT'),
    (r'[a-zA-Z_]\w*', 'IDENTIFIER'),
    (r'[0-9]+', 'NUMBER'),
    (r'==', 'EQ'),
    (r'\+', 'PLUS'),
    (r'-', 'MINUS'),
    (r'=', 'ASSIGN'),
    (r'::', 'ACCESS'),
    (r'\(', 'LPAREN'),
    (r'\)', 'RPAREN'),
    (r'{', 'LBRACE'),
    (r'}', 'RBRACE'),
]

def tokenize(code):
    tokens = []
    while code:
        match = None
        for token_regex, token_type in TOKENS:
            regex = re.compile(token_regex)
            match = regex.match(code)
            if match:
                tokens.append((match.group(0), token_type))
                code = code[match.end():]
                break
        if not match:
            raise SyntaxError(f"Unknown token in code: {code}")
    return tokens

def parse(code):
    tokens = tokenize(code)
    ast = []
    current_struct = None

    for token, token_type in tokens:
        if token_type == 'STRUCT':
            current_struct = {'type': 'struct', 'name': '', 'fields': []}
            ast.append(current_struct)
        elif token_type == 'ARG':
            field_name = tokens[tokens.index((token, token_type)) + 1][0]
            current_struct['fields'].append(field_name)
        elif token_type == 'PRINT':
            message = tokens[tokens.index((token, token_type)) + 1][0]
            ast.append({'type': 'print', 'message': message})

    return ast
