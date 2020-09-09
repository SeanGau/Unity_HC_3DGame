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

    AudioSource aud;
    IEnumerator Type()
    {
        textContent.text = "";
        string dialog = data.dialogs[0];

        for(int i=0; i < dialog.Length; i++)
        {            
            aud.PlayOneShot(data.typeSound, 0.5f);
            textContent.text += dialog[i];
            yield return new WaitForSeconds(data.typeSpeed/10);
        }
    }

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }
    void NoMission()
    {

    }

    void InMission()
    {

    }

    void Finish()
    {

    }

    void DialogStart()
    {
        panel.SetActive(true);
        textName.text = name;
        StartCoroutine(Type());
    }

    void DialogStop()
    {
        panel.SetActive(false);
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
