from aocUtils import loadInputLines

lines = loadInputLines(2020, 5)
def part1():
    seats = []
    for line in lines:
        row = line[:7]
        aisle = line[7:]
        row = int(row.replace('F','0').replace('B','1'), 2)
        aisle = int(aisle.replace('L', '0').replace('R', '1'), 2)
        seats.append(row*8 + aisle)
    return max(seats)

def part2():
    passes = {}
    for line in lines:
        row = line[:7]
        aisle = line[7:]
        row = int(row.replace('F','0').replace('B','1'), 2)
        aisle = int(aisle.replace('L', '0').replace('R', '1'), 2)
        passes[row*8 + aisle] = 1
    for i in range(0, 127*8 + 8):
        if i not in passes and i-1 in passes and i+1 in passes:
            return i

print(part1())
print(part2())