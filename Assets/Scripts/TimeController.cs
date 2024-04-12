using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class TimeController : MonoBehaviour
{
    private readonly string _worldTimeAPI = "https://worldtimeapi.org/api/timezone/Europe/Moscow";
    [DllImport("__Internal")] private static extern void ShowAlert(string str);
    
    public void OnRefresh()
    {
        StartCoroutine(FetchMoscowTime());
    }

    private IEnumerator FetchMoscowTime()
    {
        using UnityWebRequest request = UnityWebRequest.Get(_worldTimeAPI);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            MoscowDataTime moscowTime = JsonConvert.DeserializeObject<MoscowDataTime>(request.downloadHandler.text);
            string moscowTimeText = moscowTime.datetime.ToString();

            ShowAlert(moscowTimeText);
        }
        else
        {
            Debug.LogError("ошибка подключения к API.");
        }
    }

    public class MoscowDataTime
    {
        public string abbreviation { get; set; }
        public string client_ip { get; set; }
        public DateTime datetime { get; set; }
        public int day_of_week { get; set; }
        public int day_of_year { get; set; }
        public bool dst { get; set; }
        public object dst_from { get; set; }
        public int dst_offset { get; set; }
        public object dst_until { get; set; }
        public int raw_offset { get; set; }
        public string timezone { get; set; }
        public int unixtime { get; set; }
        public DateTime utc_datetime { get; set; }
        public string utc_offset { get; set; }
        public int week_number { get; set; }
    }
}