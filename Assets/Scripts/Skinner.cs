using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Skinner : MonoBehaviour
{
	public Sprite[] imagenes;
    void Start()
    {
		GetComponent<SpriteRenderer>().sprite = imagenes[Random.Range(0, imagenes.Length)];
    }
}
