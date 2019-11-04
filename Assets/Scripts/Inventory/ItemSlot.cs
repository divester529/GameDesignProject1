using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    [SerializeField] Image image;
    private Item _item;
    public Item Item {
      get {return _item;}
      set {
        _item = value;
        if (_item == null) {
          image.enabled = false;
        } else {
          image.sprite = _item.Icon;
          image.enabled = true;
        }
      }
    }

    private void OnValidate() {
      image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
