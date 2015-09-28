//
//  Enum.h
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#ifndef __Learning__Enum__
#define __Learning__Enum__

#include <stdio.h>

#include <iostream>

using namespace std;

enum ShapeType{
    circle,
    square,
    rectangle
};

union Packed{
    char i;
    short j;
    int k;
    long l;
    float f;
    double d;
};

class Enum{
public:
    virtual void show();
    virtual void show2();
    virtual void showArray11();
};

#endif /* defined(__Learning__Enum__) */
