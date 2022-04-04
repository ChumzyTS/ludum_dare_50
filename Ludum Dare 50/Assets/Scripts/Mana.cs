using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float amount;
    public float max;

    public GameObject manaBar;
    public GameObject manaBack;

    // Start is called before the first frame update
    void Start()
    {
        amount = max;
    }

    // Update is called once per frame
    public void AddMana(float addAmount)
    {
        amount += addAmount;
        amount = Mathf.Clamp(amount, 0, max);

        UpdateBar();
    }


    public void UpdateBar()
    {
        float manaPerc = amount / max;

        manaBar.transform.localScale = new Vector3(manaPerc, manaPerc, 0);
    }
}
