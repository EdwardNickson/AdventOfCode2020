from aocUtils import loadInputLines, printT

lines = loadInputLines(2020, 10)
nums = [int(line) for line in lines]
nums = [0] + sorted(nums) + [max(nums)]

def part1():
    diffs = {}
    for i in range(1, len(nums)):
        diff = nums[i]-nums[i-1]
        if diff in diffs:
            diffs[diff] += 1
        else:
            diffs[diff] = 1
    return diffs[1] * diffs[3]

def combinations(numSet, f):
    count = 1
    for i in range(f, len(numSet)-1):
        if numSet[i+1] - numSet[i-1] <= 3:
            count += combinations(numSet[:i] + numSet[i+1:], i)
    return count

def part2():
    l = [0]
    c = 1
    for i in range(1, len(nums)):
        l.append(nums[i])
        if nums[i] - nums[i-1] >= 3:
            c *= combinations(l, 1)
            l = [nums[i]]
    if len(l) > 1:
        c *= combinations(l, 1)
    return c

printT(part1())
printT(part2())
