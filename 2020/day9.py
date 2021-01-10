from aocUtils import loadInputLines, printT
import re

lines = loadInputLines(2020, 9)
nums = [int(line) for line in lines]

def part1():
    for i in range(25, len(nums)-25):
        valid = False
        for x in range(i-25, i-1):
            for y in range(x+1, i):
                if nums[x] + nums[y] == nums[i]:
                    valid =True
        if valid == False:
            return nums[i]

def part2():
    for i in range(0, len(nums)-1):
        sum = 0
        m1 = None
        m2 = None
        for j in range(i, len(nums)):
            sum += nums[j]
            if m1 == None:
                m1 = nums[j]
            else:
                m1 = min(nums[j], m1)
            if m2 == None:
                m2 = nums[j]
            else:
                m2 = max(nums[j], m2)
            if sum == 1930745883:
                return m1 + m2

printT(part1())
printT(part2())
