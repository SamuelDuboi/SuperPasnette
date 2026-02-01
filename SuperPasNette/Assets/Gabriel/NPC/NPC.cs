using System;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public float baseSpeed;
    public SanityCollider colision;
    public Animator anim;

    public float sanityDecreaseValue;

    public bool isAtDestination = true;
    public event Action Arrived;
    public event Action<float> SanityDecrease;

    private void Start()
    {
        colision.OnCollide += Colision_OnCollide;
        agent.speed = baseSpeed;
    }

    private void Colision_OnCollide()
    {
        SanityDecrease?.Invoke(sanityDecreaseValue);
    }

    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
        anim.SetBool("IsMoving", true);
        isAtDestination = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isAtDestination)
        {
            return;
        }
        else if (Mathf.Round(agent.remainingDistance) <= 0.1f)
        {
            Arrived?.Invoke();
            anim.SetBool("IsMoving", true);
            isAtDestination = true;
        }
    }
}
