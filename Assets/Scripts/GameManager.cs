using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private int maxEnemyGenerateCount;

    [SerializeField, HideInInspector]
    private float generateInterval;

    [SerializeField, HideInInspector]
    private int generateGemCount;

    [FormerlySerializedAs("playerChara")] [SerializeField] 
    private PlayerController playerCharaPrefab;
    
    [SerializeField] 
    private Transform startTran;

    [SerializeField] 
    private CameraControllerFromCinemachine cameraController;
    
    private EnemyGenerator enemyGenerator;
    private GemGenerator gemGenerator;

    private int enemyGenerateCount;

    private int destroyEnemyCount;


    void Start()
    {
        if(TryGetComponent(out enemyGenerator)) {
            StartCoroutine(PrepareGenerateEnemy());
            StartCoroutine(ObserveEnemyCount());
        }

        if (TryGetComponent(out gemGenerator)) {
            gemGenerator.GenerateGems(generateGemCount);
        }

        UserData.instance.SetCurrentCharaData();
        playerCharaPrefab = Instantiate(DataBaseManager.instance.GetSkinData(UserData.instance.currentCharaData.id),
            startTran.position, Quaternion.identity);
        
        playerCharaPrefab.SetUpPlayer(cameraController);
        
        cameraController.SetTarget(playerCharaPrefab.gameObject);
    }

    /// <summary>
    /// ?G?l?~?[?????????
    /// </summary>
    /// <returns></returns>
    private IEnumerator PrepareGenerateEnemy() {

        float timer = 0;

        while (maxEnemyGenerateCount > enemyGenerateCount) {
            timer += Time.deltaTime;
            if (generateInterval <= timer) {
                timer = 0;
                enemyGenerateCount++;
                enemyGenerator.GenerateEnemy();
            }           
            yield return null;
        }
        Debug.Log("?G?l?~?[??????I??");
    }

    /// <summary>
    /// ?G?l?~?[??|???????????
    /// </summary>
    /// <returns></returns>
    private IEnumerator ObserveEnemyCount() {

        while (maxEnemyGenerateCount > destroyEnemyCount) {

            // TODO ?Q?[???X?e?[?g???????~?@?\????

            yield return null;
        }
        Debug.Log("?Q?[???N???A");
    }

    /// <summary>
    /// ?G?l?~?[??|???????????Z
    /// </summary>
    public void AddDestroyEnemyCount() {
        destroyEnemyCount++;
    }
}
