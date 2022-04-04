using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    public Sprite[] cracks;
    public Vector2[] crackOffsets;
    public Vector2[] crackScales;

    private void Start()
    {
        int pickedCrackInd = Random.Range(0, crackOffsets.Length);


        gameObject.GetComponent<SpriteMask>().sprite = cracks[pickedCrackInd];
        transform.localPosition = crackOffsets[pickedCrackInd];
        transform.localScale = crackScales[pickedCrackInd];
    }
}
