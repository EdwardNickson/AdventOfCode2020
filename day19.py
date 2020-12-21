from aocUtils import loadInputLines, printT
import re

lines = loadInputLines(2020, 19)

def addLine(rules, line):
    parts = line.split('|')
    p1 = parts[0].split(':')
    rule = p1[0]
    matches = [r for r in p1[1].split(' ') if r != '']
    matches2 = []
    if len(parts) > 1:
        matches2 = [r for r in parts[1].split(' ') if r != '']
    rules[rule] = (matches, matches2)

i = 0
rules = {}
while lines[i] != '':   
    addLine(rules, lines[i])

messages = lines[i+1:]

def evaluate(rule):
    if rule[0][0] == '"a"' or rule[0][0] == '"b"':
        return rule[0][0][1:2]
    r = '('
    for next in rule[0]:
        r += evaluate(rules[next])
    r += ')'
    if len(rule[1]) >0:
        r = '(' + r + '|' + '('
        for next in rule[1]:
            r += evaluate(rules[next])
        r += '))'
    return r

def part1():
    test = '\\b' + evaluate(rules['0']) + '\\b'
    count = 0
    for m in messages:
        match = re.search(test, m)
        if match != None:
            count += 1
    return count

def evaluate2(rule):
    r = ''
    if rule[0][0] == '42':
        if len(rule[0]) == 1:
            r = '(' + evaluate2(rules['42']) +')+'
        else:
            f2 = evaluate2(rules['42'])
            t1 = evaluate2(rules['31'])
            r = '('
            for i in range(1, 6):
                r += '(' + f2 * i + t1 * i + ')|'
            r = r[:-1] + ')'
    else:        
        if rule[0][0] == '"a"' or rule[0][0] == '"b"':
            return rule[0][0][1:2]
        r = '('
        for next in rule[0]:
            r += evaluate2(rules[next])
        r += ')'
        if len(rule[1]) >0:
            r = '(' + r + '|' + '('
            for next in rule[1]:
                r += evaluate2(rules[next])
            r += '))'
    return r

def part2():
    test = '\\b' + evaluate2(rules['0']) + '\\b'
    count = 0
    for m in messages:
        match = re.search(test, m)
        if match != None:
            count += 1
    return count

printT(part1())

rules = {}
i = 0
while lines[i] != '':    
    if lines[i].startswith('8:'):
        lines[i] = '8: 42 '     
    if lines[i].startswith('11:'):
        lines[i] = '11: 42 31'        
    parts = lines[i].split('|')
    p1 = parts[0].split(':')
    rule = p1[0]
    matches = [r for r in p1[1].split(' ') if r != '']
    matches2 = []
    if len(parts) > 1:
        matches2 = [r for r in parts[1].split(' ') if r != '']
    rules[rule] = (matches, matches2)
    i += 1
printT(part2())