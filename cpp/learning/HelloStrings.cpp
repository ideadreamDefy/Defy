//
//  HelloStrings.cpp
//  Learning
//
//  Created by Coco on 15/9/8.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "HelloStrings.h"
#include <iostream>
using namespace std;

void HelloStrings::helloStringOutput(){
    string s1,s2;
    string s3 = "hello,world.";
    string s4 = ("i am");
    s2 = "today";
    s1 = s3 + " " + s4;
    s1 += " 8 ";
    cout<<s1+s2+" ! "<<endl;

}