using UnityEngine;
using UnityEngine.UI;

public class ScoreMeterLogic : MonoBehaviour
{
    [SerializeField] private int distance = 0;
    [SerializeField] private float delayBeforeRun;
    [SerializeField] private float intervalSeconds = 0.5f; // how often distance increases
    [SerializeField] private int metersPerTick = 1;

    private Text distanceText;

    void Start()
    {
        distanceText = GetComponent<Text>();
        UpdateText();
        StartCoroutine(DistanceCounter());
    }

    private System.Collections.IEnumerator DistanceCounter()
    {
        yield return new WaitForSeconds(delayBeforeRun);

        while (true)
        {
            yield return new WaitForSeconds(intervalSeconds);
            distance += metersPerTick;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        distanceText.text = $"Distance: {distance}m";
    }
}
