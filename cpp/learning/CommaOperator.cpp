
//
//  CommaOperator.cpp
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "CommaOperator.h"

void CommaOperator::show(){
    int a = 0,b = 1,c = 2, d = 3,e = 4;
    a = (b++,c++,d++,e++);
    cout<<" a = "<<a<<endl;
    (a = b++),c++,d++,e++;
    cout<<" a = "<<a<<endl;
}