
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject food;
    public Agent agent;
    public Text scoreText;
    private int _score=0;

    
    private void Start()
    {
        SpawnFood();
    }

    public void SpawnFood(){

        float height = Camera.main.orthographicSize -0.5f;
        float width = height*Camera.main.aspect - 0.5f ;

        float x = Random.Range(-width,width);
        float y = Random.Range(-height,height);

        Instantiate(food,new Vector3(x,y,0),Quaternion.identity);
        agent.GetFoodPos(new Vector2(x,y));

        scoreText.text = _score.ToString();
        _score++;

    }


}
