using TextRPG_TeamProject2nd.Object;
namespace TextRPG_TeamProject2nd.Manager
{
    internal class ObjectManager
    {
        //------------------------------------------------------//
        public static ObjectManager Instance()
        {
            if (Instance == null)
                instance = new ObjectManager();

            return instance;
        }   ///생성자 및 초기화 함수
        public void Init()
        {
            
        }
        //------------------------------------------------------//
        /// <summary>
    /// 아이템의 ID를 이용하여 해당 아이템을 생성합니다.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
        public Item     GetItem(int _id)
        {
            foreach(Item item in items)
            {
                if (item.id == _id)
                    return item.Clone();
            }

            return null;
        }           // 외부에 노출 되는 클래스의 기능 함수()
        /// <summary>
        /// 맵의 ID를 이용하여 맵을 생성합니다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Map      GetMap(int _id)
        {
            foreach (Map map in maps)
            {
                if (map.id == _id)
                    return map.Clone();
            }

            return null;
        }
        /// <summary>
        /// 몬스터의 ID를 이용하여 몬스터를 생성합니다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Monster  GetMonster(int _id)
        {
            foreach (Monster mob in monsters)
            {
                if (mob.id == _id)
                    return mob.Clone();
            }

            return null;
        }
        /// <summary>
        /// 스킬의 ID를 이용하여 스킬을 생성합니다.(스킬 오브젝트)
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Skill    GetSkill(int _id)
        {
            foreach (Skill skill in skills)
            {
                if (skill.id == _id)
                    return skill.Clone();
            }

            return null;
        }
        //------------------------------------------------------//
                                                      // 내부에서만 사용 할 함수


        //------------------------------------------------------//
        private List<Item>    items    =  new List<Item>(); // 변수 목록
        private List<Map>     maps     =  new List<Map>();
        private List<Monster> monsters =  new List<Monster>();
        private List<Skill>   skills   =  new List<Skill>();
        static ObjectManager instance  =  null;
    }
}
