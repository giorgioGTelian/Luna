# processor.py

from utils import table_string_from_tokens, string_from_tokens

def process_tokens(tokens, left, right):
    """
    Process a range of tokens and return a formatted string.
    """
    return string_from_tokens(tokens, left, right)


def create_token_table(tokens, table_name="Tokens Table"):
    """
    Create a formatted table of tokens for output.
    """
    return table_string_from_tokens(tokens, table_name)
