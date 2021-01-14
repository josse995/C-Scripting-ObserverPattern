using UnityEngine;

public delegate void PowerupHandler();

public class PowerupController : MonoBehaviour, IEndGameObserver
{
    #region Field Declarations

    public GameObject explosion;

    [SerializeField]
    private PowerType powerType;

    #endregion

    public event PowerupHandler PowerupCollision;

    #region Movement

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 3, Space.World);

        if (ScreenBounds.OutOfBounds(transform.position))
            RemoveAndDestroy();
    }

    private void RemoveAndDestroy()
    {
        var gameScene = FindObjectOfType<GameSceneController>();
        gameScene.RemoveObserver(this);
        Destroy(gameObject);
    }

    #endregion

    #region Collisons

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (powerType == PowerType.Shield)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.EnableShield();
            }
            RemoveAndDestroy();
        }
    }

    #endregion

    public void Notify()
    {
        Destroy(gameObject);
    }
}

public enum PowerType
{
    Shield,
    X2
};