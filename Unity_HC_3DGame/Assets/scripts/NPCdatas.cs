using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateNPC
{
    NoMission, InMission, Finished
}

[CreateAssetMenu(fileName = "NPC 資料", menuName = "NPC 資料")]
public class NPCdatas : ScriptableObject
{
    [Tooltip("打字速度"), Range(0f, 3f)]
    public float typeSpeed = 1f;
    [Tooltip("打字音效")]
    public AudioClip typeSound;
    [Tooltip("任務需求數量"), Range(5, 50)]
    public int missionRequire = 30;
    [Tooltip("對話"), TextArea(3, 10)]
    public string[] dialogs = new string[3];
    [Tooltip("NPC State")]
    public StateNPC state;
}