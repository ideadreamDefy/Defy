
#coding:gbk
import  Worker
s = "abca"

print("string abcd-------->s[:]",s[:])
print("string abcd-------->s[1:]",s[:])
print("string abcd-------->s[:len(s)]",s[:(len(s))])
print("string abcd-------->s[1:(len(s)-1)]",s[1:(len(s)-1)])

#print("s[1],s[2],s[3],s[-1],s[-2]",s[1],s[2],s[3],s[-1],s[-2])
#find 操作
print("s.find('pa')=======",s.find('pa'))
print("s.find('a')=======",s.find('bc'))

print("s.replace('ab','xyz'========",s.replace('ab','xyz'))
print("s === after replace ======",s)

line = 'aaa,bbb,ccccc,dd'
array = line.split(',')
print("array ======",array)

print("line.isalpha()=======",line.isalpha()) 

lineWord = "aaaaaaa\n\n\n\n\n\n\n"
print("lineWord.isalpha() ======",lineWord.isalpha())

print("lineWord.rstrip()=======",lineWord.rstrip())

print('%s,eggs,and %s' %('spam','SPAM!'))
print('{0},eggs,and {1}'.format("spam","SPAM!"))

S = 'A\nB\tC'
length = len(S)
# print('length=======',length)

for i in range(0,length):
    print(S[i])
else:
    print('for end')
    # print(S[i])

    import re
    match = re.match('Hello[ \t]*(.*)world','Hellodddasd  Pythonasdasdf  world')
    print("match.group(1)=========",match.group(1))
    print("match.group()========+++++++=",match.groups())
    # print("match.group()=======++++++=",match.group(2))
    # print("match.group()=======++++++=",match.group(0))

    match1 = re.match('/(.*)/(.*)/(.*)','/usr/home/lumberjack')
    print('match1.groups()',match1.groups())

    # 序列
    L = [123,'spam',1.23]
    print(len(L))
    print(L[0])
    print(L[:-1])
    print(L+[4,5,6])
    L.append("NI")
    print(L)

    M = ['bb','aa','cc']
    M.sort()
    print(M)
    # 翻转操作
    M.reverse()
    print(M)

    M1 = [
        [1,2,3],
        [4,5,6],
        [7,8,9]
        ]

        print(M1)
        print(M1[1])
        print(M1[1][2])

        col1 = [row[0] for row in M1]
        col2 = [row[1] for row in M1]
        col3 = [row[2] for row in M1 if row[2]%2 == 0 ]
            print(col1,col2,col3)

            diag = [M1[i][i] for i in [0,1,2]]
                print("diag ======",diag)

                doubles = [c*20 for c in 'spam']
                    print('doubles ======',doubles)

                    G = (sum(row) for row in M1)
                        print(next(G))
                        print(next(G))
                        print(next(G))

                        List = list(map(sum,M1))
                        print('List =====',List)

                        List2 = {i:sum(M1[i]) for i in range(3)}
                            print(List2)

                            List3 = [ord(x) for x in 'spaam']
                                print(List3)

                                List4 = {ord(x) for x in 'spaaakmm'}
                                    print(List4)

                                    List5 = {x:ord(x) for x in 'spaamspaam' }
                                        print(List5)


                                        D = {'food':'Spam','quantity':4,'color':'pink','food':10}
                                        D['food'] += 1
                                        print(D['food'])

                                        D1 = {}
                                        D1['name'] = 'Bob'
                                        D1['job'] = 'dev'
                                        D1['age'] = 40

                                        print("D1 ===",D1)

                                        rec = {'name':{'first':'Bob','last':'Smith'},
                                               'job':['dev','mgr'],
                                               'age':40.5}

                                            # print(row[0] for row in rec)
                                        print(rec['name']['first'])
                                        print(rec['job'][-1])
                                        print(rec['job'][0])
                                        print(rec['age'])
                                        rec['job'].append('janitors')
                                        print(rec)

                                        Ks = list(D.keys())
                                        print('Ks========',Ks)

                                        Ks.sort()
                                        print('Ks========',Ks)

                                        for key in Ks:
                                            print(key)
                                            x = 4
                                            while x>0:
                                                print('spam!'*x)
                                                x -= 1

                                                squares = [x ** 2 for x in [1,2,3,4,5,6]]
                                                print(squares)

                                                for x in [1,2,3,4,5]:
                                                    squares.append(x **2)
                                                    print(squares)

                                                    D2 = {'a':1,'c':3,'b':2}
                                                    D2['e'] = 99
                                                    D2['f'] = 100
                                                    print(D2.keys())
                                                    D2.keys().sort()
                                                    print(D2.keys())
                                                    print('f' in D2)

                                                    if not('f' in D2):
                                                        print("miss f")
                                                    else:
                                                        print("have f")

                                                        #元组
                                                        T = (1,2,3,4,5,6,7,8,9,10,21,1,1,1,1)
                                                        print(len(T))

                                                        T+(5,6)
                                                        print(T)

                                                        print(T[0])
                                                        print(T.index(21))
                                                        print(T.index(22) if 22 in T else 0 )
                                                        print(T.count(1) if 1 in T else 0)

                                                        X = set('sopam')
                                                        Y = {'h','h','a','m'}
                                                            print(X&Y)
                                                            print(X|Y)
                                                            print(X-Y)
                                                            print(Y-X)

                                                            
                                                            print({x**2 for x in [1,2,3,4]})
                                                                print(type(D2))
                                                                print(type(L))
                                                                print(type(line))
                                                                print(type(T))



                                                                # workTest = Worker('Sue Jones',60000)
                                                                # print(workTest.text())

                                                                import math
                                                                print(5/2)
                                                                print(5//-2)
                                                                print(math.trunc(5/-2))




