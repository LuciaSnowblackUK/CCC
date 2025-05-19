using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PageMonitor : MonoBehaviour
{

    public TMP_Text PAGE;


    //We only have 2 pages in this demo
    public int TotalPages = 2;
    public int hhh = 1;
    public int CurrentPage = 1;

    public GameObject Page1;
    public GameObject Page2;


    private void Update()
    {
        PAGE.text = $"Page:{CurrentPage}/{TotalPages}";
    }

    public void GoToPage(int page)
    {
        Transform ThisPage = transform.Find($"Page{hhh}");
        if (ThisPage != null)
        {
            ThisPage.gameObject.SetActive(false); // 反激活当前页
            
        }


        Transform ThatPage = transform.Find($"Page{page}");
        if (ThisPage != null)
        {
            ThatPage.gameObject.SetActive(true); // 激活当前页
           
        }
    }

    public bool nextPage()
    {
        hhh = CurrentPage;
        if (CurrentPage < TotalPages) 
        {
            CurrentPage++;
            return true;
        }
        return false;
    }

    public bool lastPage()
    {
        hhh = CurrentPage;
        if (CurrentPage > 1)
        {
            CurrentPage--;
            return true;
        }
        return false;
    }
}
