//
//  printBinary.cpp
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "printBinary.h"

void printBinary::printB(const unsigned char val){
    for (int i = 7;i >= 0;i--){
//        cout<<"val & (1<<i)======"<<(val & (1<<i));
        if (val & (1<<i)){
            cout<<"1";
        }else{
            cout<<"0";
        }
//        cout<<endl;
    }
}