using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Utils;


namespace TextRPG_TeamProject2nd.Object
{
    internal class Monster: IClone<Monster>
    {
        public void Init()
        {
            mobInfo = new MobInfo();
            Player player = GameManager.Instance().GetCurrentPlayer();
            player.isAttack += Damaged;
        }

        public void UseSkill()
        {
            if (mobInfo == null)
                return;

            Random random = new Random();
            int range = mobInfo.skillList.Length;
            int index = random.Next(0, range);
     
            isAttack?.Invoke(skillList[index].power + mobInfo.attack);           
        }

        public void Damaged(int damage)
        {
            if (mobInfo == null)
                return;

            mobInfo.hp = Math.Max(0, mobInfo.hp - damage);
        }

        public Monster Clone()
        {
            Monster ret = new Monster();
            ret.mobInfo = mobInfo;
            return ret;
        }
        //--------------------------------------
       

       
        //--------------------------------------
        private MobInfo? mobInfo;
        private List<Skill>? skillList;
        public event UseSkillCallback? isAttack;
    }
}
