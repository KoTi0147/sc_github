// DialogueManager.cs
using UnityEngine;
using TMPro; // TextMeshPro 사용
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // 씬 관리를 위해 추가

public class DialogueManager : MonoBehaviour
{
    // --- UI 연결 변수 ---
    [Header("UI Components")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    public GameObject dialoguePanel; // 대화창 배경 패널
    public GameObject continueIcon; // 다음 대사 표시 아이콘

    // --- 대화 데이터 변수 ---
    [Header("Dialogue Data")]
    public DialogueData currentDialogueData; // 현재 진행할 대화 데이터 (ScriptableObject)
    public float typingSpeed = 0.05f; // 글자 출력 속도

    private int currentLineIndex = 0; // 현재 진행 중인 대화 줄 인덱스
    private bool isTyping = false; // 현재 타이핑이 진행 중인지 여부

    void Start()
    {
        // 씬 로드 시, 대화를 즉시 시작 (GameManager로부터 데이터를 받아왔다고 가정)
        // 여기서는 테스트를 위해 인스펙터에 할당된 currentDialogueData를 사용합니다.
        if (currentDialogueData != null)
        {
            StartDialogue(currentDialogueData);
        }
    }

    // 외부에서 대화를 시작할 때 호출됩니다. (예: GamePlay 씬에서 데이터 전달받아 호출)
    public void StartDialogue(DialogueData data)
    {
        currentDialogueData = data;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true); // 대화 패널 활성화
        continueIcon.SetActive(false); // 초기에는 아이콘 비활성화
        DisplayNextLine();
    }

    // 다음 대화 줄을 화면에 표시합니다.
    private void DisplayNextLine()
    {
        if (isTyping)
        {
            // 타이핑 중이라면, 타이핑을 즉시 완료
            StopAllCoroutines();
            dialogueText.text = currentDialogueData.dialogueLines[currentLineIndex].dialogueText;
            isTyping = false;
            continueIcon.SetActive(true); // 타이핑 완료 후 아이콘 활성화
            return;
        }

        // 모든 대화가 끝났을 경우
        if (currentLineIndex >= currentDialogueData.dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        // 다음 대사 정보 가져오기
        DialogueLine line = currentDialogueData.dialogueLines[currentLineIndex];

        // UI 업데이트
        nameText.text = line.characterName;
        portraitImage.sprite = line.portrait;
        portraitImage.gameObject.SetActive(line.portrait != null); // 초상화가 있으면 활성화

        // 타이핑 효과 시작
        dialogueText.text = ""; // 텍스트 초기화
        continueIcon.SetActive(false); // 타이핑 중에는 아이콘 비활성화
        StartCoroutine(TypeSentence(line.dialogueText));

        currentLineIndex++; // 다음 대사를 위해 인덱스 증가
    }

    // --- 핵심 기능: 타이핑 효과 구현 ---
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        continueIcon.SetActive(true); // 타이핑 완료
    }

    // 대화 종료
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false); // 대화 패널 비활성화
        // TODO: 게임 플레이 씬으로 돌아가는 로직 구현 (예: SceneManager.LoadScene("GamePlayScene"))
        Debug.Log("Dialogue Ended. Returning to GamePlay Scene.");
        // 예시: 메인 씬으로 돌아가기
        // SceneManager.LoadScene("GamePlayScene");
    }

    // 마우스 입력 또는 키 입력을 감지하여 다음 대사로 넘깁니다.
    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0)) // 대화 패널이 활성화되어 있고 마우스 왼쪽 버튼 클릭 시
        {
            DisplayNextLine();
        }
    }
}