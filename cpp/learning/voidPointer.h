//
//  voidPointer.h
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#ifndef __Learning__voidPointer__
#define __Learning__voidPointer__

#include <stdio.h>

#include <iostream>

using namespace std;

class voidPointer{
public:
    void *vp;
    char c;
    int i;
    float f;
    double d;
    
    virtual void opeatorPointer();

};


#endif /* defined(__Learning__voidPointer__) */
