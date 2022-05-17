using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPhysicNonAlloc : MonoBehaviour
{
    [SerializeField] float distance = 5f;
    [SerializeField] RaycastHit[] hits = new RaycastHit[10];
    [SerializeField] LayerMask mask;
    [SerializeField] List<GameObject> listHits = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        listHits.Clear();
        Ray ray = new Ray(transform.position, transform.right);
        var hitCount = Physics.RaycastNonAlloc(ray, hits, distance, mask);
        Debug.DrawRay(transform.position, transform.right * distance, Color.green);
        for (int i = 0; i < hitCount; i++)
        {
            Debug.Log($"Hit: {hits[i].collider.name}");
            listHits.Add(hits[i].collider.gameObject);
        }
    }
}
