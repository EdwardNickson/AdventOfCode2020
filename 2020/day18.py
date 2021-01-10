from aocUtils import loadInputLines, printT

lines = [line.replace(' ','') for line in loadInputLines(2020, 18)]

def evaluate(expression: str):
    
    s = expression.find('(')
    if s >= 0:
        count = 1
        f = s
        while True:
            f += 1
            if expression[f] == '(':
                count += 1
            elif expression[f] == ')':
                count -= 1
            if count == 0:
                break
        e = ''
        if s > 0:
            e += expression[:s]
        e += str(evaluate(expression[s+1:f]))
        if f+1 < len(expression):
            e += expression[f+1:]
        # if s ==0:
        #     res = evaluate(expression[s+1:f])
        # else:
        #     res = evaluate(expression[:s-1])
        #     if expression[s-1] == '*':
        #         res *= evaluate(expression[s+1:f])
        #     else:
        #         res += evaluate(expression[s+1:f])
        # if f+1 < len(expression):
        #     if expression[f+1] == '*':
        #         res *= evaluate(expression[f+2:])
        #     else:
        #         res += evaluate(expression[f+2:])
        return evaluate(e)
    res = None
    num = ''
    operator = None
    while True:
        s = expression.find('+')
        if s == -1:
            break
        l = s-1
        while l >= 0 and expression[l].isdigit():
            l -= 1
        lower = int(expression[l+1:s])
        u = s+1
        while u < len(expression) and expression[u].isdigit():
            u += 1
        upper = int(expression[s+1:u])
        expression = expression[:l+1] + str(lower + upper) + expression[u:]
    t = [int(k) for k in expression.split('*')]
    r = 1
    for v in t:
        r *= v
    return r
    for i in range(0, len(expression)):
        if expression[i].isdigit():
            num += expression[i]
            if i == len(expression)-1:
                if operator == None:
                    res = int(num)
                elif operator == '*':
                    res *= int(num)
                else:
                    res += int(num)
        else:
            if res == None:
                res = int(num)
            else:
                if operator == '*':
                    res *= int(num)
                else:
                    res += int(num)
            operator = expression[i]
            num = ''
    return res
            
        

def part1():
    return sum(evaluate(line) for line in lines)

def part2():
    return

printT(part1())
printT(part2())