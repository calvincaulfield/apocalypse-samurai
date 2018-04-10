using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLanguage: MonoBehaviour {

	Dictionary<string, string> kor;
	Dictionary<string, string> eng;
	Dictionary<string, string> jpn;
	Dictionary<string, Dictionary<string, string>> data;

	void Awake() {
		kor = new Dictionary<string, string>();
		eng = new Dictionary<string, string>();
		jpn = new Dictionary<string, string>();
		data = new Dictionary<string, Dictionary<string, string>>();
		data.Add("kor", kor);
		data.Add("eng", eng);
		data.Add("jpn", jpn);
		setKor();
		setJpn();
		setEng();
	}

	void setKor() {
		//Debug.Log("init");
		kor.Add("Hp", "체력");
		kor.Add("Stamina", "스태미너");
		kor.Add("Level", "레벨");
		kor.Add("Exp", "경험치");

		kor.Add("Tutorial_01", "튜토리얼을 보려면 좌클릭 건너띄려면 스페이스\n(처음엔 꼭 보세요)");
		kor.Add("Tutorial_05", "핵전쟁으로 99% 이상의 인류가 지구상에서 사라진지도 100년이 지났다.");
		kor.Add("Tutorial_06", "암흑과도 같았던 지구에도 또다시...");
		kor.Add("Tutorial_07", "하늘과");
		kor.Add("Tutorial_08", "땅과");
		kor.Add("Tutorial_09", "사람");
		kor.Add("Tutorial_10", "그리고, 벽");
		kor.Add("Tutorial_11", "방사능으로 오염된 괴물만이 가득한 벽 바깥 세상.\n 인류는 벽안에 갇힌 신세가 되었다.");
		kor.Add("Tutorial_12", "그러나 최근 인류의 마지막 보루인 벽 안에서\n방사능 생물이 발견되었다는 보고가 들어왔다. ");
		kor.Add("Tutorial_13", "\"벽 안의 방사능 생물을 조사하고 필요하면 사살하라\"");
		kor.Add("Tutorial_14", "이것이 당신의 임무이다\n그 어떤 대가를 치루더라도 완수해야만 하는...");
		kor.Add("Tutorial_15", "검술의 기본은 보법.\n마우스를 우클릭하면 된다.\n목표 지점을 순서대로 방문해보자.");
		kor.Add("Tutorial_20", "");

		kor.Add("Tutorial_21", "마지막 목표 지점에는 보상이 주어진다.\n하지만 컨이 안되는 사람은 패스해도 된다.");
		kor.Add("Tutorial_22", "보상을 만끽했으면 길을 따라 전진해 보자.");
		kor.Add("Tutorial_30", "");

		kor.Add("Tutorial_31", "다음은 검격의 기본인 내려치기이다.\n적은 양의 스태미너를 사용하고 큰 데미지를 준다.\n하지만 시전하는 동안 이동할 수 없다는 점을 주의하자.");
		kor.Add("Tutorial_32", "원하는 방향에 마우스 커서를 놓고 Q를 누르면 된다.");
		kor.Add("Tutorial_33", "모든 검격에는 일정량의 스태미너가 소모된다.\n공격을 하지 않고 일정시간이 지나면 스태미너가 서서히 회복된다.");
		kor.Add("Tutorial_34", "Q를 눌러서 내려치기를 3회 연습하도록 하자.");
		kor.Add("Tutorial_40", "");

		kor.Add("Tutorial_41", "내려치기를 잘 연습했다.\n길을 따라 이동해보자");
		kor.Add("Tutorial_50", "");

		kor.Add("Tutorial_51", "잡몹을 대상으로 실전을 익혀보자.\n방사능 괴물에 몸이 닿으면 피해를 입을 뿐 아니라 일시적으로 제어불가 상태가 되므로 주의하자.");
		kor.Add("Tutorial_52", "방사능 괴물을 처치할 때마다 경험치를 획득하고, 일정량 축척되면 레벨업을 하게 된다.");
		kor.Add("Tutorial_53", "괴물을 모두 처치하면서 전진하자.");
		kor.Add("Tutorial_60", "");

		
		kor.Add("Tutorial_61", "가만히 서서 당신의 칼을 맞아줄 적은 여기까지이다.\n내려치기만으로는 힘들테니 회전베기를 전수하겠다.");
		kor.Add("Tutorial_62", "스태미너 소모가 크지만, 이동하면서 시전할 수 있다는 장점이 있다.");
		kor.Add("Tutorial_63", "W를 눌러서 회전베기를 3회 연습하자.");
		kor.Add("Tutorial_70", "");

		kor.Add("Tutorial_71", "회전베기를 잘 연습했다.");
		kor.Add("Tutorial_72", "이제부터 마주치게 될 적들은 다양한 움직임으로 당신을 공격하려 들 것이다.\n저마다 움직이는 패턴이 있으니 잘 이용하자.");
		kor.Add("Tutorial_73", "또한 몸의 일부분만이 방사능 물질인 괴물도 있으니 잘 이용하자.");
		kor.Add("Tutorial_74", "튜토리얼은 이것으로 마친다.\n건투를 빈다!");

		kor.Add("Tutorial_80", "");
		kor.Add("Tutorial_81", "당신은 죽었습니다.\n(이거하나 못 깨고 죽다니...)");

		kor.Add("Tutorial_90", "");
		kor.Add("Tutorial_91", "축하합니다! 모든 괴물을 처치했습니다.\n끝까지 플레이해주셔서 감사합니다~");

		//kor.Add("Tutorial_90", "적에게 피해를 입으면 짧은 시간 동안 몸이 빨갛게 되면서 이동속도가 느려지고 공격불가 생태가 된다. 허접해 보이는 게임이라고 쉽게 클리어할 수 있다고 생각하면 오산이다.");


		//kor.Add("Tutorial_51", "당신을 쫒아오는 적들에게 내려치기 검법을 시전하다가는 쳐맞기 좋을 것이다. 이제 두번째 검법 휘둘러베기를 전수하겠다. W를 누르면 눌러서 360도 공격을 이용해 뒤에서 쫓아오는 적을 제압하자.");
		//kor.Add("Tutorial_52", "휘둘러베기 검법은 사방을 한번에 공격할 수 있고, 이동하면서 시전할 수 있다는 장점이 있다 (시전 후 짦은 시간 동안은 이동할 수 없다). 한편 스태미너 소모량이 크고 내려치기보다 데미지가 약하다는 것이 단점이다.");

		kor.Add("Enemy_01", "하룻강아지");
		kor.Add("Enemy_02", "날렵한 껑충이");
		kor.Add("Enemy_03", "괴팍한 스토커");
		kor.Add("Enemy_05", "방사능 여왕불가사리");
	}

	void setJpn()
	{
		//Debug.Log("init");
		jpn.Add("Hp", "生命力");
		jpn.Add("Stamina", "スタミナ");
		jpn.Add("Level", "レベル");
		jpn.Add("Exp", "経験値");

		jpn.Add("Tutorial_01", "左クリックしてチュートリアルを見る\nスペース押してスキップ\n(最初の方は必ずチュートリアルに)");
		jpn.Add("Tutorial_05", "核戦争で99％以上の人類がなくなってからも100年がたった。");
		jpn.Add("Tutorial_06", "真っ暗な地球に再び");
		jpn.Add("Tutorial_07", "天");
		jpn.Add("Tutorial_08", "地");
		jpn.Add("Tutorial_09", "人");
		jpn.Add("Tutorial_10", "そして、壁");
		jpn.Add("Tutorial_11", "放射能モンスターばかりの外の世界。\n 人類は壁に囲まれて生きるしかないのだ。");
		jpn.Add("Tutorial_12", "ところでこの最後の砦である壁の内側にまで\n放射能モンスターが侵入したという情報が入っている。");
		jpn.Add("Tutorial_13", "\"壁の中の放射能モンスターを索敵し殲滅せよ。\"");
		jpn.Add("Tutorial_14", "これが貴方に与えられたミッションである。");
		jpn.Add("Tutorial_15", "剣術の基本は歩法.\nマウスを右クリックすればいい。\n目標物を順番に尋ねよう。");
		jpn.Add("Tutorial_20", "");

		jpn.Add("Tutorial_21", "最後の目標には補償がある。\n出来なかったらパスしても構わない。");
		jpn.Add("Tutorial_22", "終わったら前に進もう。");
		jpn.Add("Tutorial_30", "");

		jpn.Add("Tutorial_31", "次は真向斬りを学ぼう。");
		jpn.Add("Tutorial_32", "攻撃対象にカーソルを位置させて、Qを押す。");
		jpn.Add("Tutorial_33", "すべての攻撃にはスタミナが必要となる。\nスタミナは休んでる時自然回復する。");
		jpn.Add("Tutorial_34", "Qをおして真向斬りを3回練習しよう。");
		jpn.Add("Tutorial_40", "");

		jpn.Add("Tutorial_41", "よーし。\nじゃあ次の位置まで進もう。");
		jpn.Add("Tutorial_50", "");

		jpn.Add("Tutorial_51", "これからは実戦を経験することになる。\n放射能モンスターに触れたらダメージを受けるだけではなく\n一時的に攻撃不能状態になるから注意しよう。");
		jpn.Add("Tutorial_52", "モンスターを倒すたびに経験値を得て\nレベルアップすることが出来る");
		jpn.Add("Tutorial_53", "モンスターどもを一匹残らず駆逐しよう！");
		jpn.Add("Tutorial_60", "");


		jpn.Add("Tutorial_61", "これからはもっと強い敵が待ってるのだ。\n回転切りを教えてあげる。\nWを押せばいい。");
		jpn.Add("Tutorial_62", "回転切りはスタミナ消費が大きいが、\n移動しながらも攻撃を続けられる。");
		jpn.Add("Tutorial_63", "Wをおして回転切りを3回練習してみよう。");
		jpn.Add("Tutorial_70", "");

		jpn.Add("Tutorial_71", "よーし");
		jpn.Add("Tutorial_72", "これからの敵はそれぞれのパターンがある。\n動きのパターンを把握して効率よく戦おう。");
		jpn.Add("Tutorial_73", "体の一部だけが放射能物質であり、他は触れてもいい場合もある。\n白い部分は汚染されてない部分である。");
		jpn.Add("Tutorial_74", "ちゅとーリアルはここまで.\nでは、健闘を祈る！");

		jpn.Add("Tutorial_80", "");
		jpn.Add("Tutorial_81", "貴方は死亡しました。\n(こんなところで。。。無念)");

		jpn.Add("Tutorial_90", "");
		jpn.Add("Tutorial_91", "おめでとう！すべてのモンスターを倒しました！\n最後までプレーしてくれてありがとうね");

		//jpn.Add("Tutorial_90", "적에게 피해를 입으면 짧은 시간 동안 몸이 빨갛게 되면서 이동속도가 느려지고 공격불가 생태가 된다. 허접해 보이는 게임이라고 쉽게 클리어할 수 있다고 생각하면 오산이다.");


		//jpn.Add("Tutorial_51", "당신을 쫒아오는 적들에게 내려치기 검법을 시전하다가는 쳐맞기 좋을 것이다. 이제 두번째 검법 휘둘러베기를 전수하겠다. W를 누르면 눌러서 360도 공격을 이용해 뒤에서 쫓아오는 적을 제압하자.");
		//jpn.Add("Tutorial_52", "휘둘러베기 검법은 사방을 한번에 공격할 수 있고, 이동하면서 시전할 수 있다는 장점이 있다 (시전 후 짦은 시간 동안은 이동할 수 없다). 한편 스태미너 소모량이 크고 내려치기보다 데미지가 약하다는 것이 단점이다.");

		jpn.Add("Enemy_01", "ワンちゃん");
		jpn.Add("Enemy_02", "狂狼");
		jpn.Add("Enemy_03", "突撃戦車");
		jpn.Add("Enemy_05", "星形の化け物");
	}


	void setEng()
	{
		eng.Add("Hp", "HP");
		eng.Add("Stamina", "Stamina");
		eng.Add("Level", "Level");
		eng.Add("Exp", "Exp");

		eng.Add("Tutorial_01", "Left click to view tutorial (you must if this is your first play)\nPress space key to skip tutorial");
		eng.Add("Tutorial_05", "100 years have passed since 99% of mankind disappeared from earth by nuclear warfare");
		eng.Add("Tutorial_06", "But again from darkness");
		eng.Add("Tutorial_07", "Sky,");
		eng.Add("Tutorial_08", "Land,");
		eng.Add("Tutorial_09", "Man,");
		eng.Add("Tutorial_10", "and walls stand");
		eng.Add("Tutorial_11", "World is totally irradiated\nThe only asylum is inside the walls.");
		eng.Add("Tutorial_12", "But again, we got information that\nIrradiated mutants infiltrated the walls.");
		eng.Add("Tutorial_13", "Your mission objective:");
		eng.Add("Tutorial_14", "\"Search and Destroy all mutants, at all costs.\"");
		eng.Add("Tutorial_15", "Basic of sword fighting is footwork.\nRight click to move to destinations in order.");
		eng.Add("Tutorial_20", "");

		eng.Add("Tutorial_21", "There will be a reward for the last destination.\nYou can pass if you don't feel like");
		eng.Add("Tutorial_22", "March forward。");
		eng.Add("Tutorial_30", "");

		eng.Add("Tutorial_31", "Next we learn vertical swing.");
		eng.Add("Tutorial_32", "Face opponent, and press Q");
		eng.Add("Tutorial_33", "Attack costs stamina\nStamina restores after some time.");
		eng.Add("Tutorial_34", "Let's practive vertical swing 3 three times. (press Q)");
		eng.Add("Tutorial_40", "");

		eng.Add("Tutorial_41", "Good\nMarch forward.");
		eng.Add("Tutorial_50", "");

		eng.Add("Tutorial_51", "You will face irradiated mutants from now\nWhen touched by them, you will lose HP and be unable to attack for a short period.");
		eng.Add("Tutorial_52", "Each mutant you kill will earn exp\nYou will level up after you have got enough exp.");
		eng.Add("Tutorial_53", "Now is the time for blood. Kill them all!");
		eng.Add("Tutorial_60", "");


		eng.Add("Tutorial_61", "You will face a bit more dangerous enemies from now on");
		eng.Add("Tutorial_62", "Press W, and you will do sweeping attack.");
		eng.Add("Tutorial_63", "Practice sweeping attack 3 times (press W)");
		eng.Add("Tutorial_70", "");

		eng.Add("Tutorial_71", "Good");
		eng.Add("Tutorial_72", "Each monster has unique movement and attack pattern.\nUse it to your advantage");
		eng.Add("Tutorial_73", "Some mutants have been partially irradiated\nWhite part of their body is safe to touch");
		eng.Add("Tutorial_74", "This ends the tutorial\nGood luck!");

		eng.Add("Tutorial_80", "");
		eng.Add("Tutorial_81", "You died");

		eng.Add("Tutorial_90", "");
		eng.Add("Tutorial_91", "Congratulations！\nYou killed all of them\nThanks for playing");

		eng.Add("Enemy_01", "Cute Dog");
		eng.Add("Enemy_02", "Crazy Wolf");
		eng.Add("Enemy_03", "Chariot");
		eng.Add("Enemy_05", "Star-shaped Irradiation");
	}

	public string GetWord(string lan, string key) {
		//Debug.Log(lan + " " + key);
		Dictionary<string, string> dict;
		data.TryGetValue(lan, out dict);
		string result;
		dict.TryGetValue(key, out result);
		return result;
	}
}
