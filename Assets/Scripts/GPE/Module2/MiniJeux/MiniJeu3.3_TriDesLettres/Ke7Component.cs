using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

class MailPlacingInfo
{
    public MailComponent   mail;
    public float           currentTime;
    public Vector3         startPosition;
    public Vector3         targetPosition;
    public Quaternion      startRotation;
    public Quaternion      targetRotation;
}

public class Ke7Component : MonoBehaviour
{
    List<MailComponent>                 mails = new List<MailComponent>();
    List<MailPlacingInfo>               mailsToPlace = new List<MailPlacingInfo>();
    [SerializeField] Vector3            targetPos;
    [SerializeField] Vector3            targetRot;
    [SerializeField] Vector3            posOffset;
    [SerializeField] int                maxLetterCount;
    [SerializeField] float              timeToPlace;

    [SerializeField] LetterSortMiniGame miniGame;

    bool ContainsMachinable()
    {
        foreach (MailComponent _mail in mails)
            if (_mail.IsMachinable) return true;
        return false;
    }

    void AddMail(MailComponent _mail)
    {
        mails.Add(_mail);
        if (_mail.IsMachinable && miniGame) miniGame.ke7ContainsMachnable = true;
    }

    void ComputeNewTargetPos()
    {
        targetPos += posOffset;
    }

    void PlaceMail(MailComponent _mail)
    {
        XRGrabInteractable _XRGrab = _mail.GetComponent<XRGrabInteractable>();
        if (_XRGrab) _XRGrab.enabled = false;
        Rigidbody _rb = _mail.GetComponent<Rigidbody>();
        if (_rb)
        {
            _rb.isKinematic = true;
            _rb.detectCollisions = false;
        }

        AddMail(_mail);

        MailPlacingInfo _info = new MailPlacingInfo();
        _info.mail = _mail;
        _info.currentTime = 0.0f;
        _info.startPosition = _mail.transform.position;
        _info.startRotation = _mail.transform.rotation;
        _info.targetRotation = Quaternion.Euler(targetRot);
        _info.targetPosition = targetPos;
        mailsToPlace.Add(_info);

        if (maxLetterCount < mails.Count)
            _mail.gameObject.SetActive(false);
        ComputeNewTargetPos();
    }

    void Update()
    {
        ComputeLerp();
    }

    void ComputeLerp()
    {
        List<int> _toRemove = new List<int>();
        int _mailInfoCount = mailsToPlace.Count;
        for (int _index = 0; _index < _mailInfoCount; _index++)
        {
            MailPlacingInfo _mailInfo = mailsToPlace[_index];
            _mailInfo.currentTime += Time.deltaTime;
            _mailInfo.mail.transform.position = Vector3.Lerp(_mailInfo.startPosition, _mailInfo.targetPosition, _mailInfo.currentTime / timeToPlace);
            _mailInfo.mail.transform.rotation = Quaternion.Lerp(_mailInfo.startRotation, _mailInfo.targetRotation, _mailInfo.currentTime / timeToPlace);
            if (_mailInfo.currentTime >= timeToPlace) _toRemove.Add(_index);
        }

        foreach (int _index in _toRemove) mailsToPlace.RemoveAt(_index);
    }

    void OnTriggerEnter(Collider _other)
    {
        MailComponent _mail = _other.GetComponent<MailComponent>();
        if (!_mail) return;

        PlaceMail(_mail);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        int _count = maxLetterCount - mails.Count;
        for (int _index = 0; _index < _count; _index++)
            Gizmos.DrawWireSphere(targetPos + posOffset * _index, 0.1f);

        Gizmos.color = Color.white;
    }
}
