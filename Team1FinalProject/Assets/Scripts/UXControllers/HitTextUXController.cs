using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitTextUXController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _hitText;
    private readonly float _timeLength = 1.5f;
    private float _timeElapsed = 0;

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        _canvasGroup.alpha = 1 - _timeElapsed / _timeLength;
        if (_timeElapsed > _timeLength)
            Destroy(gameObject);
    }

    public void SetLocation(Vector3 pos)
    {
        _hitText.rectTransform.localPosition = pos;
    }
}
