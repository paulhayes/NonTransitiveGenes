using System;
using UnityEngine;

public class Genescape : ScriptableObject
{
	public const int width = 8;
	public const int height = 8;

	[SerializeField]
	public Enviroment enviroment;
	
	public void OnEnable()
	{
		Debug.Log ("OnEnable");
		if( enviroment == null ){
			enviroment = new Enviroment();
			enviroment.Fill(width,height);			
		}
	}
	
	
}




