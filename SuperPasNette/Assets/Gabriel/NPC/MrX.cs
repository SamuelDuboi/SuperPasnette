using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MrX : MonoBehaviour
{
    public NavMeshAgent agent;
    public float baseSpeed;
    public MrXCollider colision;

    public event Action OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        colision.OnCollide += Colision_OnCollide;
        agent.speed = baseSpeed;
    }

    private void Colision_OnCollide()
    {
        OnDeath?.Invoke();
    }
}
