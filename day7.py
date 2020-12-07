from aocUtils import loadInputLines, printT
import re

lines = loadInputLines(2020, 7)

bags = {}
for line in lines:
    outerBag = re.search(r'([a-z|\s]+)s contain', line).group(1)
    innerBags = re.findall(r'([0-9]+?)\s(.+?bag)', line)
    bags[outerBag] = innerBags

printT('Bags Parsed')

def containsShinyBag(innerBags):
    for bag in innerBags:
        if bag[1] == 'shiny gold bag' or containsShinyBag(bags[bag[1]]):
            return True
    return False

def part1():
    return sum(containsShinyBag(innerBags) for innerBags in bags.values())

def bagCount(innerBags):
    return sum(int(bag[0]) + int(bag[0]) * bagCount(bags[bag[1]]) for bag in innerBags)

def part2():
    return bagCount(bags['shiny gold bag'])

printT(part1())
printT(part2())