using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengesManager : MonoBehaviour
{
    public GameObject challenge1;
    public GameObject challenge2;
    public GameObject challenge3;

    void Start()
    {
        challenge1.SetActive(false);
        challenge2.SetActive(false);
        challenge3.SetActive(false);
    }
}
