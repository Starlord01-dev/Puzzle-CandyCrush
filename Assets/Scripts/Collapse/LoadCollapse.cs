using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCollapse : MonoBehaviour
{
    public int column;
    public int row;
    public bool matched = false;
    public GameObject PopPrefab;
    private LoadableBoard board;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<LoadableBoard>();
        column = (int)transform.position.x;
        row = (int)transform.position.y;
    }

    public void isMatch(GameObject popable)
    {
        if (matched == false)
        {
            if (gameObject.CompareTag(popable.tag))
            {
                matched = true;
                board.Match((int)Mathf.Round(transform.position.y), (int)Mathf.Round(transform.position.x));
            }
        }
    }

    public IEnumerator Particle_Effect_Pop()
    {
        ParticleSystem.MainModule settings = PopPrefab.GetComponent<ParticleSystem>().main;
        switch (this.tag)
        {
            case "Beige":
                settings.startColor = new Color(245, 245, 220);
                break;

            case "Black":
                settings.startColor = Color.black;
                break;

            case "BlancSalle":
                settings.startColor = new Color(217, 208, 193);
                break;

            case "Blue":
                settings.startColor = Color.blue;
                break;

            case "Bordaux":
                settings.startColor = new Color(95, 2, 31);
                break;

            case "Brown":
                settings.startColor = new Color(165, 42, 42);
                break;

            case "Grass":
                settings.startColor = new Color(34, 139, 34);
                break;

            case "Green":
                settings.startColor = Color.green;
                break;

            case "Grey":
                settings.startColor = Color.gray;
                break;

            case "Lavander":
                settings.startColor = new Color(230, 230, 250);
                break;

            case "Lime":
                settings.startColor = new Color(0, 255, 0);
                break;

            case "Magenta":
                settings.startColor = new Color(255, 0, 255);
                break;

            case "NavyBlue":
                settings.startColor = new Color(0, 0, 139);
                break;

            case "NeonBlue":
                settings.startColor = new Color(224, 255, 255);
                break;

            case "Orange":
                settings.startColor = new Color(255, 165, 0);
                break;

            case "Pink":
                settings.startColor = new Color(255, 192, 203);
                break;

            case "Purple":
                settings.startColor = new Color(128, 0, 128);
                break;

            case "Red":
                settings.startColor = Color.red;
                break;

            case "SkyBlue":
                settings.startColor = new Color(135, 206, 235);
                break;

            case "Turquoise":
                settings.startColor = new Color(0,206,209);
                break;

            case "White":
                settings.startColor = Color.white;
                break;

            case "Yellow":
                settings.startColor = Color.yellow;
                break;

            default:
                Debug.Log("Couldn't find matching color to the tag.");
                break;
        }
        PopPrefab.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.5f);
    }

}
