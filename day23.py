from aocUtils import loadInputLines, printT

input = '389125467'

def part1():
    i = 0
    cups = list(int(i) for i in input)
    for x in range(0, 100):
        removed = cups[1:4]
        cups = cups[:1] + cups[4:]
        for j in range(cups[0]-1, cups[0]-5, -1):
            if j in removed:
                continue
            if j == 0:
                i = cups.index(max(cups))
                break
            i = cups.index(j)
            break
        cups = cups[:i+1] + removed + cups[i+1:]
        cups = cups[1:] + cups[:1]
    one = cups.index(1)
    return ''.join(str(c) for c in (cups[one+1:] + cups[:one]))

def part2():
    cups = [int(i) for i in input]
    cupCount = 1000000
    turns = 10000000

    link = [0]*(cupCount+1)
    for i in range(0, cupCount):
        if i < len(cups) - 1:
            link[cups[i]] = cups[i+1]
        elif i == len(cups) -1:
            link[cups[i]] = len(cups) +1
        elif i == cupCount -1:
            link[cupCount] = cups[0]
        else:
            link[i+1] = i+2
    current = cups[0]

    for _ in range(0, turns):
        first = link[current]
        second = link[first]
        third = link[second]
        removed = [first, second, third]
        target = 0
        for j in range(current-1, current-5, -1):
            if j in removed:
                continue
            if j == 0:
                target = cupCount
                while target in removed:
                    target -= 1
                break
            target = j
            break
        link[current] = link[third]
        link[third] = link[target]
        link[target] = first
        current = link[current]

    return link[1] * link[link[1]]

printT(part1())
printT(part2())