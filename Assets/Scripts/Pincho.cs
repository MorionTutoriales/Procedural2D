using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pincho : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			collision.transform.GetComponent<Motor>().Matar();
			print(gameObject.name);
		}
	}
}
