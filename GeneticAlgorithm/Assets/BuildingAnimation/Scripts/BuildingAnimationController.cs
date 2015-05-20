using UnityEngine;
using System.Collections;

public class BuildingAnimationController : MonoBehaviour {

    public ParticleSystem MainSmoke;
    public ParticleSystem BrokenPlanks;
    public GameObject[] BuildingObjects = new GameObject[3];
    public GameObject finalHouse;
    public void OffSmoke()
    {
        MainSmoke.Stop();
    }

    public void BrokenPlanksEnable()
    {
        BrokenPlanks.gameObject.SetActive(true);
    }

    public void AnimatiionFinished()
    {
        StartCoroutine(OffAllObjects());
    }

    private IEnumerator OffAllObjects()
    {
        yield return new WaitForSeconds(3.0f);
        foreach (GameObject o in BuildingObjects)
        {
            o.SetActive(false);
            finalHouse.transform.parent = null;
            //Vector3 position = BuildingObjects[0].transform.position;
            //Instantiate(finalHouse, new Vector3(position.x, 0, position.y), Quaternion.identity);
            Destroy(gameObject);
            yield return null;
        }
    }
}
