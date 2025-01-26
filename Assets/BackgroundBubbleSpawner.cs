using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class BackgroundBubbleSpawner : MonoBehaviour
{
    [SerializeReference] GameObject bubblePrefab;

    float spawnFrequency = 0;
    float timer = 0.0f;

    [SerializeField] float alpha = 0.9f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        spawnFrequency = spawnFrequency * (alpha) + AudioManager.instance.samples.AsEnumerable<float>().Average() * 4000 * (1 - alpha);

        timer += Time.deltaTime;

        if (timer > 1 / spawnFrequency)
        {
            SpawnBubble();
            timer = 0.0f;
        }

    }

    private void SpawnBubble()
    {
        Vector3 pos = transform.position + new Vector3(Random.Range(-9.0f, 9.0f), 0.0f, 0.0f);
        Instantiate(bubblePrefab, pos, transform.rotation);
    }
}
