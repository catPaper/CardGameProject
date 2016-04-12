using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogTextManager : MonoBehaviour {

	[SerializeField][Header("ログテキスト")]
	private Text _logText;

	private void Awake()
	{
		_logText.enabled = false;
	}

	/// <summary>
	/// ログメッセージを表示する
	/// </summary>
	/// <param name="message">Message.</param>
	private void PopUpLog(string message)
	{
		StopCoroutine ("IndicateLog");
		StartCoroutine ("IndicateLog", message);
	}

	/// <summary>
	/// ターン開始のMessageを表示
	/// </summary>
	public void PopUpStartTurnMessage(GameManager.Turn _whoseTurn)
	{

		if (_whoseTurn == GameManager.Turn.MY)
			PopUpLog ("あなたのターンだ");
		else
			PopUpLog ("相手のターンだ");
	}

	/// <summary>
	/// 相手に挑発持ちがいるため攻撃できない
	/// </summary>
	public void PopUpCantAtack_EnemyHasTaunt()
	{
		PopUpLog ("相手に挑発持ちがいるぞ");
	}

	/// <summary>
	/// ミニオンが召喚酔い、攻撃済みで攻撃できない
	/// </summary>
	public void PopUpCantAtack_PlayThisTurn()
	{
		PopUpLog ("そのミニオンはこのターン攻撃できないぞ");
	}

	/// <summary>
	/// マナが足りないことを通知する
	/// </summary>
	public void PopUpCanUseMana()
	{
		PopUpLog ("マナが足りないぞ");
	}

	private IEnumerator IndicateLog(string message)
	{
		_logText.enabled = true;
		_logText.text = message;
		yield return new WaitForSeconds (2);
		_logText.enabled = false;
	}
}
