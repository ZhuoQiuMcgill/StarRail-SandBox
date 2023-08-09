using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapResources; 

namespace Star
{
    public class Star
    {
        public int id { get; }
        public int type { get; set; }           // 0Ϊ��Դ����ϵ��1Ϊ�ڶ�
        public Vector2 pos { get; set; }        // λ����Ϣ
        public bool isLivable = false;          // �Ƿ��˾�
        public bool isDestroyed = false;        // �Ƿ񱻴ݻ�
        public List<Star> adj { get; set; }     // ��¼���ڵ���ϵ

        // ��Դ��ֵ
        public Dictionary<MapResources.Resources, int> resources = new Dictionary<MapResources.Resources, int>()
            {
                {MapResources.Resources.Population, 0},
                {MapResources.Resources.Food, 0},
                {MapResources.Resources.Metal, 0},
                {MapResources.Resources.Energy, 0},
                {MapResources.Resources.Technology, 0}
            };

        
        public int resTick = 1000;     // �ݻ���ϵʱ�ɻ�ȡ���ٸ�tick����Դ
        public int maxResValue = 21;   // �����Դ��ֵ
        public int minResValue = 5;    // ��С��Դ��ֵ


        public Star(int id, Vector2 pos, double livableRate, double blackholeRate)
        {
            this.id = id;
            this.pos = pos;
            this.adj = new List<Star>();

            // �ж��Ƿ��Ǻڶ�
            float randomNumber = UnityEngine.Random.Range(0f, 1f);
            if (randomNumber > blackholeRate)
            {
                this.type = 0;

                // �ж��Ƿ��˾�
                randomNumber = UnityEngine.Random.Range(0f, 1f);
                if (randomNumber < livableRate) { this.isLivable = true; }
                else { this.isLivable = false; }
            }
            else { this.type = 1; this.isLivable = false; }

            fillResources();
        }


        /**
         * ��һ����ϵ�����Դ
         */
        public void fillResources()
        {
            if (this.type == 1)
            {
                this.resources[MapResources.Resources.Technology] = UnityEngine.Random.Range(minResValue * 2, maxResValue);
                this.resources[MapResources.Resources.Energy] = UnityEngine.Random.Range(minResValue * 2, maxResValue);
            }
            else
            {
                this.resources[MapResources.Resources.Energy] = UnityEngine.Random.Range(minResValue, maxResValue);
                this.resources[MapResources.Resources.Metal] = UnityEngine.Random.Range(minResValue, maxResValue);
                if (this.isLivable)
                {
                    this.resources[MapResources.Resources.Food] = UnityEngine.Random.Range(minResValue, maxResValue);
                }
            }
        }

        /**
         * �ݻ���ϵʱʹ�����Method�����شݻٺ��õ���Դ��ֵ�Լ���ո���ϵ��Դ����
         */
        public Dictionary<MapResources.Resources, int> destroy()
        {
            this.isLivable = false;
            this.isDestroyed = true;

            Dictionary<MapResources.Resources, int> res = new Dictionary<MapResources.Resources, int>();

            foreach (KeyValuePair<MapResources.Resources, int> resource in this.resources)
            {
                res[resource.Key] = resource.Value * this.resTick;
                this.resources[resource.Key] = 0;  // �ٻ����޷��ٻ�ȡ��Դ
            }

            return res;
        }

    }
}
