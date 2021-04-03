using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] List<RectTransform> imagesRT;
    List<Image> images;

    private void Start()
    {
        images = new List<Image>();
        for (int i = 0; i < imagesRT.Count;i ++)
        {
            images.Add(imagesRT[i].GetComponent<Image>());
        }
    }
    public void SetSprite(Sprite _sprite, int id)
    {
        images[id].gameObject.SetActive(true);
        images[id].sprite = _sprite;
    }

    public void Hide(int id)
    {
        images[id].gameObject.SetActive(false);
    }
}
