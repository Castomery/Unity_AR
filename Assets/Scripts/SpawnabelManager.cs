using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SpawnabelManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    [SerializeField]
    List<GameObject> spawnablePrefab = new List<GameObject>();

    private Camera _arCam;
    private GameObject _spawnedObject;

    [SerializeField]
    private Button _panel;

    [SerializeField]
    private UIManager _uIManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnedObject = null;
        _arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        _uIManager.Initialize(spawnablePrefab.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = _arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && _spawnedObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Spawnable")
                    {
                        _spawnedObject = hit.collider.gameObject;
                    }
                    else
                    {
                        SpawnPrefab(m_Hits[0].pose.position);
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && _spawnedObject != null)
            {
                _spawnedObject.transform.position = m_Hits[0].pose.position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _spawnedObject = null;
            }
        }
    }

    private void SpawnPrefab(Vector3 spawnPos)
    {
        if (_spawnedObject != null)
        {
            Destroy(_spawnedObject);
            _spawnedObject = null;
        }

        _spawnedObject = Instantiate(spawnablePrefab[_uIManager.currentIndexValue], spawnPos, Quaternion.identity);
    }
}
