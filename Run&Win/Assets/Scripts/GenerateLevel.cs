using System.Collections;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] sections;
    public int zPosition = 300;
    public bool createSec = false;
    public int secNo;
    void Update()
    {
        if (createSec == false)
        {
            createSec = true;
            StartCoroutine(GenerateSection());
        }
    }

    IEnumerator GenerateSection()
    {
        secNo = Random.Range(0, 2);
        Instantiate(sections[secNo], new Vector3(0, 0, zPosition), Quaternion.identity);
        zPosition += 300;
        yield return new WaitForSeconds(5);
        createSec = false;
    }
}
