using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystemTest : MonoBehaviour
{
    public GameObject Explode;
    ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StartExplosion());
        }
    }

    IEnumerator StartExplosion()
    {
        GameObject tempExplosion = Instantiate(Explode, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        tempExplosion.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(5);
        Destroy(tempExplosion);
    }

}
