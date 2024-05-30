using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class M_CardSlotUI : MonoBehaviour
{
    [SerializeField] public Button cardButton;
    [SerializeField] public TextMeshProUGUI cardName;
    [SerializeField] public Animator animator;

    public M_MonsterConfiguration configuration;

    public event Action<M_CardSlotUI> pressed;
    public void Initialize(M_MonsterConfiguration configuration)
    {
        this.configuration = configuration;
        print(configuration.information.name);
        cardName.text = configuration.information.name;
        cardButton.onClick.AddListener(onPressed);
        animator.Play("None");
    }

    private void onPressed()
    {
        pressed?.Invoke(this);
        animator.Play("Selected");
    }

    private void OnDestroy()
    {
        cardButton.onClick.RemoveAllListeners();
    }
}
