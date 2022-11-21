using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Income : MonoBehaviour
{
    public Income(float cash)
    {
        Cash = (int)cash;
    }

    public Income(float cash, float time)
    {
        Cash = (int)cash;
        StartCoroutine(AddCash());
    }

    IEnumerator AddCash()
    {
        Income income = new Income(10, 5);
        yield return new WaitForSeconds(5);
        StartCoroutine(AddCash());
    }
    // Chcialem to zrobic w ten sposob ze tworzac budynek tworze nowy income, ktory co 5s sie odpala i dodaje 10 do przychodow


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Cash { get; set; }
}