//
//  SimpleStruct.h
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#ifndef __Learning__SimpleStruct__
#define __Learning__SimpleStruct__

#include <stdio.h>

#include <iostream>

struct Structure1{
    char c;
    int i;
    float f;
    double d;
};

typedef struct{
    char c;
    int i;
    float f;
    double d;
}Structure2;

class SimpleStruct{
public:
    virtual void show();
    

};

#endif /* defined(__Learning__SimpleStruct__) */
