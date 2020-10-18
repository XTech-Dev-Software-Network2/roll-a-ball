using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DebugPlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public UnityEngine.UI.Text DebugText;
    
    private void Awake() // 最初に1回呼ばれる
    {
        _rigidbody = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    void Update() // 毎フレーム呼ばれる
    {
        var rotRate = Input.gyro.rotationRate;
        DebugText.text = rotRate.ToString();
        var force = new Vector3(rotRate.y, 0.0f, -rotRate.x);
        _rigidbody.AddForce(20*force);
    }
}
