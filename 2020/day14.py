from aocUtils import loadInputLines, printT

lines = loadInputLines(2020, 14)

def applyMask(mask, s):
    res = ''
    for i in range(0, len(s)):
        if mask[i] == 'X':
            res += s[i]
        else:
            res += mask[i]
    return res

def part1():
    mem = {}
    for line in lines:
        if line.startswith('mask'):
            mask = line[7:]
        else:
            l = line.split('=')
            val = int(l[1].strip())
            l = l[0].split('[')
            addr = l[1].split(']')[0]
            mem[addr] = applyMask(mask, bin(val).replace("0b", "").zfill(36))
    return sum(int(b, 2) for b in mem.values())

def applyMask2(mask, s):
    res = ''
    for i in range(0, len(s)):
        if mask[i] == '0':
            res += s[i]
        else:
            res += mask[i]
    return res

def getAddresses(val: str):
    for i in range(0, len(val)):
        if val[i] == 'X':
            v1 = val[:i] + '0' + val[i+1:]
            v2 = val[:i] + '1' + val[i+1:]
            return getAddresses(v1) + getAddresses(v2)
    return [val]

def part2():
    mem = {}
    for line in lines:
        if line.startswith('mask'):
            mask = line[7:]
        else:
            l = line.split('=')
            val = int(l[1].strip())
            l = l[0].split('[')
            addr = l[1].split(']')[0]
            addies = getAddresses(applyMask2(mask, bin(int(addr)).replace("0b", "").zfill(36)))
            for a in addies:
                mem[int(a, 2)] = bin(val).replace("0b", "").zfill(36)
    return sum(int(b, 2) for b in mem.values())
        
printT(part1())
printT(part2())