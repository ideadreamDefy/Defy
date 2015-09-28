//
//  PassAddress.cpp
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "PassAddress.h"
void PassAddress::f(int a){
    cout<<"a = "<<a<<endl;
    a = 5;
    cout<<"a = "<<a<<endl;
}

void PassAddress::f1(int *p){
    cout<<"p = "<<p<<endl;
    cout<<"*p = "<<*p<<endl;
    *p = 5;
    cout<<"p = "<<p<<endl;
}

void PassAddress::f2(int &r){
    cout<<"r = "<<r<<endl;
    cout<<"*r = "<<&r<<endl;
    r = 5;
    cout<<"r = "<<r<<endl;
}

void PassAddress::showPointer(){
    int x = 47;
    cout<<"x = "<<x<<endl;
    f(x);
    cout<<"x = "<<x<<endl;
    cout<<"*************************************************"<<endl;
    int y = 47;
    cout<<"y = "<<y<<endl;
    cout<<"&y = "<<&y<<endl;
    f1(&y);
    cout<<"y = "<<y<<endl;
    cout<<"&y = "<<&y<<endl;
    cout<<"*************************************************"<<endl;
    int r = 57;
    cout<<"r = "<<r<<endl;
    cout<<"&r = "<<&r<<endl;
    f2(r);
    cout<<"r = "<<r<<endl;
    cout<<"&r = "<<&r<<endl;
    
    
}
