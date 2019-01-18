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

    public override void AcademyReset()
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            foreach (GameObject col in collectiblesObjects[i])
            {
                col.SetActive(true);
            }
        }
    }

}
