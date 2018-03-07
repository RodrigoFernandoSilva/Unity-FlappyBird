using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject pipe;

    public float speed = 0;

	// Use this for initialization
	void Start ()
    {
        /*
         * Como o cano de cima é o mesmo do de baixo, é necessario ver se o mesmo foi criado para ser o
         * cano de cima ou o de baixo, se ele for o de cima, sua movimentação tem que ser invertida, caso
         * contrario não precisa
         */
        if (transform.rotation.z != 0)
        {
            speed *= -1;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Só pode mover o cano se o jogador não tiver colidido com o passaro no chão ou no cano
        if (!GameObject.Find("Bird").GetComponent<Bird>().GetPlayEnable() &&
            !GameObject.Find("Bird").GetComponent<Bird>().GetCollidedPipe())
        {
            //Move o cano, o 'Time.deltaTime' faz com que a movimetação seja igual para todos os computadores
            transform.Translate(speed * Time.deltaTime, 0, 0);

            //Destroi o passaro após ele passar pelo passaro e não estiver mais dentro da área da câmera
            if (transform.position.x < -4)
            {
                Destroy(gameObject);
            }
        }
	}

    public void DestroyPipe()
    {
        Destroy(gameObject);
    }
}
