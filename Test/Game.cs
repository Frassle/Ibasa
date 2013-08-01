using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Game : Ibasa.Game.Game
    {
        public override void Initalize()
        {
            Console.WriteLine("Initalize");     
      
            base.Initalize();
        }

        protected override void FixedUpdate(Ibasa.Game.GameTime time)
        {
            Console.WriteLine(time);
        }

        protected override void Update(Ibasa.Game.GameTime time)
        {
            System.Threading.Thread.Sleep(16);
        }
    }
}
