using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() // 毎フレーム呼ばれる
    {
        
        var force = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        _rigidbody.AddForce(5*force);
    }
}