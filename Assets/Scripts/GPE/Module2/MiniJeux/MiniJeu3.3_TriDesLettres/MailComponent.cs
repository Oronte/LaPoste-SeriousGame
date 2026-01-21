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

    public MailType           Type 
    { 
        get => type;
        set
        {
            type = value;
            GetComponent<Renderer>().material.color = GetColorFromType(type);
            // TODO Load le mesh de la lettre en fontion du type de lettre 
        }
    }
    public bool               IsMachinable => type == MailType.Machinable;

    // TODO remove
    Color GetColorFromType(MailType mailType)
    {
        switch (mailType)
        {
            case MailType.Machinable:
                return Color.white;

            case MailType.Colored:
                return Color.yellow;

            case MailType.Package:
                return new Color(0.6f, 0.3f, 0.1f);

            case MailType.PlasticWrapped:
                return Color.cyan;

            case MailType.Reconditioning:
                return Color.gray;

            case MailType.Bubble:
                return Color.magenta;

            case MailType.ReturnReceipts:
                return Color.green;

            case MailType.Torn:
                return Color.red;

            default:
                return Color.white;
        }
    }
}
