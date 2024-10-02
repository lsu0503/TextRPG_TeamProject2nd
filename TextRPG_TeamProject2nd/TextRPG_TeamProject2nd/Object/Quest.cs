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
        public string questDesc           { get; set; } //����Ʈ ���� ����
        public int questTargetId          { get; set; } //��ǥ ����
        public int questTargetAmount      { get; set; } //����Ʈ ��ǥġ
        public int questProgressAmount    { get; set; } //���� ����Ʈ �޼�ġ
        public int rewardGold             { get; set; } //���� ���
        public int rewardItemId           { get; set; } //���� ������

    }

}