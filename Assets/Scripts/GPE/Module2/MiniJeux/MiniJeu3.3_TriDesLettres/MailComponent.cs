using Unity.VisualScripting;
using UnityEngine;


// This represents all the types of mail that can be found on the depilator
public enum MailType
{
    Machinable,         // Machine-readable letters, corresponding to the correct format
    Colored,            // Colored or "SEC" type envelopes
    Package,            // Small packages (cardboard box)
    PlasticWrapped,     // The laminated letter
    Reconditioning,     // Repackaging envelopes
    Bubble,             // Bubble envelopes
    ReturnReceipts,     // Acknowledgments of receipt
    Torn                // The torn letters
}

public class MailComponent : MonoBehaviour
{
    [SerializeField] MailType type;

    public MailType           Type => type;
    public bool               IsMachinable => type == MailType.Machinable;
}
