using UnityEngine;
using System.Collections;

public class ClickEventManager : MonoBehaviour {

	[SerializeField][Header("GameManager")]
	private GameManager _myGameManager;

	/// <summary>
	/// 現在ドラッグ対象は自分の所有物のみ
	/// </summary>
	private enum DragObjectType
	{
		FIELD_CHARACTER,
		FIELD_HERO,
		HAND_CHARACTER,
		HAND_MAGIC,
		HAND_EQUIPMENT,
		NULL
	}

	private DragObjectType _dragObjectType = new DragObjectType();

	//ドラッグ時判定用
	private GameObject ClickStartObject;

	void OnEnable()
	{
		DragObject_Reset ();
	}


	// Update is called once per frame
	void Update () {

		if (_myGameManager._phase != GameManager.Phase.MAIN) {
			DragObject_Reset ();
			return;
		}
			
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider) {
				if (IsHandCard (hit.collider.gameObject)) {
					//TODO手札のカードの識別
					//対象がミニオンの場合
					if (IsMinion (hit.collider.gameObject)) {
						if (CanDrag (hit.collider.gameObject))
							DragObject_Set (hit.collider.gameObject, DragObjectType.HAND_CHARACTER);
					}
				}
				if (IsFieldSpace (hit.collider.gameObject)) {
					if (CanDrag (hit.collider.gameObject)) {
						//TODO
					}
				}
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider) {
				if (ClickStartObject != null) {
					if (_dragObjectType == DragObjectType.HAND_CHARACTER) {
						if (IsFieldSpace (hit.collider.gameObject)) {
							if (CanDrag (hit.collider.gameObject)) {
								_myGameManager.PlayCharacterCard (ClickStartObject.GetComponent<CardArea_CardInformation> (), hit.collider.gameObject.GetComponent<BattleArea_CardInformation> ());
							}
						}	
					}
				}
			}
			DragObject_Reset ();
		}
	
	}

	/// <summary>
	/// 対象がフィールドの空きスペースかどうか
	/// </summary>
	/// <returns><c>true</c> if this instance is field space the specified targetObject; otherwise, <c>false</c>.</returns>
	/// <param name="targetObject">Target object.</param>
	private bool IsFieldSpace(GameObject targetObject)
	{
		if (targetObject.transform.FindChild ("CharaPrefab") == null)
			return false;

		GameObject childObject = targetObject.transform.FindChild ("CharaPrefab").gameObject;

		if (!childObject.activeSelf)
			return true;
		else
			return false;
	}

	/// <summary>
	/// ドラッグ開始オブジェクトがハンドカードかどうか
	/// </summary>
	/// <returns><c>true</c> if this instance is hand card; otherwise, <c>false</c>.</returns>
	private bool IsHandCard(GameObject dragObject)
	{
		if (dragObject.name == ("CardPrefab"))
			return true;
		else
			return false;
	}

	/// <summary>
	/// 対象がミニオンかどうか
	/// </summary>
	/// <returns><c>true</c> if this instance is minion the specified targetObject; otherwise, <c>false</c>.</returns>
	/// <param name="targetObject">Target object.</param>
	private bool IsMinion(GameObject targetObject)
	{
		if (targetObject.GetComponent<CardInterface> ().IsMinion ())
			return true;
		else
			return false;
	}

	/// <summary>
	/// 対象オブジェクトが自分のかどうか
	/// </summary>
	/// <returns><c>true</c> if this instance can drag the specified dragObject; otherwise, <c>false</c>.</returns>
	/// <param name="dragObject">Drag object.</param>
	private bool CanDrag(GameObject dragObject)
	{
		//自分の所有物
		if (dragObject.GetComponent<CardInterface> ().isOwn ()) {
			if (_myGameManager._turn == GameManager.Turn.MY)
				return true;
			else
				return false;
		} else {
			if (_myGameManager._turn == GameManager.Turn.ENEMY)
				return true;
			else
				return false;
		}

	}

	/// <summary>
	/// ドラッグ開始オブジェクトをリセットする
	/// </summary>
	private void DragObject_Reset()
	{
		ClickStartObject = null;
		_dragObjectType = DragObjectType.NULL;
	}


	/// <summary>
	/// ドラッグ開始オブジェクトをセットする
	/// </summary>
	/// <param name="dragObject">ドラッグ開始オブジェクト</param>
	/// <param name="dragObjectType">オブジェクトタイプ</param>
	private void DragObject_Set(GameObject dragObject,DragObjectType dragObjectType)
	{
		ClickStartObject = dragObject;
		_dragObjectType = dragObjectType;
	}
}
