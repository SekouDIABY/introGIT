using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    private float vie;
    public void TakeDamage(int degats)
    {
        vie -= degats;
        if (vie <= 0) Destroy(gameObject);
    }
}
