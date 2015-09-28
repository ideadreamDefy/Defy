//
//  Mathops.cpp
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "Mathops.h"

void Mathops::show(){
    int i,j,k;
    float u,v,w;
    cout<<"enter an integer:";
    cin>>j;
    cout<<"enter anthor integer:";
    cin>>k;
    
    PRINT("j",j);

    PRINT("k",k);
    
    i = i + k;PRINT("i + k",i);
    i = i - k;PRINT("i - k",i);
    i = k / j;PRINT("k / j",i);
    i = k * j;PRINT("k * j",i);
    i = k % j;PRINT("k % j",i);
    
}