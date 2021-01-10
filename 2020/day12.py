from aocUtils import loadInputLines, printT

commands = [(line[0], int(line[1:])) for line in loadInputLines(2020, 12)]

def part1():
    v = 90
    pos = (0,0)
    for c in commands:
        if c[0] == 'L':
            v = (v - c[1]) % 360
        elif c[0] == 'R':
            v = (v + c[1]) % 360
        elif c[0] == 'N' or (c[0] == 'F' and v == 0):
            pos = (pos[0], pos[1] + c[1])
        elif c[0] == 'E' or (c[0] == 'F' and v == 90):
            pos = (pos[0] + c[1], pos[1])
        elif c[0] == 'S' or (c[0] == 'F' and v == 180):
            pos = (pos[0], pos[1] - c[1])
        elif c[0] == 'W' or (c[0] == 'F' and v == 270):
            pos = (pos[0] - c[1], pos[1])
    return abs(pos[0]) + abs(pos[1])

def part2():
    pos = (0,0)
    waypoint = (10, 1)
    for c in commands:
        if c[0] == 'L':
            for _ in range(0, int(c[1]/90)):
                waypoint = (waypoint[1] * -1, waypoint[0])
        elif c[0] == 'R':
            for _ in range(0, int(c[1]/90)):
                waypoint = (waypoint[1], waypoint[0] * -1)
        elif c[0] == 'F':
            pos = (pos[0] + waypoint[0]*c[1], pos[1] + waypoint[1]*c[1])
        elif c[0] == 'N':
            waypoint = (waypoint[0], waypoint[1] + c[1])
        elif c[0] == 'E':
            waypoint = (waypoint[0] + c[1], waypoint[1])
        elif c[0] == 'S':
            waypoint = (waypoint[0], waypoint[1] - c[1])
        elif c[0] == 'W':
            waypoint = (waypoint[0] - c[1], waypoint[1])
    return abs(pos[0]) + abs(pos[1])

printT(part1())
printT(part2())