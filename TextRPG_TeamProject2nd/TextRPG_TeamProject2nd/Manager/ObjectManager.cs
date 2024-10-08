﻿using TextRPG_TeamProject2nd.Object;
namespace TextRPG_TeamProject2nd.Manager
{
    internal class ObjectManager
    {
        //------------------------------------------------------//
        public static ObjectManager Instance()
        {
            if (instance == null)
                instance = new ObjectManager();

            return instance;
        }   
        public void Init()
        {
            items       = FileManager.Instance().GetItemFile();
            maps        = FileManager.Instance().GetMapFile();
            monsters    = FileManager.Instance().GetMonsterFile();
            skills      = FileManager.Instance().GetSkillFile();
            races       = FileManager.Instance().GetRaceFile();
            quests      = FileManager.Instance().GetQuestFile();
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
        }           
        /// <summary>
        /// 맵의 ID를 이용하여 맵을 생성합니다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Map      GetMap(int _id)
        {
            foreach (Map map in maps)
            {
                if (map.mapInfo.id == _id)
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
                if (mob.GetInfo().id == _id)
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
        /// <summary>
        /// 직업의 ID를 이용하여 직업을 생성합니다(직업 오브젝트)
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Race    GetRace(int _id)
        {
            foreach (Race race in races)
            {
                if (race.id == _id)
                    return race.Clone();
            }

            return null;
        }
        /// <summary>
        /// 퀘스트의 ID를 이용하여 직업을 생성합니다(퀘스트 오브젝트)
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Quest GetQuest(int _id)
        {
            foreach (Quest quest in quests)
            {
                if (quest.questId == _id)
                    return quest.Clone();
            }

            return null;
        }
        //------------------------------------------------------//
        public List<Race> GetRaceList() { return races; }
        public List<Map> GetMapList() { return maps; }
        public List<Item> GetItemList() { return items; }

        //------------------------------------------------------//
        private List<Item>    items     =  new List<Item>(); 
        private List<Map>     maps      =  new List<Map>();
        private List<Monster> monsters  =  new List<Monster>();
        private List<Skill>   skills    =  new List<Skill>();
        private List<Race>    races     = new List<Race>();
        private List<Quest>    quests     = new List<Quest>();
        static ObjectManager? instance  =  null;
    }
}
