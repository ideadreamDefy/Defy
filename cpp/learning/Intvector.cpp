//
//  Intvector.cpp
//  Learning
//
//  Created by Coco on 15/9/8.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "Intvector.h"

void Intvector::testVector(){
    std::vector<int> v;
    
    for(int i = 0;i<10;i++){
        v.push_back(i);
    }
    
    for(int i = 0;i<v.size();i++){
        std::cout<<v[i]<<",";
    }
    
    std::cout<<std::endl;
    
    for (int i = 0 ;i<v.size();i++){
        v[i] = v[i]*10;
    }
    
    for (int i = 0;i<v.size();i++){
        std::cout<<v[i]<<std::endl;
    }
    
    std::cout<<std::endl;
    
}
