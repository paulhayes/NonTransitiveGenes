using UnityEngine;
using System.Collections;

public class CellView : MonoBehaviour {

	public Cell cell;
    private TextMesh textMesh;
    private Material material;
    
    private CreatureDetailDisplay display;
	
	private Vector4 sides1234;
	private Vector4 sides5678;

	// Use this for initialization
	void Start () {
		textMesh = GetComponentInChildren<TextMesh>(); 
		display = FindObjectOfType<CreatureDetailDisplay>();
	}
	
	// Update is called once per frame
	void Update () {
		if( cell == null ){
			return;
		}
		float[] sides = cell.ToVector();
		Vector4 destSides1234 = (sides==null) ? Vector4.zero : new Vector4(sides[0],sides[1],sides[2],sides[3]);
		Vector4 destSides5678 = (sides==null) ? Vector4.zero : new Vector4(sides[4],sides[5],0,0);
		sides1234 = Vector4.MoveTowards(sides1234,destSides1234,2f * Time.deltaTime);
		sides5678 = Vector4.MoveTowards(sides5678,destSides5678,1f * Time.deltaTime);
		material.SetVector("_Sides1234",sides1234);
		material.SetVector("_Sides5678",sides5678);
		material.color = cell.ToColor();
        
	}
	
	void OnMouseEnter(){
		display.SetCreature( cell.creature );
	}
	
	public void SetRect(Rect rect){
		material = renderer.material = new Material( renderer.material ); 
		//material.mainTextureScale = new Vector2(rect.width,rect.height);
		//material.mainTextureOffset = new Vector2(rect.x,rect.y);
	}
}
