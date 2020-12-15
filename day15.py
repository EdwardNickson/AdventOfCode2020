from aocUtils import loadInputLines, printT

def containsCycle(numbers):
    if len(numbers) < 4:
        return False
    for i in range(0, int(len(numbers)/2)):
        if numbers[i] != numbers[-i-1]:
            return False
    return True

def playTheGame(turn):
    numbers = [int(line) for line in loadInputLines(2020, 15)[0].split(',')]
    spoken = {}
    i = 0
    last = 0
    turn = turn - 1
    while(i < len(numbers)):
        last = numbers[i]
        if last not in spoken:
            spoken[last] = (i, 0, 1)
        else:
            spoken[last] = (i, spoken[last][0], spoken[last][2] + 1)
        i += 1
    while True:
        last = 0 if spoken[last][2] == 1 else spoken[last][0] - spoken[last][1]
        if last not in spoken:
            spoken[last] = (i, 0, 1)
        else:
            spoken[last] = (i, spoken[last][0], spoken[last][2] + 1)
        if i == turn:
            return last
        i += 1

def part1():
    return playTheGame(2020)

def part2():
    return playTheGame(30000000)
        
printT(part1())
printT(part2())