using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseScript : MonoBehaviour
{
    public void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
    public void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
