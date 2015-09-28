//
//  StringOperation.h
//  Learning
//
//  Created by Coco on 15/9/15.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#ifndef __Learning__StringOperation__
#define __Learning__StringOperation__

#include <stdio.h>

#include <string>
#include <iostream>

#define GET_ARRAY_LEN(array,len){len = (sizeof(array) / sizeof(array[0]));}


using namespace std;

class StringOperation{
public:
    virtual char* operatorString(char* str);

};

#endif /* defined(__Learning__StringOperation__) */
