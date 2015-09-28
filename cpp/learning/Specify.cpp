//
//  Specify.cpp
//  Learning
//
//  Created by Coco on 15/9/8.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "Specify.h"



void Specify::weiOutput(){
    char c;
    unsigned char cu;
    int i;
    unsigned int iu;
    short int is;
    short iis;
    unsigned short int isu;
    unsigned short iisu;
    long int il;
    long iil;
    unsigned long int ilu;
    unsigned long iilu;
    float f;
    double d;
    long double ld;
    std::cout<<"\n char = "<<sizeof(c)
        <<"\n unsigned char = "<<sizeof(cu)
        <<"\n unsigned int = "<<sizeof(iu)
        <<"\n int = "<<sizeof(i)
        <<"\n short = "<<sizeof(iis)
        <<"\n short int = "<<sizeof(is)
        <<"\n unsigned short int = "<<sizeof(isu)
        <<"\n unsigned short = "<<sizeof(iisu)
        <<"\n long int = "<<sizeof(il)
        <<"\n long = "<<sizeof(iil)
        <<"\n unsigned long int = "<<sizeof(ilu)
        <<"\n unsigned long = "<<sizeof(iilu)
        <<"\n long double = "<<sizeof(ld)
        <<"\n double= "<<sizeof(d)
        <<"\n float = "<<sizeof(f);
    
    

}
