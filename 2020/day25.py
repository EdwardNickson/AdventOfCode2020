from aocUtils import loadInputLines, printT

lines = loadInputLines(2020, 25)
cardPublicKey = int(lines[0])
doorPublicKey = int(lines[1])

def getLoopSize(publicKey, subjectNumber):
    value = 1
    loopSize = 0
    while value != publicKey:
        value = (value * subjectNumber) % 20201227
        loopSize += 1
    return loopSize

def transform(subjectNumber, loopSize):
    value = 1
    for _ in range(0, loopSize):
        value = (value * subjectNumber) % 20201227
    return value

def part1():
    cardLoopSize = getLoopSize(cardPublicKey, 7)
    return transform(doorPublicKey, cardLoopSize)

def part2():
    return

assert getLoopSize(5764801, 7) == 8
assert getLoopSize(17807724, 7) == 11
assert transform(17807724, 8) == 14897079
assert transform(5764801, 11) == 14897079

printT(part1())
printT(part2())