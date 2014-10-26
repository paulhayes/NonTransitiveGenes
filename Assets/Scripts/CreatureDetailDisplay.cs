using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreatureDetailDisplay : MonoBehaviour {

	public Text currentDescription;
	public Text lastDescription;
	public Text competeResults;
	public Slider timeSpeed;
	public Slider pointsSlider;
	public Text pointsTextValue;
	
	private Creature currentCreature;
	private Creature lastCreature;
	
	public void SetCreature(Creature creature){
		lastCreature = currentCreature;
		currentCreature = creature;
	}

	// Use this for initialization
	void Start () {
		pointsSlider.value = Settings.Instance.points;
		timeSpeed.value = Settings.Instance.speed;
	}
	
	// Update is called once per frame
	void Update () {
		
		if( currentCreature != null ){
			currentDescription.text = currentCreature.ToString();
		}
		
		if( lastCreature != null ){
			lastDescription.text = lastCreature.ToString();
		}
		
		if( ( currentCreature != null ) && ( lastCreature != null ) ){
			
			float aBeatsB = Creature.CompareAtoB(currentCreature,lastCreature);
			float bBeatesA = Creature.CompareAtoB(lastCreature, currentCreature);
			string colorA = ( aBeatsB > bBeatesA ) ? "green" : "red";
			string colorB = ( aBeatsB < bBeatesA ) ? "green" : "red";
            if( aBeatsB == bBeatesA ){
            	colorA = "white";
            	colorB = "white";
            }
			competeResults.text = string.Format("Results\nCreature A <color={3}>{0:0.0}%</color>\nCreature B <color={4}>{1:0.0}%</color>\nDraw:{2:0.0}",aBeatsB*100f,bBeatesA*100f,100f*(1-aBeatsB-bBeatesA),colorA,colorB);
		}
		
		pointsTextValue.text = pointsSlider.value.ToString();
	}
	
	public void UpdateSpeed(){
		Settings.Instance.speed = timeSpeed.value;
	}
	
	public void PointsSliderChanged(){
		Settings.Instance.points = (int)pointsSlider.value;
		Settings.Instance.HandlePointsChange();
	}
	
}
