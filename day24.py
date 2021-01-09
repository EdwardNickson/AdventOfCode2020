from aocUtils import loadInputLines, printT

lines = loadInputLines(2020, 24)

directions = {}
directions['se'] = (-1, 1)
directions['sw'] = (-1, -1)
directions['ne'] = (1, 1)
directions['nw'] = (1, -1)
directions['e'] = (0, 2)
directions['w'] = (0, -2)

paths = []
for line in lines:
    i = 0
    path = []
    while i < len(line):
        directionWidth = 2 if (line[i] == 's' or line[i] == 'n') else 1
        path.append(directions[line[i:i+directionWidth]])
        i += directionWidth
    paths.append(path)

def getInitalLayout():
    tiles = {}
    for path in paths:
        current = (0,0)
        for step in path:
            current = (current[0] + step[0], current[1] + step[1])
        if current in tiles:
            tiles[current] = 'W' if tiles[current] == 'B' else 'B'
        else:
            tiles[current] = 'B'
    return tiles

def blackNeighbours(tiles, point):
    return sum([t =='B' for t in [tiles.get(neighbour, 'W') for neighbour in getNeighbours(point)]])

def getNeighbours(p):
    return [(p[0] + d[0], p[1] + d[1]) for d in directions.values()]

def addWhiteNeighbours(tiles):
    for point in [point for point in tiles]:
        for neighbour in getNeighbours(point):
            if neighbour not in tiles:
                tiles[neighbour] = 'W'

def part1():
    tiles = getInitalLayout()
    return sum(t == 'B' for t in tiles.values())

def part2():
    tiles = getInitalLayout()
    addWhiteNeighbours(tiles)
    for _ in range(0, 100):
        newTiles = {}
        for point in tiles:
            colour = tiles.get(point, 'W')
            count = blackNeighbours(tiles, point)
            if colour == 'B':
                if count == 0 or count > 2:
                    colour = 'W'
            else:
                if count == 2:
                    colour = 'B'
            if colour == 'B':
                newTiles[point] = 'B'
        addWhiteNeighbours(newTiles)
        tiles = newTiles
    return sum([tiles.get(point, 'W') == 'B' for point in tiles])

printT(part1())
printT(part2())