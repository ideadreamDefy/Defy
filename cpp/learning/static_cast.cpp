
//
//  static_cast.cpp
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "static_cast.h"
void staticCast::func(int a){

}


void staticCast::show(){
    int i = 0x7fff;
    long l;
    float f;
    
    l = i;
    f = i;
    
    l = static_cast<long>(i);
    f = static_cast<float>(i);
    
    i = l;
    i = f;
    
    i = static_cast<int>(l);
    i = static_cast<int>(f);
    
    char c = static_cast<char>(i);
    
    void *vp = &i;
    
    float *fp = (float*) vp;
    fp = static_cast<float*>(vp);
    
    double d = 0.0;
    int x = d;
    x = static_cast<int>(d);
    func(d);
    func(static_cast<int>(d));
}