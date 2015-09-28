//
//  StringOperation.cpp
//  Learning
//
//  Created by Coco on 15/9/15.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "StringOperation.h"
using namespace std;

char* StringOperation::operatorString(char *str){
    int len;
    bool isHave = false;
    GET_ARRAY_LEN(str,len);
    for(int i = 0;i<len;i++){
        if (str[i] == ':'){
            cout<<"str["<<i<<"]= "<<str[i]<<endl;
        }
    }
    return str;
}