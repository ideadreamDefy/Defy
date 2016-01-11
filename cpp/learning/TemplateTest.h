//
//  TemplateTest.h
//  
//
//  Created by Coco on 15/11/18.
//
//

#ifndef ____TemplateTest__
#define ____TemplateTest__

#include <stdio.h>
#include <iostream>

using namespace std;

template <typename T,typename T1>
  
class TemplateTest{
public:
    //二分查找
    int binSearch(T arr[],int first,int last,T &target){
        //定义中间位置标志位
        int mid;
        //定义中间数组
        T midValue;
        //记录最后一个位置的tag
        int origLast = last;
        //循环比较大小
        while(first < last){
            mid = (last+ first)/2;
            
            midValue = arr[mid];
            if(target == midValue){
                return mid;
            }else if(target < midValue){
                last = mid;
            }else if(target > midValue){
                first = mid + 1;
            }
        }
        return origLast;
    };
    //选择排序
    void selectionSort(T arr[],int n,T1 a = 0){
        int smallIndex;
        int pass,j;
        T temp;
        for(pass = 0;pass < n - 1;pass ++)
        {
            smallIndex = pass;
            for ( j = pass +1 ;j<n ;j++)
            {
                if(arr[smallIndex]>arr[j]){
                    smallIndex = j;
                    if(smallIndex != pass)
                    {
                        temp = arr[pass];
                        arr[pass] = arr[smallIndex];
                        arr[smallIndex] = temp;
                    }
                }
            }
        }
    }
};

#endif /* defined(____TemplateTest__) */
