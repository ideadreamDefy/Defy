for i in [1,2,3,4]:
    print(i)

# for line in open('hello.py'):
#     print(line)
    
# f = open("hello.py")

# while True:
#     line = f.readLine()
#     if not line:break
#     print(line.upper())

# L = [1,2,3,4,5,6,7,8,9]
# I = iter(L)
# while I.next():
#     print(I.next());

# L = [1,2,3]

# for X in L:
#     print(X**2)

# I = iter(L)
# while True:
#     try:
#         X = next(I)
#     except StopIteration:
#         break
#     print(X**2)

D ={'a':1,'b':2,'c':3}
for key in D.keys():
    print(key,D[key])

iterA = iter(D)

for key in D:
    print(key,D[key])


print([x+y for x in 'abc' for y in 'lmn'])    

R = range(10)
print(R)

print(list(R))



M = map(abs,(-1,0,1))

# print(M)
# print(next(M))

# while True:
#     try:
#         X = next(M)
#     except StopIteration:
#         break
#     print(X)

Z = zip((1,2,3),(10,20,30))
print(Z)

print(list(Z))

for pair in Z:print(pair)

filter(bool,['spam','','ni'])

# print(boimpoerol)

print(list(filter(bool,['spam','','ni'])))

def times(x,y):
    return x*y

print(times(2,4))

def intersect(seq1,seq2):
    res = []
    for x in seq1:
        if x in seq2:
            res.append(x)
    return res

print(intersect([1,2,3,4,5],[1,2,3,4,5,6,7,8]))
print(intersect((1,2,3,4),{3,4,5,6,7,8}))
print(intersect({1,2,3,4},[2,3,4,5,6,7,8]))
