using System.IO;
using UnityEngine;

public class PlayerDBManager : MonoBehaviour
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "playerDB.json"); // 저장경로는 playerDB.json 파일

    //싱글톤 사용으로 PlayerToTalManager가 이 인스턴스를 통해 다른 매니저에게 분배할 예정 ㅇㅇ
    public static PlayerDBManager instance;

    public PlayerDB playerDB; // 플레이중인 플레이어의 데이터

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject); // 씬 전환때 오브젝트 파괴 방지
    }

    private void Start()
    {
        playerDB = Load();
    }

    public void Save(PlayerDB _data)
    {
        string json = JsonUtility.ToJson(_data, true); // 데이터를 json문자열로 바꾸고
        File.WriteAllText(SavePath, json); // 파일에 덮어씌움
    }

    public PlayerDB Load()
    {
        if(File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath); // 저장된 파일경로에서 파일 읽고
            PlayerDB data = JsonUtility.FromJson<PlayerDB>(json); // json을 PlayerDB클래스에 맞게 변환
            return data;
        }
        return new PlayerDB(); // 파일 없으면 기본값으로 세팅
    }

    public void DeleteSaveFile()
    {
        if(File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
    }

}
