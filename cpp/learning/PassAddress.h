//
//  PassAddress.h
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#ifndef __Learning__PassAddress__
#define __Learning__PassAddress__

#include <stdio.h>

#include <iostream>

using namespace std;

class PassAddress{
public:
    virtual void showPointer();
    
    virtual void f(int a);
    
    virtual void f1(int *p);
    
    virtual void f2(int &r);
private:

};

#endif /* defined(__Learning__PassAddress__) */
