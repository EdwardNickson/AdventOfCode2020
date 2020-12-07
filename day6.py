from aocUtils import loadInputLines

lines = loadInputLines(2020, 6)

def part1():
    t = 0
    letters = set()
    for line in lines:
        for char in line:
            letters.add(char)
        if line == '':
            t += len(letters)
            letters.clear()
    t += len(letters)
    return t

def part2():
    t = 0
    letters = {}
    people = 0
    for line in lines:
        if line == '':
            t += sum(i == people for i  in letters.values())
            letters.clear()
            people = 0
        else:
            people += 1
            for char in line:
                if  char in letters:
                    letters[char] += 1
                else:
                    letters[char] = 1
    t += sum(i == people for i  in letters.values())
    return t

print(part1())
print(part2())