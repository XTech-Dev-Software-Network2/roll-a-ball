using UnityEngine;

public class TextWave : MonoBehaviour
{
    private RectTransform _rectTransform;
    [SerializeField] private AnimationCurve animationCurve; 

    private void Awake()
    {
        _rectTransform = this.gameObject.GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        var rectPos = _rectTransform.anchoredPosition;
        rectPos.y = animationCurve.Evaluate(Time.time);
        _rectTransform.anchoredPosition = rectPos;
    }
}
