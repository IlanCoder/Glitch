using Unity.VisualScripting;
using UnityEngine;

namespace Items {
    public abstract class Item : ScriptableObject {
        [Header("Basic Info")]
        [SerializeField] protected string itemName;
        [SerializeField] protected Sprite icon;
        public Sprite Icon { get { return icon; } }
        [SerializeField, TextArea] protected string itemDescription;
        [SerializeField] protected int itemID;
        public int ItemID { get { return itemID; } }

        public void SetItemID(int id) {
            itemID = id;
        }
    }
}
