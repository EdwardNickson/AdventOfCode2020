from fileUtils import loadInputLines

#TODO - Learn Regex

lines = loadInputLines(2020, 2)
def part1():
    c = 0
    for line in lines:
        a = line.split(':')
        l = a[0].split(' ')[1]
        num = a[0].split(' ')[0].split('-')
        count = a[1].count(l)
        if count >= int(num[0]) and count <= int(num[1]):
            c += 1
    return c

def part2():
    c = 0
    for line in lines:
        a = line.split(':')
        l = a[0].split(' ')[1]
        num = a[0].split(' ')[0].split('-')
        t = 0
        if (a[1][int(num[0])] == l):
            t += 1
        if (a[1][int(num[1])] == l):
            t += 1
        if t == 1:
            c += 1
    return c

print(part1())
print(part2())