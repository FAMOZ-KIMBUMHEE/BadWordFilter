using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ChatFilter
{
    class Program : MonoBehaviour
    {
        [SerializeField] InputField inputField;
        [SerializeField] Button resultButton;
        [SerializeField] Text text;

        SlangFilter chatFilter;
        private void Start()
        {
            chatFilter = new SlangFilter();

            resultButton.onClick.AddListener(() =>
            {
                FilterResult();
            });

            LoadJson();
        }

        void LoadJson()
        {
            string loadstring = File.ReadAllText(Application.streamingAssetsPath + "/badwords.json");

            BadwordsJsonData data = JsonConvert.DeserializeObject<BadwordsJsonData>(loadstring);

            //
            // 금칙어를 등록하자.
            //
            foreach (var word in data.badwords)
            {
                chatFilter.AddSlang(word);
            }
        }

        public void FilterResult()
        {
            if (chatFilter != null)
            {
                string chat = inputField.text;
                text.text = "필터링 결과 : " + chatFilter.Filter(chat);
            }
        }
    }

    //출처 : https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=anjindago&logNo=220063905743
}
