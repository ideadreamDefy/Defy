
//
//  voidPointer.cpp
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "voidPointer.h"

void voidPointer::opeatorPointer(){
    int i = 99;
    void* vp = &i;
    *((int*)vp) = 3;
    cout<<" i= "<<i<<endl;    
}
