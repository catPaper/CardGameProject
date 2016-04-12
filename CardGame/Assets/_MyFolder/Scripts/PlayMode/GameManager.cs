using UnityEngine;
using System.Collections;


/// <summary>
/// プレイモードの一括管理
/// </summary>
public class GameManager : MonoBehaviour {

	[SerializeField]
	private PlayerInformation _myPlayerInformation;
	[SerializeField]
	private PlayerInformation _enemyPlayerInformation;
	[SerializeField]
	private CardInformation _coinInformation;
	[SerializeField][Header("テストデッキ")]
	private DeckInformation _sampleDeck;

	[SerializeField]
	private DeckInformation _useDeck;

	private LogTextManager _logTextManager;

	public enum Who{
		MY,
		ENEMY
	}

	public enum Phase{
		CHECK,		
		MANA_RELOAD,
		DRAW,
		MAIN,
		END
	}

	public enum Turn{
		MY,
		ENEMY
	}

	public Phase _phase = new Phase ();
	public Turn _turn = new Turn ();

	void Awake()
	{
		_logTextManager = GameObject.FindGameObjectWithTag ("LogText").GetComponent<LogTextManager>();
	}

	// Use this for initialization
	void Start () {
		bool isMyFirst = ( (Random.Range (0, 100) < 50) ? true : false );
		if (isMyFirst)
			_turn = Turn.MY;
		else
			_turn = Turn.ENEMY;

		//盤面の掃除
		_myPlayerInformation.StartCleanField();
		_enemyPlayerInformation.StartCleanField ();
		//デッキの反映
		_myPlayerInformation.DeckSetting (_sampleDeck.Cards ());
		_enemyPlayerInformation.DeckSetting (_sampleDeck.Cards ());
		//カードのドロー
		_myPlayerInformation.StartDrawProcess (isMyFirst);
		_enemyPlayerInformation.StartDrawProcess (!isMyFirst);
		//コインの追加
		if (isMyFirst)
			_enemyPlayerInformation.GetCoin (_coinInformation);
		else
			_myPlayerInformation.GetCoin (_coinInformation);


		_phase = Phase.MANA_RELOAD;
	}

	void Update()
	{
		//マルチプレイ時には相手の処理は同期により行う
		switch (_phase) {
		case Phase.CHECK:
			//TODO モンスター等の効果発揮Phase
			//ミニオンを攻撃可能状態にする
			if (_turn == Turn.MY) {
				AtackPossibleProcess (_myPlayerInformation);
			} else {
				AtackPossibleProcess (_enemyPlayerInformation);
			}
			_phase = Phase.MANA_RELOAD;
			break;
		case Phase.MANA_RELOAD:
			if (_turn == Turn.MY) {
				ManaReloadProcess (_myPlayerInformation);
			} else {
				ManaReloadProcess (_enemyPlayerInformation);
			}
			_phase = Phase.DRAW;
			break;
		case Phase.DRAW:
			if (_turn == Turn.MY) {
				DrawProcess (_myPlayerInformation);
			} else {
				DrawProcess (_enemyPlayerInformation);
			}
			_logTextManager.PopUpStartTurnMessage (_turn);
			_phase = Phase.MAIN;
			break;
		case Phase.MAIN:
			//TODO	プレイヤーの操作可能時間
			break;
		case Phase.END:
			//TODO  モンスター等の終了時効果発揮時間
			ChangeTurn();
			break;
		}
	}

