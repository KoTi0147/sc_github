// DialogueData.cs
using UnityEngine;
using System;

[Serializable] // 인스펙터 창에서 편집 가능하도록 설정
public class DialogueLine
{
    // [Tooltip("대사를 할 캐릭터의 이름입니다.")]
    public string characterName;

    // [Tooltip("현재 대사에서 사용할 캐릭터 초상화입니다.")]
    public Sprite portrait;

    [TextArea(3, 10)] // 인스펙터에서 여러 줄 입력 가능하도록 설정
    // [Tooltip("실제 출력될 대사 내용입니다.")]
    public string dialogueText;
}

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}
