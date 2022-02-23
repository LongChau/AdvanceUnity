using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SelectableCube : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log($"Select {gameObject.name}");
        EmitParams param = new EmitParams();
        param.position = transform.position;
        _particleSystem.Emit(param, 1);
    }
}
