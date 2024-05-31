using System;
using System.Collections.Generic;
using UnityEngine;

public class M_SlotControllerUI : MonoBehaviour
{
    [SerializeField] public List<M_CardSlotUI> slotList = new List<M_CardSlotUI>();
    public M_CardSlotUI currentSelectedCard;

    public event Action<M_CardSlotUI> selected;
    public void Initialize(List<M_MonsterConfiguration> configurationList)
    {
        ResetSlots();

        for (int index = 0; index < configurationList.Count; index++)
        {
            slotList[index].gameObject.SetActive(true);
            slotList[index].Initialize(configurationList[index]);
            slotList[index].pressed += onPressed;
        }
    }

    private void onPressed(M_CardSlotUI selectedCard)
    {
        if (currentSelectedCard != null ) { 
            currentSelectedCard.animator.Play("Deselect");

            if (currentSelectedCard == selectedCard)
            {
                currentSelectedCard = null;
                return;
            }
         
        }

        currentSelectedCard = selectedCard;
        selected?.Invoke(selectedCard);
    }

    public void ResetCurrentSelected()
    {
        onPressed(null);
    }

    private void ResetSlots()
    {
        foreach(M_CardSlotUI slot in slotList)
        {
            slot.gameObject.SetActive(false);
            slot.pressed -= onPressed;
        }

    }
}
