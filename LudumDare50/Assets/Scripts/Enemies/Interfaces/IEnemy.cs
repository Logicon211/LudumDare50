using UnityEngine;

namespace Enemy.Interface
{
    public interface IEnemy
    {
        // Move the enemy
        void Move(float tarX, float tarY);
    }
}
