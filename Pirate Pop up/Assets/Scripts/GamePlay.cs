using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour
{
    int chosenOne;
    [SerializeField]
    Transform[] holePos;

    [SerializeField]
    float offset;

    [SerializeField]
    GameObject swordPrefab;

    [SerializeField]
    GameObject explosionVFXPrefab;

    [SerializeField]
    Transform explosionPosition;

    [SerializeField]
    Rigidbody pirateHead;

    [SerializeField]
    float explosionRange;

    [SerializeField]
    GameObject UI;

    [SerializeField]
    SwordDrawer sD;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    float duration;

    Vector3 origin, endpoint;

    float elapsedTime;

    [SerializeField]
    AudioSource explosionSource;

    [SerializeField]
    AudioSource clickSource;
    // Start is called before the first frame update
    void Start()
    {
        chosenOne = Random.Range(1, 12);
    }

    public void SpawnSword(int index)
    {
        GameObject sword = Instantiate(swordPrefab, holePos[index - 1]);
        sword.transform.position -= offset * sword.transform.forward;
        origin = sword.transform.position;
        endpoint = holePos[index - 1].position;
        elapsedTime = 0;
        StartCoroutine(PlayAnimation(index, sword.transform));
        //disable UI
        if(sD.isDragging)
        {
            sD.Return();
        }
        UI.SetActive(false);
    }

    IEnumerator PlayAnimation(int hole, Transform sword)
    {
        while (elapsedTime < duration)
        {
            sword.position = Vector3.Lerp(origin, endpoint, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        CheckHole(hole);
    }

    void CheckHole(int index)
    {
        if (index == chosenOne)
        {
            //game over
            Instantiate(explosionVFXPrefab, explosionPosition.position, explosionPosition.rotation);
            Vector3 direction = new Vector3(Random.Range(-1, 1), 1, Random.Range(-1, 1));
            pirateHead.AddForce(explosionRange * direction, ForceMode.Impulse);
            gameOverPanel.SetActive(true);
            explosionSource.Play();
        }
        else
        {
            //next turn
            clickSource.Play();
            UI.SetActive(true);
        }
    }
}
