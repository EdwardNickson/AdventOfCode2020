from aocUtils import loadInputLines, printT
import re

lines = loadInputLines(2020, 9)
origOps = [(l[0], list(map(int, l[1:]))) for l in (line.split(' ') for line in lines)]

def runBoot(ops):
    acc = 0
    i = 0
    opsRun = set()
    while True:
        if i in opsRun:
            return (acc, 'infinite loop')
        if i == len(ops):
            return (acc, 'terminated')
        op, args = ops[i]
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

# def part1():
#     return runBoot(origOps)[0]

# def part2():
#     for i in range(0, len(origOps)):
#         newOps = [(o[0], o[1][:]) for o in origOps]
#         if newOps[i][0] == 'jmp':
#             newOps[i] = ('nop', newOps[i][1])
#         elif newOps[i][0] == 'nop':
#             newOps[i] = ('jmp', newOps[i][1])
#         result = runBoot(newOps)
#         if result[1] == 'terminated':
#             return result[0]