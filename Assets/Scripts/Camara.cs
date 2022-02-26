using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
	public Transform jugador;
	public float velocidad;
	public static Camara instancia;

	private void Awake()
	{
		instancia = this;
	}

	void Update()
    {
		this.transform.position = Vector3.Lerp(transform.position, jugador.position, velocidad * Time.deltaTime);
    }
}
