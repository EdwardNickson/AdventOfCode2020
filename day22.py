from aocUtils import loadInputLines, printT

lines = loadInputLines(2020, 22)
deckSize = int((len(lines)-3)/2)

def scoreHand(hand):
    count = 0
    for i in range(0, len(hand)):
        count += hand[i] * (len(hand) - i)
    return count

def part1():
    i = 0
    p1 = [int(n) for n in lines[1:deckSize+1]]
    p2 = [int(n) for n in lines[deckSize+3:]]
    while len(p1) > 0 and len(p2) > 0:
        i += 1
        if p1[0] > p2[0]:
            p1.append(p1[0])
            p1.append(p2[0])
        else:
            p2.append(p2[0])
            p2.append(p1[0])
        p1.remove(p1[0])
        p2.remove(p2[0])
    return scoreHand(p1) if len(p1) > 0 else scoreHand(p2)

def playGame(p1, p2):
    history = set()
    while len(p1) > 0 and len(p2) > 0:
        currentState = (''.join([str(c) for c in p1]), ''.join([str(c) for c in p2]))
        if currentState in history:
            return [1],[]
        else:
            history.add(currentState)
            
        c1 = p1.pop(0)
        c2 = p2.pop(0)
        winner = None
        
        if c1 <= len(p1) and c2 <= len(p2):
            p1Sub, _ = playGame(p1[:c1], p2[:c2])
            winner = 1 if len(p1Sub) > 0 else 0
        else:
            winner = 1 if c1 > c2 else 0

        if winner == 1:
            p1.append(c1)
            p1.append(c2)
        else:
            p2.append(c2)
            p2.append(c1)
    return p1, p2

def part2():
    p1 = [int(n) for n in lines[1:deckSize+1]]
    p2 = [int(n) for n in lines[deckSize+3:]]
    p1, p2 = playGame(p1, p2)
    return max(scoreHand(p1), scoreHand(p2))

printT(part1())
printT(part2())