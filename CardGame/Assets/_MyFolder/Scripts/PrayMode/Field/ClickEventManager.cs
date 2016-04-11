using UnityEngine;
using System.Collections;

public class ClickEventManager : MonoBehaviour {

	[SerializeField][Header("GameManager")]
	private GameManager _myGameManager;

	private GameObject ClickStartObject;

	void OnEnable()
	{
		ClickStartObject = null;
	}
	// Update is called once per frame
	void Update () {
		if (_myGameManager._phase == GameManager.Phase.MAIN) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit.collider) {
					if (hit.collider.gameObject.name == "CardPrefab") {
						if (hit.collider.gameObject.GetComponent<CardArea_CardInformation> ().isOwn () &&
						    _myGameManager._turn == GameManager.Turn.MY)
							ClickStartObject = hit.collider.gameObject;
						//相手のターンに操作可能（後にAIや他プレイヤによる操作に切り替え
						if (!hit.collider.gameObject.GetComponent<CardArea_CardInformation> ().isOwn () &&
						    _myGameManager._turn == GameManager.Turn.ENEMY)
							ClickStartObject = hit.collider.gameObject;
					}
				}
			}

			if (Input.GetMouseButtonUp (0)) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit.collider) {
					if (ClickStartObject != null) {
						if (ClickStartObject.name == "CardPrefab") {
							if (hit.collider.transform.FindChild ("CharaPrefab") != null) {
								GameObject childObject = hit.collider.transform.FindChild ("CharaPrefab").gameObject;
								if (hit.collider.gameObject.GetComponent<BattleArea_CardInformation> ().isOwn () &&
								    _myGameManager._turn == GameManager.Turn.MY) {
									if (!childObject.activeSelf) {
										_myGameManager.PlayCharacterCard (ClickStartObject.GetComponent<CardArea_CardInformation> (), hit.collider.gameObject.GetComponent<BattleArea_CardInformation> ());
									}
								}
								//相手のターンに操作かのう（後にAIや他プレイヤによる操作に切り替え
								if (!hit.collider.gameObject.GetComponent<BattleArea_CardInformation> ().isOwn () &&
								    _myGameManager._turn == GameManager.Turn.ENEMY) {
									if (!childObject.activeSelf)
										_myGameManager.PlayCharacterCard (ClickStartObject.GetComponent<CardArea_CardInformation> (), hit.collider.gameObject.GetComponent<BattleArea_CardInformation> ());
								}
							}
						}
					}
				}
				ClickStartObject = null;
			}
		} else {
			ClickStartObject = null;
		}
	}
}
