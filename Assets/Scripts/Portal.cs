using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
	public GameObject espirales;

	public void ActivarPortal()
	{
		espirales.SetActive(true);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			int record = PlayerPrefs.GetInt("nivel", 1);
			int actual = PlayerPrefs.GetInt("nivelCargado", 1);
			if (actual >= record)
			{
				PlayerPrefs.SetInt("nivel", record+1);
			}
			Invoke("Menu", 0.5f);
		}
	}

	void Menu()
	{
		SceneManager.LoadScene("Menu");
	}
}
