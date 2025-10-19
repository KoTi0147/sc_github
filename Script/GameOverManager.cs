using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // TextMeshPro 관련 네임스페이스 추가

public class GameOverManager : MonoBehaviour
{
    // UI 요소들
    public float timeToNextScene = 2f; // 타이틀 화면으로 넘어가는 시간 (초)
    public TextMeshProUGUI gameOverText;  // TextMeshProUGUI로 변경

    private bool isGameOver = false; // 게임 오버 상태 체크

    void Start()
    {
        // 게임 오버 텍스트는 처음에 숨깁니다.
        gameOverText.gameObject.SetActive(false);
    }

    // 게임 오버 함수 (HP가 0이 될 때 호출)
    
    public void GameOver()
    {
        if (!isGameOver) // 게임 오버 중복 호출 방지
        {
            isGameOver = true; // 중복 호출 방지
            ShowGameOverText(); // 게임 오버 텍스트 표시
            Invoke("LoadTitleScene", timeToNextScene); // 2초 후 타이틀 씬으로 전환
        }
    }

    // 게임 오버 텍스트를 화면에 표시하는 함수
    private void ShowGameOverText()
    {
        gameOverText.gameObject.SetActive(true); // 게임 오버 텍스트 보이기
    }

    // 타이틀 화면으로 전환하는 함수
    private void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene"); // "TitleScene" 씬으로 전환
    }
}