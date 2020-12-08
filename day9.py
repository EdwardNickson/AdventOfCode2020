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
    return

def part2():
    return

printT(part1())
printT(part2())
