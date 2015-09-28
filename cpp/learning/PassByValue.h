//
//  PassByValue.h
//  Learning
//
//  Created by Coco on 15/9/8.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#ifndef __Learning__PassByValue__
#define __Learning__PassByValue__

#include <stdio.h>

#include <iostream>

using namespace std;

class PassByValue{
private:
    int dog,cat,bird,fish;
public:
    virtual void f(int a);
    virtual void show();
};

#endif /* defined(__Learning__PassByValue__) */
