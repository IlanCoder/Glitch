using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI.HUD{
	public class UIWeaponSlot : MonoBehaviour {
		[Header("Animation Params")]
		[SerializeField] float changeSizeTime;
		[SerializeField] float bigSizeScale;
		[SerializeField] float smallSizeScale;
		
		[Header("Objects")]
		[SerializeField] Image iconImage;
		
		public bool IsBig { get; private set; }

		public void SetSprite(Sprite newSprite) {
			iconImage.sprite = newSprite;
		}

		public void GoBig() {
			if (IsBig) return;
			IsBig = true;
			transform.DOScale(bigSizeScale, changeSizeTime);
		}

		public void GoSmall() {
			if (!IsBig) return;
			IsBig = false;
			transform.DOScale(smallSizeScale, changeSizeTime);
		}
	}
}
