using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FurnitureExcelItem
{
    public int GetSupportEffect(FurniType type)
    {
        if(type == supportFurni1)
        {
            return supportDelta1;
        }
        else if (type == supportFurni2)
        {
            return supportDelta2;
        }
        else
        {
            return 0;
        }
    }

    public string GetSupportEffectDesc
    {
        get
        {
            string tempDesc = "";

            if (supportFurni1 != FurniType.None)
            {
                tempDesc += supportFurni1.ToString();
                if (supportDelta1 > 0)
                {
                    tempDesc += "+" + supportDelta1.ToString();

                }
                else
                {
                    tempDesc += supportDelta1.ToString();
                }
            }
            tempDesc += " ";


            if (supportFurni2 != FurniType.None)
            {
                tempDesc += supportFurni2.ToString();
                if (supportDelta2 > 0)
                {
                    tempDesc += "+" + supportDelta2.ToString();

                }
                else
                {
                    tempDesc += supportDelta2.ToString();
                }
            }

            return tempDesc;

        }
    }

}
