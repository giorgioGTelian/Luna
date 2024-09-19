# config.py

# Example configuration options for the compiler
CONFIG = {
    "target_architecture": "x86",
    "max_identifier_length": 32,
    "output_format": "assembly"
}

def get_config(option):
    """Helper function to get configuration values."""
    return CONFIG.get(option, None)
