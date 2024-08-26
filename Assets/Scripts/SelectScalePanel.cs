using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectScalePanel : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform contentParent;
    private List<string> menuItems;
    // Start is called before the first frame update
    void Start()
    {
        menuItems = Helper.notesInScale.Keys.ToList();
        PopulateMenu();
    }

    void PopulateMenu()
    {
        foreach (var item in menuItems)
        {
            Debug.Log(item);
            GameObject newBtn = Instantiate(buttonPrefab, contentParent);
            newBtn.GetComponentInChildren<TMP_Text>().text = item;
            newBtn.GetComponent<Button>().onClick.AddListener(() => OnMenuItemClick(item));

        }
    }

    void OnMenuItemClick(string itemName)
    {
        Debug.Log("Cliked on : " + itemName);
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
