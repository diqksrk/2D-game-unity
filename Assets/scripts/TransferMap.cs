using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour {

    public string transferMapName; //이동할 맵 이름

    public Transform target;
    public BoxCollider2D targetBound;

    public Animator anim_1;
    public Animator anim_2;

    public int door_count;

    [Tooltip("UP,DOWN, LEFT,RIGHT")]
    public string direction;  //캐릭터가 바로보고 있는 방향.
    private Vector2 vector;

    [Tooltip("문이 열린다 : true, 문이 없으면 : false")]
    public bool door;

    private PlayerManager thePlayer;
    private CameraManager theCamera;
    private FadeManager theFade;
    private orderManager theOrder;

    // Use this for initialization
    void Start () {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<orderManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!door)
        {
            if (collision.gameObject.name == "Player")
            {
                StartCoroutine(TransferCoroutine());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (door)
        {
            if (collision.gameObject.name == "Player")
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    vector.Set(thePlayer.animator.GetFloat("Dirx"), thePlayer.animator.GetFloat("Diry"));
                    switch (direction)
                    {
                        case "UP":
                            if (vector.y==1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "DOWN":
                            if (vector.y == -1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "RIGHT":
                            if (vector.x == 1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "LEFT":
                            if (vector.x == -1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        default:
                            StartCoroutine(TransferCoroutine());
                            break;
                    }
                }
            }
        }
    }

    IEnumerator TransferCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        theFade.FadeOut();
        if (door)
        {
            anim_1.SetBool("Open", true);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", true);
            }
        }
        yield return new WaitForSeconds(0.3f);
        theOrder.SetTransparent("Player");
        if (door)
        {
            anim_1.SetBool("Open", false);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", false);
                Debug.Log(anim_2.GetBool("Open").ToString());
            }
        }
        yield return new WaitForSeconds(0.7f);
        theOrder.SetUnTransparent("Player");
        thePlayer.currentMapName = transferMapName;
        theCamera.SetBound(targetBound);
        //SceneManager.LoadScene(transferMapName);
        theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        thePlayer.transform.position = target.transform.position;
        theFade.FadeIn();
        yield return new WaitForSeconds(0.5f);
        theOrder.Move();
    }
}
