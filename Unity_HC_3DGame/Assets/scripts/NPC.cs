using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public NPCdatas data;
    public GameObject panel;
    public Text textName;
    public Text textContent;
    public RectTransform missionRect;
    public GameObject missionPoint;

    AudioSource aud;
    Animator ani;
    PlayerControl player;

    IEnumerator Type()
    {
        textContent.text = "";
        player.isStop = true;
        string dialog = data.dialogs[(int)data.state];

        for(int i=0; i < dialog.Length; i++)
        {            
            aud.PlayOneShot(data.typeSound, 0.5f);
            textContent.text += dialog[i];
            yield return new WaitForSeconds(data.typeSpeed/10);
        }

        player.isStop = false;
        NoMission();
    }

    IEnumerator ShowMission()
    {
        while(missionRect.anchoredPosition.x > 0)
        {
            missionRect.anchoredPosition -= new Vector2(1000 * Time.deltaTime, 0);
            yield return null;
        }
    }

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerControl>();
        ani = GetComponent<Animator>();
        data.state = 0;
    }
    void NoMission()
    {
        if (data.state != StateNPC.NoMission) return;
        data.state = StateNPC.InMission;
        missionPoint.SetActive(true);
        StartCoroutine(ShowMission());
    }

    void InMission()
    {

    }

    public void Finish()
    {
        data.state = StateNPC.Finished;
        ani.SetBool("isDancing", true);
    }

    void DialogStart()
    {
        panel.SetActive(true);
        textName.text = name;
        StartCoroutine(Type());
        ani.SetBool("isTalking", true);
    }

    void DialogStop()
    {
        panel.SetActive(false);
        ani.SetBool("isTalking", false);
    }

    void LookAtPlayer(Collider cl)
    {
        Quaternion angle = Quaternion.LookRotation(cl.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * 5);
    }

    private void OnTriggerEnter(Collider cl)
    {
        if (cl.name == "Hero")
        {
            DialogStart();
        }
    }
    private void OnTriggerExit(Collider cl)
    {
        if (cl.name == "Hero")
        {
            DialogStop();
        }
    }

    void OnTriggerStay(Collider cl)
    {
        if (cl.name == "Hero")
        {
            LookAtPlayer(cl);
        }
    }
}
