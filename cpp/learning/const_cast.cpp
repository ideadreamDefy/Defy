//
//  const_cast.cpp
//  Learning
//
//  Created by Coco on 15/9/9.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "const_cast.h"

void ConstCast::show(){
    const int i = 0;
    int* j = (int*)&i;
    volatile int k = 0;
    int *u = const_cast<int*>(&k);

}

void print(X* x){
    for(int i = 0;i<sz;i++){
        cout<<x->a[i]<<' '<<endl;
        cout<<"x->a["<<i<<"] = "<<x->a[i]<<endl;
    }
    cout<<endl<<"----------------------------------------"<<endl;
}

void ConstCast::show2(){
    X x;
    print(&x);
    int *xp = reinterpret_cast<int*>(&x);
    
    for (int *i = xp;i < xp + sz;i++){
        *i = 0;
    }
    
    print(reinterpret_cast<X*>(xp));
    
    print(&x);
}