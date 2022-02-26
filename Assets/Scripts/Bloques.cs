using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloques : MonoBehaviour
{
	public int numero;
	public GameObject[] puertas;
	public GameObject[] internas;


	void Start()
    {

    }
	
    public void Inicializar(int n)
    {
		numero = n;
		puertas[0].SetActive((numero%2)!=1);
		puertas[1].SetActive(( Mathf.FloorToInt(numero/2) % 2) != 1);
		puertas[2].SetActive(( Mathf.FloorToInt(numero/4) % 2) != 1);
		puertas[3].SetActive(( Mathf.FloorToInt(numero/8) % 2) != 1);
		Instantiate(internas[Random.Range(0,internas.Length)],  transform.position, transform.rotation);
	}
}
