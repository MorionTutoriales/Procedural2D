using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Motor : MonoBehaviour
{
	public float velocidad;
	public float fuerzaSalto;
	public Animator miAnimator;
	public float radioSalto;
	public LayerMask capaPared;
	public bool vivo = true;
	public float horizontal;
	public UnityEngine.UI.Slider slHorizontal;
	public GameObject uiPerder;
	public GameObject bomba;
	public int bombas;
	public GameObject botonBombas;

	private Rigidbody2D miRB;

	private void Awake()
	{
		miRB = GetComponent<Rigidbody2D>();
		ActualizarBotonBombas();
	}
	
	public void CambiarHorizontal(float hor)
	{
		horizontal = hor;
	}

    void Update()
    {
		if (!vivo)
		{
			return;
		}
		horizontal = slHorizontal.value;
		transform.Translate((Input.GetAxis("Horizontal")+horizontal) * Time.deltaTime * velocidad * Vector3.right);
		miAnimator.SetBool("caminando", (Input.GetAxis("Horizontal")+horizontal) != 0);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Saltar();
		}
    }

	public void Bombear()
	{
		GameObject b = Instantiate(bomba, transform.position, transform.rotation);
		b.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
		bombas--;
		ActualizarBotonBombas();
	}

	void ActualizarBotonBombas()
	{
		botonBombas.SetActive(bombas > 0);
	}

	public void Saltar()
	{
		if (!vivo)
		{
			return;
		}
		Vector2 punto = new Vector2(transform.position.x, transform.position.y);
		Collider2D cols = Physics2D.OverlapCircle(punto, radioSalto, capaPared);
		if (cols != null)
		{
			miRB.velocity = Vector2.zero;
			miRB.AddForce(Vector2.up * fuerzaSalto);
			miAnimator.SetTrigger("saltar");
		}
	}

	public void Matar()
	{
		vivo = false;
		miAnimator.SetBool("vivo", false);
		uiPerder.SetActive(true);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radioSalto);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("bomba"))
		{
			Destroy(collision.gameObject);
			bombas++;
			ActualizarBotonBombas();
		}
	}
}
