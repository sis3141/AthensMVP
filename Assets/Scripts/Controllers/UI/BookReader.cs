using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookReader : MonoBehaviour
{
    Transform _panel;
    Transform _page;
    Transform _move_prev;
    Transform _move_next;

    //Sprite[] _pages;
    int _curr_page = 0;
    int _max_page = 29;
    int _book_index;
    void Awake()
    {
        _panel = transform.GetChild(0);
        _page = _panel.GetChild(0);
        _move_prev = _panel.GetChild(1);
        _move_next = _panel.GetChild(2);

        Utils.BindTouchEvent(gameObject,MovePage);
    }

    public void LoadBook(int index)
    {
        _curr_page = 0;
        _book_index = index;
        Debug.Log("sb index :"+index);
        _page.GetComponent<Image>().sprite = Resources.Load<Sprite>("Book/"+_book_index+"/"+_curr_page);
    }

    void MovePage(PointerEventData evt)
    {
        Transform direction = evt.pointerEnter.transform;
        Sprite _image;
        Debug.Log(direction.name);
        if(direction == _move_prev)   //prev page
        {
            if(_curr_page == 0)
            {
                Debug.Log("first page!");
                return;
            }
            _curr_page --;
            _image = Resources.Load<Sprite>("Book/"+_book_index+"/"+_curr_page);
            _page.GetComponent<Image>().sprite = _image;
            Managers.sound.Play_UI(0);
            
        }
        else if(direction == _move_next)       //next page
        {
            if(_curr_page == _max_page)
            {
                Debug.Log("End page!");
                return;
            }
            _curr_page ++;
            _image = Resources.Load<Sprite>("Book/"+_book_index+"/"+_curr_page);
            _page.GetComponent<Image>().sprite = _image;
            Managers.sound.Play_UI(0);
        }
        Debug.Log(_curr_page+" page");
    }
}
