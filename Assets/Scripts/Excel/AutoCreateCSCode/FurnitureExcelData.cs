/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class FurnitureExcelItem : ExcelItemBase
{
	public string name;
	public FurniType furnitureType;
	public string desc;
	public int height;
	public int width;
	public int coinDelta;
	public int energyDelta;
	public int specialDelta;
	public int timeDelta;
	public FurniType supportFurni1;
	public int supportDelta1;
	public FurniType supportFurni2;
	public int supportDelta2;
	public float xoffset;
	public float yoffset;
	public bool canBuy;
	public int price;
	public string iconUrl;
}

[CreateAssetMenu(fileName = "FurnitureExcelData", menuName = "Excel To ScriptableObject/Create FurnitureExcelData", order = 1)]
public partial class FurnitureExcelData : ExcelDataBase<FurnitureExcelItem>
{
}

#if UNITY_EDITOR
public class FurnitureAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		FurnitureExcelItem[] items = new FurnitureExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new FurnitureExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].furnitureType = (FurniType) Enum.Parse(typeof(FurniType), allItemValueRowList[i]["furnitureType"], true);
			items[i].desc = allItemValueRowList[i]["desc"];
			items[i].height = Convert.ToInt32(allItemValueRowList[i]["height"]);
			items[i].width = Convert.ToInt32(allItemValueRowList[i]["width"]);
			items[i].coinDelta = Convert.ToInt32(allItemValueRowList[i]["coinDelta"]);
			items[i].energyDelta = Convert.ToInt32(allItemValueRowList[i]["energyDelta"]);
			items[i].specialDelta = Convert.ToInt32(allItemValueRowList[i]["specialDelta"]);
			items[i].timeDelta = Convert.ToInt32(allItemValueRowList[i]["timeDelta"]);
			items[i].supportFurni1 = (FurniType) Enum.Parse(typeof(FurniType), allItemValueRowList[i]["supportFurni1"], true);
			items[i].supportDelta1 = Convert.ToInt32(allItemValueRowList[i]["supportDelta1"]);
			items[i].supportFurni2 = (FurniType) Enum.Parse(typeof(FurniType), allItemValueRowList[i]["supportFurni2"], true);
			items[i].supportDelta2 = Convert.ToInt32(allItemValueRowList[i]["supportDelta2"]);
			items[i].xoffset = Convert.ToSingle(allItemValueRowList[i]["xoffset"]);
			items[i].yoffset = Convert.ToSingle(allItemValueRowList[i]["yoffset"]);
			items[i].canBuy = Convert.ToBoolean(allItemValueRowList[i]["canBuy"]);
			items[i].price = Convert.ToInt32(allItemValueRowList[i]["price"]);
			items[i].iconUrl = allItemValueRowList[i]["iconUrl"];
		}
		FurnitureExcelData excelDataAsset = ScriptableObject.CreateInstance<FurnitureExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(FurnitureExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


