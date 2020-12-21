from aocUtils import loadInputLines, printT
import math

def flipVertical(tile):
    copy = []
    for i in range(len(tile)-1, -1, -1):
        copy.append(tile[i][:])
    return copy

def Rotate90(tile):
    copy = [list(line) for line in tile]
    for i in range(0, len(tile)):
        for j in range(0, len(tile)):
            copy[j][-1-i] = tile[i][j]
    copy = [''.join(line) for line in copy]
    return copy

def getTilePermutations(tile):
    ps = []
    rot = tile
    ps.append(rot)
    for _ in range(0, 3):
        rot = Rotate90(rot)
        ps.append(rot)
    rot = flipVertical(tile)
    ps.append(rot)
    for _ in range(0, 3):
        rot = Rotate90(rot)
        ps.append(rot)
    return ps


def matches(prev, new, border):
    if border == 'eastwest':
        for i in range(0, len(prev)):
            if prev[i][len(prev)-1] != new[i][0]:
                return False
    else:
        if prev[len(prev)-1] != new[0]:
            return False
    return True

def validForPrevPoints(map, point, perm):
    valid = True
    if (point[0]-1, point[1]) in map:
        valid = valid and matches(map[(point[0]-1, point[1])][1], perm, 'eastwest')
    if (point[0], point[1]-1) in map:
        valid = valid and matches(map[(point[0], point[1]-1)][1], perm, 'northsouth')
    return valid

def addTilesToMap(map, tiles, width, point):
    if len(tiles) == 0:
        return True
    nextX = point[0] + 1
    nextY = point[1]
    if nextX == width:
        nextX = 0
        nextY = nextY + 1
    for i in range(0, len(tiles)):
        for perm in tiles[i][1]:
            if validForPrevPoints(map, point, perm):
                map[point] = (tiles[i][0], perm)
                valid = addTilesToMap(map, tiles[:i] + tiles[i+1:], width, (nextX, nextY))
                if valid:
                    return True
                else:
                    del map[point]

def containsMonster(map, point):
    monster = []
    monster.append(point)
    monster.append((point[0]+1, point[1]+1))
    monster.append((point[0]+4, point[1]+1))
    monster.append((point[0]+5, point[1]))
    monster.append((point[0]+6, point[1]))
    monster.append((point[0]+7, point[1]+1))
    monster.append((point[0]+10, point[1]+1))
    monster.append((point[0]+11, point[1]))
    monster.append((point[0]+12, point[1]))
    monster.append((point[0]+13, point[1]+1))
    monster.append((point[0]+16, point[1]+1))
    monster.append((point[0]+17, point[1]))
    monster.append((point[0]+18, point[1]-1))
    monster.append((point[0]+18, point[1]))
    monster.append((point[0]+19, point[1]))

    for part in monster:
        if map.get(part, '.') == '.':
            return False
    return True

def getInner(tile):
    res = []
    for i in range(1, len(tile) - 1):
        res.append(tile[i][1:len(tile)-1])
    return res

def getSolution(tiles):
    map = {}
    addTilesToMap(map, tiles, int(math.sqrt(len(tiles))), (0, 0))
    return map

def part1(map):
    width = int(math.sqrt(len(map)))
    return map[(0,0)][0] * map[(0, width-1)][0] * map[(width-1, 0)][0] * map[(width-1, width-1)][0]

def part2(map):
    
    width = int(math.sqrt(len(map)))
    fullMap = []
    for i in range(0, width):
        for j in range(0, width):
            inner = getInner(map[(i,j)][1])
            if i == 0:
                fullMap += inner
            else:
                for k in range(0, len(inner)):
                    fullMap[j*len(inner)+k] += inner[k]
    monsters = 0
    total = 0
    for p in getTilePermutations(fullMap):
        map = {}
        for i in range(0, len(p)):
            for j in range(0, len(p)):
                map[(i,j)] = p[i][j]
        monstersInner = 0
        for point in map:
            if containsMonster(map, point):
                monstersInner += 1
        if monsters > monstersInner:
            total = 0
            for point in map:
                if map.get(point, '.') != '.':
                    total += 1
        monsters = max(monsters, monstersInner)
         
    total = total - monsters * 15
    
    return total

lines = loadInputLines(2020, 20)

tiles = {}
tile = ''
for line in lines:
    if line.startswith('Tile'):
        tile = line.split(' ')[1][:-1]
        tiles[tile] = []
    elif len(line) > 0:
        tiles[tile].append(line)
tiles = [(int(tile), getTilePermutations(tiles[tile])) for tile in tiles]

printT('Finished parsing input')
map = getSolution(tiles)
printT(part1(map))
printT(part2(map))