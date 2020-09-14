using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Tooltip("移動速度"), Range(0, 1000)]
    public float speed = 5;
    [Tooltip("旋轉速度"), Range(0, 1000)]
    public float turn = 5;
    [Tooltip("攻擊力"), Range(0, 1000)]
    public float attack = 20;
    [Tooltip("血量"), Range(0, 1000)]
    public float hp = 250;
    [Tooltip("魔力"), Range(0, 1000)]
    public float mp = 50;

    public float exp;
    public int lv = 1;

    public Text missionText;
    public AudioClip getSound;

    private Animator anim;
    private Rigidbody rig;
    private Transform cam;
    private AudioSource aud;
    private NPC npc;

    private int getCount = 0;

    [HideInInspector]
    public bool isStop = false;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        rig = this.GetComponent<Rigidbody>();
        aud = this.GetComponent<AudioSource>();
        npc = FindObjectOfType<NPC>();
        cam = GameObject.Find("攝影機根物件").transform;
    }
    void GetProp()
    {
        getCount ++;
        missionText.text = "牙齒:" + getCount + " / "+npc.data.missionRequire;

        if(getCount >= npc.data.missionRequire)
        {
            npc.Finish();
        }
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 pos = cam.forward * v + cam.right * h;
        if(h!=0 || v!=0)
            transform.forward = Vector3.Lerp(transform.forward, cam.forward, 0.5f * turn * Time.deltaTime);
        rig.MovePosition(transform.position + pos * speed * Time.deltaTime);
        print(pos);
        anim.SetFloat("move", Mathf.Sqrt(h * h + v * v));
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        if (isStop)
        {
            anim.SetFloat("move", 0);
            return;
        }
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "skull")
        {
            aud.PlayOneShot(getSound);
            Destroy(other.gameObject);
            GetProp();
        }
    }
}
