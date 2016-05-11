using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;

    public CameraShake cameraShake;


    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        }
    }

    void Start()
    {
        if (cameraShake == null)
        {
            Debug.LogError("No camera shake referenced in game master");
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

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }
    public void _KillEnemy(Enemy _enemy)
    {
        GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 3f);
        cameraShake.Shake(_enemy.shakeAmount, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }
}
