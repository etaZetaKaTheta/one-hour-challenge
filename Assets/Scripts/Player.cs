using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootLocation;
    [SerializeField] private AudioSource sound;

    [SerializeField] private float shootingRate;
    [SerializeField] private float shootingForce;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxEnemies;

    [SerializeField] private KeyCode shootingKeyCode;

    

    float keyX, keyY;
    bool allowShooting = true;
    int enemiesKilled = 0;

    private void OnEnable()
    {
        Enemy.EnemyHit += Hit;
    }

    private void OnDisable()
    {
        Enemy.EnemyHit -= Hit;
    }

    private void Update()
    {
        InputCall();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void InputCall()
    {
        keyX = Input.GetAxisRaw("Horizontal");
        keyY = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(shootingKeyCode) && allowShooting)
        {
            allowShooting = false;
            Shoot();
            PlayAudio();
        }
    }

    private void Shoot()
    {
        Invoke("ResetBool", shootingRate);
        GameObject instance = Instantiate(projectile, shootLocation.position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce(instance.transform.right * shootingForce, ForceMode2D.Impulse);
    }

    private void ResetBool()
    {
        allowShooting = true;
    }

    private void Movement()
    {
        rb.AddForce(new Vector2(keyX, keyY) * movementSpeed, ForceMode2D.Impulse);
    }

    private void Hit()
    {
        enemiesKilled++;
        if(enemiesKilled == maxEnemies)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void PlayAudio()
    {
        sound.Play();
    }
}
