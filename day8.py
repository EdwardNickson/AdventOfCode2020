from aocUtils import loadInputLines, printT
import re

def runBoot(ops):
    acc = 0
    i = 0
    opsRun = set()
    while True:
        if i in opsRun:
            return (acc, 'infinite loop')
        if i == len(ops):
            return (acc, 'terminated')
        op, arg = ops[i].split(' ')
        opsRun.add(i)
        if op == 'nop':
            i += 1
        elif op == 'acc':
            acc += int(arg)
            i += 1
        elif op == 'jmp':
            i += int(arg)

lines = loadInputLines(2020, 8)

def part1():
    return runBoot(lines)[0]

def part2():
    for i in range(0, len(lines)):
        newLines = lines[:]
        if newLines[i][:3] == 'jmp':
            newLines[i] = 'nop' + lines[i][3:]
        elif newLines[i][:3] == 'nop':
            newLines[i] = 'jmp' + lines[i][3:]
        result = runBoot(newLines)
        if result[1] == 'terminated':
            return result[0]

printT(part1())
printT(part2())