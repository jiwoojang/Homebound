using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostCardOverlay : MonoBehaviour
{

    [SerializeField]
    public RectTransform _finalTransform;

    [SerializeField]
    public RectTransform _imageTransform;

    [SerializeField]
    private float _scaleSpeed;

    private bool _shouldAnimatePostCard;

    // Start is called before the first frame update
    void Start()
    {
        _shouldAnimatePostCard = false;
    }

    public void BringInPostCard() 
    {
        if (!_shouldAnimatePostCard)
        {
            _shouldAnimatePostCard = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldAnimatePostCard) 
        {
            _imageTransform.localScale = Vector3.Lerp(_imageTransform.localScale, _finalTransform.localScale, _scaleSpeed * Time.deltaTime);

            if ((_finalTransform.localScale.magnitude / _imageTransform.localScale.magnitude) > 0.99) {
                _shouldAnimatePostCard = false;
            } 
        }
    }
}
