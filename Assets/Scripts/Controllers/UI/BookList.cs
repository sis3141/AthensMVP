using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructure;
using Define;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookList : MonoBehaviour
{
    Transform _booklist;
    Transform _page;
    Sprite[] _book_icon;
    Sprite _empty_image;

    int _slot_count = 16;

    Transform _book_reader;
    void Start()
    {
        _booklist = transform;
        _page = _booklist.GetChild(0);
        _book_reader = Managers.ui._scene_UI_dict[SceneUI.BookReader].transform;
        _empty_image = Managers.data._empty_image;
        LoadBookList();
        Utils.BindTouchEvent(gameObject,OpenBook);
    }

    void LoadBookList()
    {
        //ref Dictionary<DBHeader,Array> books = ref Managers.data._book_DB;
        ref CSVDict books = ref Managers.data._book_DB;
        int book_count = books[DBHeader.index].Length;
        _book_icon = Resources.LoadAll<Sprite>("Images/Icon/book");
        _empty_image = _book_icon[15];
        Debug.Log("book count :"+book_count);
        for(var i = 0; i<_slot_count ;i++)
        {
            Transform slot = _page.GetChild(i);
            Image image = Utils.GetOrAddComponent<Image>(slot);
            if(i<book_count)
            {
                image.sprite = _book_icon[i];
            }
            else
            {
                image.sprite = _empty_image;
            }
        }
    }

    void OpenBook(PointerEventData evt)
    {
        int index = evt.pointerEnter.transform.GetSiblingIndex();
        Managers.sound.Play_UI();
        _book_reader.gameObject.SetActive(true);
        _book_reader.GetComponent<BookReader>().LoadBook(index);
    }
}
