# def func():
#     x = 4
#     action = (lambda n: x**n)
#     return action

# x = func()
# print(x(2))
# print(x(5))
# print(x())

def func1():
    x = 4
    action = (lambda n,x=x:x**n)
    return action

y = func1()
print(y(3))

def makeActions():
    # print("--------------------------------------------")
    # print(lambda z:5**z))
    # print("--------------------------------------------")
    
    acts = []
    for i in range(5):
        print(i)
        acts.append(lambda x:i**x)
        print(acts)
        # print("--------------------------------------------")
    return acts

acts = makeActions()

print(acts[0](2))
print(acts[1](2))
print(acts[2](2))
print(acts[3](2))
print(acts[4](2))

def appendX():
    acts2 = []
    for i in range(5):
        print(i)
        acts2.append(i)
    return acts2

print(appendX())

def makeActions2():
    acts = []
    for i in range(5):
        acts.append(lambda x, i = i:i**x )
    return acts

acts3 = makeActions2()
print(acts3[0](2))
print(acts3[1](2))
print(acts3[2](2))
print(acts3[3](2))
print(acts3[4](2))

def f1():
    x = 99
    def f2():
        def f3():
            print(x)
        f3()
    f2()

f1()


def tester(start):
    state = start
    def nested(label):
        print(label,state)
    return nested

F = tester(0)
print(F('spam'))

# print(F(0)('spam'))

def tester1(start):
    state = start
    def nested(label):
        nonlocal state
        print(label,state)
        state += 1
    return nested

F1 = tester1(0)
print(F1("spam"))


