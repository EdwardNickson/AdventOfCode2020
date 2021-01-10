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
    t = 1
    step = 1
    for i, bus in enumerate(id for id in lines[1].split(',')):
        if (bus == 'x'):
            continue
        bus = int(bus)
        while((t+i)%bus):
            t += step
        step *= bus
    return t
        
printT(part1())
printT(part2())