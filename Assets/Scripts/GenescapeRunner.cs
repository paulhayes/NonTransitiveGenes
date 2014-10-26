using UnityEngine;
using System.Collections;

public class GenescapeRunner : MonoBehaviour {

	public Genescape genescape;
	public Settings settings;
	public GameObject prefab;
	public Transform spawnLocation;
	private CellView[,] cellView;
	private CellInput[,] cellInput;
	
	
	// Use this for initialization
	void Start () {
	
		settings.OnPointsChange += UpdatePoints;
	
		CreatureTests tests = new CreatureTests();	
		//cellInfo = new TextMesh[Genescape.width,Genescape.height];
		//cellColor = new Material[Genescape.width,Genescape.height];
		cellInput = new CellInput[Genescape.width,Genescape.height];
		cellView = new CellView[Genescape.width,Genescape.height];
		
		Vector2 cellSize = new Vector2(1.1f,1.1f);
		for(int i=0;i<Genescape.width;i++){
			for(int j=0;j<Genescape.height;j++){
				GameObject cell = Instantiate(prefab,spawnLocation.position + ( new Vector3(i*cellSize.x,j*cellSize.y,0) ),Quaternion.identity) as GameObject;
				cell.transform.parent = spawnLocation;
				cellView[i,j] = cell.GetComponent<CellView>();
				cellInput[i,j] = cell.GetComponent<CellInput>();
				cellView[i,j].cell = genescape.enviroment.GetCell(i,j);
				cellInput[i,j].renderer.enabled = !genescape.enviroment.GetCell(i,j).uninhabitable;
				cellView[i,j].SetRect( new Rect(1f*i/Genescape.width,1f*j/Genescape.height,1f/Genescape.width,1f/Genescape.height) );
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		float r = Time.time - Mathf.Floor(Time.time);
		if( r > settings.speed ) return;
		
		Cell[] neighbours;
		bool anyCreatures = false;
		for(int i=0;i<Genescape.width;i++){
			for(int j=0;j<Genescape.height;j++){
				Cell cell = genescape.enviroment.GetCell(i,j);
				if( cell.uninhabitable ){
					cell.creature = null;
					cell.nextCreature = null;	
						
				}
				if( cell.creature == null ) continue;
				anyCreatures = true;
				neighbours = genescape.enviroment.GetNeighbors(i,j);
				foreach(var neighbour in neighbours){
					if( neighbour.uninhabitable ) {
						
						continue;
					}
					if( neighbour.nextCreature == null ){
						//Debug.Log (string.Format("Copying creature at {0},{1} to neightboor cell ",i,j));
						
						neighbour.nextCreature = cell.creature.Copy();
						Creature.Mutate( neighbour.nextCreature, settings.mutationsPerGeneration );						
					}
					else if( cell.creature > neighbour.nextCreature ){
					
						//Debug.Log (string.Format("Copying creature {0}, beat {1} ",cell.creature,neighbour.nextCreature));
						
					
						Creature.RandomCopy( cell.creature, neighbour.nextCreature, settings.copyChance, settings.copiesPerSuccess );
						Creature.Mutate( neighbour.nextCreature, settings.mutationRate, settings.mutationsPerFail );						
                    }
				}
				Creature.Mutate( cell.creature, settings.mutationRate,settings.mutationsPerGeneration );
			
			}
		}
		if( !anyCreatures ){
			Debug.Log ("Adding creature to 0,0");
			genescape.enviroment.GetCell(0,0).nextCreature = new Creature();
		}
		
		for(int i=0;i<Genescape.width;i++){
			for(int j=0;j<Genescape.height;j++){
				genescape.enviroment.GetCell(i,j).Next();
				genescape.enviroment.GetCell(i,j).uninhabitable = !cellInput[i,j].renderer.enabled;
				//cellInfo[i,j].text = genescape.enviroment.GetCell(i,j).ToString();
				//cellColor[i,j].color = 
				
			}
		}
	}
	
	public void UpdatePoints(){
		for(int i=0;i<Genescape.width;i++){
			for(int j=0;j<Genescape.height;j++){
				Creature creature = genescape.enviroment.GetCell(i,j).creature;
				if( creature == null ) continue;
				
				creature.UpdatePoints();
			}
		}
	}
	
}
