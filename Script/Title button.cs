using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    // 게임 시작 버튼 클릭 시 호출되는 함수
    public void OnStartButtonClicked()
    {
        // 게임의 첫 번째 씬으로 전환
        SceneManager.LoadScene("Project_Retro1.04");
    }
}