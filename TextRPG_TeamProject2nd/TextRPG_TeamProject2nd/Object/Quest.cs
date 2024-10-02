using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Object;
using TextRPG_TeamProject2nd.Utils;

namespace TextRPG_TeamProject2nd.Object
{
    internal class Quest: IClone<Quest>
    {
        public Quest Clone()
        {
            Quest ret = new Quest();


            ret.questId                        = questId;
            ret.questName                      = questName;
            ret.questDesc                      = questDesc;
            ret.questTargetId                  = questTargetId;
            ret.questTargetAmount              = questTargetAmount;
            ret.questProgressAmount            = questProgressAmount;
            ret.rewardGold                     = rewardGold;
            ret.rewardItemId                   = rewardItemId;
            return ret;
        }

        public void ProgressQuest(Monster defeatedMonster)
        {
            MobInfo mobInfo = defeatedMonster.GetInfo();

            if(questTargetId == mobInfo.id)
            {
                questProgressAmount += 1;
                if (questProgressAmount >= questTargetAmount)
                {
                    questProgressAmount = questTargetAmount;
                }
            }
        }
        public bool CheckProgress()
        {
            if(questProgressAmount >= questTargetAmount) { return true; }
            else {  return false; }
        }

        public int questId                { get; set; }
        public string questName           { get; set; }
        public string questDesc           { get; set; } //퀘스트 내용 설명
        public int questTargetId          { get; set; } //목표 몬스터
        public int questTargetAmount      { get; set; } //퀘스트 목표치
        public int questProgressAmount    { get; set; } //현재 퀘스트 달성치
        public int rewardGold             { get; set; } //보상 골드
        public int rewardItemId           { get; set; } //보상 아이템

    }

}