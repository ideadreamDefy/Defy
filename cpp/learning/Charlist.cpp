//
//  Charlist.cpp
//  Learning
//
//  Created by Coco on 15/9/8.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "Charlist.h"

void Charlist::outPut(){
    for (int i = 0; i < 128;i++){
        if (i != 26)
            cout<<"value:"<<i<<" charcter: "<<char(i)<<endl;
    }
}