	/// <summary>
	/// 敵のミニオンにミニオンで攻撃する(攻撃できない場合はできない処理を実装
	/// </summary>
	/// <param name="_myMinionInfo">My minion info.</param>
	/// <param name="_enemyMinionInfo">Enemy minion info.</param>
	public void AtackToEnemyMinion(BattleArea_CardInformation _myMinionInfo,BattleArea_CardInformation _enemyMinionInfo)
	{
		//挑発持ちでない場合
		if (!_enemyMinionInfo.SearchSkill (SkillInformation.SkillType.TAUNT)) {
			if (_turn == Turn.MY) {
				if (_enemyPlayerInformation.SearchSkill (SkillInformation.SkillType.TAUNT)) {
					_logTextManager.PopUpCantAtack_EnemyHasTaunt ();
					return;
				}
			} else {
				if (_myPlayerInformation.SearchSkill (SkillInformation.SkillType.TAUNT)) {
					_logTextManager.PopUpCantAtack_EnemyHasTaunt ();
					return;
				}
			}
		}

		_myMinionInfo.Damage (_enemyMinionInfo.MyAtackPower ());
		_enemyMinionInfo.Damage (_myMinionInfo.MyAtackPower ());
		_myMinionInfo.AtackFinish ();
	}

	/// <summary>
	/// 敵のヒーローにミニオンで攻撃する処理（攻撃できない場合はできない処理を実装
	/// </summary>
	/// <param name="enemyHeroManager">Enemy hero manager.</param>
	/// <param name="atackMinionInformation">Atack minion information.</param>
	public void AtackToEnemyHero(HeroManager enemyHeroManager,BattleArea_CardInformation atackMinionInformation)
	{
		if (_turn == Turn.MY) {
			if(_enemyPlayerInformation.SearchSkill(SkillInformation.SkillType.TAUNT)){
				_logTextManager.PopUpCantAtack_EnemyHasTaunt ();
				return;
			}
		} else {
			if (_myPlayerInformation.SearchSkill (SkillInformation.SkillType.TAUNT)) {
				_logTextManager.PopUpCantAtack_EnemyHasTaunt ();
				return;
			}
		}

		enemyHeroManager.Damage(atackMinionInformation.MyAtackPower());
		atackMinionInformation.AtackFinish ();
	}

	/// <summary>
	/// ミニオンを攻撃できる状態にする
	/// </summary>
	/// <param name="_targetInformation">Target information.</param>
	private void AtackPossibleProcess(PlayerInformation _targetInformation)
	{
		_targetInformation.AllMinionAtackReflesh ();
	}

	/// <summary>
	/// カードをドローするプロセス
	/// </summary>
	private void DrawProcess(PlayerInformation _targetInformation)
	{
		_targetInformation.DrawProcess ();
	}

	/// <summary>
	/// マナを補充するプロセス
	/// </summary>
	/// <param name="_targetInformation">Target information.</param>
	private void ManaReloadProcess(PlayerInformation _targetInformation)
	{
		_targetInformation.ManaReloadProcess ();
	}

	/// <summary>
	/// プレイヤーのターンが入れ替わる
	/// </summary>
	private void ChangeTurn()
	{
		if (_turn == Turn.MY)
			_turn = Turn.ENEMY;
		else
			_turn = Turn.MY;

		_phase = Phase.CHECK;
	}

	/// <summary>
	/// 手札からクリーチャーカードをプレイする
	/// </summary>
	/// <param name="_handTargetInfo">Hand target info.</param>
	/// <param name="_fieldTargetInfo">Field target info.</param>
	public void PlayCharacterCard(CardArea_CardInformation _handTargetInfo,BattleArea_CardInformation _fieldTargetInfo)
	{
		//Fieldにプレイ
		if (_turn == Turn.MY) {
			_myPlayerInformation.PlayCharacterCard (_handTargetInfo, _fieldTargetInfo);
		} else {
			_enemyPlayerInformation.PlayCharacterCard (_handTargetInfo, _fieldTargetInfo);
		}
	}

	public void PopUpCantAtack_PlayThisTurn()
	{
		_logTextManager.PopUpCantAtack_PlayThisTurn ();
	}

	/// <summary>
	/// 自分のturnを終了する
	/// </summary>
	public void TurnEnd(){
		_phase = Phase.END;
	}
}
