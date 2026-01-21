using UnityEngine;

public class DroppedMailDetection : MonoBehaviour
{
    [SerializeField]
    LetterSortMiniGame miniGame = null;

    void OnCollisionEnter(Collision _collision)
    {
        if (!_collision.gameObject.GetComponent<MailComponent>()) return;

        miniGame.hasDroppedMail = true;
    }
}
