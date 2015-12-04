/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：globalTool
 * 简    述：
 * 创建时间：2015年10月22日14:42:09
 * 创 建 人：=== Coco ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using System.Collections;

namespace BoTing.FourBull
{
    public class FourBullGlobalTool
    {
        public static void moveAction(GameObject mainObj, GameObject referenceObj, float passTime)
        {
            //移动物体到参照物物体
            if (mainObj != null && referenceObj != null && referenceObj.transform != null && passTime != 0)
            {
                LeanTween.moveLocal(mainObj, referenceObj.transform.localPosition, passTime).setEase(LeanTweenType.easeInQuad);
            }
        }

        public static Sprite GetSpriteByFilePath(string fileName)
        {
            return Resources.Load(fileName, typeof(Sprite)) as Sprite;
        }


        
    }
}