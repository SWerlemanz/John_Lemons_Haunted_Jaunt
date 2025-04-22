using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    public GameObject boostButton;
    public GameObject rechargeText;

    private bool isBoosting = false;
    private bool isRecharging = false;
    private bool canBoost = false;
    private float currentSpeed;

    public float normalSpeed;
    public float boostedSpeed;
    public float boostDuration = 2f;
    public float rechargeTime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();

        currentSpeed = normalSpeed;
        boostButton.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }


        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(SpeedBoost());
        }

    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    IEnumerator SpeedBoost()
    {
        canBoost = true;
        isBoosting = true;
        currentSpeed = boostedSpeed;
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * 1.5f * m_Animator.deltaPosition.magnitude);
        


        yield return new WaitForSeconds(boostDuration);

        currentSpeed = normalSpeed;

        canBoost = false;
        isBoosting = false;
        boostButton.SetActive(false);

        StartCoroutine(RechargeBoost());
    }

    IEnumerator RechargeBoost()
    {
        isRecharging = true;
        canBoost = false;

        rechargeText.SetActive(true);

        yield return new WaitForSeconds(rechargeTime);

        isRecharging = false;
        canBoost = true;

        rechargeText.SetActive(false);

        boostButton.SetActive(true);
    }


}

