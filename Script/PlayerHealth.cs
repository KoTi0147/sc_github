using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // �ִ� HP
    private int currentHealth; // ���� HP
    public GameOverManager gameOverManager; // GameOverManager ����

    void Start()
    {
        currentHealth = maxHealth; // HP �ʱ�ȭ
    }

    void Update()
    {
        // ����: �÷��̾ �������� �Ծ��� ��
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽� �ٸ� ������ �������� ����
        {
            TakeDamage(20); // 20��ŭ ������
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage; // HP ����
        if (currentHealth <= 0)
        {
            currentHealth = 0; // HP�� 0 ���Ϸ� �������� 0���� ����
            gameOverManager.GameOver(); // ���� ���� ȣ��
        }
    }
}
