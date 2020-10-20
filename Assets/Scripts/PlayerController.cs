using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public List<GameObject> ActiveWhenFinish = new List<GameObject>();
    
    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        foreach (var obj in ActiveWhenFinish)
        {
            obj.SetActive(false);
        }
    }

    void Update() // 毎フレーム呼ばれる
    {
        
        var force = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        _rigidbody.AddForce(5*force);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Finish");
            foreach (var obj in ActiveWhenFinish)
            {
                obj.SetActive(true);
            }
        }
    }

}