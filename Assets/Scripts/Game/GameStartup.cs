using UnityEngine;
using CapybaraAdventure.Player;

namespace CapybaraAdventure
{
    public class GameStartup : MonoBehaviour
    {
        #region MonoBehaviour

        private void Start()
        {
            InitPlayer();
        }

        private void InitPlayer()
        {
            var playerJump = new PlayerJump();
        }

        #endregion
    }
}
