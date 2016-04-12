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
	public void PopUpLog(string message)
	{
		StopCoroutine ("IndicateLog");
		StartCoroutine ("IndicateLog", message);
	}

	private IEnumerator IndicateLog(string message)
	{
		_logText.enabled = true;
		_logText.text = message;
		yield return new WaitForSeconds (2);
		_logText.enabled = false;
	}
}
