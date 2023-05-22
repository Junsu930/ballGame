using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{

    public float jumpPower = 10;
    bool isJump;
    public int itemCount = 0;
    public GM manager;
    AudioSource audio;

    Rigidbody rigid;
    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }

        
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "we")
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
        }else if(other.tag == "Finish")
        {
            if(manager.TotalItemCount == itemCount)
            {
                SceneManager.LoadScene("Stage2");

            }
            else
            {
                // 리스타트
                SceneManager.LoadScene("Stage1");
            }
        }
    }


}
