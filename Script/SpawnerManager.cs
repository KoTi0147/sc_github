using System.Collections;
using UnityEngine;
using TMPro;
using Playniax.Pyro; // ← SimpleSpawner 인식 위해 추가

public class SpawnerManager : MonoBehaviour
{
    public SimpleSpawner[] spawnerSequence;
    public TextMeshProUGUI messageText;
    public float delayBetweenWaves = 2f;
    public string[] waveNames;

    void Start()
    {
        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        for (int i = 0; i < spawnerSequence.Length; i++)
        {
            SimpleSpawner spawner = spawnerSequence[i];

            if (spawner == null) continue;

            if (messageText != null && waveNames != null && i < waveNames.Length)
            {
                messageText.text = $"{waveNames[i]} 등장!";
            }

            yield return new WaitForSeconds(delayBetweenWaves);

            if (messageText != null)
            {
                messageText.text = "";
            }

            spawner.enabled = true;

            yield return new WaitUntil(() => spawner.enabled == false);
        }
    }
}
