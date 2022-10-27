using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int currentIndexValue = 0;
    [SerializeField]
    private Text textOfIndex;
    private int countOfPrefabs = 0;

    public void Initialize(int value)
    {
        countOfPrefabs = value;
    }

    public void IncreaseIndexValue()
    {
        currentIndexValue = Mathf.Clamp(currentIndexValue + 1, 0, countOfPrefabs - 1);
        ChangeData();
    }

    public void DicreaseIndexValue()
    {
        currentIndexValue = Mathf.Clamp(currentIndexValue - 1 , 0 ,countOfPrefabs - 1);
        ChangeData();
    }

    private void ChangeData()
    {
        textOfIndex.text = currentIndexValue.ToString();
    }


}
