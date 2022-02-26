using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbExistir : MonoBehaviour
{
	public int indice;

    void Start()
    {
		gameObject.SetActive(!(Random.Range(0f, 1f) > Matriz.singleton.dificultad[indice]));
    }
}
