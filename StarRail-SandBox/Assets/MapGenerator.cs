using System.Collections.Generic; 
using UnityEngine; 

public class Star
{
    public int id;
    public Vector2 pos { get; set; }

    public Star(int id, Vector2 pos)
    {
        this.id = id;
        this.pos = pos;
    }
}


public class Path
{
    public Star star1 { get; set; }
    public Star star2 { get; set; }

    public Path(Star star1, Star star2)
    {
        this.star1 = star1;
        this.star2 = star2;
    }
}


public class MapGenerator : MonoBehaviour
{
    public Sprite starSprite;
    public List<Star> stars = new List<Star>();
    public List<Path> paths = new List<Path>();
    public int width = 10;
    public int height = 5;

    void Generate()
    {
        for (int i = 1; i < width; i++)
        {
            Vector2 pos1 = new Vector2(i, 0);
            Vector2 pos2 = new Vector2(-i, 0);
            CreateStar(i, pos1);
            CreateStar(-i, pos2);
        }
    }

    void CreateStar(int id, Vector2 pos)
    {
        Star star = new Star(id, pos);
        stars.Add(star);

        GameObject starObj = new GameObject("Star" + id);

        // Create and set up the SpriteRenderer
        SpriteRenderer renderer = starObj.AddComponent<SpriteRenderer>();
        renderer.color = Color.white;
        renderer.sprite = starSprite; // Use the starSprite

        float scale = 0.2f; // Adjust this to set your desired size
        starObj.transform.localScale = new Vector3(scale, scale, 1f);


        // Create and set up the CircleCollider2D
        CircleCollider2D collider = starObj.AddComponent<CircleCollider2D>();
        collider.radius = 0.2f;

        starObj.transform.position = pos;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
