using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Water")]
    public Image waterBar;
    float water;
    public float maxWater;
    public float waterDown;
    public float timeWaitAfterDrink;

    [Header("Piss")]
    public Image pissBar;
    float piss;
    public float maxPiss;
    public float pissUp, pissDown;
    public float timeWaitAfterPiss;

    bool piGrow, waterGoes, canPiss, pissing;

    bool triggerWater, triggerPiss;
    // Start is called before the first frame update
    void Start()
    {
        piss = 0;
        waterGoes = false;
        canPiss = false;
        piGrow = false;
    }

    // Update is called once per frame
    void Update()
    {
        waterBar.fillAmount = water / maxWater;

        pissBar.fillAmount = piss / maxPiss;

        if (Input.GetKeyDown(KeyCode.Space) && water <=0 && triggerWater == true)
        {
            StartCoroutine(drinkWater());
        }

        if(piGrow)
        {
            PissUpBehaviour();
        }

        if(Input.GetKeyDown(KeyCode.Space) && canPiss && triggerPiss == true)
        {
            pissing = true;
        }

        if(pissing == true)
        {
            PissOut();
        }

        if(waterGoes)
        {
            WaterDownBehaviour();
        }
    }

    void PissOut()
    {
        piss -= pissDown * Time.deltaTime;
        if (piss <= 0)
        {
            StartCoroutine(pissWater());
        }
    }
    void PissUpBehaviour()
    {
        if (piss < maxPiss)
        {
            piss += pissUp * Time.deltaTime;
        }

        if (piss >= maxPiss)
        {
            piss = maxPiss;
            piGrow = false;
            canPiss = true;
        }
    }

    void WaterDownBehaviour()
    {
        water -= waterDown * Time.deltaTime;
        if(water <= 0)
        {
            water = 0;
            waterGoes = false;
        }
    }

    IEnumerator pissWater()
    {
        piss = 0;
        pissing = false;
        canPiss = false;
        yield return new WaitForSeconds(timeWaitAfterPiss);
        waterGoes = true;
    }

    IEnumerator drinkWater()
    {
        water = maxWater;
        yield return new WaitForSeconds(timeWaitAfterDrink);
        piGrow = true;

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            triggerWater = true;
        }

        if (other.CompareTag("Piss"))
        {
            triggerPiss = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        triggerPiss = false;
        triggerWater = false;
    }
}
