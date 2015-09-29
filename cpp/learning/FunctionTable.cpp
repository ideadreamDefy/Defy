//
//  FunctionTable.cpp
//  Learning
//
//  Created by Coco on 15/9/29.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "FunctionTable.h"


void FunctionTable::show(){
    while(1){
        cout<<"press a key from 'a' to 'g'""or q to quit"<<endl;
        char c,cr;
        cin.get(c);
        cin.get(cr);
        
        if(c == 'q')
        {
            break;
        }
        
        if( c <'a' || c >'g'){
            continue;
        }
        cout<<"c-'a'"<<(c-'a')<<endl;
//        (*func_table[c-'a'])();
    }
    
    
}