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

    Transform _book_reader;
    void Start()
    {
        _booklist = transform;
        _page = _booklist.GetChild(0);
        _book_reader = Managers.ui._scene_UI_dict[SceneUIType.BookReader].transform;
        LoadBookList();
        Utils.BindTouchEvent(gameObject,OpenBook);
    }

    void LoadBookList()
    {
        ref Dictionary<DBHeader,Array> books = ref Managers.data._book_DB;
        int book_count = books[DBHeader.index].Length;
        _book_icon = Resources.LoadAll<Sprite>("Images/book_icon");
        Debug.Log("book count :"+book_count);
        for(var i = 0; i<book_count ;i++)
        {
            Transform slot = _page.GetChild(i);
            Image image = Utils.GetOrAddComponent<Image>(slot);
            image.sprite = _book_icon[i];
        }
    }

    void OpenBook(PointerEventData evt)
    {
        int index = evt.pointerEnter.transform.GetSiblingIndex();
        _book_reader.GetComponent<BookReader>().LoadBook(index);
        _book_reader.gameObject.SetActive(true);
    }
}
