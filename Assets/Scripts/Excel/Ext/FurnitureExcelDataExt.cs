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

}
