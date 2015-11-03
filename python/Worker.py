class Worker:
    def _init_(self,name,pay):
        self.name = name
        self.pay = pay
    def lastName(self):
        return self.name.split()[-1]
    def giveRaise(self,percent):
        self.pay*=(1.0+percent)

    # def text(self):
    #     bob = Worker('Bob smith',50000)
    #     bob.lastNam

# workTest = Worker('Sue Jones',60000)
# print(workTest.text())
    
