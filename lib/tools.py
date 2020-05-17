"""lib/tools:

A module containing some tools used throughout the game that aren't large enough for their own libraries.
This will be added to throughout the game."""


def parseInt(string='', *args):
    output = ''
    for p in range(0, len(string)):
        if string[p].isnumeric():
            output += str(string[p])
        elif (string[p] == '.') and ('.' not in output):
            output += '.'
            pass
        pass
    return int(float(output))

def parseFloat(string='', *args):
    output = ''
    for p in range(0, len(string)):
        if string[p].isnumeric():
            output += str(string[p])
        elif (string[p] == '.') and ('.' not in output):
            output += '.'
            pass
        pass
    return float(output)

def parseRawInt(string='', *args):
    output = ''
    for p in range(0, len(string)):
        if string[p].isnumeric():
            output += str(string[p])
        elif (string[p] == '.') and ('.' not in output):
            output += ''
            pass
        pass
    return int(float(output))
