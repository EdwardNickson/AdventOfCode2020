from aocUtils import loadInputLines
import re

lines = loadInputLines(2020, 4)

def part1():
    vals = {}
    count = 0
    for val in ['byr:','iyr:','eyr:','hgt:','hcl:','pid:','ecl:']:
        vals[val] = False

    for i in range(0, len(lines)):
        line = lines[i]
        for val in vals:
            if line.count(val) > 0:
                vals[val] = True
        if line == '' or i == len(lines)-1:
            if list(vals.values()).count(False) == 0:
                count += 1
            for val in vals:
                vals[val] = False
    return count

def part2():
    vals = {}
    count = 0
    for val in ['byr:','iyr:','eyr:','hgt:','hcl:','pid:','ecl:']:
        vals[val] = False

    for i in range(0, len(lines)):
        line = lines[i]
        for val in vals:
            if val == 'byr:':
                m = re.search(r'byr:([0-9]{4})\b', line)
                if m != None:
                    vals[val] = int(m.group(1)) >= 1920 and int(m.group(1)) <= 2002
            if val == 'iyr:':
                m = re.search(r'iyr:([0-9]{4})\b', line)
                if m != None:
                    vals[val] = int(m.group(1)) >= 2010 and int(m.group(1)) <= 2020
            if val == 'eyr:':
                m = re.search(r'eyr:([0-9]{4})\b', line)
                if m != None:
                    vals[val] = int(m.group(1)) >= 2020 and int(m.group(1)) <= 2030
            if val == 'hgt:':
                m = re.search(r'hgt:([0-9]{2})in\b', line)
                if m != None:
                    vals[val] = int(m.group(1)) >= 59 and int(m.group(1)) <= 76
                else:
                    m = re.search(r'hgt:([0-9]{3})cm\b', line)
                    if m != None:
                        vals[val] = int(m.group(1)) >= 150 and int(m.group(1)) <= 193
            if val == 'hcl:':
                m = re.search(r'hcl:#[0-9, a-f]{6}\b', line)
                if m != None:
                    vals[val] = True
            if val == 'ecl:':
                m = re.search(r'ecl:(amb|blu|brn|gry|grn|hzl|oth)\b', line)
                if m != None:
                    vals[val] = True
            if val == 'pid:':
                m = re.search(r'pid:[0-9]{9}\b', line)
                if m != None:
                    vals[val] = True
        
        if line == '' or i == len(lines)-1:
            if list(vals.values()).count(False) == 0:
                count += 1
            for val in vals:
                vals[val] = False
    return count

print(part1())
print(part2())