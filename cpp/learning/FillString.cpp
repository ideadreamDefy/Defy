//
//  FillString.cpp
//  Learning
//
//  Created by Coco on 15/9/8.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "FillString.h"
//读取文件输出文件到控制台（console）
void FillString::fillOperation(){
//    ifstream in("main.cpp");
    ifstream in("/Users/coco/Desktop/Learning/Learning/main.cpp");
    string s,line;

    while(getline(in,line)){
        s += line+"\n";
    }
    
    cout<<s;
}