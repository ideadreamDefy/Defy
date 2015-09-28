//
//  FloatingAsBinary.h
//  Learning
//
//  Created by Coco on 15/9/10.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#ifndef __Learning__FloatingAsBinary__
#define __Learning__FloatingAsBinary__

#include <stdio.h>
#include "printBinary.h"
#include <iostream>
#include <cstdlib>

using namespace std;

typedef struct{
    char c;
    short s;
    int i;
    long l;
    float f;
    double d;
    long double ld;
}Primitives;

class FloatingAsBinary{
public:
    virtual void show();

};

#endif /* defined(__Learning__FloatingAsBinary__) */
