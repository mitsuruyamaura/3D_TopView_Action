using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int maxEnemyGenerateCount;

    [SerializeField]
    private float generateInterval;

    [SerializeField]
    private int generateGemCount;

    [SerializeField] 
    private PlayerController playerChara;
    
    [SerializeField] 
    private Transform startTran;
    
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
        playerChara = Instantiate(DataBaseManager.instance.GetSkinData(UserData.instance.currentCharaData.id),
            startTran.position, Quaternion.identity);
    }

    /// <summary>
    /// �G�l�~�[�̐�������
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
        Debug.Log("�G�l�~�[�̐����I��");
    }

    /// <summary>
    /// �G�l�~�[�̓|���ꂽ�����Ď�
    /// </summary>
    /// <returns></returns>
    private IEnumerator ObserveEnemyCount() {

        while (maxEnemyGenerateCount > destroyEnemyCount) {

            // TODO �Q�[���X�e�[�g�ɂ��ꎞ��~�@�\��ǉ�

            yield return null;
        }
        Debug.Log("�Q�[���N���A");
    }

    /// <summary>
    /// �G�l�~�[�̓|�����������Z
    /// </summary>
    public void AddDestroyEnemyCount() {
        destroyEnemyCount++;
    }
}
