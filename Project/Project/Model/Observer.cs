using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Model
{
    interface Observer
    {
        void playerJump();

        void playerDied();

        void playerWon();

        void playerTransformed();

        void playerLanded();
    }
}
