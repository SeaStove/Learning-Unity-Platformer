using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        }
    }

    public IEnumerator RespawnPlayer()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);
        Transform spawnParticles = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(spawnParticles.gameObject,3);
    }
    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());

    }
}
