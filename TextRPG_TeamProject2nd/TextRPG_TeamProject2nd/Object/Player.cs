using TextRPG_TeamProject2nd.Utils;
using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Object;
using Microsoft.VisualBasic;

namespace TextRPG_TeamProject2nd.Object
{

    internal class Player
    {
        public void Init()
        {
            skillList = new List<Skill>();
            playerInfo = new PlayerInfo();
        }
        //-----------------
        /// <summary>
        /// 사용할 스킬(전투중 액션 번호), 몬스터 객체를 입력받아 해당 몬스터 객체에게 스킬효과를 가합니다.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="mob"></param>
        public void UseSkill(int index, Monster mob = null)
        {
            isAttack += mob.Damaged;
            SKILLTYPE type = skillList[index].type;
            
            if (playerInfo != null && (type == SKILLTYPE.ATTACK))
            {
                isAttack?.Invoke(playerInfo.attack + skillList[index].power); //attack
            }
            else if (playerInfo != null && type == SKILLTYPE.HEAL)
            {
                playerInfo.hp = Math.Min(playerInfo.maxHp, playerInfo.hp + skillList[index].power);
            }
        }
        public void Damaged(int damage)
        {
            if (playerInfo != null)
                playerInfo.hp = Math.Max(0, playerInfo.hp - damage);
        }
        //-----------------
        void CreatePlayer(PlayerInfo info)
        {
            playerInfo = info;
            //세이브()

        }

        void SavePlayer()
        {
            //세이브
           
        }

        void LoadPlayer()
        {
            //로드
        }


        //-----------------

        //-----------------

        PlayerInfo? playerInfo;
        List<Skill>? skillList;
        Item? weapon = null;
        Item? armor = null;

        //==
        public event UseSkillCallback? isAttack;
    }
}
