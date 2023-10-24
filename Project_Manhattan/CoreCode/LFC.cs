using Microsoft.Xna.Framework;
using Project_Manhattan.Content;
using Project_Manhattan.Screen_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan.CoreCode
{
    public class LFC        //เป็น World Class ใช้ list และ array ร่วมกับ class อื่น; ย่อจาก List Friend Center
    {
        public static List<Friend> friendList = new List<Friend>();//Player List Skill
        public static Friend[] friend = new Friend[3];         //Player Array Skill
        public static int[] select = new int[3];

        public static AnimatedTexture CheckFrame = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
        MainGame game0;
        public LFC(MainGame game)
        {
            CheckFrame.Load(game.Content, "2D/Friend/Mickey/Mig_idlet", 4, 1, 8);

            //////////Object + Status Zone//////////////////
            Mickey mickey = new Mickey(game);
            Heng heng = new Heng(game);
            Ohm ohm = new Ohm(game);
            Dome dome = new Dome(game);
            JaBing jaBing = new JaBing(game);
            Tata tata = new Tata(game);

            friendList.Add(heng);
            friendList.Add(mickey);
            friendList.Add(ohm);
            friendList.Add(dome);
            friendList.Add(jaBing);
            friendList.Add(tata);

            select[0] = 0;
            select[1] = 1;
            select[2] = 2;

            this.game0 = game;
        }

        public void UpdateList(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            friend[0] = friendList[select[0]];
            friend[1] = friendList[select[1]];
            friend[2] = friendList[select[2]];

            for(int i = 0; i < friend.Length; i++)
            {
                friend[i].UpdateAction();
                if (friend[i].Anime == "S1")
                {
                    friend[i].This_Ani[2].UpdateFrame(elapsed);
                }
                else if (friend[i].Anime == "S2")
                {
                    friend[i].This_Ani[3].UpdateFrame(elapsed);
                }
            }

            for(int i = 0;i < friendList.Count; i++)
            {
                friendList[i].This_Ani[0].UpdateFrame(elapsed);
            }
        }
    }
}
