//
//  PointerLeader.cpp
//  Learning
//
//  Created by Coco on 15/9/28.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "PointerLeader.h"

#include <iostream>

using namespace std;

void PointerLeader::show(){
    int i = 0;
    int &ref = i;
    ref++;
    cout<<"i = "<<i<<endl;
    cout<<"ref = "<<ref<<endl;
    int j = 20;
    ref = j;
    ref++;
    
    cout<<"i = "<<i<<endl;
    cout<<"ref = "<<ref<<endl;
    cout<<"i ="<<i<<endl;
}
