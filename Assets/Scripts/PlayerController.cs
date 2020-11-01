using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public bool quickStart = true;
    public bool quickFinish = true;
    public List<GameObject> ActiveWhenStart;
    public List<GameObject> ActiveWhenFinish;
    
    public bool isPlaying = true;
    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if(quickStart) PlayerStart();
        foreach (var elem in ActiveWhenStart) elem.SetActive(true);
        foreach (var elem in ActiveWhenFinish) elem.SetActive(false);   
    }

    void Update() // 毎フレーム呼ばれる
    {
        if (!(Camera.main is null) && isPlaying)
        {
            var t = Camera.main.transform;
            var right = Input.GetAxis("Horizontal")*t.right;
            var forward = Input.GetAxis("Vertical")*t.forward;
            var force = right + forward;
            force.y = 0.0f;
            _rigidbody.AddForce(10*force);    
        }
    }

    public void PlayerStart()
    {
        Debug.Log("PlayerStart");
        isPlaying = true;
        if (GameInstance.Instance) GameInstance.Instance.PlayerStart();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            isPlaying = false;
            foreach (var elem in ActiveWhenFinish) elem.SetActive(true);
            if(quickFinish) PlayerFinish();
        }
    }
    public void PlayerFinish()
    {
        Debug.Log("PlayerFinish");
        if (GameInstance.Instance) GameInstance.Instance.OnSceneFinish(gameObject.scene);
    }
}