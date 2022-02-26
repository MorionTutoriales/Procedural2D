using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonUI : MonoBehaviour
{
	public int nivel;
	public int x;
	public int y;

	public Text txt;
	public Button boton;

    void Start()
    {
		int k = PlayerPrefs.GetInt("nivel", 1);
		if (k>=nivel)
		{
			boton.interactable = true;
		}
		else
		{
			boton.interactable = false;
		}
		txt.text = nivel.ToString("00");
    }
	

	public void Activar()
	{
		PlayerPrefs.SetInt("x", x);
		PlayerPrefs.SetInt("y", y);
		PlayerPrefs.SetInt("nivelCargado", nivel);
		SceneManager.LoadScene("Juego");
	}

	public void Salir()
	{
		Application.Quit();
	}
}
