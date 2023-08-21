using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapResources; 

namespace MapElement
{
    public class Star
    {
        public int id { get; }
        public int type { get; set; }           // 0为资源型星系；1为黑洞
        public Vector2 pos { get; set; }        // 位置信息
        public bool isLivable = false;          // 是否宜居
        public bool isDestroyed = false;        // 是否被摧毁
        public List<Star> adj { get; set; }     // 记录相邻的星系
        public Vector4 color { get; set; }

        // 资源数值
        public Dictionary<MapResources.Resources, int> resources = new Dictionary<MapResources.Resources, int>()
            {
                {MapResources.Resources.Population, 0},
                {MapResources.Resources.Food, 0},
                {MapResources.Resources.Metal, 0},
                {MapResources.Resources.Energy, 0},
                {MapResources.Resources.Technology, 0}
            };

        
        public int resTick = 1000;     // 摧毁星系时可获取多少个tick的资源
        public int maxResValue = 21;   // 最大资源数值
        public int minResValue = 5;    // 最小资源数值


        public Star(int id, Vector2 pos, double livableRate, double blackholeRate)
        {
            this.id = id;
            this.pos = pos;
            this.adj = new List<Star>();

            // 判定是否是黑洞
            float randomNumber = UnityEngine.Random.Range(0f, 1f);
            if (randomNumber > blackholeRate)
            {
                this.type = 0;

                // 判定是否宜居
                randomNumber = UnityEngine.Random.Range(0f, 1f);
                if (randomNumber < livableRate) { this.isLivable = true; }
                else { this.isLivable = false; }
            }
            else { this.type = 1; this.isLivable = false; }

            fillResources();
            setColor();
        }


        /**
         * 给一个星系填充资源
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
         * 摧毁星系时使用这个Method，返回摧毁后获得的资源数值以及清空该星系资源生成
         */
        public Dictionary<MapResources.Resources, int> destroy()
        {
            this.isLivable = false;
            this.isDestroyed = true;

            Dictionary<MapResources.Resources, int> res = new Dictionary<MapResources.Resources, int>();

            foreach (KeyValuePair<MapResources.Resources, int> resource in this.resources)
            {
                res[resource.Key] = resource.Value * this.resTick;
                this.resources[resource.Key] = 0;  // 毁坏后无法再获取资源
            }

            return res;
        }

        public void setColor()
        {
            if (this.type == 0) { this.color = new Vector4(148.0f / 255.0f, 0, 211.0f / 255.0f, 0.5f); }
            else
            {
                if (this.isLivable) { this.color = new Vector4(0, 1.0f, 127.0f / 255.0f, 0.5f); }
                else { this.color = new Vector4(220.0f / 255.0f, 20.0f / 255.0f, 60.0f / 255.0f, 0.5f); }
            }
        }

    }
}
