from fileUtils import loadInputLines
import re

lines = loadInputLines(2020, 7)

bags = {}
for line in lines:
    outerBag = re.search(r'([a-z|\s]+)s contain', line).group(1)
    innerBags = re.findall(r'([0-9]+?)\s(.+?bag)', line)
    bags[outerBag] = innerBags

def containsShinyBag(bagContents):
    if len(bagContents) == 0:
        return False
    return max(bag[1] == 'shiny gold bag' or containsShinyBag(bags[bag[1]]) for bag in bagContents)

def part1():
    return sum(containsShinyBag(bag) for bag in bags.values())

def bagCount(bagContents):
    return sum(int(bag[0]) + int(bag[0]) * bagCount(bags[bag[1]]) for bag in bagContents)

def part2():
    return bagCount(bags['shiny gold bag'])

print(part1())
print(part2())