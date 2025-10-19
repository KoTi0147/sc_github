using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // 최대 HP
    private int currentHealth; // 현재 HP
    public GameOverManager gameOverManager; // GameOverManager 연결

    void Start()
    {
        currentHealth = maxHealth; // HP 초기화
    }

    void Update()
    {
        // 예시: 플레이어가 데미지를 입었을 때
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 바를 누르면 데미지를 입음
        {
            TakeDamage(20); // 20만큼 데미지
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage; // HP 감소
        if (currentHealth <= 0)
        {
            currentHealth = 0; // HP가 0 이하로 떨어지면 0으로 설정
            gameOverManager.GameOver(); // 게임 오버 호출
        }
    }
}
