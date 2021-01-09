using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FourCharacterPhrase.Shared
{
    public class BordEntity
    {
        public List<CellEntity> Cells { get; set; } = new List<CellEntity>();

        public List<WordEntity> Words { get; set; } = new List<WordEntity>();

        public List<string> AllWords { get; set; } = new List<string>();

        public AnswerNumberEntity AnswerNumber { get; set; } = new AnswerNumberEntity();

        private DateTime startTime;

        public BordEntity()
        {
            SetAllWords();
        }

        public async Task SetData()
        {
            SetWords();
            SetCells();

            startTime = DateTime.Now;
            AnswerNumber.Count = 0;
            AnswerNumber.ElapsedTime = 0;

            await WebApiService.PostRequest("AnswerNumber", AnswerNumber);
        }

        private void SetWords()
        {
            var usedNumbers = new List<int>();

            Words = new List<WordEntity>();

            while (Words.Count < 9)
            {
                var randomNumber = RandomNumber(AllWords.Count);
                if (Words.Any(m => m.Value == AllWords[randomNumber]) == false)
                {
                    Words.Add(new WordEntity() { Value = AllWords[randomNumber] });
                }
            }
        }

        private void SetCells()
        {
            Cells = new List<CellEntity>();

            var list = new List<char>();
            foreach(var item in Words)
            {
                list.AddRange(item.GetOneCharacter());
            }

            int i = 1;
            while (list.Count > 0)
            {
                var randomNumber = RandomNumber(list.Count);
                Cells.Add(new CellEntity() { Value = list[randomNumber], No = i, Name = AnswerNumber.Name });
                list.Remove(list[randomNumber]);
                i++;
            }
        }

        private int RandomNumber(int maxNumber)
        {
            System.Random r = new System.Random();
            return r.Next(maxNumber);
        }

        public async void Click(CellEntity cell)
        {
            Console.WriteLine(" WebApiService:終了");

            if (IsFourSelecting() == true && cell.Status != CellStatus.Selecting) return;

            cell.ChangeStatus();

            if (IsFourSelecting() == false) return;

            if (IsCorrectAnswer() == false) return;

            ChangeCellsStatusSelectingToCompleted();

            Console.WriteLine("PostRequest:開始");

            AnswerNumber.Count += 1;
            AnswerNumber.ElapsedTime = GetElapsedTime();

            await WebApiService.PostRequest("AnswerNumber", AnswerNumber);
            //Console.WriteLine(JsonConvert.SerializeObject(a));
        }

        public async void PostCells()
        {
           await WebApiService.PostRequest("Cells", Cells);
        }

        public int GetElapsedTime()
        {
            return (int)(DateTime.Now - startTime).TotalSeconds;
        }


        public bool IsCompleted()
        {
            if (Cells.Where(m => m.Status != CellStatus.Completed).Count() == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 四文字選択されている状態かチェックする
        /// </summary>
        /// <returns></returns>
        private bool IsFourSelecting()
        {
            if (Cells.Where(m => m.Status == CellStatus.Selecting).Count() == 4)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 洗濯されている文字が4字熟語になっているかチェックする
        /// </summary>
        /// <returns></returns>
        private bool IsCorrectAnswer()
        {
            var selectChars = new List<char>();
            selectChars = Cells.Where(m => m.Status == CellStatus.Selecting).Select(m => m.Value).ToList();
            selectChars.Sort();
            if (Words.Any(m => m.GetCharSortValue() == new String(selectChars.ToArray()))) return true;
            return false;
        }

        /// <summary>
        /// cellsのStatusでSelectingのものをCompletedにする
        /// </summary>
        private void ChangeCellsStatusSelectingToCompleted()
        {
            Cells.Where(m => m.Status == CellStatus.Selecting).ToList().ForEach(m => m.Status = CellStatus.Completed);
        }

        private void SetAllWords()
        {
            AllWords = new List<string>();

            AllWords.Add("悪戦苦闘");
            AllWords.Add("暗中模索");
            AllWords.Add("唯唯諾諾");
            AllWords.Add("意気消沈");
            AllWords.Add("異口同音");
            AllWords.Add("意志薄弱");
            AllWords.Add("一網打尽");
            AllWords.Add("一攫千金");
            AllWords.Add("一喜一憂");
            AllWords.Add("一騎当千");
            AllWords.Add("一石二鳥");
            AllWords.Add("威風堂堂");
            AllWords.Add("因果応報");
            AllWords.Add("右往左往");
            AllWords.Add("有象無象");
            AllWords.Add("紆余曲折");
            AllWords.Add("汚名返上");
            AllWords.Add("温故知新");
            AllWords.Add("開口一番");
            AllWords.Add("花鳥風月");
            AllWords.Add("冠婚葬祭");
            AllWords.Add("危機一髪");
            AllWords.Add("起死回生");
            AllWords.Add("起承転結");
            AllWords.Add("疑心暗鬼");
            AllWords.Add("奇想天外");
            AllWords.Add("喜怒哀楽");
            AllWords.Add("九死一生");
            AllWords.Add("狂喜乱舞");
            AllWords.Add("玉石混交");
            AllWords.Add("空前絶後");
            AllWords.Add("九分九厘");
            AllWords.Add("荒唐無稽");
            AllWords.Add("極悪非道");
            AllWords.Add("国士無双");
            AllWords.Add("古今東西");
            AllWords.Add("五臓六腑");
            AllWords.Add("五里霧中");
            AllWords.Add("言語道断");
            AllWords.Add("三寒四温");
            AllWords.Add("自画自賛");
            AllWords.Add("時期尚早");
            AllWords.Add("四苦八苦");
            AllWords.Add("試行錯誤");
            AllWords.Add("自業自得");
            AllWords.Add("時代錯誤");
            AllWords.Add("十中八九");
            AllWords.Add("自暴自棄");
            AllWords.Add("四方八方");
            AllWords.Add("四面楚歌");
            AllWords.Add("弱肉強食");
            AllWords.Add("縦横無尽");
            AllWords.Add("終始一貫");
            AllWords.Add("十人十色");
            AllWords.Add("取捨選択");
            AllWords.Add("酒池肉林");
            AllWords.Add("順風満帆");
            AllWords.Add("支離滅裂");
            AllWords.Add("心機一転");
            AllWords.Add("新陳代謝");
            AllWords.Add("森羅万象");
            AllWords.Add("生殺与奪");
            AllWords.Add("清廉潔白");
            AllWords.Add("切磋琢磨");
            AllWords.Add("絶体絶命");
            AllWords.Add("千載一遇");
            AllWords.Add("前代未聞");
            AllWords.Add("全知全能");
            AllWords.Add("大義名分");
            AllWords.Add("大言壮語");
            AllWords.Add("大胆不敵");
            AllWords.Add("大同小異");
            AllWords.Add("他力本願");
            AllWords.Add("単刀直入");
            AllWords.Add("魑魅魍魎");
            AllWords.Add("猪突猛進");
            AllWords.Add("適材適所");
            AllWords.Add("徹頭徹尾");
            AllWords.Add("天下一品");
            AllWords.Add("電光石火");
            AllWords.Add("天真爛漫");
            AllWords.Add("東奔西走");
            AllWords.Add("難攻不落");
            AllWords.Add("二者択一");
            AllWords.Add("二束三文");
            AllWords.Add("日進月歩");
            AllWords.Add("拍手喝采");
            AllWords.Add("馬耳東風");
            AllWords.Add("八方美人");
            AllWords.Add("波瀾万丈");
            AllWords.Add("罵詈雑言");
            AllWords.Add("品行方正");
            AllWords.Add("風林火山");
            AllWords.Add("不倶戴天");
            AllWords.Add("粉骨砕身");
            AllWords.Add("文武両道");
            AllWords.Add("平穏無事");
            AllWords.Add("平平凡凡");
            AllWords.Add("満身創痍");
            AllWords.Add("三日天下");
            AllWords.Add("三日坊主");
            AllWords.Add("問答無用");
            AllWords.Add("悠悠自適");
            AllWords.Add("油断大敵");
            AllWords.Add("竜頭蛇尾");
            AllWords.Add("良妻賢母");
            AllWords.Add("臨機応変");
        }
    }
}
