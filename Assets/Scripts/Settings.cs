using UnityEngine;
using System.Collections;
using System;

public class Settings : ScriptableObject 
{
	public event Action OnPointsChange = delegate {
	
				};

	public float mutationRate;
	public int mutationsPerGeneration;
	public int mutationsPerFail;
	public float copyChance;
	public int copiesPerSuccess;
	public int points;
	public float speed;
	
	public static Settings Instance {
		get;
		protected set;
	}
	
	void OnEnable(){
		Instance = this;
	}
	
	public void HandlePointsChange(){
		OnPointsChange();
	}
	
	
}
