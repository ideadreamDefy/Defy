//
//  FunctionTable.h
//  Learning
//
//  Created by Coco on 15/9/29.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#ifndef __Learning__FunctionTable__
#define __Learning__FunctionTable__

#include <stdio.h>

#include <iostream>

using namespace std;
//
//#define DF(N) void N(){\
//    cout<<"function "#N" called "<<endl;}
//
//DF(a);DF(b);DF(c);DF(d);DF(e);DF(f);DF(g);

class FunctionTable{
public:
    virtual void show();
//    void (*func_table[])() = {a,b,c,d,e,f,g};


};



#endif /* defined(__Learning__FunctionTable__) */
