using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girador : MonoBehaviour
{
	public Vector2 velocidadRandom;
	float velocidad;

    void Start()
    {
		velocidad = Random.Range(velocidadRandom.x, velocidadRandom.y);
    }
    void Update()
    {
		transform.Rotate(Vector3.forward*velocidad*Time.deltaTime);
    }
}
