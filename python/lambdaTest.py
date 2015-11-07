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
 # 只记录最后一次循环i的值
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
    

