using System.Collections;
using TMPro;
using UnityEngine;

public class M_NotificationUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI notificationLabel;
    public IEnumerator Initialize(string notifString , float duration)
    {
        this.gameObject.SetActive(true);
        notificationLabel.text = notifString;
        yield return new WaitForSeconds(duration);
        this.gameObject.SetActive(false);
    }
}
