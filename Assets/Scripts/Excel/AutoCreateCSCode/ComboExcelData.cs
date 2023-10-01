/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class ComboExcelItem : ExcelItemBase
{
	public int originalID;
	public List<int> checkID;
	public int changeID;
}

[CreateAssetMenu(fileName = "ComboExcelData", menuName = "Excel To ScriptableObject/Create ComboExcelData", order = 1)]
public partial class ComboExcelData : ExcelDataBase<ComboExcelItem>
{
}

#if UNITY_EDITOR
public class ComboAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		ComboExcelItem[] items = new ComboExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new ComboExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].originalID = Convert.ToInt32(allItemValueRowList[i]["originalID"]);
			items[i].checkID = new List<int>(Array.ConvertAll((allItemValueRowList[i]["checkID"]).Split(';'), int.Parse));
			items[i].changeID = Convert.ToInt32(allItemValueRowList[i]["changeID"]);
		}
		ComboExcelData excelDataAsset = ScriptableObject.CreateInstance<ComboExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(ComboExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


