using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class L3MenuScalesList : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform contentParent;
    private PlayMode playMode;
    private List<string> menuItems;
    // Start is called before the first frame update
    void Start()
    {
        //PopulateMenu();
        playMode = FindObjectOfType<PlayMode>();
    }

    public void SetMenuItems(List<string> items)
    {
        menuItems = items;
        BuildMenu();
    }

    void BuildMenu()
    {
        ClearMenu();
        PopulateMenu();
    }

    void PopulateMenu()
    {
        foreach (var item in menuItems)
        {
            GameObject newBtn = Instantiate(buttonPrefab, contentParent);
            newBtn.GetComponentInChildren<TMP_Text>().text = item;
            newBtn.GetComponent<Button>().onClick.AddListener(() => OnMenuItemClick(item));

        }
    }

    void ClearMenu()
    {
        GameObject[] allChildren = new GameObject[contentParent.childCount];
        int i = 0;
        foreach (Transform child in contentParent)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            Destroy(child.gameObject);
        }
    }

    void OnMenuItemClick(string itemName)
    {
        playMode.ChangeNotesToPractice(itemName);
        //Disable();
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
