using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public UnityEngine.UI.Text DebugText;

    // Update is called once per frame
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    void Update()
    {
        var acc = Input.gyro.rotationRate;
        var f = new Vector3(acc.y, 0.0f, -acc.x);
        _rigidbody.AddForce(20.0f*f);
        DebugText.text = $"{acc.x}\n" +
                         $"{acc.y}\n" +
                         $"{acc.z}";
    }
}
