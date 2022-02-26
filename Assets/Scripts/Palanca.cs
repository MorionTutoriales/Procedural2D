using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Palanca : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player") && !Matriz.singleton.palancaActiva)
		{
			Matriz.singleton.ActivarPalanca();
			GetComponent<Animator>().SetBool("activo", true);
		}
	}
}
