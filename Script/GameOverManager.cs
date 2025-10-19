using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // TextMeshPro ���� ���ӽ����̽� �߰�

public class GameOverManager : MonoBehaviour
{
    // UI ��ҵ�
    public float timeToNextScene = 2f; // Ÿ��Ʋ ȭ������ �Ѿ�� �ð� (��)
    public TextMeshProUGUI gameOverText;  // TextMeshProUGUI�� ����

    private bool isGameOver = false; // ���� ���� ���� üũ

    void Start()
    {
        // ���� ���� �ؽ�Ʈ�� ó���� ����ϴ�.
        gameOverText.gameObject.SetActive(false);
    }

    // ���� ���� �Լ� (HP�� 0�� �� �� ȣ��)
    
    public void GameOver()
    {
        if (!isGameOver) // ���� ���� �ߺ� ȣ�� ����
        {
            isGameOver = true; // �ߺ� ȣ�� ����
            ShowGameOverText(); // ���� ���� �ؽ�Ʈ ǥ��
            Invoke("LoadTitleScene", timeToNextScene); // 2�� �� Ÿ��Ʋ ������ ��ȯ
        }
    }

    // ���� ���� �ؽ�Ʈ�� ȭ�鿡 ǥ���ϴ� �Լ�
    private void ShowGameOverText()
    {
        gameOverText.gameObject.SetActive(true); // ���� ���� �ؽ�Ʈ ���̱�
    }

    // Ÿ��Ʋ ȭ������ ��ȯ�ϴ� �Լ�
    private void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene"); // "TitleScene" ������ ��ȯ
    }
}