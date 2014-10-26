using UnityEngine;
using UnityEditor;

public class MenuItems {

	[MenuItem("Assets/Create/Genes")]
	static void CreateGenescape()
	{
		ProjectWindowUtil.CreateAsset( ScriptableObject.CreateInstance<Genescape>(), "New Genescape.asset" );
	}
	
	[MenuItem("Assets/Create/Settings")]	
	static void CreateSettings()
	{
		ProjectWindowUtil.CreateAsset( ScriptableObject.CreateInstance<Settings>(), "New Settings.asset" );		
	}
}
