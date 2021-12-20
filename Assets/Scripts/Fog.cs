using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    enum GameMode
    {
        Default,
        Fog
    }
    class GameModeController
    {
        public GameMode mode { get; set; }
        
        private Fog parent;
        
        public GameModeController(Fog parent, GameMode initialMode)
        {
            this.mode = initialMode;
            this.parent = parent;
        }
        
        
        public void ChangeSprite()
        {
            if (mode == GameMode.Fog)
            {
                parent.spriteRenderer.sprite = parent.fogSprite;
            }
            
        }

        public bool Fog()
        {
            return this.mode == GameMode.Fog;
        }
    }
    
    private Rigidbody2D body;
    private BoxCollider2D boxCollider2D;
    private GameModeController modeController;
    
    private SpriteRenderer spriteRenderer;
    public Sprite fogSprite;

    public float ascendSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        modeController = new GameModeController(this, GameMode.Default);
        spriteRenderer = GetComponent<SpriteRenderer>();
        // var animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // disable gravity effect
            this.modeController.mode = GameMode.Fog;
            this.modeController.ChangeSprite();
        }
        
        if (this.modeController.Fog() && Input.GetButtonDown("Jump") )
        {
            body.velocity = Vector2.up * ascendSpeed;
        }
    }
}
