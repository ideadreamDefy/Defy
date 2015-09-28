//
//  getWords.cpp
//  Learning
//
//  Created by Coco on 15/9/8.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include "getWords.h"
#include <iostream>

void getWords::readCpp(){
    vector<string> words;
    ifstream in("/Users/coco/Desktop/Learning/Learning/main.cpp");
    string word;
    while(in>>word){
        words.push_back(word);
        for (int i = 0 ;i<words.size();i++){
            std::cout<<words[i]<<endl;
        }
    }

}

