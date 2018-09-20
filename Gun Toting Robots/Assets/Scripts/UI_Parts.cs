using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Parts : MonoBehaviour, IPointerClickHandler
{

    public GameObject CreatorRef;
    public GameObject PrefabToSpawn;
    public int GoldCost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 MousePos = new Vector2(pos.x, pos.y);

        if (CreatorRef.GetComponent<Creator>().UpdateGold(-GoldCost))
        {
            GameObject GO = Instantiate(PrefabToSpawn, new Vector3(MousePos.x, MousePos.y, 0), Quaternion.identity);
            CreatorRef.GetComponent<Creator>().PartSpawned(GO);
        }
        else
        {
            Debug.Log("Not enough gold");
        }
        
            
    }

}
