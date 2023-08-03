using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapResources; 

namespace Star
{
    public class Star
    {
        public int id { get; }
        public int type { get; set; }
        public Vector2 pos { get; set; }
        public bool isLivable = false;
        public Dictionary<MapResources.Resources, int> resources = new Dictionary<MapResources.Resources, int>()
            {
                {MapResources.Resources.Population, 0},
                {MapResources.Resources.Food, 0},
                {MapResources.Resources.Metal, 0},
                {MapResources.Resources.Energy, 0},
                {MapResources.Resources.Technology, 0}
            };

        public Star(int id, Vector2 pos, double livableRate, double blackholeRate)
        {
            this.id = id;
            this.pos = pos;

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
        }

        public void fillResources()
        {
            if (this.type == 1)
            {
                this.resources[MapResources.Resources.Technology] = UnityEngine.Random.Range(10, 21);
                this.resources[MapResources.Resources.Energy] = UnityEngine.Random.Range(10, 21);
            }
            else
            {
                this.resources[MapResources.Resources.Energy] = UnityEngine.Random.Range(5, 21);
                this.resources[MapResources.Resources.Metal] = UnityEngine.Random.Range(5, 21);
                if (this.isLivable)
                {
                    this.resources[MapResources.Resources.Food] = UnityEngine.Random.Range(5, 21);
                }
            }
        }



    }
}
