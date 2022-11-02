using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{

    public GameObject Zumbi;

    private float contadorTempo = 0;

    public float TempoGerarZumbi = 1;

    // Update is called once per frame
    void Update()
    {
        contadorTempo += Time.deltaTime;
        if (contadorTempo >= TempoGerarZumbi)
        {
            contadorTempo = 0;
            Instantiate(Zumbi, transform.position, transform.rotation);
        }
    }
}
