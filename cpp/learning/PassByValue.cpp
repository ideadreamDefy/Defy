//
//  PassByValue.cpp
//  Learning
//
//  Created by Coco on 15/9/8.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "PassByValue.h"

void PassByValue::f(int pet){
    cout<<"pet id number: "<<pet<<endl;
}

void PassByValue::show(){
    int i,j,k;
//    cout<<"f():"<<(long)&f<<endl;
    cout<<"dog:"<<(long)&dog<<endl;
    cout<<"cat:"<<(long)&(cat)<<endl;
    cout<<"bird:"<<(long)&(bird)<<endl;
    cout<<"fish:"<<(long)&(fish)<<endl;
    cout<<"i:"<<(long)&(i)<<endl;
    cout<<"j:"<<(long)&(j)<<endl;
    cout<<"k:"<<(long)&(k)<<endl;
    

}


