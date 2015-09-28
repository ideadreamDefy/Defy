//
//  const_cast.h
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#ifndef __Learning__const_cast__
#define __Learning__const_cast__

#include <stdio.h>

#include <iostream>

using namespace std;

const int sz = 100;

struct X{
    int a[sz];
};

class ConstCast{
public:
    virtual void show();
    
    virtual void show2();
};

#endif /* defined(__Learning__const_cast__) */
