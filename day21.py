from aocUtils import loadInputLines

def parseData():
    data = {}
    allIngredients = []
    for line in lines:
        parts = line[:-1].split(' (contains ')
        ingredients = parts[0].split(' ')
        allIngredients.append(ingredients)
        allergens = parts[1].split(', ')
        for a in allergens:
            if a not in data:
                data[a] = []
            data[a].append(ingredients[:])
    return data, allIngredients

def part1():
    data, allIngredients = parseData()
    changed = True
    while changed:
        changed = False
        for d in data:
            res = set(data[d][0]).intersection(*data[d])
            if len(res) == 1:
                changed = True
                unique = res.pop()
                for a in data:
                    for list in data[a]:
                        if unique in list:
                            list.remove(unique)
                data[d] = [unique]
    for d in data:
        for line in allIngredients:
            if data[d][0] in line:
                line.remove(data[d][0])
    return sum(len(line) for line in allIngredients)

def part2():
    data, _ = parseData()
    changed = True
    while changed:
        changed = False
        for d in data:
            res = set(data[d][0]).intersection(*data[d])
            if len(res) == 1:
                changed = True
                unique = res.pop()
                for a in data:
                    for list in data[a]:
                        if unique in list:
                            list.remove(unique)
                data[d] = [unique]
    ordered = []
    for d in data:
        ordered.append((d, data[d][0]))
    ordered.sort()
    return ''.join(i[1] +',' for i in ordered)[:-1]

lines = loadInputLines(2020, 21)

print(part1())
print(part2())