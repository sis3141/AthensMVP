using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookReader : MonoBehaviour
{
    Transform _panel;
    Transform _left_page;
    Transform _right_page;

    Sprite[] _pages;
    int _curr_page = 0;
    int _max_page;
    void Awake()
    {
        _panel = transform.GetChild(0);
        _left_page = _panel.GetChild(0);
        _right_page = _panel.GetChild(1);
        Utils.BindTouchEvent(gameObject,MovePage);
    }

    public void LoadBook(int index)
    {
        _pages = Resources.LoadAll<Sprite>("Book/"+index+"/");
        Debug.Log("pages :"+_pages.Length);
        _max_page = _pages.Length;
        _left_page.GetComponent<Image>().sprite = _pages[0];
        _right_page.GetComponent<Image>().sprite = _pages[1];
    }

    void MovePage(PointerEventData evt)
    {
        int direction = evt.pointerEnter.transform.GetSiblingIndex();
        if(direction == 1)   //next page
        {
            if(_curr_page+2 >= _max_page)
            {
                Debug.Log("End page!");
                return;
            }
            _curr_page += 2;
            _left_page.GetComponent<Image>().sprite = _pages[_curr_page];
            _right_page.GetComponent<Image>().sprite = _pages[_curr_page+1];
        }
        else            //previous page
        {
            if(_curr_page == 0)
            {
                Debug.Log("first page!");
                return;
            }
            _curr_page -= 2;
            _left_page.GetComponent<Image>().sprite = _pages[_curr_page];
            _right_page.GetComponent<Image>().sprite = _pages[_curr_page+1];
        }
    }
}
