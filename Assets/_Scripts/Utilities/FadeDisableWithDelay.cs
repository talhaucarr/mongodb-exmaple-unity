using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeDisableWithDelay : MonoBehaviour
{
    public float delay;
    [SerializeField] private AnimationCurve curve;
    private float counter = 0;

    private void OnEnable()
    {
        counter = 0;
    }

    public void Update()
    {
        counter += Time.deltaTime;
        if (counter >= delay) { gameObject.SetActive(false); return; }

        float alpha = Mathf.Sqrt(curve.Evaluate(1 - (counter / delay)));

        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer selfSr))
        {
            selfSr.color = new Color(selfSr.color.r, selfSr.color.g, selfSr.color.b, alpha);
        }

        foreach (Transform child in transform)
        {
            if(child.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            }
        }
    }
}
