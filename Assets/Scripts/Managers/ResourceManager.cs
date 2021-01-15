using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public T Instantiate<T>(string path, Transform parent = null) where T : Object
    {
        T original = Load<T>(path);
        if (original == null)
        {
            Debug.Log($"Failed to Load : {path}");
            return null;
        }

        //기존 유니티 함수
        T copy = Object.Instantiate(original,parent);
        //기존옵젝에서 복사해서 생성하기때문에 이름뒤에 복사됬다는게 붙어서 이름정리용
        copy.name = original.name;

        return copy;
    }

    public void Destroy(GameObject go)
    {
        if(go == null)
            return;
        //풀링 사용을 대비해서 만든 커스텀함수
        
        Object.Destroy(go);
    }
    public void Destroy(Component component)
    {
        GameObject go = component.gameObject;
        Destroy(go);
    }
}
