using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject pipe;

    private bool yetCount = false;
    private float speed = 0;

    // Use this for initialization
    void Start ()
    {
        speed = GameObject.Find("Floor").GetComponent<Floor>().GetSpeed();

        /*
         * Como o cano de cima é o mesmo do de baixo, é necessario ver se o mesmo foi criado para ser o
         * cano de cima ou o de baixo, se ele for o de cima, sua movimentação tem que ser invertida, caso
         * contrario não precisa
         */
        if (transform.rotation.z != 0)
        {
            yetCount = true;
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

    //Métodos para pegar e mudar valores desse script
    /// <summary>
    /// Retorna o da variável que mostra se o ponto desse cano já foi considerado
    /// </summary>
    /// <returns></returns>
    public bool GetYetCount()
    {
        return yetCount;
    }

    /// <summary>
    /// Muda o valor da variável que control se o ponto deste cano já foi considerado
    /// </summary>
    /// <param name="value"></param>
    public void SetYetCount(bool value)
    {
        yetCount = value;
    }
}
