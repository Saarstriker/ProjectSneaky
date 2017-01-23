﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSneaky
{
    class Player
    {
        public Texture2D playerTexture;
        public Vector2 playerPosition;
        public Rectangle playerHitbox;

        float health;
        float speed;
        Vector2 move;
        public Vector2 size;
        

        public Player(Texture2D _playerTexture,Vector2 _playerPosition, float _speed)
        {
            playerTexture = _playerTexture;
            playerPosition = _playerPosition;
            health = 5;
           
            speed = _speed;
            size = new Vector2(_playerTexture.Width, _playerTexture.Height);
            playerHitbox = _playerTexture.Bounds;
        }

        public void ApplyDamage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                playerPosition = new Vector2(60, 410);
                GameStuff.Instance.guard1.guardPosition = new Vector2(280, 30);
                GameStuff.Instance.guard2.guardPosition = new Vector2(75, 40);
                GameStuff.Instance.guard3.guardPosition = new Vector2(745, 40);
                GameStuff.Instance.guard1.changeDetectionStatus(false);
                GameStuff.Instance.guard2.changeDetectionStatus(false);
                GameStuff.Instance.guard3.changeDetectionStatus(false);
                //obiges resettet nach dem Tod die Spielerposition, die Guardposition, und, dass die Guards einen nicht weiter verfolgen
                health = 5;
            }

        }

        
         void Movement(Tilemap tileMap)
        {
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Up) || (key.IsKeyDown(Keys.W)))
                move.Y -= 1 * speed;

            if (key.IsKeyDown(Keys.Down) || (key.IsKeyDown(Keys.S)))
                move.Y += 1 * speed;

            if (key.IsKeyDown(Keys.Left) || (key.IsKeyDown(Keys.A)))
                move.X -= 1 * speed;

            if (key.IsKeyDown(Keys.Right) || (key.IsKeyDown(Keys.D)))
                move.X += 1 * speed;

            if (tileMap.Walkable(playerPosition + move)
               && tileMap.Walkable(playerPosition + move + new Vector2(playerTexture.Width / 2, - playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(playerTexture.Width / 2, 0))
               && tileMap.Walkable(playerPosition + move + new Vector2(playerTexture.Width / 2, playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(- playerTexture.Width / 2, - playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(- playerTexture.Width / 2, 0))
               && tileMap.Walkable(playerPosition + move + new Vector2(- playerTexture.Width / 2, playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(0, - playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(0, playerTexture.Height / 2)))
            {
                playerPosition += move;
            }

            move.X = 0;
            move.Y = 0;
        }


        public void Update(Tilemap tileMap)
        {
            Movement(tileMap);
            System.Console.WriteLine(playerPosition); // Console Output playerPosition
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(playerTexture, new Vector2 (playerPosition.X - playerTexture.Width/2, playerPosition.Y - playerTexture.Height / 2), Color.White);
        }
    }

}
