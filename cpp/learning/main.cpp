//
//  main.cpp
//  Learning
//
//  Created by Coco on 15/9/7.
//  Copyright (c) 2015年 Coco. All rights reserved.
//

#include <iostream>
#include "NumConv.h"
#include "HelloStrings.h"
#include "FillString.h"
#include "getWords.h"
#include "Return.h"
#include "Charlist.h"
#include "Specify.h"
#include "PassByValue.h"
#include "PassAddress.h"
#include "voidPointer.h"
#include "Mathops.h"
#include "printBinary.h"
#include "CommaOperator.h"
#include "static_cast.h"
#include "const_cast.h"
#include "Enum.h"
#include "FloatingAsBinary.h"
#include "StringOperation.h"
#include "PointerLeader.h"
#include "FuncPointer.h"
#include "FunctionTable.h"

#include <string>
#include <vector>

using namespace std;

//结构体
typedef struct vectorrect
{
    int classId;
    string className;
}VectorRect;
//向量数据结构
vector <VectorRect> vec;

//工具方法
//系统统一推出入口
void outSystem(){
    exit(0);
}

//系统信息打印
void outputInfo(){
    int length = (int)vec.size();
    if (length > 0) {
        vector<VectorRect>::iterator it;
//        cout<<"vec.begin()= "<<vec.begin()->className<<endl;
        for (int i = 0;i<length;i++){
            cout<<"序号 "<<vec.at(i).classId<<" --------------- "<< vec.at(i).className <<endl;
        }
        
    }else{
        cout<<"系统信息暂缺！请及时补充";
        outSystem();
    }
}


void searchClassEntry(){
    int id;
    string classname;
    cout<<"******************************************************"<<endl;
    outputInfo();
    cout<<"请输入你要跑的示例类名"<<endl;
    cout<<"******************************************************"<<endl;
    cin>>id;
    
    
    switch (id) {
        case 0:
            {
                cout<<"谢谢使用再会!";
                exit(0);
            }
        case 1:
            {
                NumConv *numConv = new NumConv();
                numConv->cinPrint();
            }
            break;
        case 2:
            {
                HelloStrings *hello = new HelloStrings();
                hello->helloStringOutput();
                
            }
            break;
        case 3:
            {
                FillString *fill = new FillString();
                fill->fillOperation();
            }
            break;
        case 4:
            {
                getWords *getW = new getWords();
                getW->readCpp();
            }
        case 5:
            {
                Return *ret = new Return();
                ret->operatorCfunc();
            }
            break;
        case 6:
            {
                Charlist *char1 = new Charlist();
                char1->outPut();
            }
            break;
        case 7:
            {
                Specify *specify = new Specify();
                specify->weiOutput();
            }
            break;
        case 8:
            {
                PassByValue *pass = new PassByValue();
                pass->show();
            }
            break;
        case 9:
            {
                PassAddress *pass = new PassAddress();
                pass->showPointer();
            }
            break;
        case 10:
            {
                voidPointer *voidP = new voidPointer();
                voidP->opeatorPointer();
            }
            break;
        case 11:
            {
                Mathops *mathops = new Mathops();
                mathops->show();
            }
            break;
        case 12:
            {
                printBinary *p = new printBinary();
                p->printB(11);
            }
            break;
        case 13:
            {
                CommaOperator *com = new CommaOperator();
                com->show();
            }
            break;
        case 14:
            {
                staticCast *sc = new staticCast();
                sc->show();
            }
            break;
        case 15:
            {
                ConstCast *con = new ConstCast();
//                con->show();
                con->show2();
            }
            break;
        case 16:
            {
                Enum *enum1 = new Enum();
                enum1->show();
                enum1->show2();
                enum1->showArray11();
            }
            break;
        case 17:
            {
                FloatingAsBinary *fa = new FloatingAsBinary();
                fa->show();
            }
            break;
        case 18:
            {
                StringOperation *so = new StringOperation();
                so->operatorString("hellowwwww");
            }
            break;
        case 19:
            {
                PointerLeader *pL = new PointerLeader();
                pL->show();
            }
            break;
        case 20:
            {
                FuncPointer *fP = new FuncPointer();
                fP->func();
            }
            break;
        case 21:
            {
                FunctionTable *fT = new FunctionTable();
                fT->show();
            }
        default:
            {
                cout<<"输入错误请重新输入!";
                searchClassEntry();
            }
            break;
    }
    
}

int main(){
//    初始化数据
    VectorRect rect;
    rect.classId = 1;
    rect.className = "NumConv";
    vec.push_back(rect);
    
    VectorRect rect1;
    rect1.classId = 2;
    rect1.className = "HelloStrings";
    vec.push_back(rect1);
    
    VectorRect rect3;
    rect3.classId = 3;
    rect3.className = "FillString";
    vec.push_back(rect3);
    
    VectorRect rect4;
    rect4.classId = 4;
    rect4.className = "getWords";
    vec.push_back(rect4);
    
    VectorRect rect5;
    rect5.classId = 5;
    rect5.className = "Return";
    vec.push_back(rect5);
    
    VectorRect rect6;
    rect6.classId = 6;
    rect6.className = "Charlist";
    vec.push_back(rect6);
    
    VectorRect rect7;
    rect7.classId = 7;
    rect7.className = "Specify";
    vec.push_back(rect7);
    
    VectorRect rect8;
    rect8.classId = 8;
    rect8.className = "PassByValue";
    vec.push_back(rect8);
    
    VectorRect rect9;
    rect9.classId = 9;
    rect9.className = "PassAddress";
    vec.push_back(rect9);
    
    VectorRect rect10;
    rect10.classId = 10;
    rect10.className = "PassAddress";
    vec.push_back(rect10);
    
    VectorRect rect11;
    rect11.classId = 11;
    rect11.className = "Mathops";
    vec.push_back(rect11);
    
    VectorRect rect12;
    rect12.classId = 12;
    rect12.className = "Mathops";
    vec.push_back(rect12);
    
    VectorRect rect13;
    rect13.classId = 13;
    rect13.className = "CommaOperator";
    vec.push_back(rect13);
    
    VectorRect rect14;
    rect14.classId = 14;
    rect14.className = "staticcast";
    vec.push_back(rect14);
    
    VectorRect rect15;
    rect15.classId = 15;
    rect15.className = "constcast";
    vec.push_back(rect15);
    
    VectorRect rect16;
    rect16.classId = 16;
    rect16.className = "Enum";
    vec.push_back(rect16);
    
    VectorRect rect17;
    rect17.classId = 17;
    rect17.className = "FloatingAsBinary";
    vec.push_back(rect17);
    
    VectorRect rect18;
    rect18.classId = 18;
    rect18.className = "StringOperation";
    vec.push_back(rect18);
    
    VectorRect rect19;
    rect19.classId = 19;
    rect19.className = "PointerLeader";
    vec.push_back(rect19);
    
    VectorRect rect20;
    rect20.classId = 20;
    rect20.className = "FuncPointer";
    vec.push_back(rect20);
    
    VectorRect rect21;
    rect21.classId = 21;
    rect21.className = "FunctionTable";
    vec.push_back(rect21);
    
    searchClassEntry();
}

//int main(int argc,char *argv[]){
//    cout<<"argc = "<<argc<<endl;
//    for (int i = 0;i<argc;i++){
//        cout<< "argv["<<i<<"]= "<<argv[i]<<endl;
//    }
//}
