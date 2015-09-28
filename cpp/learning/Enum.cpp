
//
//  Enum.cpp
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "Enum.h"

void Enum::show(){
    ShapeType shape = circle;
    
    switch (shape) {
        case circle:
            
            break;
        case square:
            
        case rectangle:
            
        default:
            break;
    }
}

void Enum::show2(){
    cout<<"sizeof(Packed) = "<<sizeof(Packed)<<endl;
    Packed x;
    x.i = 'C';
    cout<<x.i<<endl;
    x.d = 3.14159;
    cout<<x.d<<endl;
}


void Enum::showArray11(){
    int a[10];
    cout<<"a = "<<a<<endl;
    cout<<"&a[0] = "<<&a[0]<<endl;
}