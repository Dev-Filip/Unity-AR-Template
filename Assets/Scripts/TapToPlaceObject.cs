using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlaceObject : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_raycast;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject spawnObject;
    public GameObject invisPanel;

    private GameObject lastSpawned;
    

    private bool objectSpawned;

    private bool rotateLeft;
    private bool rotateRight;
    private bool scaleUp;
    private bool scaleDown;

    private bool menuDisplayed;

    private float rotateSpeed = 50f;
    private float scaleSpeed = 0.1f;

    public void TogglePanel()
    {
        if (menuDisplayed == false)
        {
            invisPanel.SetActive(true);
            menuDisplayed = true;
        }
        else
        {
            invisPanel.SetActive(false);
            menuDisplayed = false;
        }
       

    }
    public void RotateLeft()
    {
        rotateLeft = true;
    }

    public void RotateReleaseLeft()
    {
        rotateLeft = false;
    }

    public void RotateRight()
    {
        rotateRight = true;
    }

    public void RotateReleaseRight()
    {
        rotateRight = false;
    }

    public void ScaleUp()
    {
        scaleUp = true;
    }

    public void ReleaseScaleUp()
    {
        scaleUp = false;
    }

    public void ScaleDown()
    {
        scaleDown = true;
    }

    public void ReleaseScaleDown()
    {
        scaleDown = false;
    }

    void Start()
    {
        objectSpawned = false;
        menuDisplayed = false;
    }

    public void DestroyObject()
    {
        Destroy(lastSpawned);
        objectSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateLeft)
        {
            lastSpawned.transform.Rotate(Vector3.up,rotateSpeed * Time.deltaTime);
        }

        if (rotateRight)
        {
            lastSpawned.transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
        }

        if (scaleUp)
        {
            lastSpawned.transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
        }

        if (scaleDown)
        {
            lastSpawned.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject())
            {
                if (m_raycast.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon) && objectSpawned == false)
                {
                    Pose hitpose = hits[0].pose;

                    lastSpawned = Instantiate(spawnObject, hitpose.position, hitpose.rotation);

                    objectSpawned = true;

                }
            }
          
        
        }
    }
}
