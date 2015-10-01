#coding:gbk
##-*- coding : gbk -*-
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
match = re.match('Hello[ \t]*(.*)world','Hello  Python world')
print("match.group(1)=========",match.group(1))

L = [123,'spam',1.23]
print(len(L))
print(L[0])
print(L[:-1])
print(L+[4,5,6])

M = ['bb','aa','cc']
M.sort()

print(M)
M.reverse()
print(M)



