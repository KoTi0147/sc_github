// DialogueData.cs
using UnityEngine;
using System;

[Serializable] // �ν����� â���� ���� �����ϵ��� ����
public class DialogueLine
{
    // [Tooltip("��縦 �� ĳ������ �̸��Դϴ�.")]
    public string characterName;

    // [Tooltip("���� ��翡�� ����� ĳ���� �ʻ�ȭ�Դϴ�.")]
    public Sprite portrait;

    [TextArea(3, 10)] // �ν����Ϳ��� ���� �� �Է� �����ϵ��� ����
    // [Tooltip("���� ��µ� ��� �����Դϴ�.")]
    public string dialogueText;
}

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}
