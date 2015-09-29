//
//  FuncPointer.cpp
//  Learning
//
//  Created by Coco on 15/9/29.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "FuncPointer.h"

void FuncPointer::func2()
{
    cout<<"func2() called ..."<<endl;
}

void FuncPointer::func()
{
    void (*fp)();
//    fp = func2;
//    (*fp)();
//    void (*fp2)() = func2;
//    (*fp2)();
}