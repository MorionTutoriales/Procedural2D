using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
	public float radio;
	public LayerMask capaParedes;
	public GameObject explocion;
	public float tiempoExplotar = 5;

	private void Start()
	{
		Invoke("Explotar", tiempoExplotar);
	}

	public void Explotar()
	{
		Collider2D[] elementos = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radio, capaParedes);
		for (int i = 0; i < elementos.Length; i++)
		{
			Destroy(Instantiate(explocion, elementos[i].transform.position, Quaternion.identity) as GameObject,3);
			Destroy(elementos[i].gameObject);
		}
		elementos = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radio);
		for (int i = 0; i < elementos.Length; i++)
		{
			if (elementos[i].CompareTag("Player"))
			{
				elementos[i].GetComponent<Motor>().Matar();
			}
		}
		Destroy(gameObject);
	}
}
