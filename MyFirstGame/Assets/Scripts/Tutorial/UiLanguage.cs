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
	}

	void setKor() {
		//Debug.Log("init");
		kor.Add("Hp", "체력");
		kor.Add("Stamina", "스태미너");
		kor.Add("Level", "레벨");
		kor.Add("Exp", "경험치");

		kor.Add("Enemy_01_Name", "껑충이");

		kor.Add("Tutorial_01", "핵전쟁으로 99% 이상의 인류가 지구상에서 사라진지도 100년이 지났다");
		kor.Add("Tutorial_02", "암흑과도 같았던 지구에도 또다시...");
		kor.Add("Tutorial_03", "하늘과");
		kor.Add("Tutorial_04", "땅과");
		kor.Add("Tutorial_05", "사람");
		kor.Add("Tutorial_06", "그리고, 벽");
		kor.Add("Tutorial_07", "방사능으로 오염된 괴물만이 가득한 벽 바깥 세상. 인류는 벽안에 갇힌 신세가 되었다.");
		kor.Add("Tutorial_08", "그러나 최근 인류의 마지막 보루인 벽 안에서 방사능 생물이 발견되었다는 보고가 들어왔다. ");
		kor.Add("Tutorial_09", "\"벽 안의 방사능 생물을 조사하고 필요하면 사살하라\"");
		kor.Add("Tutorial_10", "이것이 당신의 임무이다\n그 어떤 대가를 치루더라도 완수해야만 하는...");
		kor.Add("Tutorial_11", "검술의 기본은 보법. 마우스를 우클릭하면 된다. 목표 지점을 순서대로 방문해보자.");
		kor.Add("Tutorial_20", "");

		kor.Add("Tutorial_21", "마지막 목표 지점에는 보상이 주어진다. 하지만 컨이 안되는 사람은 패스해도 된다.");
		kor.Add("Tutorial_22", "보상을 만끽했으면 길을 따라 전진해 보자.");
		kor.Add("Tutorial_30", "");

		kor.Add("Tutorial_31", "다음은 검격의 기본인 내려치기이다. 원하는 방향에 마우스 커서를 놓고 Q를 누르면 된다.");
		kor.Add("Tutorial_32", "모든 검격에는 일정량의 스태미너가 소모된다. 공격을 하지 않고 일정시간이 지나면 스태미너가 서서히 회복된다.");
		kor.Add("Tutorial_33", "Q를 눌러서 내려치기를 3회 연습하도록 하자.");
		kor.Add("Tutorial_40", "");

		kor.Add("Tutorial_41", "잡몹을 대상으로 연습해 보자. 방사능 괴물에 몸이 닿으면 어떻게 될지는 상식적으로 생각하자.");
		kor.Add("Tutorial_50", "");

		//kor.Add("Tutorial_90", "적에게 피해를 입으면 짧은 시간 동안 몸이 빨갛게 되면서 이동속도가 느려지고 공격불가 생태가 된다. 허접해 보이는 게임이라고 쉽게 클리어할 수 있다고 생각하면 오산이다.");


		kor.Add("Tutorial_51", "당신을 쫒아오는 적들에게 내려치기 검법을 시전하다가는 쳐맞기 좋을 것이다. 이제 두번째 검법 휘둘러베기를 전수하겠다. W를 누르면 눌러서 360도 공격을 이용해 뒤에서 쫓아오는 적을 제압하자.");
		kor.Add("Tutorial_52", "휘둘러베기 검법은 사방을 한번에 공격할 수 있고, 이동하면서 시전할 수 있다는 장점이 있다 (시전 후 짦은 시간 동안은 이동할 수 없다). 한편 스태미너 소모량이 크고 내려치기보다 데미지가 약하다는 것이 단점이다.");

		kor.Add("Enemy_01", "하룻강아지");
		kor.Add("Enemy_02", "껑충이");
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
