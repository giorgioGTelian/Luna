# utils.py

def table_string_from_tokens(tokens, table_name=""):
    """
    Generate a formatted table string from the list of tokens.
    """
    lines = []
    abv, blw, current_line = "", "", ""
    max_width = 240
    separator = "    "

    for i, token in enumerate(tokens):
        if i > 0 and len(current_line) + len(separator) + len(token.Text) < max_width:
            abv += separator
            current_line += separator
            blw += separator

            abv += " " * len(token.Text)
            blw += " " * len(token.Text)
            current_line += token.Text
        else:
            if i > 0:
                lines.extend([abv, current_line, blw, ""])
                abv, blw = "", ""
            abv += " " * len(token.Text)
            blw += " " * len(token.Text)
            current_line = token.Text

    lines.extend([abv, current_line, blw])
    return "\n".join(lines)


def string_from_tokens(tokens, left, right):
    """
    Concatenate text from a list of tokens between specified indices.
    """
    return " ".join([tokens[i].Text for i in range(left, right + 1)])
