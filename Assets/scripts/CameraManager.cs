using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    static public CameraManager instance;

    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed;
    private Vector3 targetPosition;

    public BoxCollider2D bound;
    private Vector3 minBound;
    private Vector3 maxBound;

    //박스컬라이더 영역의 최소 최대 xyz값을 지님.

    private float halfWidth;
    private float halfHeight;

    private Camera theCamera;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);

        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {

        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }
	
	// Update is called once per frame
	void Update () {
		
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);
            
            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }

    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    } //camera bound.(for focusing)

    public  float GetHalf()
    {
        return this.transform.position.x;
    }
}
