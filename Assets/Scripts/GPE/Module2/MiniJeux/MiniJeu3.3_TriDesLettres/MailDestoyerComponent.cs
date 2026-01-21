using UnityEngine;

public class MailDestroyerComponent : MonoBehaviour
{
    DepilatorComponent depilator = null;
    [SerializeField] LetterSortMiniGame miniGame = null;

    void Start()
    {
        depilator = GetComponentInParent<DepilatorComponent>();
    }

    void OnTriggerEnter(Collider _other)
    {
        if (!depilator) return;

        MailComponent _mail = _other.GetComponent<MailComponent>();
        if (!_mail) return;

        if (!_mail.IsMachinable && miniGame) ++miniGame.discardedNonMachinableMailCount;
        depilator.DestroyMail(_mail);
    }
}
