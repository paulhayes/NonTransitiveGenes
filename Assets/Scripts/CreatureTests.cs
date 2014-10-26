using UnityEngine;

public class CreatureTests
{
	Creature a;
	Creature b;
	Creature c;
	Creature empty;
	
	public CreatureTests ()
	{
		a = new Creature();
		b = new Creature();
		c = new Creature();
		empty = new Creature();
		
		a.SetSide(0, 1);
		a.SetSide(1, 2);
        a.SetSide(2, 3);

		b.SetSide(0, 2);
		b.SetSide(1, 3);
		b.SetSide(2, 4);
               
        c.SetSide(0,5);
		c.SetSide(1,6);
		c.SetSide(2,6);
		
        PrintTestResults("Test1",Test1(),0.5f);
		PrintTestResults("Test2",Test2(),0.5f);
		PrintTestResults("Test3",Test3(),0.25f);
		PrintTestResults("Test3",Test4(),1f/3);
	}
    
    public float Test1(){
		return Creature.CompareAtoB(a,empty);
	}
	
	public float Test2(){
		return Creature.CompareAtoB(b,empty);
    }
    
    public float Test3(){
    	return Creature.CompareAtoB(a,b);
    }
    
	public float Test4(){
		return Creature.CompareAtoB(b,a);
	}
    
    void PrintTestResults(string testName, object got, object expected){
		bool passed = ( expected.Equals(got) );
		Debug.Log(string.Format("Test:{0}, got:{1}, expected:{2}, {3} ",testName,got, expected,(passed?"passed":"failed")));
	}
	
	
}