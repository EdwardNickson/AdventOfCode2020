from aocUtils import loadInputLines, printT
import time

lines = loadInputLines(2020, 17)

def activeNeighbours(space, point):
    return sum([space.get(neighbour, 0) for neighbour in getNeighbours(point)])

def getNeighbours(p):
    neighbours = []
    neighbours.append([])
    for i in range(0, len(p)):
        newNeighbours = []
        for n in neighbours:
            for x in range(p[i]-1, p[i]+2):
                newNeighbours.append(n[:] + [x])
        neighbours = newNeighbours
    return [t for t in [tuple(n) for n in neighbours] if t != p]

def AddEmptyNeighbours(space):
    for point in [point for point in space]:
        for neighbour in getNeighbours(point):
            if neighbour not in space:
                space[neighbour] = 0

def runSpaceTimeSimulation(dimensions):
    space = {}
    for y in range(0, len(lines)):
        for x in range(0, len(lines[0])):
            if lines[y][x] == '#':
                space[(x, y) + (0,) * (dimensions-2)] = 1
    AddEmptyNeighbours(space)
    for _ in range(0, 6):
        newSpace = {}
        for point in space:
            active = space.get(point, 0)
            count = activeNeighbours(space, point)
            if active == 1:
                if count != 2 and count != 3:
                    active = 0
            elif active == 0:
                if count == 3:
                    active = 1
            if active == 1:
                newSpace[point] = 1
        AddEmptyNeighbours(newSpace)
        space = newSpace
    return sum([space.get(point, 0) for point in space])

def part1():
    return runSpaceTimeSimulation(3)

def part2():
    return runSpaceTimeSimulation(4)

printT(part1())
printT(part2())