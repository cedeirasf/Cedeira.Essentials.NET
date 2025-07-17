def to_uppercase(text):
    return text.upper()

def reverse_string(text):
    return text[::-1]

def is_palindrome(text):
    text = text.lower().replace(" ", "")
    return text == text[::-1]
