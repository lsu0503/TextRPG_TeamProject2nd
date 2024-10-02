using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Utils;


namespace TextRPG_TeamProject2nd.Object
{
    internal class Monster: IClone<Monster>
    {
        public void Init()
        {
            Player player = GameManager.Instance().GetCurrentPlayer();
            player.isAttack += Damaged;
        }

        public void UseSkill()
        {
            if (mobInfo == null)
                return;

            Random random = new Random();
            int range = mobInfo.skillList.Count;
            int index = random.Next(0, range);
     
            isAttack?.Invoke(skillList[index].power + mobInfo.attack);           
        }


        public void Damaged(int damage)
        {
            if (mobInfo == null)
                return;

            damage /= mobInfo.defence; 
            mobInfo.hp = Math.Max(0, mobInfo.hp - damage);
        }

        public Item Drops()
        {
            Random rand = new Random();

            int id = mobInfo.dropList[rand.Next(0, mobInfo.dropList.Count)];
            return ObjectManager.Instance().GetItem(id);

        }

        public Monster Clone()
        {
            Monster ret = new Monster();
            ret.mobInfo = mobInfo;
            return ret;
        }

        public MobInfo GetInfo()
        {
            return mobInfo;
        }
        //--------------------------------------
       

       
        //--------------------------------------
        private MobInfo? mobInfo = new MobInfo();
        private List<Skill>? skillList = new List<Skill>();
        public event UseSkillCallback? isAttack;
    }
}
