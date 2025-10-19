// DialogueManager.cs
using UnityEngine;
using TMPro; // TextMeshPro ���
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // �� ������ ���� �߰�

public class DialogueManager : MonoBehaviour
{
    // --- UI ���� ���� ---
    [Header("UI Components")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    public GameObject dialoguePanel; // ��ȭâ ��� �г�
    public GameObject continueIcon; // ���� ��� ǥ�� ������

    // --- ��ȭ ������ ���� ---
    [Header("Dialogue Data")]
    public DialogueData currentDialogueData; // ���� ������ ��ȭ ������ (ScriptableObject)
    public float typingSpeed = 0.05f; // ���� ��� �ӵ�

    private int currentLineIndex = 0; // ���� ���� ���� ��ȭ �� �ε���
    private bool isTyping = false; // ���� Ÿ������ ���� ������ ����

    void Start()
    {
        // �� �ε� ��, ��ȭ�� ��� ���� (GameManager�κ��� �����͸� �޾ƿԴٰ� ����)
        // ���⼭�� �׽�Ʈ�� ���� �ν����Ϳ� �Ҵ�� currentDialogueData�� ����մϴ�.
        if (currentDialogueData != null)
        {
            StartDialogue(currentDialogueData);
        }
    }

    // �ܺο��� ��ȭ�� ������ �� ȣ��˴ϴ�. (��: GamePlay ������ ������ ���޹޾� ȣ��)
    public void StartDialogue(DialogueData data)
    {
        currentDialogueData = data;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true); // ��ȭ �г� Ȱ��ȭ
        continueIcon.SetActive(false); // �ʱ⿡�� ������ ��Ȱ��ȭ
        DisplayNextLine();
    }

    // ���� ��ȭ ���� ȭ�鿡 ǥ���մϴ�.
    private void DisplayNextLine()
    {
        if (isTyping)
        {
            // Ÿ���� ���̶��, Ÿ������ ��� �Ϸ�
            StopAllCoroutines();
            dialogueText.text = currentDialogueData.dialogueLines[currentLineIndex].dialogueText;
            isTyping = false;
            continueIcon.SetActive(true); // Ÿ���� �Ϸ� �� ������ Ȱ��ȭ
            return;
        }

        // ��� ��ȭ�� ������ ���
        if (currentLineIndex >= currentDialogueData.dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        // ���� ��� ���� ��������
        DialogueLine line = currentDialogueData.dialogueLines[currentLineIndex];

        // UI ������Ʈ
        nameText.text = line.characterName;
        portraitImage.sprite = line.portrait;
        portraitImage.gameObject.SetActive(line.portrait != null); // �ʻ�ȭ�� ������ Ȱ��ȭ

        // Ÿ���� ȿ�� ����
        dialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        continueIcon.SetActive(false); // Ÿ���� �߿��� ������ ��Ȱ��ȭ
        StartCoroutine(TypeSentence(line.dialogueText));

        currentLineIndex++; // ���� ��縦 ���� �ε��� ����
    }

    // --- �ٽ� ���: Ÿ���� ȿ�� ���� ---
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        continueIcon.SetActive(true); // Ÿ���� �Ϸ�
    }

    // ��ȭ ����
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false); // ��ȭ �г� ��Ȱ��ȭ
        // TODO: ���� �÷��� ������ ���ư��� ���� ���� (��: SceneManager.LoadScene("GamePlayScene"))
        Debug.Log("Dialogue Ended. Returning to GamePlay Scene.");
        // ����: ���� ������ ���ư���
        // SceneManager.LoadScene("GamePlayScene");
    }

    // ���콺 �Է� �Ǵ� Ű �Է��� �����Ͽ� ���� ���� �ѱ�ϴ�.
    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0)) // ��ȭ �г��� Ȱ��ȭ�Ǿ� �ְ� ���콺 ���� ��ư Ŭ�� ��
        {
            DisplayNextLine();
        }
    }
}