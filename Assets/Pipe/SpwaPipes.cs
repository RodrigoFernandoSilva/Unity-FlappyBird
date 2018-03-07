using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwaPipes : MonoBehaviour
{
    public Random generator;
    public GameObject pipe;

    //Controla o máximo de tempo entre o spaw de um cano e outro
    public float maxTime = 0;
    //Este é o contador que fica sendo acresentado a caada update
    private float timer;
    //Salva a posição em trandomica onde o cano vai ser criada
    private float y;

    // Use this for initialization
    void Start ()
    {
        //Iguala ao máximo de tempo para o jogo já começar sumonando um cano
        timer = maxTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Só pode sumonar canos em quanto o jogador não estiver colidido com o chão ou com o cano
        if (!GameObject.Find("Bird").GetComponent<Bird>().GetPlayEnable() &&
            !GameObject.Find("Bird").GetComponent<Bird>().GetCollidedPipe()) {
            //Sumona os dois canos entre um espaço aleatorio
            if (timer > maxTime)
            {
                y = Random.Range(-2f, -4f);
                Instantiate(pipe, new Vector3(transform.position.x, y, 0), transform.rotation);
                y += 6.8f;
                Instantiate(pipe, new Vector3(transform.position.x, y, 0), new Quaternion(0, 0, 1, 0));
                timer = 0;
            }

            timer += 1 * Time.deltaTime;
        }
	}
}
