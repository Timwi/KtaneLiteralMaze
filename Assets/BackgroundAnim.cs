using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnim : MonoBehaviour
{
    public KMBombModule Module;
    public Image BackgroundColourBottom;
    public Image BackgroundColourTop;
    public Image BarsA;
    public Image BarsB;
    public Image BarsC;
    public Image BarsD;

    void Start()
    {
        Module.OnActivate += delegate { StartCoroutine(BackgroundAnimRainbow()); StartCoroutine(BackgroundAnimBars()); };

        BarsA.transform.localScale = BarsB.transform.localScale = BarsC.transform.localScale = BarsD.transform.localScale = Vector3.zero;
        BackgroundColourBottom.color = BackgroundColourTop.color = Color.black;
    }

    private IEnumerator BackgroundAnimBars(float interval = 10f)
    {
        BarsA.transform.localScale = BarsB.transform.localScale = BarsC.transform.localScale = BarsD.transform.localScale = Vector3.one;

        while (true)
        {
            BarsA.transform.localEulerAngles = Vector3.forward * Mathf.Lerp(0, 360, (Time.time / interval) % 1);
            BarsB.transform.localEulerAngles = Vector3.forward * Mathf.Lerp(360, 0, (Time.time / interval) % 1);
            BarsC.transform.localEulerAngles = Vector3.forward * Mathf.Lerp(0, 360, ((Time.time / interval) + 0.25f) % 1);
            BarsD.transform.localEulerAngles = Vector3.forward * Mathf.Lerp(360, 0, ((Time.time / interval) + 0.25f) % 1);
            yield return null;
        }
    }

    private IEnumerator BackgroundAnimRainbow(float interval = 4f, float intensity = 0.25f)
    {
        interval *= Mathf.PI;   // To make things a little more "random".

        while (true)
        {
            var timer = ((Time.time * 6) / interval) % 6;

            var r = new[] { 1, 2 - timer, 0, 0, timer - 4, 1 }[Mathf.FloorToInt(timer)] * intensity;
            var g = new[] { timer, 1, 1, 4 - timer, 0, 0 }[Mathf.FloorToInt(timer)] * intensity;
            var b = new[] { 0, 0, timer - 2, 1, 1, 6 - timer }[Mathf.FloorToInt(timer)] * intensity;

            BackgroundColourBottom.color = BackgroundColourTop.color = new Color(r, g, b, 1);
            yield return null;
        }
    }
}
