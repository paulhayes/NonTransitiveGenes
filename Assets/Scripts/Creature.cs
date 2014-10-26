using System;
using UnityEngine;
using System.Linq;

[Serializable]
public class Creature
{
	public const int divisions = 6;
	//public const float mutationRate = 0.999f;
	protected int[] sides = new int[divisions];		
	protected int[] processedSides = new int[divisions];
	protected string description;

	public static void RandomCopy(Creature from, Creature to, float copyChance)
	{
		if( UnityEngine.Random.value < copyChance ) return;
		int pos = UnityEngine.Random.Range(0,divisions);
		to.SetSide( pos, from.sides[pos] );		
		Array.Sort<int>( to.sides );
	}
	
	public static void RandomCopy(Creature from, Creature to, float copyChance, int amount)
	{
		for(int i=0;i<amount;i++){
			RandomCopy(from, to, copyChance);
		}
	}
	
	public static void Mutate(Creature c, float mutationChance)
	{
		if( UnityEngine.Random.value < mutationChance ) return;
		int pos = UnityEngine.Random.Range(0,divisions);
		int sidePoints = UnityEngine.Random.Range(0,Settings.Instance.points+1);
		c.SetSide(pos,sidePoints);
	}
	
	public static void Mutate(Creature c, float mutationChance,int amount)
	{
		for(int i=0;i<amount;i++){
			Mutate(c, mutationChance);
		}
	}
	
	public void SetSide(int sideIndex, int value){		
		sides[Mathf.Clamp(sideIndex,0,divisions-1)] = Mathf.Clamp(value,0,Settings.Instance.points);
		Array.Sort<int>( sides );
		ProcessSides();    
	}
	
	public static bool operator >(Creature a, Creature b)
	{
		return CompareAtoB(a,b) > CompareAtoB(b,a);
	} 
	
	public static bool operator <(Creature a, Creature b){
		return b>a;
	}
	
	public static float CompareAtoB(Creature a, Creature b)
	{
		string logMsg="";
		int lastDivisionA = 0;
		int lastDivisionB = 0;
		int scoreA = 0;
		int pointsB = 0;
		int pointsA = 0;
		
		//Debug.Log( string.Join(",",a.sides.Select(n=>n.ToString()).ToArray()) );
		//Debug.Log( string.Join(",",b.sides.Select(n=>n.ToString()).ToArray()) );
        
		logMsg+="    ";
		for(int j=0;j<divisions;j++){
			pointsB = b.sides[j] - lastDivisionB;
			logMsg+=string.Format("{0,3}",pointsB);
			lastDivisionB = b.sides[j];
        }
        logMsg+="\n";
        for(int i=0;i<divisions;i++){
			
            pointsA = a.sides[i] - lastDivisionA;
			
			logMsg+=string.Format("{0,3}:",pointsA);
			lastDivisionB = 0;
            for(int j=0;j<divisions;j++){
				pointsB = b.sides[j] - lastDivisionB;
				if( pointsA > pointsB ){
					scoreA++;
				}
				logMsg+=string.Format("{0,3}",(pointsA > pointsB)? "x":"o");
				
				
                lastDivisionB = b.sides[j];
			}
			logMsg+=("\n");
            
			
			lastDivisionA = a.sides[i];
		}
		//Debug.Log(logMsg);
		return 1f*scoreA/(divisions*divisions);
	}
	
	public override string ToString ()
	{
		return description;
	}
	
	public float[] ToVector()
	{
		float max = 1f / Settings.Instance.points;
		return new float[]{
			max * processedSides[0],
			max * processedSides[1],
			max * processedSides[2],
			max * processedSides[3],
			max * processedSides[4],
			max * processedSides[5]
		};
	}
	
	public Color ToColor()
	{
		float max = 2f / Settings.Instance.points;
		return new Color(max * processedSides[0] + max * processedSides[1],max * processedSides[2] + max * processedSides[3],max * processedSides[4] + max * processedSides[5]);
	}
	
	public Creature Copy(){
		var newCreature = this.MemberwiseClone() as Creature;
		newCreature.sides = (int[])sides.Clone();
		newCreature.processedSides = (int[])sides.Clone();
		newCreature.ProcessSides();
		return newCreature;
	}
	
	public void ProcessSides(){
		int points=0;
		int lastVal=0;
		for(int j=0;j<divisions;j++){
			points = sides[j] - lastVal;
			processedSides[j] = points;
			lastVal = sides[j];
		}
		//Array.Sort( processedSides );
		description = string.Join(", ",processedSides.Select(n=>n.ToString()).ToArray() );
	}
	
	public void UpdatePoints(){
		int points = Settings.Instance.points;
		for(int i=0;i<divisions;i++){
			sides[i] = Mathf.Clamp(sides[i],0,points);
		}
		ProcessSides();
	}
	
}

