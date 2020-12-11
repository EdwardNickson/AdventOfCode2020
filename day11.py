from aocUtils import loadInputLines, printT
from collections import Counter 

lines = loadInputLines(2020, 11)

def adj(lines, i, j):
    res = []
    if i > 0:
        if j > 0:
            res.append(lines[i-1][j-1])
        res.append(lines[i-1][j])
        if j+1 < len(lines[i]):
            res.append(lines[i-1][j+1])
    if i+1 < len(lines):
        if j > 0:
            res.append(lines[i+1][j-1])
        res.append(lines[i+1][j])
        if j+1 <= len(lines[i])-1:
            res.append(lines[i+1][j+1])
    if j > 0:
        res.append(lines[i][j-1])
    if j+1 <= len(lines[i])-1:
        res.append(lines[i][j+1])
    return res


def adj2(lines, i, j):
    res = []
    for i2 in range(i+1, len(lines)):
        if lines[i2][j] != '.':
            res.append(lines[i2][j])
            break
    for i2 in range(i-1, -1, -1):
        if lines[i2][j] != '.':
            res.append(lines[i2][j])
            break
    for j2 in range(j+1, len(lines[0])):
        if lines[i][j2] != '.':
            res.append(lines[i][j2])
            break
    for j2 in range(j-1, -1, -1):
        if lines[i][j2] != '.':
            res.append(lines[i][j2])
            break
    for x in range(1, max(len(lines), len(lines[0]))):
        if i+x >= len(lines) or j+x >= len(lines[0]):
            break
        if lines[i+x][j+x] != '.':
            res.append(lines[i+x][j+x])
            break
    for x in range(1, max(len(lines), len(lines[0]))):
        if i-x < 0 or j-x < 0:
            break
        if lines[i-x][j-x] != '.':
            res.append(lines[i-x][j-x])
            break
    for x in range(1, max(len(lines), len(lines[0]))):
        if i+x >= len(lines) or j-x < 0:
            break
        if lines[i+x][j-x] != '.':
            res.append(lines[i+x][j-x])
            break
    for x in range(1, max(len(lines), len(lines[0]))):
        if i-x < 0 or j+x >= len(lines[0]):
            break
        if lines[i-x][j+x] != '.':
            res.append(lines[i-x][j+x])
            break
    return res

def part1():
    changed = True
    prevLines = [line[:] for line in lines]
    while changed:
        copy = [line[:] for line in prevLines]
        changed = False
        for i in range(0, len(lines)):
            for j in range(0, len(lines[0])):
                if prevLines[i][j] == 'L':
                    count = Counter(adj(prevLines, i, j))
                    if count['#'] == 0:
                        copy[i] = copy[i][:j] + '#' + copy[i][j+1:]
                        changed = True
                elif  prevLines[i][j] == '#':
                    count = Counter(adj(prevLines, i, j))
                    if count['#'] >= 4:
                        copy[i] = copy[i][:j] + 'L' + copy[i][j+1:]
                        changed = True
        prevLines = [line[:] for line in copy]
    c = 0
    for line in prevLines:
        count = Counter(line)
        c += count['#']
    return c

        

def part2():
    changed = True
    prevLines = [line[:] for line in lines]
    while changed:
        copy = [line[:] for line in prevLines]
        changed = False
        for i in range(0, len(lines)):
            for j in range(0, len(lines[0])):
                if prevLines[i][j] == 'L':
                    count = Counter(adj2(prevLines, i, j))
                    if count['#'] == 0:
                        copy[i] = copy[i][:j] + '#' + copy[i][j+1:]
                        changed = True
                elif  prevLines[i][j] == '#':
                    count = Counter(adj2(prevLines, i, j))
                    if count['#'] >= 5:
                        copy[i] = copy[i][:j] + 'L' + copy[i][j+1:]
                        changed = True
        prevLines = [line[:] for line in copy]
    c = 0
    for line in prevLines:
        count = Counter(line)
        c += count['#']
    return c

printT(part1())
printT(part2())
