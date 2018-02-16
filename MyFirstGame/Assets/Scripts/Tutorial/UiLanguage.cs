using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLanguage: MonoBehaviour {

	Dictionary<string, string> kor;
	Dictionary<string, string> eng;
	Dictionary<string, string> jpn;
	Dictionary<string, Dictionary<string, string>> data;

	void Start() {
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

		kor.Add("Tutorial_01", "핵전쟁으로 인류가 멸망한지 딱 100년이 되는 날");
		kor.Add("Tutorial_02", "암흑과도 같은 지구에 또다시");
		kor.Add("Tutorial_03", "하늘과");
		kor.Add("Tutorial_04", "땅과");
		kor.Add("Tutorial_05", "사람");

		//	kor.Add("Tutorial_01", "힘을 가진 자들이 세상을 지배하는 세기말. 당신은 한 자루 검으로 세상을 구해야만 한다.");
		//kor.Add("Tutorial_02", "검술의 기본은 보법. 마우스를 우클릭하면 된다. 목표 지점을 순서대로 방문해보자.");
		//kor.Add("Tutorial_03", "대부분의 적들은 몸뚱아리로 당신을 압박해 올 것이다 (즉, 길막이다). 따라서 정확하고 신속한 이동은 생존에 필수이다. 마지막 목표 지점을 방문해 보자. 광클은 기본이다.");
		//kor.Add("Tutorial_04", "보법에 대한 설명은 여기까지로 한다. 다음은 검격의 기본인 내려치기이다. 원하는 방향에 마우스 커서를 놓고 Q를 누르면 된다. 연습해 보자.");
		kor.Add("Tutorial_04_1", "모든 검격에는 일정량의 스태미너가 소모된다. 공격을 하지 않고 일정시간이 지나면 스태미너가 서서히 회복된다. 위험한 순간에 스태미너가 없다면 죽었다고 생각하면 된다.");
		//kor.Add("Tutorial_05", "내려치기 검법의 주의사항을 알려주겠다. 검을 위로 드는 준비동작에서는 적에게 피해를 주지 않는다. 또한 내려치기는 강력한 일격인 만큼 준비동작이 느린 편이고, 공격중과 공격후 일정시간동안 이동을 할 수 없다.");
		kor.Add("Tutorial_06", "이제 내려치기 검법으로 약한 적들을 물리치면서 모험을 시작해 보자! 나머지 검법들은 차차 진행하면서 알려주겠다.");

		kor.Add("Tutorial_07", "적에게 피해를 입으면 짧은 시간 동안 몸이 빨갛게 되면서 이동속도가 느려지고 공격불가 생태가 된다. 허접해 보이는 게임이라고 쉽게 클리어할 수 있다고 생각하면 오산이다.");

		kor.Add("Tutorial_08", "당신을 쫒아오는 적들에게 내려치기 검법을 시전하다가는 쳐맞기 좋을 것이다. 이제 두번째 검법 휘둘러베기를 전수하겠다. W를 누르면 눌러서 360도 공격을 이용해 뒤에서 쫓아오는 적을 제압하자.");
		kor.Add("Tutorial_09", "휘둘러베기 검법은 사방을 한번에 공격할 수 있고, 이동하면서 시전할 수 있다는 장점이 있다 (시전 후 짦은 시간 동안은 이동할 수 없다). 한편 스태미너 소모량이 크고 내려치기보다 데미지가 약하다는 것이 단점이다.");

		kor.Add("HP", "체력");
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
