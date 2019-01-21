using UnityEngine;
using UnityEngine.UI;
using MLAgents;


public class TestAcademy : Academy {

    // To be assigned in editor
    public string[] detectableObjects;
    public string[] collectibles;

    private GameObject[][] collectiblesObjects;

    public override void InitializeAcademy()
    {
        collectiblesObjects = new GameObject[collectibles.Length][];

        for (int i = 0; i < collectibles.Length; i++)
        {
            collectiblesObjects[i] = GameObject.FindGameObjectsWithTag(collectibles[i]);
        }
    }

    public void moveCollectibles()
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            foreach (GameObject col in collectiblesObjects[i])
            {
                Rotator rotator = (Rotator)col.GetComponent(typeof(Rotator));
                rotator.moveRandomnly();
                col.SetActive(true);
            }
        }
    }

    public override void AcademyReset()
    {
        moveCollectibles();
        /*
        for (int i = 0; i < collectibles.Length; i++)
        {
            foreach (GameObject col in collectiblesObjects[i])
            {
                col.SetActive(true);
            }
        }*/
    }

}
