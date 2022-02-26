using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Matriz : MonoBehaviour
{
	public static Matriz singleton;
    public int x, y;
	public int[,] matrizJuego;
	public int[,] matrizBloques;

	public GameObject bloque;
	public GameObject jugador;
	public GameObject palanca;
	public Portal portal;

	[Range(0f, 1f)]
	public float[] dificultad;

	public bool palancaActiva= false;
	public bool verGizmos=false;

	private void Awake()
	{
		if (singleton==null)
		{
			singleton = this;
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	}

	void Start()
	{
		x = PlayerPrefs.GetInt("x", 10);
		y = PlayerPrefs.GetInt("y", 10);
		CrearMundo();
	}
	
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.R))
		{
			Reiniciar();
		}
    }

	public void Reiniciar()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Salir()
	{
		SceneManager.LoadScene("Menu");
	}

	public void CrearMundo()
	{
		matrizJuego = new int[x, y];
		matrizBloques = new int[x, y];
		TrazarRuta();
		DeterminarBloques();
		CrearBloques();
	}

	void TrazarRuta()
	{
		int puntoInicio = Random.Range(0, x);
		int puntoFin = Random.Range(0, x);


		int[] puntoFlotante = { puntoInicio, y - 1 };
		while (!(puntoFlotante[0] == puntoFin && puntoFlotante[1] == 0))
		{
			int movimiento;
			bool repetir = false;
			do
			{
				repetir = false;
				movimiento = Random.Range(1, 4);
				if (movimiento == 1)
				{
					puntoFlotante[0]++;
					if (puntoFlotante[0] >= x)
					{
						puntoFlotante[0]--;
						repetir = true;
					}
				}
				else if (movimiento == 2)
				{
					puntoFlotante[1]--;
					if (puntoFlotante[1] < 0)
					{
						puntoFlotante[1]++;
						repetir = true;
					}
				}
				else if (movimiento == 3)
				{
					puntoFlotante[0]--;
					if (puntoFlotante[0] < 0)
					{
						puntoFlotante[0]++;
						repetir = true;
					}
				}
			} while (repetir);
			matrizJuego[puntoFlotante[0], puntoFlotante[1]] = 3;

		}


		int[] habitacion = { Random.Range(0, x), Random.Range(1, y-1) };
		matrizJuego[habitacion[0], habitacion[1]] = 4;

		//Conectar habitación
		if (ValorBloque(habitacion[0], habitacion[1])==0)
		{
			int columa = -1;
			for (int i = 0; i < x; i++)
			{
				if (matrizJuego[i,habitacion[1]] != 0 && i!=habitacion[0])
				{
					columa = i;
					break;
				}
			}
			if (columa>habitacion[0])
			{
				for (int i = habitacion[0]+1; i < columa; i++)
				{
					matrizJuego[i, habitacion[1]] = 3;
				}
			}
			else
			{
				for (int i = columa; i < habitacion[0]; i++)
				{
					matrizJuego[i, habitacion[1]] = 3;
				}
			}

		}


		matrizJuego[puntoInicio, y - 1] = 1;
		matrizJuego[puntoFin, 0] = 2;

		jugador.transform.position = new Vector3(puntoInicio * 10, (y - 1) * 10);
		palanca.transform.position = new Vector3(habitacion[0] * 10, habitacion[1] * 10);
		portal.transform.position = new Vector3(puntoFin * 10, 0);
		Camara.instancia.transform.position = jugador.transform.position;
	}

	void DeterminarBloques()
	{
		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < y; j++)
			{
				matrizBloques[i, j] = ValorBloque( i,  j);
			}
		}
	}

	public int ValorBloque(int i, int j)
	{
		int resultado = 0;
		resultado += 1 * SaberSiHayBloque(i, j + 1);
		resultado += 2 * SaberSiHayBloque(i + 1, j);
		resultado += 4 * SaberSiHayBloque(i, j - 1);
		resultado += 8 * SaberSiHayBloque(i - 1, j);
		return resultado;
	}

	void CrearBloques()
	{
		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < y; j++)
			{
				if (matrizJuego[i,j]!=0)
				{
					GameObject bl = Instantiate(bloque, new Vector3(i * 10, j * 10, 0), Quaternion.identity) as GameObject;
					bl.GetComponent<Bloques>().Inicializar(matrizBloques[i, j]);
				}
			}
		}
	}

	public int SaberSiHayBloque(int _x, int _y)
	{
		if (_x<0 || _y<0 || _x>=x || _y >= y)
		{
			return 0;
		}
		if (matrizJuego[_x,_y] == 0)
		{
			return 0;
		}
		return 1;
	}

	public void ActivarPalanca()
	{
		palancaActiva = true;
		portal.ActivarPortal();
	}

	private void OnDrawGizmos()
	{
		if (!verGizmos)
		{
			return;
		}
		if (matrizJuego== null)
		{
			return;
		}
		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < y; j++)
			{
				if (matrizJuego[i, j] == 0)
				{
					Gizmos.color = Color.black;
				}
				else if (matrizJuego[i, j] == 1)
				{
					Gizmos.color = Color.green;
				}
				else if (matrizJuego[i, j] == 2)
				{
					Gizmos.color = Color.red;
				}
				else if (matrizJuego[i, j] == 3)
				{
					Gizmos.color = Color.yellow;
				}
				else if (matrizJuego[i, j] == 4)
				{
					Gizmos.color = Color.blue;
				}
				Gizmos.DrawCube(new Vector3(i * 10, j * 10, 0), Vector3.one * 10);
			}
		}
	}
}
