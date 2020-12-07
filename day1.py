from aocUtils import loadInputLines

lines = loadInputLines(2020, 1)

def part1():
    intLines = list(map(int, lines))
    for i in range(0, len(intLines)-1):
        for j in range(i+1, len(intLines)):
            if intLines[i] + intLines[j] == 2020:
                return intLines[i] * intLines[j]

def part2():
    intLines = list(map(int, lines))
    for i in range(0, len(intLines)-2):
        for j in range(i+1, len(intLines)-1):
            for k in range(j+1, len(intLines)):
                if intLines[i] + intLines[j] + intLines[k]== 2020:
                    return intLines[i] * intLines[j] * intLines[k]

print(part1())
print(part2())