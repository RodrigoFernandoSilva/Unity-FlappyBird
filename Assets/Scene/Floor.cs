using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public float speed = 0;
    private float initialX;

    // Use this for initialization
    void Start () {
        initialX = transform.position.x;

    }
	
	// Update is called once per frame
	void Update () {
        //Só pode mover o chão em quanto o jogador não estiver colidido com o chão ou com o cano
        if (!GameObject.Find("Bird").GetComponent<Bird>().GetPlayEnable() &&
            !GameObject.Find("Bird").GetComponent<Bird>().GetCollidedPipe())
        {
            //Move o chão, o 'Time.deltaTime' faz com que a movimetação seja igual para todos os computadores
            transform.Translate(speed * Time.deltaTime, 0, 0);

            if (transform.position.x + 3.58f < initialX)
            {
                transform.position = new Vector3(transform.position.x + 3.58f, 0, 0);
            }
        }
    }

    //Métodos usados para pegar ou mudar valores desse script
    /// <summary>
    /// Retorna a velocidade que o chão esta se movendo, ou seja, a velocidade de dificuldade do jogo
    /// </summary>
    /// <returns></returns>
    public float GetSpeed() {
        return speed;
    }
}
