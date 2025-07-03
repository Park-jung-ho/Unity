using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PortalController : MonoBehaviour
{
    public int SceneIndex;
    public GameObject effect;
    public GameObject loadingImage;
    public FadeRoutine fade;
    public TMP_Text loadingText;
    public Image progressBar;
    public float loadingDuration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PortalRoutine());
        }
    }

    IEnumerator PortalRoutine()
    {
        effect.SetActive(true);
        yield return StartCoroutine(fade.Fade(2f,Color.white, true));
        loadingImage.SetActive(true);
        effect.SetActive(false);
        progressBar.fillAmount = 0;
        yield return StartCoroutine(fade.Fade(1f,Color.white, false));
        float percent = 0;
        while (progressBar.fillAmount < 1f)
        {
            percent += Time.deltaTime / loadingDuration;
            percent += Random.Range(0.001f,0.02f);
            loadingText.text = (percent * 100).ToString("00.0") + "%";
            progressBar.fillAmount = percent;
            yield return null;
        }
        loadingText.text = "100%";
        progressBar.fillAmount = 1;
        SceneManager.LoadScene(SceneIndex);
    }
}
