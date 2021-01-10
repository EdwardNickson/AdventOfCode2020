from aocUtils import loadInputLines, printT
import re

lines = loadInputLines(2020, 16)
i = 0
fields = {}
while lines[i] != '':
    m = re.search(r'(.*): ([0-9]+)-([0-9]+) or ([0-9]+)-([0-9]+)', lines[i])
    fields[m.group(1)] = (int(m.group(2)), int(m.group(3)), int(m.group(4)), int(m.group(5)))
    i += 1
i += 2
myTicket = [int(v) for v in lines[i].split(',')]
i += 3
tickets = []
while i < len(lines):
    tickets.append([int(v) for v in lines[i].split(',')])
    i += 1

def validValueForField(field, value):
    return (value >= field[0] and value <= field[1]) or (value >= field[2] and value <= field[3] )

def validValueForAnyField(value):
    for field in fields:
        if validValueForField(fields[field], value):
            return True
    return False

def part1():
    errorRate = 0
    for ticket in tickets:
        errorRate += sum(v for v in ticket if not validValueForAnyField(v)) 
    return errorRate

def part2():
    validTickets = []
    fieldColumns = {}
    for ticket in tickets:
        if sum(v for v in ticket if not validValueForAnyField(v)) == 0:
            validTickets.append(ticket)
    for field in fields:
        fieldColumns[field] = []
        for i in range(0, len(myTicket)):
            fieldValidForCol = True
            for ticket in validTickets:
                fieldValidForCol = fieldValidForCol and validValueForField(fields[field], ticket[i])
                if not fieldValidForCol:
                    break
            if fieldValidForCol:
                fieldColumns[field].append(i)
    modified = True
    while modified:
        modified = False
        for field in fieldColumns:
            if len(fieldColumns[field]) == 1:
                for otherField in fieldColumns:
                    if otherField != field and fieldColumns[otherField].count(fieldColumns[field][0]) > 0:
                        fieldColumns[otherField].remove(fieldColumns[field][0])
                        modified = True
    result =  1
    for key in fieldColumns:
        if key.startswith('departure'):
            result *= myTicket[fieldColumns[key][0]]
    return result

        
printT(part1())
printT(part2())