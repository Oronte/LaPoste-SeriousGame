using UnityEngine;

public class MailTreadmillDetection : MonoBehaviour
{
    DepilatorComponent depilator = null;


    void Start()
    {
        depilator = GetComponentInParent<DepilatorComponent>();
    }

    void OnTriggerEnter(Collider _other)
    {
        // TODO mettre sécurité si on attrape la lettre
        MailComponent _mail = _other.GetComponent<MailComponent>();
        if (!_mail) return;
        depilator.AddToTreadmill(_mail);
    }

    void OnTriggerExit(Collider _other)
    {
        // TODO mettre sécurité si on attrape la lettre
        MailComponent _mail = _other.GetComponent<MailComponent>();
        if (!_mail) return;
        depilator.RemoveFromTreadmill(_mail);
    }
}
