from fileUtils import loadInputLines

lines = loadInputLines(2020, 3)

def part1():
    count = 0
    x = 0
    for line in lines:
        if line[x] == '#':
            count += 1
        x = (x + 3) % len(line)
    return count

def part2():
    m = 1
    for l in [(1,1), (3,1), (5,1), (7,1) ,(1,2)]:
        count = 0
        x = 0
        for i in range(0, len(lines), l[1]):
            if lines[i][x] == '#':
                count += 1
            x = (x + l[0]) % len(lines[i])
        m = m * count
    return m

print(part1())
print(part2())