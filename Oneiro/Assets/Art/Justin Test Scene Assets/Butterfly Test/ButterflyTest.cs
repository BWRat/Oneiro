using System.Collections;
using UnityEngine;

public class ButterflyTest : MonoBehaviour
{

    public Animator[] anim;
    public string nameOfTrigger;
    public float randomMin;
    public float randomMax;

    private void OnGUI()
    {
        if (GUILayout.Button("Trigger Animation"))
            StartCoroutine(TriggerButterflies());
    }

    public IEnumerator TriggerButterflies()
    {
        foreach (Animator a in anim)
        {
            a.SetTrigger(nameOfTrigger);
            yield return new WaitForSeconds(Random.Range(randomMin, randomMax));
        }
    }
}