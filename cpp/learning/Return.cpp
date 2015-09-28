//
//  Return.cpp
//  Learning
//
//  Created by Coco on 15/9/8.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "Return.h"

char Return::cfunc(int i){
    if( i == 0 ){
        return 'a';
    }
    
    if( i == 1 ){
        return 'g';
    }
    
    if( i == 5 ){
        return 'z';
    }
    return 'c';
}

void Return::operatorCfunc(){
    cout<<"type an integer:";
    int val;
    cin>>val;
    cout<<cfunc(val)<<endl;
}