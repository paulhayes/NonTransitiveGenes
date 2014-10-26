using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Cell 
{
	public Creature creature;
	public Creature nextCreature;
	[NonSerialized]
	public Cell[] neighbors;
	public bool uninhabitable;
	
	public override string ToString ()
	{
		if( creature != null ) return creature.ToString();
		else return "";
	}
	
	public Color ToColor()
	{
		if( creature != null ) return creature.ToColor();
		else return Color.white;
		
	}
	
	public float[] ToVector()
	{
		if( creature != null ) return creature.ToVector();
		else return null;
	}
	
	public void Next(){
		creature = nextCreature;
	}
	
	
}
