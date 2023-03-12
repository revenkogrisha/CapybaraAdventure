using UnityEngine;
using CapybaraAdventure.Player;

namespace CapybaraAdventure
{
    public class GameStartup : MonoBehaviour
    {
        private PlayerJump _playerJump;

        #region MonoBehaviour

        private void Start()
        {
            InitPlayer();
        }

        private void OnDisable()
        {
            _playerJump.Disable();
        }

        #endregion

        private void InitPlayer()
        {
            _playerJump = new PlayerJump();
        }
    }
}
