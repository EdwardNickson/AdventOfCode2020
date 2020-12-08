from aocUtils import loadInputLines, printT
import re

lines = loadInputLines(2020, 8)
origIns = [line.split(' ') for line in lines]

def runBoot(ins):
    acc = 0
    i = 0
    opsRun = set()
    while True:
        if i in opsRun: #infinite Loop
            return (0, acc)
        if i == len(ins): #terminated correctly
            return (1, acc)
        op = ins[i][0]
        args = ins[i][1:]
        opsRun.add(i)
        if op == 'nop':
            i += 1
        elif op == 'acc':
            acc += int(args[0])
            i += 1
        elif op == 'jmp':
            i += int(args[0])

def part1():
    return runBoot(origIns)[1]

def part2():
    for i in range(0, len(origIns)):
        insCopy = [o[:] for o in origIns]
        if insCopy[i][0] == 'jmp':
            insCopy[i][0] = 'nop'
        elif insCopy[i][0] == 'nop':
            insCopy[i][0] = 'jmp'
        result = runBoot(insCopy)
        if result[0]:
            return result[1]

printT(part1())
printT(part2())
