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
        if (!(Camera.main is null))
        {
            var t = Camera.main.transform;
            var right = Input.GetAxis("Horizontal")*t.right;
            var forward = Input.GetAxis("Vertical")*t.forward;
            var force = right + forward;
            force.y = 0.0f;
            _rigidbody.AddForce(10*force);    
        }
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