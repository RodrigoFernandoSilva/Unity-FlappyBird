﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public Text scoreGui;
    public GameObject birdObj;
    public GameObject playObj;

    //Força de pulo do pássaro
    public float force = 0;
    //Velocidade que o pássaro vai cair, isso do 'Rigidbory'
    public float gravity = 0;
    //Força que o pássaro rataciona para cima e para baixo
    public float forceRotateUp = 0;
    public float forceRotateDown = 0;
    private int score;

    /*
     * Salva sé o pássaro colidio com o cano, pois após colidir com ele, o jogo tem que parar e o pássaro
     * cair até o chão
     */
    private bool collidedPipe;
    private bool isJumpping;
    /**
     * Salva a posição inicial do passaro para após ele morrer e reiniciar a fase, ele seja colocado
     * no mesmo ponto de origim, isso serve para as duas vari'aveis abaixo
     */
    private float initialX;
    private float initialY;
    //Utilizado para rotacionar o pássaro para cima e para baixo
    private float rotateZ;

    // Use this for initialization
    void Start()
    {
        birdObj.GetComponent<Rigidbody2D>().gravityScale = 0;
        birdObj.GetComponent<Rigidbody2D>().simulated = false;

        initialX = birdObj.transform.position.x;
        initialY = birdObj.transform.position.y;

        scoreGui.enabled = false;
        collidedPipe = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Faz o pássaro pular se for apertado a tecla 'espaço' ou o botão direto do mouse
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (playObj.GetComponent<Renderer>().enabled) {
                RestartLevel();
            } else if (!collidedPipe)
            {
                isJumpping = true;
            }
        }

        UpdateScore();
        UpdateJump();
        RotateBird();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
         * Mata o pássaro se ele colidir com o cano, porém, isso só serve para fazer com que ele caia até
         * o chão, pois só depois de colidir com o chão é que o jogador vai poder jogar resetar a fase
         */
        if (collision.gameObject.tag == "Pipe" && !collidedPipe)
        {
            collidedPipe = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            BirdDie();
        }
    }

    /// <summary>
    /// Contabiliza os pontos, vendo se o cano já passou pelo passaro
    /// </summary>
    void UpdateScore()
    {
        foreach (GameObject pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            if (!pipe.GetComponent<Pipe>().GetYetCount() && pipe.transform.position.x < birdObj.transform.position.x + 0.03f)
            {
                pipe.GetComponent<Pipe>().SetYetCount(true);
                score++;
            }
        }

        scoreGui.text = score.ToString();
    }

    //Métodos desse usado ness escript
    /// <summary>
    /// Reseta a fase, fazendo com que tudo seja colocado no seu devido lugar
    /// </summary>
    void RestartLevel()
    {
        foreach (GameObject pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            Destroy(pipe);
        }
        GameObject.Find("SpawPipe").GetComponent<SpawPipes>().SetTimerToSpaw();
        birdObj.transform.position = new Vector3(initialX, initialY, 0);
        rotateZ = 0;
        score = 0;
        scoreGui.enabled = true;
        collidedPipe = false;
        isJumpping = true;
    }

    /// <summary>
    /// 
    /// </summary>
    void BirdDie()
    {
        if (!playObj.GetComponent<Renderer>().enabled) {
            birdObj.GetComponent<Rigidbody2D>().gravityScale = 0;
            playObj.GetComponent<Renderer>().enabled = true;
            birdObj.GetComponent<Rigidbody2D>().simulated = false;
        }
    }
    
    /// <summary>
    /// Faz o pássaro pular quando for apertado a tecla 'espaço' ou o botão direito do mouse
    /// </summary>
    void UpdateJump() {
        if (isJumpping)
        {
            if (playObj.GetComponent<Renderer>().enabled) {
                birdObj.GetComponent<Rigidbody2D>().gravityScale = gravity;
                birdObj.GetComponent<Rigidbody2D>().simulated = true;
                playObj.GetComponent<Renderer>().enabled = false;
            }
        birdObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, force);
        isJumpping = false;
        }
    }

    /// <summary>
    /// Rotaciona o pássaro para cima ou para baixo com um determinado limite máximo para cada
    /// um dos eixos
    /// </summary>
    void RotateBird()
    {
        /*
         * Só pode rotacionar o pássaro se o jogador tiver feito o ele pula, pois a física do jogo
         * só é ativada quando o pássaro pula, e em quanto isso não acontece o passaro fica parado
         * na tela
         */
        if (birdObj.GetComponent<Rigidbody2D>().gravityScale != 0) {
            float rigidbodyVelocity = birdObj.GetComponent<Rigidbody2D>().velocity.y;

            //Roda o pássaro para cima se ele estiver se movento para cima, caso contrario, roda ele para baixo
            if (rigidbodyVelocity > 0)
            {
                //Impedi que o pássaro de um 360
                if (rotateZ < 30)
                {
                    rotateZ += (birdObj.transform.rotation.z + forceRotateUp) * Time.deltaTime;
                    birdObj.transform.eulerAngles = new Vector3(0, 0, rotateZ);
                }
            }
            else
            {
                //Impedi que o pássaro de um 360
                if (rotateZ > -60)
                {
                    rotateZ -= (birdObj.transform.rotation.z + forceRotateDown) * Time.deltaTime;
                    birdObj.transform.eulerAngles = new Vector3(0, 0, rotateZ);
                }
            }
        }
    }

    //Métodos para pegar e mudar valores desse script
    /// <summary>
    /// Retorna se o jogador está jogando ou já morreu
    /// </summary>
    /// <returns></returns>
    public bool GetPlayEnable()
    {
        return playObj.GetComponent<Renderer>().enabled;
    }

    /// <summary>
    /// Retorna se o pássaro colidio com o cano ou não
    /// </summary>
    /// <returns></returns>
    public bool GetCollidedPipe()
    {
        return collidedPipe;
    }
}
