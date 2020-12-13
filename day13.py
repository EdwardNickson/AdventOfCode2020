from aocUtils import loadInputLines, printT

lines = loadInputLines(2020, 13)

def part1():
    est = int(lines[0])
    buses = (int(ids) for ids in lines[1].split(',') if ids != 'x')
    minDiff = None
    res = 0
    for b in buses:
        diff = b - est % b
        if (minDiff == None or diff < minDiff):
            minDiff = diff
            res = minDiff * b
    return res

def part2():
    buses = [ids for ids in lines[1].split(',')]
    bs = {}
    for i in range(0, len(buses)):
        if buses[i] == 'x':
            buses[i] = None
        else:
            bs[i] = int(buses[i])
    s = 100000000000000
    while True:
        if s%bs[0] == 0:
            break
        s += 1
    found = False
    while not found:
        found = True
        for i in bs:
            if (s+i)%bs[i] != 0:
                found = False
                break
        if found:
            break
        s += bs[0]
        
    return s
        


printT(part1())
printT(part2())